#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct Tops
{
    const int Capacity = 16;
    
    readonly uint item0;
    readonly uint item1;
    readonly uint item2;
    readonly uint item3;
    readonly uint item4;
    readonly uint item5;
    readonly uint item6;
    readonly uint item7;
    readonly uint item8;
    readonly uint item9;
    readonly uint itemA;
    readonly uint itemB;
    readonly uint itemC;
    readonly uint itemD;
    readonly uint itemE;
    readonly uint itemF;
    readonly uint current;
    readonly uint begin;
    readonly int count;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Tops()
    {
        count = 1;
        begin = 0;
        current = 0;
    }

    public int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => count;
    }

    public bool Sync(in ObjStack objs, in ByteStack values)
    {
        // Update the objects-stack top cache 
        CurrentObj = (byte)objs.Top;
        
        // Update the values-stack top cache 
        CurrentValue = (byte)values.Top;
        
        // Sync the top entry to the current cache
        Top = Current;
        
        return true;
    }

    public bool Sync(in ByteStack values)
    {
        // Update the values-stack top cache 
        CurrentValue = (byte)values.Top;
        
        // Sync the top entry to the current cache
        Top = Current;
        
        return true;
    }

    public bool Sync(in ObjStack objs)
    {
        // Update the objects-stack top cache 
        CurrentObj = (byte)objs.Top;
        
        // Sync the top entry to the current cache
        Top = Current;
        
        return true;
    }

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => count == 0;
    }
 

    /// <summary>
    /// This is the state when this frame started
    /// </summary>
    ref uint Begin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in begin);
    }

    /// <summary>
    /// This is the current state of the frame
    /// </summary>
    ref uint Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in current);
    }

    /*
    public bool HasLast
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => top is not (0 or 1);
    }

    /// <summary>
    /// This is the state of the last frame
    /// </summary>
    ref uint Last
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get 
        {
            if (HasLast)
            {
                return ref Unsafe.Add(ref Unsafe.AsRef(in item0), top - 2);
            }
            else
            {
                throw new StackUnderflowException();
            }
        }
    }

    public ref byte LastBytes
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.As<uint, byte>(ref Last);
    }

    public ref byte LastPC
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AddByteOffset(ref LastBytes, 0);
    }

    public ref byte LastObj
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AddByteOffset(ref LastBytes, 1);
    }

    public ref byte LastValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AddByteOffset(ref LastBytes, 2);
    }

    public ref byte LastYield
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AddByteOffset(ref LastBytes, 3);
    }
    */

    public ref byte CurrentBytes
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.As<uint, byte>(ref Current);
    }

    public ref byte CurrentPC
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref CurrentBytes;
    }

    public ref byte CurrentObj
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AddByteOffset(ref CurrentBytes, 1);
    }

    public ref byte CurrentValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AddByteOffset(ref CurrentBytes, 2);
    }

    public ref byte CurrentYield
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AddByteOffset(ref CurrentBytes, 3);
    }
 
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool ResetFrame()
    {
        Current = Begin;
        Top = Begin;
        return true;
    }
  
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopFrame()
    {
        if (count <= 0) return false;
        
        // Clear the top entry
        Top = 0;
        
        // Make the stack 1 quieter
        ref var c = ref Unsafe.AsRef(in count);
        c--;

        // Reload the current state cache
        Current = Top;
        
        /*
        // Make sure the yield counter decreases if we're leaving the frame.
        if (CurrentYield > 0)
        {
            CurrentYield--;
            Top = Current;
        }
        */
        
        // Make sure we remember the start of this frame
        Begin = Current;
        
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PushFrame()
    {
        if (count >= Capacity) return false;

        // Save the current program-counter and then reset the cached version to have the one from the
        // start of the current frame.  That means popping the entry takes us back to the start of the
        // current frame, allowing us to loop through all elements of the iteration.
        var beginPC = Begin & 0xFF;
        var nowPC = Current & 0xFF;
        CurrentPC = (byte)beginPC;
        
        // This takes the cached current state (with the program-counter reset back to the start of this frame) and
        // copies it to the current entry at the top of the stack.
        Top = Current;
        
        // Make the top of the stack 1 louder
        ref var c = ref Unsafe.AsRef(in count);
        c++;

        // The current yield should be reset to zero because no yields have happened yet.
        // This is a `ref` to the cached current state. 
        CurrentYield = 0;

        // Reset thew current PC to the saved value
        CurrentPC = (byte)nowPC;
        
        // Now write the current state to the new entry at the top of the stack
        Top = Current;
        
        // Remember where this frame starts
        Begin = Current;
        
        return true;
    }

    ref uint Top
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in item0), count - 1);
    }    
}
