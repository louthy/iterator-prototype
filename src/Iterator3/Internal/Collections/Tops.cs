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
    const int Capacity = 15;
    
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
    readonly uint current;
    readonly int top;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Tops() =>
        top = 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Tops(params ReadOnlySpan<uint> items)
    {
        if (items.Length == 0)
        {
            top = 1;
            return;
        }
        if (items.Length > Capacity)
        {
            throw new ArgumentException("Stack overflow");
        }
        var span = MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in item0), Capacity);
        items.CopyTo(span);
        top = items.Length;
        current = items[^1];
    }

    public void Init()
    {
        // We have one entry to start with!
        ref var t = ref Unsafe.AsRef(in top);
        t = 1;

        // Zero the current cache
        Current = 0;

        // Zero the top entry
        Top = Current;
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
        get => top == 0;
    }
  
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop()
    {
        ref var t = ref Unsafe.AsRef(in top);
        t--;
        Current = Top;
        return t > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Peek(out uint value)
    {
        value = current;
        return true;
    }

    public ref uint Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in current);
    }

    public bool HasLast
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => top is not (0 or 1);
    }

    public ref uint Last
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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push()
    {
        
        // We only check out of bounds on growth
        if (top >= Capacity) return false;

        // Reference to the top index
        ref var t = ref Unsafe.AsRef(in top);
 
        // Get the current state and make sure the Entry is synced before we change the top.
        if (t > 0)
        {
            // We want to go back to the start of the co-routine when this current
            // frame is popped.  So, get the last program-counter
            var lastPC = HasLast ? LastPC : (byte)0;
            
            // Cache the latest state
            var now = current;
            
            // Synchronise the current entry to have the latest state and the previous
            // program-counter
            CurrentPC = lastPC;
            Top = current;
            
            // Make top 1 louder
            t++;

            Top = now;
            Current = now;
        }
        else
        {
            // Make top 1 louder
            t++;
            Top = 0;
            Current = 0;
        }
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push(uint value)
    {
        // We only check out of bounds on growth
        if (top >= Capacity) return false;

        // Reference to the top index
        ref var t = ref Unsafe.AsRef(in top);
 
        // Get the current state and make sure the Entry is synced before we change the top.
        if (t > 0)
        {
            // Cache the latest state
            var now = current;
            
            // Synchronise the current entry to have the latest state
            Top = now;
            
            // Make top 1 louder
            t++;

            Top = value;
            Current = value;
        }
        else
        {
            // Make top 1 louder
            t++;
            Top = value;
            Current = value;
        }
        return true;
    }

    ref uint Top
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in item0), top - 1);
    }    
}
