using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public partial class Array : Tr.IterableMutable<Array,ArrayState, ArrayStateRef>
{
    static ArrayState Tr.IterableImmutable<Array, ArrayState>.SetupImmutable<A>(in K<Array, A> ta) =>
        new (0, ((Array<A>)ta).Items.Length);

    static bool Tr.IterableImmutable<Array, ArrayState>.StepImmutable<A>(
        in K<Array, A> ta, 
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

        head = ((Array<A>)ta).Items[index];
        tail = new ArrayState(index + 1, count);

        return true;    
    }

    static ArrayStateRef Tr.IterableMutable<Array, ArrayState, ArrayStateRef>.SetupMutable<A>(K<Array, A> ta)
    {
        var     array    = ((Array<A>)ta).Items;
        ref var items    = ref MemoryMarshal.GetArrayDataReference(array);
        ref var itemsEnd = ref Unsafe.Add(ref items, array.Length);
        var     stateA   = new ArrayStateRef<A>(ref items, ref itemsEnd);
        return Unsafe.As<ArrayStateRef<A>, ArrayStateRef>(ref stateA);
    }

    static bool Tr.IterableMutable<Array, ArrayState, ArrayStateRef>.StepMutable<A>(
        K<Array, A> ta, 
        ref ArrayStateRef ts, 
        out A value)
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