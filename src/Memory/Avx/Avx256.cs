using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace IteratorPrototype.Memory;

public static class Avx256
{
    [MethodImpl(Optimisations.Max)]
    public static void CopyAligned<A>(in A src, ref A dest, int count)
    {
        if(Unsafe.SizeOf<nint>() == 8)
        {
            CopyAligned64(in src, ref dest, count);
        }
        else
        {
            CopyAligned32(in src, ref dest, count);
        }
    }
    
    [MethodImpl(Optimisations.Max)]
    static void CopyAligned32<A>(in A src, ref A dest, int count)
    {
        ref var srcBytes  = ref Unsafe.As<A, byte>(ref Unsafe.AsRef(in src));
        ref var destBytes = ref Unsafe.As<A, byte>(ref dest);

        var len = (uint)(Unsafe.SizeOf<A>() * count);
        
        // Make sure that the sizes are multiples of 4 bytes
        Debug.Assert((len & 0x11) == 0);
        
        // Mask off the bottom 5 bits (0-31) and then kill the bottom
        // 2 bits which makes everything 32 bit aligned
        var extra = len & 0b1_1100;              

        // Find the end that we can get to with just Vector256 copies  
        var alignedEnd = len - extra;
        
        // Copy the bulk
        for (var i = 0u; i < alignedEnd; i += 32)
        {
            Vector256.LoadUnsafe(in srcBytes).StoreUnsafe(ref destBytes);
            srcBytes = ref Unsafe.Add(ref srcBytes, 32);
            destBytes = ref Unsafe.Add(ref destBytes, 32);
        }

        // Copy the remainder

        if ((extra & 16) != 0)
        {
            Vector128.LoadUnsafe(in srcBytes).StoreUnsafe(ref destBytes);
            srcBytes = ref Unsafe.Add(ref srcBytes, 16);
            destBytes = ref Unsafe.Add(ref destBytes, 16);
            extra -= 16;
        }

        if ((extra & 8) != 0)
        {
            Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
            srcBytes = ref Unsafe.Add(ref srcBytes, 8);
            destBytes = ref Unsafe.Add(ref destBytes, 8);
            extra -= 8;
        }

        if ((extra & 4) != 0)
        {
            Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
        }        
    }       
    
    [MethodImpl(Optimisations.Max)]
    static void CopyAligned64<A>(in A src, ref A dest, int count)
    {
        ref var srcBytes   = ref Unsafe.As<A, byte>(ref Unsafe.AsRef(in src));
        ref var destBytes  = ref Unsafe.As<A, byte>(ref dest);

        var len = (uint)(Unsafe.SizeOf<A>() * count);
        
        // Make sure that the sizes are multiples of 8 bytes
        Debug.Assert((len & 0x111) == 0);
        
        // Mask off the bottom 5 bits (0-31) and then kill the bottom
        // 3 bits which makes everything 64 bit aligned
        var extra = len & 0b1_1000;              

        // Find the end that we can get to with just Vector256 copies  
        var alignedEnd = len - extra;
        
        // Copy the bulk
        for (var i = 0u; i < alignedEnd; i += 32)
        {
            Vector256.LoadUnsafe(in srcBytes).StoreUnsafe(ref destBytes);
            srcBytes = ref Unsafe.Add(ref srcBytes, 32);
            destBytes = ref Unsafe.Add(ref destBytes, 32);
        }

        // Copy the remainder

        if ((extra & 16) != 0)
        {
            Vector128.LoadUnsafe(in srcBytes).StoreUnsafe(ref destBytes);
            srcBytes = ref Unsafe.Add(ref srcBytes, 16);
            destBytes = ref Unsafe.Add(ref destBytes, 16);
            extra -= 16;
        }

        if ((extra & 8) != 0)
        {
            Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
        }
    }    
    
    [MethodImpl(Optimisations.Max)]
    public static void Copy(in byte src, ref byte dest, uint count)
    {
        ref var srcBytes   = ref Unsafe.AsRef(in src);
        ref var destBytes  = ref dest;
        var     len        = count;
        var     extra      = len & 31;
        var     alignedEnd = len - extra;
        
        for (var i = 0u; i < alignedEnd; i += 32)
        {
            Vector256.LoadUnsafe(in srcBytes).StoreUnsafe(ref destBytes);
            srcBytes = ref Unsafe.Add(ref srcBytes, 32);
            destBytes = ref Unsafe.Add(ref destBytes, 32);
        }
        
        if ((extra & 16) != 0)
        {
            Vector128.LoadUnsafe(in srcBytes).StoreUnsafe(ref destBytes);
            srcBytes = ref Unsafe.Add(ref srcBytes, 16);
            destBytes = ref Unsafe.Add(ref destBytes, 16);
            extra -= 16;
        }

        if (extra == 0) return;
        
        switch (extra)
        {
            case 1:
                destBytes = srcBytes;
                return;
            
            case 2:
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                return;
            
            case 3:
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 2);
                destBytes = ref Unsafe.Add(ref destBytes, 2);
                destBytes = srcBytes;
                return;

            case 4:
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                return;
            
            case 5:
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 4);
                destBytes = ref Unsafe.Add(ref destBytes, 4);
                destBytes = srcBytes;
                return;
            
            case 6:
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 4);
                destBytes = ref Unsafe.Add(ref destBytes, 4);
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                return;
            
            case 7:
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 4);
                destBytes = ref Unsafe.Add(ref destBytes, 4);
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 1);
                destBytes = ref Unsafe.Add(ref destBytes, 1);
                destBytes = srcBytes;
                return;

            case 8:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                return;
            
            case 9:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 8);
                destBytes = ref Unsafe.Add(ref destBytes, 8);
                destBytes = srcBytes;
                return;
            
            case 10:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 8);
                destBytes = ref Unsafe.Add(ref destBytes, 8);
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                return;
            
            case 11:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 8);
                destBytes = ref Unsafe.Add(ref destBytes, 8);
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 2);
                destBytes = ref Unsafe.Add(ref destBytes, 2);
                destBytes = srcBytes;
                return;

            case 12:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 8);
                destBytes = ref Unsafe.Add(ref destBytes, 8);
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                return;
            
            case 13:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 8);
                destBytes = ref Unsafe.Add(ref destBytes, 8);
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 4);
                destBytes = ref Unsafe.Add(ref destBytes, 4);
                destBytes = srcBytes;
                return;
            
            case 14:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 8);
                destBytes = ref Unsafe.Add(ref destBytes, 8);
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 4);
                destBytes = ref Unsafe.Add(ref destBytes, 4);
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                return;
            
            case 15:
                Unsafe.As<byte, ulong>(ref destBytes) = Unsafe.As<byte, ulong>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 8);
                destBytes = ref Unsafe.Add(ref destBytes, 8);
                Unsafe.As<byte, uint>(ref destBytes) = Unsafe.As<byte, uint>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 4);
                destBytes = ref Unsafe.Add(ref destBytes, 4);
                Unsafe.As<byte, ushort>(ref destBytes) = Unsafe.As<byte, ushort>(ref srcBytes);
                srcBytes = ref Unsafe.Add(ref srcBytes, 1);
                destBytes = ref Unsafe.Add(ref destBytes, 1);
                destBytes = srcBytes;
                return;
            
            default:
                throw new InvalidOperationException();
        }
    }
}