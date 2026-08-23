using System.Runtime.CompilerServices;
using IteratorPrototype.Internal;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.VM;
using LanguageExt.Traits;

namespace IteratorPrototype;


// FACTS:
//
// I need to be able to push an Object and Space onto a stack
//   * Those should be pushed together as a stack-frame.
// A 'program' needs to run on a series of instructions
//   * The program needs to acquire the T, IS, A (and B) values through inheritance
//   * The program will need a program-counter (PC) to know which instruction we're on
//   * The instructions will need a ref stack of stack-frames.  It works on what's on top of the stack.
//   * Each instruction will need to know how to pop arguments off the stack also.

[SkipLocalsInit]
public readonly struct Iterator2<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    internal readonly IteratorVM vm;
    internal readonly OpStack ops;
    internal readonly ObjStack objs;
    internal readonly ByteStack values;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator2<T, IS, A> tail)
    {
        tail = this; // Copy - consider better ways, but remember, `this` might also be `tail`.

        var frame = new StackFrame(
            ref Unsafe.AsRef(in tail.vm),
            ref Unsafe.AsRef(in tail.ops), 
            ref Unsafe.AsRef(in tail.objs), 
            ref Unsafe.AsRef(in tail.values));

        var vma = (IteratorVM<A>)vm;
        return vma.Run(ref frame, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<T, IS, A> Prepend(A head)
    {
        Iterator2<T, IS, A> iter = default;
        CopyTo(ref iter);
        ref var vm1 = ref Unsafe.AsRef(in iter.vm);
        vm1 = ((IteratorVM<A>)iter.vm).Prepend(head);
        return iter;
    }

    /*[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<T, IS, B> Map<B>(Func<A, B> f)
    {
        Iterator2<T, IS, B> iter = default;
        CopyTo(ref iter);

        ref var self = ref Unsafe.AsRef(in iter.vm);
        self = new ArrowVM<T, IS, A, B>(vm);
        return iter;
    }   */ 

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void SetVM(in IteratorVM tvm)
    {
        ref var ivm = ref Unsafe.AsRef(in vm);
        ivm = tvm;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void CopyTo(ref Iterator2<T, IS, A> other)
    {
        other.SetVM(in vm);
        ops.CopyTo(ref Unsafe.AsRef(in other.ops));
        objs.CopyTo(ref Unsafe.AsRef(in other.objs));
        values.CopyTo(ref Unsafe.AsRef(in other.values));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void CopyTo<B>(ref Iterator2<T, IS, B> other)
    {
        ops.CopyTo(ref Unsafe.AsRef(in other.ops));
        objs.CopyTo(ref Unsafe.AsRef(in other.objs));
        values.CopyTo(ref Unsafe.AsRef(in other.values));
    }    
}
