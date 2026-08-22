using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class MiniStack
{
    extension<T, IS, A, B>(ref MiniStack<IteratorFields<T, IS, A, B>> stack)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref object GetThisUntyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<K<T, A>, object>(ref Unsafe.AsRef(in fields.ta));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetThis(in object ta)
        {
            ref var fta = ref stack.GetThis();
            fta = Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in ta));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref K<T, A> GetThis()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.ta);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetThis(in K<T, A> ta)
        {
            ref var fta = ref stack.GetThis();
            fta = ta;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetAction(in IteratorAction<A, B> action)
        {
            ref var fa = ref stack.GetAction();
            fa = action;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IteratorAction<A, B> GetAction()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<B>, IteratorAction<A, B>>(ref Unsafe.AsRef(in fields.action));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IteratorAction GetActionUntyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<B>, IteratorAction>(ref Unsafe.AsRef(in fields.action));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetAction(in IteratorAction<T, IS, A, B> action)
        {
            ref var fa = ref stack.GetActionMapped();
            fa = action;
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IteratorAction<T, IS, B> GetActionTyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<B>, IteratorAction<T, IS, B>>(ref Unsafe.AsRef(in fields.action));
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IteratorAction<T, IS, A, B> GetActionMapped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<B>, IteratorAction<T, IS, A, B>>(ref Unsafe.AsRef(in fields.action));
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetSpace(in Space128 space)
        {
            ref var fs = ref stack.GetSpaceUntyped();
            fs = space;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref Space128 GetSpaceUntyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fields.space));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetSpace(in IS space)
        {
            ref var fs = ref stack.GetSpace();
            fs = space;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IS GetSpace()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.space);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<IteratorFields<T, IS, A, C>> Map<C>(in Func<B, C> f)
        {
            ref var actionAB = ref stack.GetAction();
            ref var actionAC = ref Unsafe.As<IteratorAction<A, B>, IteratorAction<C>>(ref actionAB);
            actionAC = actionAB.Map(f);
            return ref Unsafe.As<MiniStack<IteratorFields<T, IS, A, B>>, MiniStack<IteratorFields<T, IS, A, C>>>(ref stack);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<IteratorFields<T, IS, A, C>> Bind<C>(in Func<B, Iterator<C>> f)
        {
            ref var actionAB = ref stack.GetAction();
            ref var actionAC = ref Unsafe.As<IteratorAction<A, B>, IteratorAction<C>>(ref actionAB);
            actionAC = actionAB.Bind(f);
            return ref Unsafe.As<MiniStack<IteratorFields<T, IS, A, B>>, MiniStack<IteratorFields<T, IS, A, C>>>(ref stack);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<IteratorFields<T, IS, A, B>> Concat(in Iterator<B> rhs)
        {
            ref var action  = ref stack.GetAction();
            ref var actionB = ref Unsafe.As<IteratorAction<A, B>, IteratorAction<B>>(ref action);
            actionB = action.Concat(rhs);
            return ref stack;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<IteratorFields<T, IS, A, B>> Concat(in Iterator<T, IS, B> rhs)
        {
            ref var action  = ref stack.GetActionTyped();
            action = new ConcatAction<T, IS, B>(action, rhs);
            return ref stack;
        }
    }
}
