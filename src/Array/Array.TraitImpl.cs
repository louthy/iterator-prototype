using System.Runtime.CompilerServices;
using IteratorTest.Traits;
using LanguageExt.Traits;

namespace IteratorTest;

public partial class Array : IterableK<Array, ArrayState>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static ArrayState Setup<A>(K<Array, A> ta) =>
        ta is Array<A> arr
            ? new ArrayState(arr.Items, 0, arr.Items.Length)
            : throw new InvalidCastException();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static bool IterableK<Array, ArrayState>.StepMutable<A>(K<Array, A> ta, ref ArrayState ts, out A value)
    {
        var index = ts.Index;
        var count = ts.Count;
        
        if(index >= count)
        {
            value = default!;
            return false;
        }
        
        var items = ts.Items;
        ref var array = ref Unsafe.As<object, A[]>(ref items);
        ts = new ArrayState(items, index + 1, count);
        value = array[index];
        
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static bool IterableK<Array, ArrayState>.StepImmutable<A>(K<Array, A> ta, in ArrayState ts, out Iterator<Array, ArrayState, A> next)
    {
        var index = ts.Index;
        var count = ts.Count;
        
        if(index >= count)
        {
            next = default!;
            return false;
        }
        
        var     items = ts.Items;
        ref var array = ref Unsafe.As<object, A[]>(ref items);

        var ts1 = new ArrayState(items, index + 1, count);
        next = new Iterator<Array, ArrayState, A>(in array[index], ta, in ts1);
        
        return true;
    }
}
