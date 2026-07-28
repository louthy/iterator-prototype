# `Iterator<A>` prototype

_Prototyping ideas around `IterableK` trait and an allocation free `Iterator`..._

## Introduction

The [`ref-folds` branch](https://github.com/louthy/language-ext/tree/ref-folds) on the 
[`language-ext` repository](https://github.com/louthy/language-ext) has been a staging area for new 
performance improvements for foldables, iterables, and collections in general.

It all started as a new sub-type to the `Foldable<F>` trait that takes an additional parameter `FS`. One that is
a `ref struct` state value: `Foldable<F, FS>`.  It also has two additional methods:

```c#
static abstract FS StepSetup<A>(K<F, A> ta);
static abstract bool Step<A>(K<F, A> ta, ref FS refState, out A value);
```

That allows for iteration of a `Foldable<F, FS>` like so:

```c#
var state = F.StepSetup(foldable);
while (F.Step(state, out var value))
{
    // Do something with value
}
```
This turns out to be faster than using enumerators, even struct based enumerators. And it doesn't 
require allocation of anything on the heap.

The [original `Foldable<F>` trait ](https://github.com/louthy/language-ext/blob/main/LanguageExt.Core/Traits/Foldable/Foldable.Trait.cs) 
has tons of default implementations (like `ForAll`, `Fold`, `Contains`, etc.), which `Foldable<F, FS>` can override and provide extremely fast versions 
using the `Foldable<F, FS>.Step` approach. And so, all usages of `Foldable` (that implement this sub-trait) get 
an immediate performance boost without any cost to the end user.

> **This is awesome. Obviously!**

## But..

One issue is that other (generalist) code is not going to know that, behind the scenes, there is an `FS` struct value; one that 
can be used and leveraged for higher performamce code. Most generalist code will be written for `Foldable<F>` not 
`Foldable<F, FS>`.  So, they can't access the `Step` method or the constructor of the state to make their own efficient
implementations.  And if they do write for `Foldable<F, FS>` it will exclude the more generalist `Foldable<F>` types.

They would have to rely solely on the default implementations in `Foldable`, which isn't bad, but buries some of the benefits.

So, I want...   

* A way to make the implemetation of this trait the only thing an end-user collection-author needs to consider 
* It needs to be impossible for collection-consumers to accidentally use a less performant method of iteration 
* Every user of collections, no matter how they consume them, should automatically gain from this technique to make the effort worthwhile.

## `IterableK<F>`

So, part 1 of making this even more general is to create `IterableK<F>` and `IterableK<F, FS>`. These 
decouple from the notion of foldables and create a standalone concept of iterables.  They're very similar
concepts and `Foldable<F>` will derive from `IterableK<F>`, but they're supposed to just encaspsulate 
iteration of a structure.

There's the simplified base-trait (without the `FS` state baked in):

```c#
public interface IterableK<out T>
    where T : IterableK<T>
{
    static abstract Iterator<A> Forward<A>(K<T, A> ta);
}
```
This will allow something to become iterable without having to deal with `ref` types, `Unsafe` casting, or anything like 
that.  It will be easy to implement:  

For example, if you wanted to make `Option<A>` an `IterableK`:

```c#
public class Option : IterableK<Option>
{
    static Iterator<A> Forward<A>(K<Option, A> ta) =>
        ta is Option<A> option 
            ? option.Match(Some: Iterator.singleton, None: default)
            : default;
}
```
But, if you start needing sequences, then you'll want to implement `IterableK<F, FS>`:

```c#
public interface IterableK<out T, TS> : IterableK<T>
    where T : IterableK<T, TS>
    where TS : struct
{
    static abstract TS Setup<A>(K<T, A> ta);
    static abstract bool Step<A>(ref TS ts, out A value);
}
```

> _By the way, the name `Forward()` is for iterators that go from first-item to last, `IterableBackK` with 
> `Backward()` will be for iterators that go from the last-item to the first. Types can opt in to one or both 
> depending on their traits._

 ## `Iterator<A>`

Just separating out `IterableK` from `Foldable` doesn't fundamentally change anything.  The critical thing will 
be the capability of the `Iterator<A>` value that is returned from `IterableK.Forward()` and `IterableBackK.Backward()`.
It will somehow have to leverage the underlyng `IterableK<F, FS>.Step` technique whilst being flexible,
immutable, pure, allocation-free and fast ... a tall order!

One thing that's been bothering me for a long time is `IEnumerator` and the general enumerator 
pattern of C#. We have no control over the fact enumerators mutate their members.  

> It is impossible to create an immutable `IEnumerator` and have it work with C#.  

Currently `Iterator<A>` (the one that actually exists in lang-ext today) allows the lifting of `IEnumerator<A>` into it 
and tries its best to make it appear to be an immutable sequence.  However, it does not allow multiple evaluation of the 
same reference, where repeated iterations yield the same results. The current implemention is impure, but it isn't 
declarative. This can lead to confusion and bugs.  

> The iteration is impure by-default because of `IEnumerator<A>`.
> I want to create an `Iterator<A>` that is pure and an `IteratorIO<A>` that explicitly might return
different values on each evaluation.

### Designing a new `Iterator<A>`

I started using a type that looked a little like this:

```c#
public record Iterator<A>(A Head, Func<Iterator<A>> Tail);
```
That is the classic FP 'cons list'. Where the tail is lazy, so the rest of the list only evaluates
on-demand, not eagerly.  But, of course, then I wanted the head to be lazy too:

```c#
public abstract record Iterator<A>;
record IteratorCons<A>(A Head, Func<Iterator<A>> Tail) : Iterator<A>;
record IteratorLazy<A>(Func<Iterator<A>> Tail) : Iterator<A>;
```
And, singletons and empty:
```c#
public abstract record Iterator<A>;
record IteratorEmpty<A> : Iterator<A>;
record IteratorSingle<A>(A Head) : Iterator<A>;
record IteratorCons<A>(A Head, Func<Iterator<A>> Tail) : Iterator<A>;
record IteratorLazy<A>(Func<Iterator<A>> Tail) : Iterator<A>;
```
All of this ends up being a discriminated union.  And that's fine.  C# is getting unions soon, but it's
not trivial to make this into an efficient union-type.  Firstly, it's a reference type, and so it will
end up on the heap.  Converting to a struct is possible, but it limits the extensionality of the type,
for example, if I wanted to add `IteratorArr<A>(Arr<A> array)` that had a more efficient implemention,
it would probably require that I stay with `Iterator<A>` being a reference type.

Another issue is that the lazy evaluation of the tail means that every single item in the collection
will end up allocating new items to the heap: potentially the closure and definitely the newly realised
tail `Iterator<A>`.

That's a hell of an overhead.  Especially after finding a way to generalise the iteration of collections
using `IterableK<F, FS>` faster than C# does it itself! Using this approach to `Iterator<A>` generalisation
will kill of those gains and send the project backwards in terms of performance.

## This prototype

So, that's what this prototype is: it's an attempt to create an efficient `Iterator<A>` that will iterate
any `IterableK<F>` or `IterableK<F, FS>` as well as support the union-cases below:

* Empty case - this is the `default(Iterator<A>)` (allocation free)
* Singleton case - this is an iterator with a single value (allocation free)
* Cons case - `(A head, Func<Iterator<A>> tail)` - (can cause allocations) 
* Lazy case - `Func<Iterator<A>>` (can cause allocations)
* `IterableK` case - `(A head, K<F, A> collection, FS state)` - (allocation free)

That means for the most common use cases (iterating collection types from language-ext) you will
get the performance gains of the `IterableK.Step` approach.  But, if you still need to build some
lazy iterators, you can - they will be slightly less efficent - but at least they're pure unlike
`IEnumerator<A>`, which brings robustness and new opportunities.

### Issues (some you could help with...)

The eagle eyed will notice that the `IterableK` case above requires type-arguments (`F` and `FS`) that
are not in `Iterator<A>`.

And so, I have created [`Iterator<F, FS, A>`](https://github.com/louthy/iterator-prototype/blob/main/src/Iterator.TS/Iterator.cs) 
in the [Iterator.TS](https://github.com/louthy/iterator-prototype/tree/main/src/Iterator.TS) folder. This
has the following members:

```c#
readonly int tag;
readonly A head;
readonly object? obj1;
readonly VirtualTable<A>? vt;
readonly TS space;
```
`tag` specifies the case; `head` is the head value in non-Lazy cases; `obj1` generally captures 
reference values that are case dependent; and `space` carries the state for `IterableK` cases.

This is all well and good and works fine, but really we want an `Iterator<A>`, not `Iterator<F, FS, A>`.
It won't be easy to write generic code over iterators if you need to know the internal state types. 
Also, it's a bit ugly.

And so, in the [Iterator](https://github.com/louthy/iterator-prototype/tree/main/src/Iterator) folder, 
I have created `Iterator<A>` which doesn't have those type-parameters. If you look at its members, you
can see that it maps on top of `Iterator<F, FS, A>` exactly.

```c#
readonly int tag;
readonly A head;
readonly object? obj1;
readonly VirtualTable<A>? vt;
readonly Space128 space;
```

The primary difference is that `space` has the type `Space128` and not `TS`. This is a placeholder 
`struct` that takes `128` bytes.  We can then use `Unsafe.As` to convert between `Iterator<F, FS, A>` and `Iterator<A>`.
As long as `TS` is smaller than `128` bytes then `Iterator<A>` will be able to propagte the `TS` state without knowing 
its type.

`Iterator<A>` has another member: `VirtualTable<A>? vt` which works like virtual-tables for C# virtual
methods. 

```c#
public record VirtualTable<T, TS, A> : VirtualTable<A>
    where T : IterableK<T, TS>
    where TS : struct
{
    public override bool Step(object src, ref Space128 space, out Iterator<A> tail)
    {
        ref var state = ref Unsafe.As<Space128, TS>(ref space);
        if (T.Step(ref state, out A h))
        {
            var t1 = new Iterator<T, TS, A>(in h, src, in state);
            ref var t2 = ref Unsafe.As<Iterator<T, TS, A>, Iterator<A>>(ref t1);
            tail = t2;
            return true;
        }
        else
        {
            tail = default!;
            return false;
        }        
    }
}
```
It has a `Step` method, which is used when the `Iterator<A>` is set to the `IterableK` case. This
`Step` method casts `Space128` to `TS`, runs the strongly-typed `IterabkeK.Step` and then casts it
back.  

It has some risks (if `TS` is bigger than `Space128`), but safeguards can be put in for that. 

An instance of the `VirtualTable<T, TS, A>` is then statically cached as a `VirtualTable<A>`...

```c#
public static class VirtualTableCache<T, TS, A>
    where T : IterableK<T, TS>
    where TS : struct
{
    public static readonly VirtualTable<A> Cache = new VirtualTable<T, TS, A>();
}
```
Whenever an `Iterator<T, TS, A>` is constructed in a `IterableK` case, then it sets `vt` to the 
correct `VirtualTableCache<T, TS, A>.Cache` value:

```c#
internal Iterator(in A head, object source, in TS state)
{
    tag = 5;
    this.head = head;
    obj1 = source;
    vt = VirtualTableCache<T, TS, A>.Cache;
    space = state;
}
```

## All of this works by the way!

So, what's the problem? This is the output of the benchmarks from this console app...

```
[Benchmark 1] Elapsed: 409 µs    Each: 0.409 ns    Foreach C# array (1,000,000 items)
[Benchmark 2] Elapsed: 392 µs    Each: 0.392 ns    Foreach current LanguageExt Arr<A> (1,000,000 items)
[Benchmark 3] Elapsed: 520 µs    Each: 0.520 ns    IterableK trait stepping (1,000,000 items)
[Benchmark 4] Elapsed: 374 µs    Each: 0.374 ns    Foreach Array<A> (1,000,000 items)
[Benchmark 5] Elapsed: 3996 µs   Each: 3.995 ns    Strong Iterator while TryGetValue (1,000,000 items)
[Benchmark 6] Elapsed: 36665 µs  Each: 36.665 ns   Weak Iterator while TryGetValue (1,000,000 items)
```
Each test iterates over 1,000,000 items, summing a total value. The `Elapsed` value is the total time 
taken for 1,000,000 additions and the machinery of the iteration (in microseconds). The `Each` value is 
how many nanoseconds it takes to iterate one value and add to the total.

> I'm using basic benchmarking here, but it's good enough to test the concepts before breaking out 
> more advanced benchmarking tools. There are warm-up runs before a final run, which is enough when
> prototyping.

### Benchmark 1

The first one is the baseline, it is a `foreach` over a C# array:
```c#
int[] arr = ...
    
foreach (var x in arr)
{
    total += x;
}
```

> _I'm using arrays for the benchmarks because arrays are highly optimised in C#, they have very little boilerplate or fat. 
> So, a generalised immutable-array type that supports `IterableK` going head-to-head with C#'s built-in arrays and 
> mutable enumerators is the ultimate test._

### Benchmark 2

The second one is a bespoke `struct` enumerator from the currently released language-ext (`v5.0.0-beta-77`). It is fast, 
but it is mutable and doesn't generalise the concept of iterables. It should always be around the speed of `int[]` enumeration:

```c#
Arr<int> arr = ...

foreach(var x in arr)
{
    total += x;
}
```
### `Array<A>`

The rest of the benchmarks will use the prototype code from this application. They are all using `Array<A>` which has 
been created to test these concepts. It is a generalised immutable-array type that implements the `IterableK` trait-type. 

This is the core definition:

```c#
public record Array<A>(A[] Items)
    : IterableBase<Array, ArrayState, Array<A>, A>;
```
As you can see the implementation is trivial. 

The trait implementation is a little bit more complex, but not that much:
```c#
public partial class Array : IterableK<Array, ArrayState>
{
    public static ArrayState Setup<A>(K<Array, A> ta) =>
        ta is Array<A> arr
            ? new ArrayState(arr.Items, 0, arr.Items.Length)
            : throw new InvalidCastException();

    static bool IterableK<Array, ArrayState>.Step<A>(ref ArrayState ts, out A value)
    {
        var index = ts.Index;
        var count = ts.Count;
        
        if(index >= count)
        {
            value = default!;
            return false;
        }
        
        var items = ts.Items;
        var array = Unsafe.As<object, A[]>(ref items);
        ts = new ArrayState(items, index + 1, count);
        value = array[index];
        
        return true;
    }
}
```

`Array<A>` inherits from `IterableBase` which confers some instance-method defaults: 

```c#
public interface IterableBase<T, TS, TA, A> : K<T, A>
    where T : IterableK<T, TS>
    where TS : struct
    where TA : IterableBase<T, TS, TA, A>
{
    Iterator<T, TS, A> Forward()
    {
        var     ta = this;
        var     i1 = T.Forward(ta);
        ref var i2 = ref Unsafe.As<Iterator<A>, Iterator<T, TS, A>>(ref i1);
        return i2;
    }
    
    IterableKEnumerator<T, TS, A> GetEnumerator() =>
        new (this);

    ReadOnlySpan<A> AsSpan()
    {
        var ta = this;
        var w  = ArrayWriter<A>.Init();
        var s  = T.Setup(ta);
        while (T.Step(ref s, out A x))
        {
            ArrayWriter<A>.Add(ref w, x);
        }
        return w.View;
    }
        
    IEnumerable<A> AsEnumerable() =>
        new IteratorEnumerable<T, TS, A>(this);
}
````
So, we can easily get a strongly-typed `Iterator<T, TS, A>` from `Forward()`; a struct-based enumerator
from `GetEnumerator()`; and bounce the collection to a `ReadOnlySpan<A>` at speed. None of this is difficult for the
collection-author (other than the efficient implementation of `Step`).

Benchmark `3` uses the `IterableK` trait module-methods. Benchmarks `4` and `5` use the generalised methods from 
`IterableBase`. So, from this point on, these are all benchmarks for the prototype code...   

### Benchmark 3

The third benchmark is manually using the `IterableK` trait methods using the `Array<A>` type in this prototype.

```c#
var array = Array.create(..count);
var state = IterableK.setup<Array, ArrayState, int>(array);

while (IterableK.step<Array, ArrayState, int>(ref state, out var x))
{
    total += x;
}
```

It has similar performance to C# array iteration, but completely generalised over the `IterableK` trait.  Generalised
code usually loses performane. But here, we're only slightly down, but as the numbers vary on each run, sometimes it's
faster.  So, let's say Benchmark `1`, `2`, and `3` are of similar magnitude.

> We can also take the `state` value and clone it (standard `struct` allocation copy). That means we can use `ref` to 
> mutate our `state` in-place (like existing enumerators in C#), or we can use struct-copying to pass the `state` value 
> somewhere else.  So, as long as the `ArrayState` carries enough information to continue the iteration from the current
> iteration-step, this finally enables us to build immutable iterators! 

### Benchmark 4

The fourth benchmark uses `GetEnumerator()` from `Array<A>` (which is a default-implementation from `IterableBase`).  

```c#
var array = Array.create(..count);
foreach(var x in array)
{
    total += x;
}
```

> _This is always faster than the C# array iteration, for reasons I don't fully understand, because the code within the 
enumerator is the same as the previous benchmark which tends to vary +/- 50µs or so._ 

The returned enumerator is a completely generalised `struct` enumerator, that never needs to be manually written:
```c#
public struct IterableKEnumerator<T, TS, A>(K<T, A> ta)
    where T : IterableK<T, TS>
    where TS : struct
{
    TS foldState = T.Setup(ta);
    A? current;

    public bool MoveNext() =>
        T.Step(ref foldState, out current);

    public void Reset() =>
        foldState = T.Setup(ta);

    public A Current =>
        current!;
}
```

### Benchmark 5

This uses an `Iterator<T, TS, A>` which is returned from `Forward()` (again, a default-implementation from 
`IterableBase`). It is a completely generalised iterator, if slightly more awkward to use because of the additional 
type-parameters, but it is also immutable and will produce the same results for the same underlying data structure, every 
time. It can be truly treated as a value.

```c#
var array = Array.create(..count);
var iter  = array.Forward();

while (iter.TryGetValue(out var x, out iter))
{
    total += x;
}
```
This is where it becomes a bit problematic... it is between 6 - 10 times slower. Each iteration takes `~4 ns` rather than `~0.4 ns`. That's
the difference between 250 million iterations per second and 2.5 billion iterations per second. So, really, it's 
still very fast, but it's bugging me that it's slower.

The `TryGetValue` method is support for [the future C# 15 unions](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/unions). 
It is the _non-boxing union access pattern_ and will allow `Iterator` to be used with fast and efficient pattern-matching.

So, the above could look like this in the future:

```c#
while (iter is (var x, iter))
{
    total += x;
}
```
Or, for a really functional style:

```c#
public int Sum(Iterator<T, TS, A> iter) =>
    iter switch
    {
        Nil             => 0,
        (var x, var xs) => x + Sum(xs),
    }
```
If multiple `out` values are not supported, then we will be able to do this:
```c#
public int Sum(Iterator<T, TS, A> iter) =>
    iter switch
    {
        Nil                 => 0,
        Cons(var x, var xs) => x + Sum(xs),
    }
```
Anyway, that's for the future. I want to support that capability as soon as it's availble in C#. But this method is also
the slowest out of all of the iteration approaches (well, so far anyway)!

If we look at `TryGetValue` you can see each possible case being handled by a switch on the `tag`:

```c#
    public bool TryGetValue(out A h, out Iterator<T, TS, A> t)
    {
        switch (tag)
        {
            case 1:
                h = head;
                t = default;
                return true;
            
            case 2:
                h = head;
                t = ((Func<Iterator<T, TS, A>>)obj1!)();
                return true;
            
            case 3:
                h = head;
                t = (Iterator<T, TS, A>)obj1!;
                return true;

            case 4:
                return ((Func<Iterator<T, TS, A>>)obj1!)().TryGetValue(out h, out t);

            case 5:
                var s = space;
                h = head;
                
                if (T.Step<A>(ref s, out var nh))
                {
                    t = new Iterator<T, TS, A>(in nh, obj1!, in s);
                    return true;
                }
                else
                {
                    t = default;
                    return true;
                }
            
            case 6:
                var first = (Iterator<T, TS, A>)obj1!;
                if (first.TryGetValue(out h, out var nt))
                {
                    t = new Iterator<T, TS, A>(nt, head);
                }
                else
                {
                    h = head;
                    t = default;
                }
                return true;            

            default:
                h = default!;
                t = default!;
                return false;
        }
    }
```

* `0` is an empty iterator.
* `1` is a singleton iterator.
* `2` is where the tail is a lazy function that evaluates on-demand (standard `Cons` case).
* `3` is where the tail is a boxed `Iterator<T, TS, A>` (unboxed is impossible, it would make the type recursive).
* `4` is where the entire iterator is lazy and it needs to be acquired before running `TryGetValue` on the result.
* `5` is the `IterableK` trait case that acquires the tail by calling `IterableK.Step` like other benchmarks here.
* `6` is where the head is an `Iterator` and the `tail` is a singleton value (standard `Add` case).

The key area where the performance needs to improve is case `5`. It is up to 10 times slower and it's unclear exactly why...

There are things that will be slower:

* The switch statement itself can cause branch-prediction problems.
* `var s = space` clones the old state to the new because `s` is going to be passed by-ref to `Step`. We can't overwrite this immutable field, so the copy is required and doesn't exist in other benchmarks.
* `t = new Iterator<T, TS, A>(in nh, obj1!, in s)` is a new struct constructor that doesn't exist in other benchmarks. 

It doesn't feel like these should be a problem, but they seemingy increase the cost of iteration by a factor of 10. It 
may well be the cost of immutability and for supporting the `TryGetValue` union-access pattern. 


### Benchmark 6

Benchmark 6 is just like benchmark 5 except that instead of working with `Iterator<T, TS, A>` it works with `Iterator<A>`:

```c#
var array = Array.create(..count);
var iter  = IterableK.fromIterable<Array, ArrayState, int>(array);

while (iter.TryGetValue(out var x, out iter))
{
    total += x;
}
```
This is enabled by `IterableK.fromIterable` that creates a `Iterator<T, TS, A>` and then casts the struct to a 
`Iterator<A>`:

```c#
public static Iterator<A> fromIterable<T, TS, A>(K<T, A> ta)
    where T : IterableK<T, TS>
    where TS : struct
{
    var s = T.Setup(ta);
    if (T.Step(ref s, out A h))
    {
        var     i1 = new Iterator<T, TS, A>(in h, ta, in s);
        ref var i2 = ref Unsafe.As<Iterator<T, TS, A>, Iterator<A>>(ref i1);
        return i2;
    }
    else
    {
        return default;
    }
}
```

This uses the `VirtualTable` to support the casting to `Iterator<T, TS, A>`. So, those virtual-calls may be where the 
overhead lies, or the `Unsafe.As` casts.

## Conclusion

I've run out of steam a little with this, so if anyone wants to be a hero to try an make `Iterator<T, TS, A>` more 
efficient for `case 5` that would be great. And if anyone wants to be a mega-hero to get `Iterator<A>` into the same 
magnitude as `Iterator<T, TS, A>`, that would be awesome!

Rules:

* No allocations allowed
* Whilst using `Unsafe` is allowed, don't throw caution to the wind too much! 
  * The end code must be robust, reliable, and fast!

Paul