using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class MiniStack
{
    extension<T, IS, A>(ref MiniStack<IteratorFields<T, IS, A>> stack)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        [MethodImpl(Optimisations.Default)]
        public ref object GetThisUntyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<K<T, A>, object>(ref Unsafe.AsRef(in fields.ta));
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetThis(in object ta)
        {
            ref var fta = ref stack.GetThis();
            fta = Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in ta));
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref K<T, A> GetThis()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.ta);
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetThis(in K<T, A> ta)
        {
            ref var fta = ref stack.GetThis();
            fta = ta;
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetAction(in IteratorAction<A>? action)
        {
            ref var fa = ref stack.GetAction();
            fa = action;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref IteratorAction<A>? GetAction()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.action);
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref IteratorAction? GetActionUntyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<A>?, IteratorAction?>(ref Unsafe.AsRef(in fields.action));
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetAction(in IteratorAction<T, IS, A>? action)
        {
            ref var fa = ref stack.GetActionStrong();
            fa = action;
        }        
        
        [MethodImpl(Optimisations.Default)]
        public ref IteratorAction<T, IS, A>? GetActionStrong()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction<A>?, IteratorAction<T, IS, A>?>(ref Unsafe.AsRef(in fields.action));
        }        
        
        [MethodImpl(Optimisations.Default)]
        public void SetSpace(in Space128 space)
        {
            ref var fs = ref stack.GetSpaceUntyped();
            fs = space;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref Space128 GetSpaceUntyped()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fields.space));
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetSpace(in IS space)
        {
            ref var fs = ref stack.GetSpace();
            fs = space;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref IS GetSpace()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.space);
        }

        [MethodImpl(Optimisations.Default)]
        public ref MiniStack<IteratorFields<T, IS, B>> Map<B>(in Func<A, B> f)
        {
            ref var stackB  = ref Unsafe.As<MiniStack<IteratorFields<T, IS, A>>, MiniStack<IteratorFields<T, IS, B>>>(ref stack);
            ref var actionB = ref stackB.GetAction();
            ref var actionA = ref Unsafe.As<IteratorAction<B>?, IteratorAction<A>?>(ref actionB);
            actionB = (actionA ?? PureAction<T, IS, A>.Default).Map(f);
            return ref stackB;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref MiniStack<IteratorFields<T, IS, B>> Bind<B>(in Func<A, Iterator<B>> f)
        {
            ref var stackB = ref Unsafe.As<MiniStack<IteratorFields<T, IS, A>>, MiniStack<IteratorFields<T, IS, B>>>(ref stack);
            ref var actionB = ref stackB.GetAction();
            ref var actionA = ref Unsafe.As<IteratorAction<B>?, IteratorAction<A>?>(ref actionB);
            actionB = (actionA ?? PureAction<T, IS, A>.Default).Bind(f);
            return ref stackB;
        }
                
        [MethodImpl(Optimisations.Default)]
        public ref MiniStack<IteratorFields<T, IS, A>> Concat(in Iterator<A> rhs)
        {
            ref var action  = ref stack.GetAction();
            action = (action ?? PureAction<T, IS, A>.Default).Concat(rhs);
            return ref stack;
        }
                
        [MethodImpl(Optimisations.Default)]
        public ref MiniStack<IteratorFields<T, IS, A>> Concat(in Iterator<T, IS, A> rhs)
        {
            ref var action  = ref stack.GetAction();
            action = new ConcatAction<T, IS, A>((IteratorAction<T, IS, A>?)action ?? PureAction<T, IS, A>.Default, rhs);
            return ref stack;
        }
    }
}
