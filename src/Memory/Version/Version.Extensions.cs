using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

public static class VersionExtensions
{
    extension(ref Version version)
    {
        public ref int Raw
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.As<Version, int>(ref version);
        }
    }
}