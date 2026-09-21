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
    
    // Program counter: Bits 0 to 7  (8 bits)
    // Values top:      Bits 8 to 15 (8 bits)
    // Objects top:     Bits 16 - 21 (6 bits)
    // Vars top:        Bits 22, 23, 24, 25, 26, 27 (6 bits)
    // Yield counter:   Bits 28 - 31 (4 bits)

    public const uint ProgramCounterMask = 0b00000000_00000000_00000000_11111111;
    public const uint ValuesMask         = 0b00000000_00000000_11111111_00000000;
    public const uint ObjsMask           = 0b00000000_00111111_00000000_00000000;
    public const uint VarsMask           = 0b00001111_11000000_00000000_00000000;
    public const uint YieldCounterMask   = 0b11110000_00000000_00000000_00000000;

    public const uint NotProgramCounterMask = ~ProgramCounterMask;
    public const uint NotValuesMask         = ~ValuesMask;
    public const uint NotObjsMask           = ~ObjsMask;
    public const uint NotVarsMask           = ~VarsMask;
    public const uint NotYieldCounterMask   = ~YieldCounterMask;

    public const int ProgramCounterShift = 0;
    public const int ValuesShift         = 8;
    public const int ObjsShift           = 16;
    public const int VarsShift           = 22;
    public const int YieldCounterShift   = 28;
    
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

    [MethodImpl(Optimisations.InliningOnly)]
    public Tops()
    {
        count = 1;
        begin = 0;
        current = 0;
    }

    public int Count
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count;
    }

    public bool IsEmpty
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count == 0;
    }

    /// <summary>
    /// This is the state when this frame started
    /// </summary>
    ref uint BeginRef
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.AsRef(in begin);
    }

    /// <summary>
    /// This is the current state of the frame
    /// </summary>
    public ref uint CurrentRef
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.AsRef(in current);
    }

    /// <summary>
    /// This is the current state of the frame
    /// </summary>
    public uint Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current;
    }

    public int PC
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (int)((current & ProgramCounterMask) >> ProgramCounterShift);
    }

    public bool IsSingleton
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => YieldsInFrame == 0;
    }

    public bool HasYielded
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => YieldsInFrame > 0;
    } 

    public int YieldsInFrame
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => (int)((current & YieldCounterMask) >> YieldCounterShift);
    } 

    public int ValuesCount
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => (int)((current & ValuesMask) >> ValuesShift);
    } 

    public int ObjsCount
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => (int)((current & ObjsMask) >> ObjsShift);
    } 

    public int VarsCount
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => (int)((current & VarsMask) >> VarsShift);
    } 

    [MethodImpl(Optimisations.InliningOnly)]
    public int IncrementPC()
    {
        unchecked
        {
            var c = current + (1 << ProgramCounterShift);
            CurrentRef = c;
            return (int)(c & ProgramCounterMask);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void IncrementYields()
    {
        unchecked
        {
            var     c = current + (1 << YieldCounterShift);
            CurrentRef = c;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void DecrementYields()
    {
        unchecked
        {
            var     c = current - (1 << YieldCounterShift);
            CurrentRef = c;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void ClearYields()
    {
        var     c = current & NotYieldCounterMask;
        CurrentRef = c;
    }
 
    [MethodImpl(Optimisations.InliningOnly)]
    public bool ResetFrame()
    {
        CurrentRef = begin;
        TopRef = begin;
        return true;
    }
  
    [MethodImpl(Optimisations.Agro)]
    public bool PopFrame()
    {
        switch (count)
        {
            case 0:
                return false;

            case 1:
            {
                // Clear the top entry
                TopRef = 0;
        
                // Make the stack 1 quieter
                ref var c = ref Unsafe.AsRef(in count);
                c = 0;

                // Reload the current state cache
                CurrentRef = 0;
        
                // Make sure we remember the start of this frame
                BeginRef = 0;
        
                return true;
            }

            default:
            {
                // Clear the top entry
                TopRef = 0;

                // Make the stack 1 quieter
                ref var c = ref Unsafe.AsRef(in count);
                c--;

                // Load the previous frame's state
                var top = Top;

                // Reload the current state cache
                CurrentRef = top;

                // Make sure we remember the start of this frame
                BeginRef = top;

                return true;
            }
        }
    }
    
    [MethodImpl(Optimisations.Agro)]
    public bool PushFrame(uint yieldAdd)
    {
        if (count >= Capacity) return false;

        // The new top state will be the current state with the yields reset
        var newState = current & NotYieldCounterMask;
        
        // The state we're about to save (before pushing a new one) will have its program-counter reset back to the
        // start of this frame, so when it's popped, we'll be back at the start (loops).
        var newCurrent = ((current & NotProgramCounterMask) | (begin & ProgramCounterMask)) +
                         (yieldAdd << YieldCounterShift);
        
        // This takes the current state (with the program-counter reset back to the start of this frame) and
        // copies it to the current top entry at the top of the stack (before we push).
        TopRef = newCurrent;
        
        // Make the top of the stack 1 louder
        ref var c = ref Unsafe.AsRef(in count);
        c++;
        
        // Set the new state
        CurrentRef = newState;
        
        // Now write the current state to the new entry at the top of the stack
        TopRef = newState;
        
        // Remember where this frame starts
        BeginRef = newState;
        
        return true;
    }

    uint Top
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => Unsafe.Add(ref Unsafe.AsRef(in item0), count - 1);
    }    

    ref uint TopRef
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in item0), count - 1);
    }    
}
