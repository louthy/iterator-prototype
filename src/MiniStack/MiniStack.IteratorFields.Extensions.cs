using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class MiniStack
{
    extension(ref MiniStack<IteratorFields> stack)
    {
        [MethodImpl(Optimisations.Default)]
        public void SetThis(in object ta)
        {
            ref var fta = ref stack.GetThis();
            fta = ta;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref object GetThis()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.ta);
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetThis<T, A>(in K<T, A> ta)
        {
            ref var fta = ref stack.GetThis<T, A>();
            fta = ta;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref K<T, A> GetThis<T, A>()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in fields.ta));
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetAction(in IteratorAction action)
        {
            ref var fa = ref stack.GetAction();
            fa = action;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref IteratorAction GetAction()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.action);
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetAction<A>(in IteratorAction<A> action)
        {
            ref var fa = ref stack.GetAction();
            fa = action;
        }        
        
        [MethodImpl(Optimisations.Default)]
        public ref IteratorAction<A> GetAction<A>()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref Unsafe.AsRef(in fields.action));
        }        
        
        [MethodImpl(Optimisations.Default)]
        public void SetAction<T, IS, A>(in IteratorAction<T, IS, A> action)
            where T : Tr.IterableImmutable<T, IS>
            where IS : unmanaged
        {
            ref var fa = ref stack.GetAction();
            fa = action;
        }        
        
        [MethodImpl(Optimisations.Default)]
        public ref IteratorAction<T, IS, A> GetAction<T, IS, A>()
            where T : Tr.IterableImmutable<T, IS>
            where IS : unmanaged
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<IteratorAction, IteratorAction<T, IS, A>>(ref Unsafe.AsRef(in fields.action));
        }        
        
        [MethodImpl(Optimisations.Default)]
        public void SetSpace(in Space128 space)
        {
            ref var fs = ref stack.GetSpace();
            fs = space;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref Space128 GetSpace()
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.AsRef(in fields.space);
        }
        
        [MethodImpl(Optimisations.Default)]
        public void SetSpace<IS>(in IS space)
            where IS : unmanaged
        {
            ref var fs = ref stack.GetSpace<IS>();
            fs = space;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref IS GetSpace<IS>()
            where IS : struct
        {
            ref var fields = ref stack.Peek();
            return ref Unsafe.As<Space128, IS>(ref Unsafe.AsRef(in fields.space));
        }

        [MethodImpl(Optimisations.Default)]
        public ref MiniStack<IteratorFields> Map<A, B>(in Func<A, B> f)
        {
            ref var action  = ref stack.GetAction();
            ref var actionA = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref action);
            action = actionA.Map(f);
            return ref stack;
        }
        
        [MethodImpl(Optimisations.Default)]
        public ref MiniStack<IteratorFields> Bind<A, B>(in Func<A, Iterator<B>> f)
        {
            ref var action  = ref stack.GetAction();
            ref var actionA = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref action);
            action = actionA.Bind(f);
            return ref stack;
        }
                
        [MethodImpl(Optimisations.Default)]
        public ref MiniStack<IteratorFields> Concat<A>(in Iterator<A> rhs)
        {
            ref var action  = ref stack.GetAction();
            ref var actionA = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref action);
            action = actionA.Concat(rhs);
            return ref stack;
        }
    }
}
