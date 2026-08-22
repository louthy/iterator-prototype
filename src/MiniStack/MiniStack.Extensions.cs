using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public static partial class MiniStack
{
    extension<A>(ref MiniStack<A> stack)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Push(in A value)
        {
            ref var top   = ref stack.Top;
            ref var items = ref stack.item0;
            if (top == 4) throw new StackOverflowException();
            ref var t = ref Unsafe.Add(ref items, top);
            t = value;
            top++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void PushMany(in MiniStack<A> values)
        {
            ref var          top    = ref stack.Top;
            ref var          items  = ref stack.item0;
            ref readonly var vtop   = ref values.Top;
            ref readonly var vitems = ref values.item0;
            if (top + vtop >= 4) throw new StackOverflowException();

            ref var t = ref Unsafe.Add(ref items, top);
            ref var vt = ref Unsafe.Add(ref Unsafe.AsRef(in vitems), vtop);
            
            for (var i = 0; i < vtop; i++)
            {
                t = vt;
                t = ref Unsafe.Add(ref t, 1);
                vt = ref Unsafe.Add(ref vt, 1);
            }
            top++;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref A Pop()
        {
            ref var top   = ref stack.Top;
            ref var items = ref stack.item0;
            if (top == 0) throw new StackUnderflowException();
            top--;
            return ref Unsafe.Add(ref items, top);
        }
    }
    
    extension<A>(in MiniStack<A> stack)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref A Peek()
        {
            ref var s     = ref Unsafe.AsRef(in stack);
            ref var top   = ref s.Top;
            ref var items = ref s.item0;
            if (top == 0) throw new StackUnderflowException();
            return ref Unsafe.Add(ref Unsafe.AsRef(in items), top - 1);
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<B> Cast<B>() =>
            ref Unsafe.As<MiniStack<A>, MiniStack<B>>(ref Unsafe.AsRef(in stack));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void CloneAllButTop(out MiniStack<A> ns)
        {
            ns = stack;
            ref var s    = ref Unsafe.AsRef(in ns);
            ref var top  = ref s.Top;
            ref var item = ref Unsafe.Add(ref ns.item0, top);
            if (top == 0) throw new StackUnderflowException();
            top--;
            item = default!;
        }        
    }
}
