#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

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

    [MethodImpl(Optimisations.Default)]
    public Tops()
    {
        count = 1;
        begin = 0;
        current = 0;
    }

    public int Count
    {
        [MethodImpl(Optimisations.Default)]
        get => count;
    }

    [MethodImpl(Optimisations.Default)]
    public bool Sync(in Vars.State snapshot)
    {
        // Update the objects-stack top cache 
        CurrentObj = snapshot.ObjectsTop;
        
        // Update the values-stack top cache 
        CurrentValue = snapshot.ValuesTop;
        
        // Sync the top entry to the current cache
        Top = Current;
        
        return true;
    }

    public bool IsEmpty
    {
        [MethodImpl(Optimisations.Default)]
        get => count == 0;
    }
 

    /// <summary>
    /// This is the state when this frame started
    /// </summary>
    ref uint Begin
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AsRef(in begin);
    }

    /// <summary>
    /// This is the current state of the frame
    /// </summary>
    ref uint Current
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AsRef(in current);
    }

    public ref byte CurrentBytes
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.As<uint, byte>(ref Current);
    }

    public ref byte CurrentPC
    {
        [MethodImpl(Optimisations.Default)]
        get => ref CurrentBytes;
    }

    public ref byte CurrentObj
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AddByteOffset(ref CurrentBytes, 1);
    }

    public ref byte CurrentValue
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AddByteOffset(ref CurrentBytes, 2);
    }

    public ref byte CurrentYield
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AddByteOffset(ref CurrentBytes, 3);
    }
 
    [MethodImpl(Optimisations.Default)]
    public bool ResetFrame()
    {
        Current = Begin;
        Top = Begin;
        return true;
    }
  
    [MethodImpl(Optimisations.Default)]
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
        
        // Make sure we remember the start of this frame
        Begin = Current;
        
        return true;
    }
    
    [MethodImpl(Optimisations.Default)]
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
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in item0), count - 1);
    }    
}
