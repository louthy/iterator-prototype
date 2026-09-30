using System.Runtime.CompilerServices;

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

        [MethodImpl(Optimisations.InliningOnly)]
        public void NextOp() =>
            tops.Ref.NextOp();

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
    }
}