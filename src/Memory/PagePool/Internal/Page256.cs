#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory.Internal;

[SkipLocalsInit]
sealed class Page256<A> : 
    Page<Page256<A>, A>, 
    Tr.Constructor<Page256<A>>
{
    const int Size = 256;
    
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

    public Entry Item0080;
    public Entry Item0081;
    public Entry Item0082;
    public Entry Item0083;
    public Entry Item0084;
    public Entry Item0085;
    public Entry Item0086;
    public Entry Item0087;
    public Entry Item0088;
    public Entry Item0089;
    public Entry Item008A;
    public Entry Item008B;
    public Entry Item008C;
    public Entry Item008D;
    public Entry Item008E;
    public Entry Item008F;
    
    public Entry Item0090;
    public Entry Item0091;
    public Entry Item0092;
    public Entry Item0093;
    public Entry Item0094;
    public Entry Item0095;
    public Entry Item0096;
    public Entry Item0097;
    public Entry Item0098;
    public Entry Item0099;
    public Entry Item009A;
    public Entry Item009B;
    public Entry Item009C;
    public Entry Item009D;
    public Entry Item009E;
    public Entry Item009F;
    
    public Entry Item00A0;
    public Entry Item00A1;
    public Entry Item00A2;
    public Entry Item00A3;
    public Entry Item00A4;
    public Entry Item00A5;
    public Entry Item00A6;
    public Entry Item00A7;
    public Entry Item00A8;
    public Entry Item00A9;
    public Entry Item00AA;
    public Entry Item00AB;
    public Entry Item00AC;
    public Entry Item00AD;
    public Entry Item00AE;
    public Entry Item00AF;
    
    public Entry Item00B0;
    public Entry Item00B1;
    public Entry Item00B2;
    public Entry Item00B3;
    public Entry Item00B4;
    public Entry Item00B5;
    public Entry Item00B6;
    public Entry Item00B7;
    public Entry Item00B8;
    public Entry Item00B9;
    public Entry Item00BA;
    public Entry Item00BB;
    public Entry Item00BC;
    public Entry Item00BD;
    public Entry Item00BE;
    public Entry Item00BF;
    
    public Entry Item00C0; 
    public Entry Item00C1; 
    public Entry Item00C2; 
    public Entry Item00C3; 
    public Entry Item00C4; 
    public Entry Item00C5; 
    public Entry Item00C6; 
    public Entry Item00C7; 
    public Entry Item00C8; 
    public Entry Item00C9; 
    public Entry Item00CA; 
    public Entry Item00CB; 
    public Entry Item00CC; 
    public Entry Item00CD; 
    public Entry Item00CE; 
    public Entry Item00CF; 
                           
    public Entry Item00D0; 
    public Entry Item00D1; 
    public Entry Item00D2; 
    public Entry Item00D3; 
    public Entry Item00D4; 
    public Entry Item00D5; 
    public Entry Item00D6; 
    public Entry Item00D7; 
    public Entry Item00D8; 
    public Entry Item00D9; 
    public Entry Item00DA; 
    public Entry Item00DB; 
    public Entry Item00DC; 
    public Entry Item00DD; 
    public Entry Item00DE; 
    public Entry Item00DF; 
                           
    public Entry Item00E0; 
    public Entry Item00E1; 
    public Entry Item00E2; 
    public Entry Item00E3; 
    public Entry Item00E4; 
    public Entry Item00E5; 
    public Entry Item00E6; 
    public Entry Item00E7; 
    public Entry Item00E8; 
    public Entry Item00E9; 
    public Entry Item00EA; 
    public Entry Item00EB; 
    public Entry Item00EC; 
    public Entry Item00ED; 
    public Entry Item00EE; 
    public Entry Item00EF; 
                           
    public Entry Item00F0; 
    public Entry Item00F1; 
    public Entry Item00F2; 
    public Entry Item00F3; 
    public Entry Item00F4; 
    public Entry Item00F5; 
    public Entry Item00F6; 
    public Entry Item00F7; 
    public Entry Item00F8; 
    public Entry Item00F9; 
    public Entry Item00FA; 
    public Entry Item00FB; 
    public Entry Item00FC; 
    public Entry Item00FD; 
    public Entry Item00FE; 
    public Entry Item00FF;

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
    public static Page256<A> Construct() =>
        new ();
}