using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IteratorTest.Traits;
using LanguageExt.Traits;

namespace IteratorTest;

public partial class Array : IterableK<Array, ArrayState, ArrayStateRef>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ArrayState IterableK<Array, ArrayState>.SetupImmutable<A>(K<Array, A> ta) =>
        ta is Array<A> arr
            ? new ArrayState(0, arr.Items.Length)
            : throw new InvalidCastException();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ArrayStateRef IterableK<Array, ArrayState, ArrayStateRef>.SetupMutable<A>(K<Array, A> ta)
    {
        if (ta is Array<A> arr)
        {
            var     array = arr.Items;
            ref var items = ref MemoryMarshal.GetArrayDataReference(array);
            ref var itemsEnd = ref Unsafe.Add(ref items, array.Length);
            var     state = new ArrayStateRef<A>(ref items, ref itemsEnd);
            return Unsafe.As<ArrayStateRef<A>, ArrayStateRef>(ref state);
        }
        else
        {
            throw new InvalidCastException();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static bool IterableK<Array, ArrayState, ArrayStateRef>.StepMutable<A>(K<Array, A> ta, ref ArrayStateRef ts, out A value)
    {
        ref var state    = ref Unsafe.As<ArrayStateRef, ArrayStateRef<A>>(ref ts);
        ref var items    = ref state.Items;
        ref var itemsEnd = ref state.ItemsEnd;

        if (Unsafe.IsAddressGreaterThanOrEqualTo(in items, in itemsEnd))
        {
            value = default!;
            return false;
        }

        value = items;
        
        items = ref Unsafe.Add(ref items, 1);
        state = new ArrayStateRef<A>(ref items, ref itemsEnd);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static bool IterableK<Array, ArrayState>.StepImmutable<A>(K<Array, A> ta, in ArrayState ts, out A head, out ArrayState tail)
    {
        ref var array = ref Unsafe.As<K<Array, A>, Array<A>>(ref ta); 
        var     index = ts.Index;
        var     count = ts.Count;

        if (index >= count)
        {
            head = default!;
            tail = default!;
            return false;
        }

        head = array.Items[index];
        tail = new ArrayState(index + 1, count);

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static void IterableK<Array, ArrayState>.NextImmutableUntyped<A>(K<Array, A> ta, ref IteratorMutable<Array, ArrayState, A> next)
    {
        ref var array = ref Unsafe.As<K<Array, A>, Array<A>>(ref ta); 
        ref var state = ref Unsafe.As<ArrayState, ArrayStateMutable>(ref next.space);
        ref var index = ref state.Index;
        ref var count = ref state.Count;

        if (index >= count)
        {
            next = default!;
            return;
        }

        next.head = array.Items[index];
        index++;
    }
 
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static void IterableK<Array, ArrayState>.NextImmutableUntyped<A>(object taObj, ref IteratorMutable<A> next)
    {
        ref var array = ref Unsafe.As<object, Array<A>>(ref taObj); 
        ref var state = ref Unsafe.As<Space128, ArrayStateMutable>(ref next.space);
        ref var index = ref state.Index;
        ref var count = ref state.Count;

        if (index >= count)
        {
            next = default!;
            return;
        }

        next.head = array.Items[index];
        index++;
    }
}