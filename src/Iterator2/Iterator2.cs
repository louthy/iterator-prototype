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
        Test1();
        Test2();
    }
    
    public static void Test1()
    {
        var arr = Arr.create(1..6);
        var iter1 = Iterator2.fromUnmanaged<Arr, ArrState, int>(arr);
        var iter = iter1.Prepend(0);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }
    
    public static void Test2()
    {
        var arr   = Arr.create("One", "Two", "Three", "Four", "Five");
        var iter1 = Iterator2.fromManaged<Arr, ArrState, string>(arr);
        var iter  = iter1.Prepend("Zero");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }
}

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
        
        if (vm.Run(ref frame))
        {
            vma.GetItem(ref frame, out head);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
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

public static class Iterator2
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator2<T, IS, A> fromUnmanaged<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : unmanaged
    {
        var ts = T.SetupImmutable(in ta);
        Iterator2<T, IS, A> iter = default;
        
        iter.SetVM(in IteratorUnmanagedVM<T, IS, A>.Instance);
        iter.ops.Push(ta);
        iter.values.Push(ts);
        return iter;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator2<T, IS, A> fromManaged<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : class
    {
        var                 ts   = T.SetupImmutable(in ta);
        Iterator2<T, IS, A> iter = default;
        
        iter.SetVM(in IteratorManagedVM<T, IS, A>.Instance);
        iter.ops.Push(ta);
        iter.values.Push(ts);
        return iter;
    }    
}

readonly ref struct StackFrame
{
    public readonly ref IteratorVM VM;
    public readonly ref OpStack Ops;
    public readonly ref ObjStack Objs;
    public readonly ref ByteStack Values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref IteratorVM vm, ref OpStack ops, ref ObjStack objs, ref ByteStack values)
    {
        VM = ref vm;
        Ops = ref ops;
        Objs = ref objs;
        Values = ref values;
    }
}



/*
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
}*/

abstract class IteratorVM
{
    public abstract bool Run(ref StackFrame frame);
    public abstract IteratorVM Parent { get; }
}

abstract class IteratorVM<A> : IteratorVM
{
    public abstract void GetItem(ref StackFrame frame, out A value);
    public abstract IteratorVM<A> Prepend(A value);
}

class EmptyIteratorVM : IteratorVM
{
    public static readonly IteratorVM Instance = 
        new EmptyIteratorVM();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => this;
    }
}

abstract class IteratorManagedVM<A> : IteratorVM<A>
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override void GetItem(ref StackFrame frame, out A value)
    {
        value = frame.Objs.Peek<A>(); 
        frame.Objs.Pop();      
    }    
}

abstract class IteratorUnmanagedVM<A> : IteratorVM<A>
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override void GetItem(ref StackFrame frame, out A value)
    {
        value = frame.Values.Peek<A>(); 
        frame.Values.Pop();      
    }    
}

class IteratorUnmanagedVM<T, IS, A>(IteratorVM parent) : IteratorUnmanagedVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : unmanaged
{
    public static readonly IteratorVM Instance = 
        new IteratorUnmanagedVM<T, IS, A>(EmptyIteratorVM.Instance);

    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => parent;
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        // Instruction stack frame
        ref var opsFrame = ref frame.Ops.AtTop;
        ref var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in opsFrame.Self)); 
        ref var space = ref frame.Values.Peek<IS>();

        if (T.Next(in ta, ref space, out var head))
        {
            frame.Values.Push(head);
            
            while (opsFrame.NextPC<A>(out var op) && op.Run(ref frame))
                /* Left empty on purpose */;
            
            opsFrame.ResetPC();
            return true;
        }
        else
        {
            frame.Ops.Pop();            // Remove the `ops` stack-frame
            frame.VM = frame.VM.Parent; // Look for an operation to call back to
            return frame.VM.Run(ref frame);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorVM<A> Prepend(A value) =>
        new ConsUnmanagedVM<T, IS, A>(value, this);
}

class IteratorManagedVM<T, IS, A>(IteratorVM parent) : IteratorManagedVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : class
{
    public static readonly IteratorVM Instance = 
        new IteratorManagedVM<T, IS, A>(EmptyIteratorVM.Instance);

    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => parent;
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        // Instruction stack frame
        ref var opsFrame = ref frame.Ops.AtTop;
        ref var ta       = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in opsFrame.Self)); 
        ref var space    = ref frame.Values.Peek<IS>();

        if (T.Next(in ta, ref space, out var head))
        {
            frame.Objs.Push(head);
            
            while (opsFrame.NextPC<A>(out var op) && op.Run(ref frame))
                /* Left empty on purpose */;
            
            opsFrame.ResetPC();
            return true;
        }
        else
        {
            frame.Ops.Pop();            // Remove the `ops` stack-frame
            frame.VM = frame.VM.Parent; // Look for an operation to call back to
            return frame.VM.Run(ref frame);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorVM<A> Prepend(A value) =>
        new ConsManagedVM<T, IS, A>(value, this);
}

sealed class ConsManagedVM<T, IS, A>(A Head, IteratorVM Tail) : IteratorManagedVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : class
{
    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => Tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Objs.Push(Head);
        frame.VM = Tail;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorVM<A> Prepend(A value) =>
        new ConsManagedVM<T, IS, A>(value, this);
}

sealed class ConsUnmanagedVM<T, IS, A>(A Head, IteratorVM Tail) : IteratorUnmanagedVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : unmanaged
{
    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => Tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Values.Push(Head);
        frame.VM = Tail;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorVM<A> Prepend(A value) =>
        new ConsUnmanagedVM<T, IS, A>(value, this);
}

/*
internal class ArrowUnmanagedUnmanagedVM<T, IS, A, B>(IteratorVM<A> First) : IteratorVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where A : unmanaged
    where B : unmanaged
    where IS : unmanaged
{
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
}*/