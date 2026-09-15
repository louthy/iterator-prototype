using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace IteratorPrototype.Memory;

public static class Avx
{
    [MethodImpl(Optimisations.Max)]
    public static void CopyAligned<A>(in A src, ref A dest) =>
        CopyAligned(src, ref dest, 1);

    [MethodImpl(Optimisations.Max)]
    public static void CopyAligned<A>(in A src, ref A dest, int count)
    {
        if (Vector512.IsHardwareAccelerated)
        {
            Avx512.CopyAligned(src, ref dest, count);
        }
        else if (Vector256.IsHardwareAccelerated)
        {
            Avx256.CopyAligned(src, ref dest, count);
        }
        else if (Vector128.IsHardwareAccelerated)
        {
            Avx128.CopyAligned(src, ref dest, count);
        }
        else
        {
            Unsafe.CopyBlockUnaligned(
                ref Unsafe.As<A, byte>(ref dest),
                in Unsafe.As<A, byte>(ref Unsafe.AsRef(in src)),
                (uint)count);
        }
    }

    [MethodImpl(Optimisations.Max)]
    public static void Copy(in byte src, ref byte dest, uint count)
    {
        if (Vector512.IsHardwareAccelerated)
        {
            Avx512.Copy(src, ref dest, count);
        }
        else if (Vector256.IsHardwareAccelerated)
        {
            Avx256.Copy(src, ref dest, count);
        }
        else if (Vector128.IsHardwareAccelerated)
        {
            Avx128.Copy(src, ref dest, count);
        }
        else
        {
            Unsafe.CopyBlockUnaligned(ref dest, in src, count);
        }
    }
    
}