#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory.Internal;

[SkipLocalsInit]
sealed class Page128<A> : 
    Page<Page128<A>, A>, 
    Tr.Constructor<Page128<A>>
{
    const int Size = 128;
    
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
    
    public Entry Item0040; 
    public Entry Item0041; 
    public Entry Item0042; 
    public Entry Item0043; 
    public Entry Item0044; 
    public Entry Item0045; 
    public Entry Item0046; 
    public Entry Item0047; 
    public Entry Item0048; 
    public Entry Item0049; 
    public Entry Item004A; 
    public Entry Item004B; 
    public Entry Item004C; 
    public Entry Item004D; 
    public Entry Item004E; 
    public Entry Item004F; 
                           
    public Entry Item0050; 
    public Entry Item0051; 
    public Entry Item0052; 
    public Entry Item0053; 
    public Entry Item0054; 
    public Entry Item0055; 
    public Entry Item0056; 
    public Entry Item0057; 
    public Entry Item0058; 
    public Entry Item0059; 
    public Entry Item005A; 
    public Entry Item005B; 
    public Entry Item005C; 
    public Entry Item005D; 
    public Entry Item005E; 
    public Entry Item005F; 
                           
    public Entry Item0060; 
    public Entry Item0061; 
    public Entry Item0062; 
    public Entry Item0063; 
    public Entry Item0064; 
    public Entry Item0065; 
    public Entry Item0066; 
    public Entry Item0067; 
    public Entry Item0068; 
    public Entry Item0069; 
    public Entry Item006A; 
    public Entry Item006B; 
    public Entry Item006C; 
    public Entry Item006D; 
    public Entry Item006E; 
    public Entry Item006F; 
                           
    public Entry Item0070; 
    public Entry Item0071; 
    public Entry Item0072; 
    public Entry Item0073; 
    public Entry Item0074; 
    public Entry Item0075; 
    public Entry Item0076; 
    public Entry Item0077; 
    public Entry Item0078; 
    public Entry Item0079; 
    public Entry Item007A; 
    public Entry Item007B; 
    public Entry Item007C; 
    public Entry Item007D; 
    public Entry Item007E; 
    public Entry Item007F;
    
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
    public static Page128<A> Construct() =>
        new ();
}