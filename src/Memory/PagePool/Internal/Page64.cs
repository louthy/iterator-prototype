#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory.Internal;

[SkipLocalsInit]
sealed class Page64<A> : 
    Page<Page64<A>, A>, 
    Tr.Constructor<Page64<A>>
{
    const int Size = 64;
    
    public Entry Item0000;
    public Entry Item0001;
    public Entry Item0002;
    public Entry Item0003;
    public Entry Item0004;
    public Entry Item0005;
    public Entry Item0006;
    public Entry Item0007;
    public Entry Item0008;
    public Entry Item0009;
    public Entry Item000A;
    public Entry Item000B;
    public Entry Item000C;
    public Entry Item000D;
    public Entry Item000E;
    public Entry Item000F;
    
    public Entry Item0010;
    public Entry Item0011;
    public Entry Item0012;
    public Entry Item0013;
    public Entry Item0014;
    public Entry Item0015;
    public Entry Item0016;
    public Entry Item0017;
    public Entry Item0018;
    public Entry Item0019;
    public Entry Item001A;
    public Entry Item001B;
    public Entry Item001C;
    public Entry Item001D;
    public Entry Item001E;
    public Entry Item001F;
    
    public Entry Item0020;
    public Entry Item0021;
    public Entry Item0022;
    public Entry Item0023;
    public Entry Item0024;
    public Entry Item0025;
    public Entry Item0026;
    public Entry Item0027;
    public Entry Item0028;
    public Entry Item0029;
    public Entry Item002A;
    public Entry Item002B;
    public Entry Item002C;
    public Entry Item002D;
    public Entry Item002E;
    public Entry Item002F;
    
    public Entry Item0030;
    public Entry Item0031;
    public Entry Item0032;
    public Entry Item0033;
    public Entry Item0034;
    public Entry Item0035;
    public Entry Item0036;
    public Entry Item0037;
    public Entry Item0038;
    public Entry Item0039;
    public Entry Item003A;
    public Entry Item003B;
    public Entry Item003C;
    public Entry Item003D;
    public Entry Item003E;
    public Entry Item003F;
    
    public override int Capacity 
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => Size;
    }

    protected override ref A Reference
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.As<Entry, A>(ref Item0000);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Page64<A> Construct() =>
        new ();
}