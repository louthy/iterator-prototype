using LanguageExt.Traits;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using static LanguageExt.Prelude;

namespace IteratorPrototype.DSL;

class ArrMany<A>(A[] values, int start, int count) : Arr<A>
{
    int? hashCode;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override ref readonly  A AtRef(int index) => 
        ref values[start + index];
    
    public override object Value => 
        TryGetValue(out var h, out var t) 
            ? new Cons<Arr, ArrState, A>(h, t) 
            : Nil.Obj;

    public override bool HasValue =>
        true;

    public override bool IsEmpty =>
        false;

    public override int Count => 
        count;

    public override Arr<A> Tail =>
        Slice(1, Count - 1);

    public override Arr<A> Init =>
        Slice(0, Count - 1);

    public override LE.Option<A> At(Index index) =>
        (index.IsFromEnd, index.Value) switch
        {
            (false, var ix)        when ix < count  => values[start         + ix],
            (true, var ix and > 0) when ix <= count => values[start + count - ix],
            _                                       => default
        };

    public override bool TryGetValue(out Nil nil)
    {
        nil = default;
        return count == 0;
    }

    public override bool TryGetValue(out A head, out Iterator<Arr, ArrState, A> tail)
    {
        Debug.Assert(count > 0);
        head = values[start];
        tail = default!;
        return true;
    }

    public override ReadOnlySpan<A> AsSpan() =>
        new (values, start, count); 

    public override ReadOnlySpan<A> AsSpan(int skip) =>
        skip >= count
            ? ReadOnlySpan<A>.Empty
            : new (values, start + skip, count - skip);

    public override ReadOnlySpan<A> AsSpan(int skip, int take) =>
        count - skip <= 0 || take <= 0
            ? ReadOnlySpan<A>.Empty
            :  new (values, start + skip, Math.Min(take, count - skip));

    public override Arr<A> Slice(int skip) =>
        (count - skip) switch
        {
            < 1   => Empty,
            1     => new ArrSingleton<A>(values[start + skip]),
            var n => new ArrMany<A>(values, start + skip, n)
        };

    public override Arr<A> Slice(int skip, int take) =>
        take switch
        {
            < 1 => Empty,
            1   => count - skip > 0 ? new ArrSingleton<A>(values[start + skip]) : Empty,
            var t => (count - skip) switch
                     {
                         < 1   => Empty,
                         1     => new ArrSingleton<A>(values[start + skip]),
                         var n => new ArrMany<A>(values, start + skip, Math.Min(t, n))
                     }
        };

    public override LE.Option<Arr<A>> SetItem(Index index, A val)
    {
        var offset = index.GetOffset(Count);
        if(offset < 0 || offset >= Count) return None;
        var ovalues = AsSpan();
        var nvalues = new A[count - start];
        nvalues[offset] = val;
        ovalues.CopyTo(nvalues);
        return new ArrMany<A>(nvalues, 0, count);
    }

    public override Arr<A> Add(in A value)
    {
        var span   = AsSpan();
        var narray = new A[count + 1];
        var nspan  = narray.AsSpan();
        span.CopyTo(nspan);
        narray[^1] = value;
        return new ArrMany<A>(narray, 0, count + 1);
    }

    public override Arr<A> Cons(in A value)
    {
        var span   = AsSpan();
        var narray = new A[count + 1];
        var nspan  = narray.AsSpan();
        span.CopyTo(nspan[1..]);
        narray[0] = value;
        return new ArrMany<A>(narray, 0, count + 1);
    }

    public override Arr<A> AddRange(in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var lcount = Count;
        var rcount = range.Length;
        var ncount = lcount + rcount;
        var lspan  = AsSpan();
        var narray = new A[ncount];
        var nlspan = narray.AsSpan(0, lcount);
        var nrspan = narray.AsSpan(lcount, rcount);
        
        lspan.CopyTo(nlspan);
        range.CopyTo(nrspan);
        
        return new ArrMany<A>(narray, 0, ncount);
    }

    public override Arr<A> ConsRange(in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var lcount = Count;
        var rcount = range.Length;
        var ncount = lcount + rcount;
        var lspan  = AsSpan();
        var narray = new A[ncount];
        var nlspan = narray.AsSpan(0, rcount);
        var nrspan = narray.AsSpan(rcount, lcount);
        
        range.CopyTo(nlspan);
        lspan.CopyTo(nrspan);
        
        return new ArrMany<A>(narray, 0, ncount);
    }
    
    public override LE.Option<Arr<A>> Insert(Index index, in A value)
    {
        var offset = index.GetOffset(Count);
        
        if (offset == 0)
        {
            return Cons(in value);
        }
        else if (offset == Count)
        {
            return Add(in value);
        }
        else if(offset > 0 && offset < Count)
        {
            var narray  = new A[Count + 1];
            var nspan   = narray.AsSpan();
            var span    = AsSpan();
            var roffset = offset + 1;
            
            span[..offset].CopyTo(nspan[..offset]);
            narray[offset] = value;
            span[offset..].CopyTo(nspan[roffset..]);
            
            return new ArrMany<A>(narray, 0, narray.Length);
        }
        else
        {
            return LE.Option<Arr<A>>.None;
        }
    }
    
    public override LE.Option<Arr<A>> InsertRange(Index index, in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var offset = index.GetOffset(Count);
        if (offset == 0)
        {
            return ConsRange(in range);
        }
        else if (offset == Count)
        {
            return AddRange(in range);
        }
        else if(offset > 0 && offset < Count)
        {
            var narray  = new A[Count + range.Length];
            var nspan   = narray.AsSpan();
            var span    = AsSpan();
            var roffset = offset + range.Length;
            
            span.CopyTo(nspan[..offset]);
            range.CopyTo(nspan[offset..roffset]);
            span[offset..].CopyTo(nspan[roffset..]);
            
            return new ArrMany<A>(narray, 0, narray.Length);
        }
        else
        {
            return LE.Option<Arr<A>>.None;
        }
    }

    public override Arr<A> RemoveAtHead()
    {
        if (Count == 2) return new ArrSingleton<A>(values[1]); 
        var narray = new A[Count - 1];
        var nspan  = narray.AsSpan();
        var span   = AsSpan();
        span[1..].CopyTo(nspan);
        return new ArrMany<A>(narray, 0, narray.Length);
    }

    public override Arr<A> RemoveAtLast()
    {
        if (Count == 2) return new ArrSingleton<A>(values[0]); 
        var narray = new A[Count - 1];
        var nspan  = narray.AsSpan();
        var span   = AsSpan();
        span[..^1].CopyTo(nspan);
        return new ArrMany<A>(narray, 0, narray.Length);
    }

    public override Arr<A> RemoveAt(Index index)
    {
        var offset = index.GetOffset(Count);

        if (offset == 0)
        {
            return RemoveAtHead();
        }
        else if (offset == Count)
        {
            return RemoveAtLast();
        }
        else if(offset > 0 && offset < Count)
        {
            var narray = new A[Count - 1];
            var nspan  = narray.AsSpan();
            var span   = AsSpan();
            
            span[..offset].CopyTo(nspan);
            span[(offset+1)..].CopyTo(nspan[offset..]);
            
            return new ArrMany<A>(narray, 0, narray.Length);
        }
        else
        {
            return this;
        }
    }

    public override Arr<A> RemoveAt(ReadOnlySpan<Index> indices)
    {
        if (indices.IsEmpty) return this;
        HashSet<int> set = [];
        foreach (var ix in indices)
        {
            var offset = ix.GetOffset(Count);
            if (offset >= 0 && offset < Count)
            {
                set.Add(offset);
            }
        }
        
        var w = LE.ArrayWriter<A>.Init(Count - set.Count);
        var i = 0;
        foreach (var x in values)
        {
            if (!set.Contains(i))
            {
                LE.ArrayWriter<A>.Add(ref w, in x);
            }
            i++;
        }

        return w.ToArr();
    }

    public override Arr<A> RemoveRange(in Range range)
    {
        var begin = range.Start.GetOffset(Count);
        var end   = range.End.GetOffset(Count);
        (begin, end) = begin > end 
                           ? (end, begin) 
                           : (begin, end);

        var narray = new A[Count - (end - begin)];
        var nspan  = narray.AsSpan();
        var span   = AsSpan();
        
        span[..begin].CopyTo(nspan);
        span[end..].CopyTo(nspan[begin..]);

        return [.. span];
    }

    public override Arr<A> Reverse()
    {
        var span   = AsSpan();
        var narray = new A[Count];
        var nspan  = narray.AsSpan();

        span.CopyTo(nspan);
        nspan.Reverse();

        return [.. nspan];
    }
    
    public override Arr<A> Copy() =>
        new ArrMany<A>(AsSpan().ToArray(), 0, count);
    
    public override Arr<B> Map<B>(Func<A, B> f)
    {
        var ma = this;
        var w  = LE.ArrayWriter<B>.Init(Count);
        var ts = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref ts, out var x))
        {
            w.Add(f(x));
        }
        return w.ToArr();
    }

    public override Arr<B> Bind<B>(Func<A, Arr<B>> f)
    {
        var ma = this;
        var w  = LE.ArrayWriter<B>.Init();
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref sa, out var x))
        {
            var mb = f(x);
            var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, B>(mb);
            while (IterableMutable.step<Arr, ArrState, ArrStateRef, B>(mb, ref sb, out var y))
            {
                w.Add(y);
            }
        }
        return w.ToArr();
    }

    public override Arr<B> Bind<B>(Func<A, K<Arr, B>> f)
    {
        var ma = this;
        var w  = LE.ArrayWriter<B>.Init();
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref sa, out var x))
        {
            var mb = f(x);
            var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, B>(mb);
            while (IterableMutable.step<Arr, ArrState, ArrStateRef, B>(mb, ref sb, out var y))
            {
                w.Add(y);
            }
        }
        return w.ToArr();
    }
    
    public override Arr<A> Filter(Func<A, bool> f)
    {
        var ma     = this;
        var writer = LE.ArrayWriter<A>.Init();
        var ts     = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref ts, out var x))
        {
            if (f(x))
            {
                writer.Add(x);
            }
        }
        return writer.ToArr();
    }
    
    public override Arr<A> Choose(K<Arr, A> tb) =>
        +tb;

    public override string ToString() =>
        Tr.Iterable.toString(this);

    public override int GetHashCode() =>
        hashCode ??= CalculateHashCode();

    public override bool Equals<EqA>(K<Arr, A>? rhs)
    {
        if (rhs is not ArrMany<A>) return false;
        if (Count != rhs.Count) return false;

        var ta = this;
        var tb = rhs;
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ta);
        var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(tb);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ta, ref sa, out var x) &&
               IterableMutable.step<Arr, ArrState, ArrStateRef, A>(tb, ref sb, out var y))
        {
            if (!EqA.Equals(x, y)) return false;
        }
        
        return true;
    }
    
    public override int CompareTo<OrdA>(K<Arr, A>? rhs)
    {
        if (rhs is null || Count > rhs.Count) return 1;
        if (Count < rhs.Count) return -1;

        var ta = this;
        var tb = rhs;
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ta);
        var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(tb);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ta, ref sa, out var x) &&
               IterableMutable.step<Arr, ArrState, ArrStateRef, A>(tb, ref sb, out var y))
        {
            switch (OrdA.Compare(x, y))
            {
                case <0: return -1;
                case >0: return 1;
            }
        }
        return 0;
    }
    
    protected override int CalculateHashCode(int offsetBasis = -2128831035)
    {
        var hash = offsetBasis;
        const int prime = 16777619;

        unchecked
        {
            var xs = values;
            for (var current = start; current < start + count; current++)
            {
                var x = xs[current];
                hash = ((x?.GetHashCode() ?? 0) ^ hash) * prime;
            }
            return hash;
        }        
    }
}
