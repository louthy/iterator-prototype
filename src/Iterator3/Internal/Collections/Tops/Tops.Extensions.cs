using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class TopsExtensions
{
    extension(ref Tops tops)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        public void NextOp() =>
            Unsafe.As<Tops, TopsMutable>(ref tops).NextOp();

        [MethodImpl(Optimisations.InliningOnly)]
        public void SetCurrent(uint current) =>
            Unsafe.As<Tops, TopsMutable>(ref tops).Current = current;

        [MethodImpl(Optimisations.InliningOnly)]
        public int IncrementPC() =>
            Unsafe.As<Tops, TopsMutable>(ref tops).IncrementPC();

        [MethodImpl(Optimisations.InliningOnly)]
        public void SetPC(int pc) =>
            Unsafe.As<Tops, TopsMutable>(ref tops).SetPC(pc);

        [MethodImpl(Optimisations.InliningOnly)]
        public void IncrementYields() =>
            Unsafe.As<Tops, TopsMutable>(ref tops).IncrementYields();

        [MethodImpl(Optimisations.InliningOnly)]
        public void DecrementYields() =>
            Unsafe.As<Tops, TopsMutable>(ref tops).DecrementYields();

        [MethodImpl(Optimisations.InliningOnly)]
        public void ClearYields() =>
            Unsafe.As<Tops, TopsMutable>(ref tops).ClearYields();

        [MethodImpl(Optimisations.InliningOnly)]
        public void ResetFrame() =>
            Unsafe.As<Tops, TopsMutable>(ref tops).ResetFrame();

        [MethodImpl(Optimisations.Agro)]
        public bool PopFrame() =>
            Unsafe.As<Tops, TopsMutable>(ref tops).PopFrame();

        [MethodImpl(Optimisations.Agro)]
        public void PushFrame(uint yieldAdd) =>
            Unsafe.As<Tops, TopsMutable>(ref tops).PushFrame(yieldAdd);
    }
}