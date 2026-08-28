using System.Numerics;
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsExtensions
{
    extension(ref Vars stack)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Pop<A>(out A top)
        {
            if (typeof(A) == typeof(LE.Unit))
            {
                top = default!;
                return stack.values.Pop<byte>(out _);
            }
            else
            {
                return VarsGen<A>.Instance.PopImpl(ref stack, out top);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Pop<A>()
        {
            if (typeof(A) == typeof(LE.Unit))
            {
                return stack.values.Pop<byte>(out _);
            }
            else
            {
                return VarsGen<A>.Instance.PopImpl(ref stack);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Swap<A>(in A value) =>
            stack.Pop<A>() &&
            stack.Push(in value);


        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Sub1<A>() 
            where A : INumber<A> =>
            stack.Sub(A.One);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Sub1<A>(out A newValue)
            where A : INumber<A> =>
            stack.Sub(A.One, out newValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Sub<A>(in A value) 
            where A : INumber<A> =>
            stack.Pop<A>(out var x) &&
            stack.Push(x - value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Sub<A>(in A value, out A newValue)
            where A : INumber<A>
        {
            if (stack.Pop<A>(out var x))
            {
                var nx = x - value;
                if (stack.Push(in nx))
                {
                    newValue = nx;
                    return true;
                }
            }
            newValue = default!;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add1<A>() 
            where A : INumber<A> =>
            stack.Add(A.One);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add1<A>(out A newValue)
            where A : INumber<A> =>
            stack.Add(A.One, out newValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add<A>(in A value) 
            where A : INumber<A> =>
            stack.Pop<A>(out var x) &&
            stack.Push(x + value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add<A>(in A value, out A newValue)
            where A : INumber<A>
        {
            if (stack.Pop<A>(out var x))
            {
                var nx = x + value;
                if (stack.Push(in nx))
                {
                    newValue = nx;
                    return true;
                }
            }
            newValue = default!;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool IsZero<A>(in A value) 
            where A : INumber<A> =>
            stack.Peek<A>(out var x) && x == A.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool PopUnit() =>
            stack.values.Pop<byte>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Prepend<A>(in A top) =>
            VarsGen<A>.Instance.PrependImpl(ref stack, in top);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool AddArg<A>(in A top) =>
            stack.Prepend(in top);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Push<A>(in A top) =>
            typeof(A) == typeof(LE.Unit)
                ? stack.values.Push<byte>(0)
                : VarsGen<A>.Instance.PushImpl(ref stack, in top);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool PushUnit() =>
            stack.values.Push<byte>(0);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Peek<A>(out A top)
        {
            if (typeof(A) == typeof(LE.Unit))
            {
                top = default!;
                return stack.values.Peek<byte>(out _);
            }
            else
            {
                return VarsGen<A>.Instance.PeekImpl(ref stack, out top);
            }
        }
    }
}
