using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class TopsExtensions
{
    extension(in Tops tops)
    {
        public ref TopsMutable Ref
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.As<Tops, TopsMutable>(ref Unsafe.AsRef(in tops));
        }
        

        /*
        public bool IsEmpty
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => tops.Count == 0;
        }

        ReadOnlySpan<uint> Items
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in tops.item0), tops.Count);
        }

        ReadOnlySpan<uint> AllItems
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in tops.item0), Tops.Capacity);
        }

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
        public void NextOp() =>
            tops.Ref.Current++;

        [MethodImpl(Optimisations.InliningOnly)]
        public void SetCurrent(uint current) =>
            tops.Ref.Current = current;

        [MethodImpl(Optimisations.InliningOnly)]
        public int IncrementPC() =>
            tops.Ref.IncrementPC();

        [MethodImpl(Optimisations.InliningOnly)]
        public void SetPC(int pc) =>
            tops.Ref.SetPC(pc);

        [MethodImpl(Optimisations.InliningOnly)]
        public void IncrementYields() =>
            tops.Ref.IncrementYields();

        [MethodImpl(Optimisations.InliningOnly)]
        public void DecrementYields() =>
            tops.Ref.DecrementYields();

        [MethodImpl(Optimisations.InliningOnly)]
        public void ClearYields() =>
            tops.Ref.ClearYields();

        [MethodImpl(Optimisations.InliningOnly)]
        public void ResetFrame() =>
            tops.Ref.ResetFrame();

        [MethodImpl(Optimisations.Agro)]
        public bool PopFrame() =>
            tops.Ref.PopFrame();

        [MethodImpl(Optimisations.Agro)]
        public void PushFrame(uint yieldAdd) =>
            tops.Ref.PushFrame(yieldAdd);
*/            
    }
}