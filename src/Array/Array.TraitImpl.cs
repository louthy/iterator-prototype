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
    static bool IterableK<Array, ArrayState>.Step<A>(K<Array, A> ta, ref ArrayState ts, out A value)
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
}
