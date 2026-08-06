using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IteratorTest.Traits;

namespace IteratorTest;

public record Array<A>(A[] Items)
    : IterableMutable<Array<A>, ArrayState, ArrayStateRef, A>
{
    static ArrayState IterableImmutable<Array<A>, ArrayState, A>.SetupImmutable(in Array<A> ta) =>
        new (0, ta.Items.Length);

    static bool IterableImmutable<Array<A>, ArrayState, A>.StepImmutable(
        in Array<A> ta, 
        in ArrayState ts, 
        out A head, 
        out ArrayState tail)
    {
        var index = ts.Index;
        var count = ts.Count;

        if (index >= count)
        {
            head = default!;
            tail = default!;
            return false;
        }

        head = ta.Items[index];
        tail = new ArrayState(index + 1, count);

        return true;    
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void NextImmutableUntyped(
        in Array<A> ta, 
        ref IteratorMutable<A> next)
    {
        ref var state = ref Unsafe.As<Space128, ArrayStateMutable>(ref next.space);
        ref var index = ref state.Index;
        ref var count = ref state.Count;

        if (index >= count)
        {
            next = default!;
            return;
        }

        next.head = ta.Items[index];
        index++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static void IterableImmutable<Array<A>, ArrayState, A>.NextImmutable(
        in Array<A> ta, 
        ref IteratorMutable<Array<A>, ArrayState, A> next)
    {
        ref var state = ref Unsafe.As<ArrayState, ArrayStateMutable>(ref next.space);
        ref var index = ref state.Index;
        ref var count = ref state.Count;

        if (index >= count)
        {
            next = default!;
            return;
        }

        next.head = ta.Items[index];
        index++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ArrayStateRef IterableMutable<Array<A>, ArrayState, ArrayStateRef, A>.SetupMutable(Array<A> ta)
    {
        var     array    = ta.Items;
        ref var items    = ref MemoryMarshal.GetArrayDataReference(array);
        ref var itemsEnd = ref Unsafe.Add(ref items, array.Length);
        var     stateA   = new ArrayStateRef<A>(ref items, ref itemsEnd);
        return Unsafe.As<ArrayStateRef<A>, ArrayStateRef>(ref stateA);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static bool IterableMutable<Array<A>, ArrayState, ArrayStateRef, A>.StepMutable(Array<A> ta, ref ArrayStateRef ts, out A value)
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
}
