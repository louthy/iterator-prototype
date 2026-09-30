#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
struct TopsMutable
{
    public const int Capacity = 16;
    
    uint item0;
    uint item1;
    uint item2;
    uint item3;
    uint item4;
    uint item5;
    uint item6;
    uint item7;
    uint item8;
    uint item9;
    uint itemA;
    uint itemB;
    uint itemC;
    uint itemD;
    uint itemE;
    uint itemF;
    uint current;
    uint begin;
    int count;

    [MethodImpl(Optimisations.InliningOnly)]
    public TopsMutable()
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

    Span<uint> Items
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref item0, count);
    }

    Span<uint> AllItems
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref item0, Capacity);
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void NextOp() =>
        current++;

    /// <summary>
    /// This is the current state of the frame
    /// </summary>
    public uint Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current;
        set => current = value;
    }

    public int PC
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (int)((current & Tops.ProgramCounterMask) >> Tops.ProgramCounterShift);
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
        get => (int)((current & Tops.YieldCounterMask) >> Tops.YieldCounterShift);
    } 

    public int ValuesCount
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => (int)((current & Tops.ValuesMask) >> Tops.ValuesShift);
    } 

    public int ObjsCount
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => (int)((current & Tops.ObjsMask) >> Tops.ObjsShift);
    } 

    public int VarsCount
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => (int)((current & Tops.VarsMask) >> Tops.VarsShift);
    } 

    [MethodImpl(Optimisations.InliningOnly)]
    public int IncrementPC()
    {
        unchecked
        {
            current++;
            return PC;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void SetPC(int pc)
    {
        current = (current & Tops.NotProgramCounterMask) | ((uint)pc & Tops.ProgramCounterMask);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void IncrementYields()
    {
        unchecked
        {
            current += 1 << Tops.YieldCounterShift;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void DecrementYields()
    {
        unchecked
        {
            current -= 1 << Tops.YieldCounterShift;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void ClearYields()
    {
        current &= Tops.NotYieldCounterMask;
    }
 
    [MethodImpl(Optimisations.InliningOnly)]
    public void ResetFrame()
    {
        current = begin;
        Items[^1] = begin;
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
                Items[^1] = 0;
        
                // Make the stack 1 quieter
                count = 0;

                // Reload the current state cache
                current = 0;
        
                // Make sure we remember the start of this frame
                begin = 0;
        
                return true;
            }

            default:
            {
                // Clear the top entry
                Items[^1] = 0;

                // Make the stack 1 quieter
                count--;

                // Load the previous frame's state
                var top = Items[^1];

                // Reload the current state cache
                current = top;

                // Make sure we remember the start of this frame
                begin = top;

                return true;
            }
        }
    }
    
    [MethodImpl(Optimisations.Agro)]
    public void PushFrame(uint yieldAdd)
    {
        if (count == 0)
        {
            begin = yieldAdd << Tops.YieldCounterShift;
            item0 = begin;
            current = begin;
            count++;
        }
        else
        {
            // The new top state will be the current state with the yields reset
            var newState = current & Tops.NotYieldCounterMask;

            // The state we're about to save (before pushing a new one) will have its program-counter reset back to the
            // start of this frame, so when it's popped, we'll be back at the start (loops).
            var newCurrent = ((current & Tops.NotProgramCounterMask) | (begin & Tops.ProgramCounterMask)) +
                             (yieldAdd << Tops.YieldCounterShift);

            // This takes the current state (with the program-counter reset back to the start of this frame) and
            // copies it to the current top entry at the top of the stack (before we push).
            Items[^1] = newCurrent;

            // Make the top of the stack 1 louder
            count++;

            // Set the new state
            current = newState;

            // Now write the current state to the new entry at the top of the stack
            Items[^1] = newState;

            // Remember where this frame starts
            begin = newState;
        }
    }
}
