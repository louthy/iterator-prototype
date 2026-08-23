using System.Runtime.CompilerServices;
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


public class IteratorTest2
{
    public static void Run()
    {
        var arr = Arr.create(1..6);
        var iter1 = Iterator2.fromUnmanaged<Arr, ArrState, int>(arr);
        var iter = iter1.Prepend(0);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine(head);
        }
    }
}

[SkipLocalsInit]
public readonly struct Iterator2<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    internal readonly IteratorVM<A> vm;
    internal readonly OpStack ops;
    internal readonly ObjStack objs;
    internal readonly ByteStack values;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator2<T, IS, A> tail)
    {
        tail = this; // Copy - consider better ways, but remember, `this` might also be `tail`.

        var frame = new StackFrame(
            ref Unsafe.AsRef(in tail.ops), 
            ref Unsafe.AsRef(in tail.objs), 
            ref Unsafe.AsRef(in tail.values));

        ref var self = ref Unsafe.AsRef(in tail.vm); 
        return self.Run(ref self, ref frame, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<T, IS, A> Prepend(A head)
    {
        Iterator2<T, IS, A> iter = default;
        CopyTo(ref iter);

        ref var self = ref Unsafe.AsRef(in iter.vm);
        self = new ConsVM<T, IS, A>(head, vm);
        return iter;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void SetVM(in IteratorVM<A> tvm)
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
}

public static class Iterator2
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator2<T, IS, A> fromManaged<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : class
    {
        var ts = T.SetupImmutable(in ta);
        Iterator2<T, IS, A> iter = default;
        iter.SetVM(in IteratorManagedVM<T, IS, A>.Instance);
        iter.ops.Push(ta);
        iter.ops.Add(OpManaged<T, IS, A>.Id);
        iter.values.Push(ts);
        return iter;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator2<T, IS, A> fromUnmanaged<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : unmanaged
    {
        var                 ts   = T.SetupImmutable(in ta);
        Iterator2<T, IS, A> iter = default;
        iter.SetVM(in IteratorUnmanagedVM<T, IS, A>.Instance);
        iter.ops.Push(ta);
        iter.ops.Add(OpUnmanaged<T, IS, A>.Id);
        iter.values.Push(ts);
        return iter;
    }
}

readonly ref struct StackFrame
{
    public readonly ref OpStack Ops;
    public readonly ref ObjStack Objs;
    public readonly ref ByteStack Values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref OpStack ops, ref ObjStack objs, ref ByteStack values)
    {
        Ops = ref ops;
        Objs = ref objs;
        Values = ref values;
    }
}

static class OpManaged<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : class
{
    public static readonly Op Id = new PureManaged<T, IS, A>();
}

static class OpUnmanaged<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : unmanaged
{
    public static readonly Op Id = new PureUnmanaged<T, IS, A>();
}

internal abstract class Op
{
    public abstract bool Run(ref StackFrame frame);
}
internal abstract class Op<A> : Op;
internal abstract class Op<A, B> : Op<B>;

class NoOp<A> : Op<A>
{
    public override bool Run(ref StackFrame frame) =>
        false;
}

class PureManaged<T, IS, A> : Op<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        // Instruction stack frame
        ref var opsFrame = ref frame.Ops.AtTop;
        
        // Find out the iterable we're working with
        ref var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in opsFrame.Self)); 
        
        // Find out the state the iterable is in
        ref var space = ref frame.Values.Peek<IS>();

        if (T.Next(in ta, ref space, out var head))
        {
            frame.Objs.Push(in head);
            return true;
        }
        else
        {
            frame.Ops.Pop();
            return false;
        }
    }
}

class PureUnmanaged<T, IS, A> : Op<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        // Instruction stack frame
        ref var opsFrame = ref frame.Ops.AtTop;
        
        // Find out the iterable we're working with
        ref var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in opsFrame.Self)); 
        
        // Find out the state the iterable is in
        ref var space = ref frame.Values.Peek<IS>();

        if (T.Next(in ta, ref space, out var head))
        {
            frame.Values.Push(in head);
            return true;
        }
        else
        {
            frame.Ops.Pop();
            return false;
        }
    }
}

internal abstract class IteratorVM<A>
{
    public abstract bool Run(ref IteratorVM<A> self, ref StackFrame frame, out A head);
}

internal class IteratorManagedVM<T, IS, A> : IteratorVM<A>
    where A : class
{
    public static readonly IteratorVM<A> Instance = new IteratorManagedVM<T, IS, A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref IteratorVM<A> self, ref StackFrame frame, out A head)
    {
        do
        {
            if (!frame.Ops.AtPC.Run(ref frame))
            {
                // Reset - may need something more advanced
                frame.Ops.ResetPC();
                
                head = null!;
                return false;
            }
        } while (frame.Ops.NextPC());
        
        ref var result = ref frame.Objs.Peek<A>();
        head = result;
        
        // Reset - may need something more advanced
        frame.Objs.Pop();
        frame.Ops.ResetPC();
        
        return true;
    }
}

internal class IteratorUnmanagedVM<T, IS, A> : IteratorVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : unmanaged
{
    public static readonly IteratorVM<A> Instance = new IteratorUnmanagedVM<T, IS, A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref IteratorVM<A> self, ref StackFrame frame, out A head)
    {
        do
        {
            if (!frame.Ops.AtPC.Run(ref frame))
            {
                // Reset - may need something more advanced
                frame.Ops.ResetPC();
                
                head = default;
                return false;
            }
        } while (frame.Ops.NextPC());
        
        ref var result = ref frame.Values.Peek<A>();
        head = result;
        
        // Reset - may need something more advanced
        frame.Values.Pop();
        frame.Ops.ResetPC();
        
        return true;
    }
}

internal class ConsVM<T, IS, A>(A Head, IteratorVM<A> Tail) : IteratorVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref IteratorVM<A> self, ref StackFrame frame, out A head)
    {
        head = Head;
        self = Tail;
        return true;
    }
}