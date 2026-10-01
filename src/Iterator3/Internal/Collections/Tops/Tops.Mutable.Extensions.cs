
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class TopsMutableExtensions
{
    extension(ref TopsMutable tops)
    {
        public bool IsEmpty
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => tops.Count == 0;
        }

        Span<uint> Items
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref tops.item0, tops.Count);
        }

        Span<uint> AllItems
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref tops.item0, Tops.Capacity);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void NextOp() =>
            tops.Current++;

        public int PC
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => (int)((tops.Current & Tops.ProgramCounterMask) >> Tops.ProgramCounterShift);
        }

        public bool IsSingleton
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => tops.YieldsInFrame == 0;
        }

        public bool HasYielded
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => tops.YieldsInFrame > 0;
        }

        public int YieldsInFrame
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => (int)((tops.Current & Tops.YieldCounterMask) >> Tops.YieldCounterShift);
        }

        public int ValuesCount
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => (int)((tops.Current & Tops.ValuesMask) >> Tops.ValuesShift);
        }

        public int ObjsCount
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => (int)((tops.Current & Tops.ObjsMask) >> Tops.ObjsShift);
        }

        public int VarsCount
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => (int)((tops.Current & Tops.VarsMask) >> Tops.VarsShift);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public int IncrementPC()
        {
            unchecked
            {
                tops.Current++;
                return tops.PC;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void SetPC(int pc)
        {
            tops.Current = (tops.Current & Tops.NotProgramCounterMask) | ((uint)pc & Tops.ProgramCounterMask);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void IncrementYields()
        {
            unchecked
            {
                tops.Current += 1 << Tops.YieldCounterShift;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void DecrementYields()
        {
            unchecked
            {
                tops.Current -= 1 << Tops.YieldCounterShift;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void ClearYields()
        {
            tops.Current &= Tops.NotYieldCounterMask;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void ResetFrame()
        {
            tops.Current = tops.Begin;
            tops.Items[^1] = tops.Begin;
        }

        [MethodImpl(Optimisations.Agro)]
        public bool PopFrame()
        {
            switch (tops.Count)
            {
                case 0:
                    return false;

                case 1:
                {
                    // Clear the top entry
                    tops.Items[^1] = 0;

                    // Make the stack 1 quieter
                    tops.Count = 0;

                    // Reload the current state cache
                    tops.Current = 0;

                    // Make sure we remember the start of this frame
                    tops.Begin = 0;

                    return true;
                }

                default:
                {
                    // Clear the top entry
                    tops.Items[^1] = 0;

                    // Make the stack 1 quieter
                    tops.Count--;

                    // Load the previous frame's state
                    var top = tops.Items[^1];

                    // Reload the current state cache
                    tops.Current = top;

                    // Make sure we remember the start of this frame
                    tops.Begin = top;

                    return true;
                }
            }
        }

        [MethodImpl(Optimisations.Agro)]
        public void PushFrame(uint yieldAdd)
        {
            if (tops.Count == 0)
            {
                tops.Begin = yieldAdd << Tops.YieldCounterShift;
                tops.item0 = tops.Begin;
                tops.Current = tops.Begin;
                tops.Count++;
            }
            else
            {
                // The new top state will be the current state with the yields reset
                var newState = tops.Current & Tops.NotYieldCounterMask;

                // The state we're about to save (before pushing a new one) will have its program-counter reset back to the
                // start of this frame, so when it's popped, we'll be back at the start (loops).
                var newCurrent = ((tops.Current & Tops.NotProgramCounterMask) | (tops.Begin & Tops.ProgramCounterMask)) +
                                 (yieldAdd << Tops.YieldCounterShift);

                // This takes the current state (with the program-counter reset back to the start of this frame) and
                // copies it to the current top entry at the top of the stack (before we push).
                tops.Items[^1] = newCurrent;

                // Make the top of the stack 1 louder
                tops.Count++;

                // Set the new state
                tops.Current = newState;

                // Now write the current state to the new entry at the top of the stack
                tops.Items[^1] = newState;

                // Remember where this frame starts
                tops.Begin = newState;
            }
        }
    }
}