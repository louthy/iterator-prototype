using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class MiniStack
{
    extension<A>(ref MiniStack<IteratorFields<A>> stack)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetThis(in object ta)
        {
            ref var fta = ref stack.GetThis();
            fta = ta;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref object GetThis()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.ta);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetThis<T>(in K<T, A> ta)
        {
            ref var fta = ref stack.GetThis<A, T>();
            fta = ta;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref K<T, A> GetThis<T>()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in fields.ta));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetAction(in IteratorAction<A> action)
        {
            ref var fa = ref stack.GetAction();
            fa = action;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IteratorAction<A> GetAction()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.action);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IteratorAction GetActionUntyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref Unsafe.AsRef(in fields.action));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetAction<T, IS>(in IteratorAction<T, IS, A> action)
            where T : Tr.IterableImmutable<T, IS>
            where IS : unmanaged
        {
            ref var fa = ref stack.GetAction();
            fa = action;
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IteratorAction<T, IS, A> GetAction<T, IS>()
            where T : Tr.IterableImmutable<T, IS>
            where IS : unmanaged
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<A>, IteratorAction<T, IS, A>>(ref Unsafe.AsRef(in fields.action));
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetSpace(in Space128 space)
        {
            ref var fs = ref stack.GetSpace();
            fs = space;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref Space128 GetSpace()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.space);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void SetSpace<IS>(in IS space)
            where IS : struct
        {
            ref var fs = ref stack.GetSpace<A, IS>();
            fs = space;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref IS GetSpace<IS>()
            where IS : struct
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<Space128, IS>(ref Unsafe.AsRef(in fields.space));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<IteratorFields<B>> Map<B>(in Func<A, B> f)
        {
            ref var stackB  = ref Unsafe.As<MiniStack<IteratorFields<A>>, MiniStack<IteratorFields<B>>>(ref stack);
            ref var actionB = ref stackB.GetAction();
            ref var actionA = ref Unsafe.As<IteratorAction<B>, IteratorAction<A>>(ref actionB);
            actionB = actionA.Map(f);
            return ref stackB;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<IteratorFields<B>> Bind<B>(in Func<A, Iterator<B>> f)
        {
            ref var stackB = ref Unsafe.As<MiniStack<IteratorFields<A>>, MiniStack<IteratorFields<B>>>(ref stack);
            ref var actionB = ref stackB.GetAction();
            ref var actionA = ref Unsafe.As<IteratorAction<B>, IteratorAction<A>>(ref actionB);
            actionB = actionA.Bind(f);
            return ref stackB;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<IteratorFields<A>> Concat(in Iterator<A> rhs)
        {
            ref var action = ref stack.GetAction();
            action = action.Concat(rhs);
            return ref stack;
        }
    }
}
