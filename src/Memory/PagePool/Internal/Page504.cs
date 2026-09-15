#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory.Internal;

[SkipLocalsInit]
sealed class Page504<A> : 
    Page<Page504<A>, A>, 
    Tr.Constructor<Page504<A>>
{
    // Why 504?  There is a 64-byte metadata Page that comes with a Page.  The most common memory page-size
    // is 4096 bytes.  4096 - 64 = 4032.  4032 / 8 = 504.  This allows the maximum number of 8 byte items to fit
    // in a single page (8 bytes is the size of a pointer on a 64-bit system).
    
    const int Size = 504;
    
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
    
    public Entry Item0100;
    public Entry Item0101;
    public Entry Item0102;
    public Entry Item0103;
    public Entry Item0104;
    public Entry Item0105;
    public Entry Item0106;
    public Entry Item0107;
    public Entry Item0108;
    public Entry Item0109;
    public Entry Item010A;
    public Entry Item010B;
    public Entry Item010C;
    public Entry Item010D;
    public Entry Item010E;
    public Entry Item010F;

    public Entry Item0110;
    public Entry Item0111;
    public Entry Item0112;
    public Entry Item0113;
    public Entry Item0114;
    public Entry Item0115;
    public Entry Item0116;
    public Entry Item0117;
    public Entry Item0118;
    public Entry Item0119;
    public Entry Item011A;
    public Entry Item011B;
    public Entry Item011C;
    public Entry Item011D;
    public Entry Item011E;
    public Entry Item011F;

    public Entry Item0120;
    public Entry Item0121;
    public Entry Item0122;
    public Entry Item0123;
    public Entry Item0124;
    public Entry Item0125;
    public Entry Item0126;
    public Entry Item0127;
    public Entry Item0128;
    public Entry Item0129;
    public Entry Item012A;
    public Entry Item012B;
    public Entry Item012C;
    public Entry Item012D;
    public Entry Item012E;
    public Entry Item012F;

    public Entry Item0130;
    public Entry Item0131;
    public Entry Item0132;
    public Entry Item0133;
    public Entry Item0134;
    public Entry Item0135;
    public Entry Item0136;
    public Entry Item0137;
    public Entry Item0138;
    public Entry Item0139;
    public Entry Item013A;
    public Entry Item013B;
    public Entry Item013C;
    public Entry Item013D;
    public Entry Item013E;
    public Entry Item013F;

    public Entry Item0140; 
    public Entry Item0141; 
    public Entry Item0142; 
    public Entry Item0143; 
    public Entry Item0144; 
    public Entry Item0145; 
    public Entry Item0146; 
    public Entry Item0147; 
    public Entry Item0148; 
    public Entry Item0149; 
    public Entry Item014A; 
    public Entry Item014B; 
    public Entry Item014C; 
    public Entry Item014D; 
    public Entry Item014E; 
    public Entry Item014F; 
    
    public Entry Item0150; 
    public Entry Item0151; 
    public Entry Item0152; 
    public Entry Item0153; 
    public Entry Item0154; 
    public Entry Item0155; 
    public Entry Item0156; 
    public Entry Item0157; 
    public Entry Item0158; 
    public Entry Item0159; 
    public Entry Item015A; 
    public Entry Item015B; 
    public Entry Item015C; 
    public Entry Item015D; 
    public Entry Item015E; 
    public Entry Item015F; 
    
    public Entry Item0160; 
    public Entry Item0161; 
    public Entry Item0162; 
    public Entry Item0163; 
    public Entry Item0164; 
    public Entry Item0165; 
    public Entry Item0166; 
    public Entry Item0167; 
    public Entry Item0168; 
    public Entry Item0169; 
    public Entry Item016A; 
    public Entry Item016B; 
    public Entry Item016C; 
    public Entry Item016D; 
    public Entry Item016E; 
    public Entry Item016F; 
    
    public Entry Item0170; 
    public Entry Item0171; 
    public Entry Item0172; 
    public Entry Item0173; 
    public Entry Item0174; 
    public Entry Item0175; 
    public Entry Item0176; 
    public Entry Item0177; 
    public Entry Item0178; 
    public Entry Item0179; 
    public Entry Item017A; 
    public Entry Item017B; 
    public Entry Item017C; 
    public Entry Item017D; 
    public Entry Item017E; 
    public Entry Item017F; 

    public Entry Item0180;
    public Entry Item0181;
    public Entry Item0182;
    public Entry Item0183;
    public Entry Item0184;
    public Entry Item0185;
    public Entry Item0186;
    public Entry Item0187;
    public Entry Item0188;
    public Entry Item0189;
    public Entry Item018A;
    public Entry Item018B;
    public Entry Item018C;
    public Entry Item018D;
    public Entry Item018E;
    public Entry Item018F;

    public Entry Item0190;
    public Entry Item0191;
    public Entry Item0192;
    public Entry Item0193;
    public Entry Item0194;
    public Entry Item0195;
    public Entry Item0196;
    public Entry Item0197;
    public Entry Item0198;
    public Entry Item0199;
    public Entry Item019A;
    public Entry Item019B;
    public Entry Item019C;
    public Entry Item019D;
    public Entry Item019E;
    public Entry Item019F;

    public Entry Item01A0;
    public Entry Item01A1;
    public Entry Item01A2;
    public Entry Item01A3;
    public Entry Item01A4;
    public Entry Item01A5;
    public Entry Item01A6;
    public Entry Item01A7;
    public Entry Item01A8;
    public Entry Item01A9;
    public Entry Item01AA;
    public Entry Item01AB;
    public Entry Item01AC;
    public Entry Item01AD;
    public Entry Item01AE;
    public Entry Item01AF;

    public Entry Item01B0;
    public Entry Item01B1;
    public Entry Item01B2;
    public Entry Item01B3;
    public Entry Item01B4;
    public Entry Item01B5;
    public Entry Item01B6;
    public Entry Item01B7;
    public Entry Item01B8;
    public Entry Item01B9;
    public Entry Item01BA;
    public Entry Item01BB;
    public Entry Item01BC;
    public Entry Item01BD;
    public Entry Item01BE;
    public Entry Item01BF;

    public Entry Item01C0; 
    public Entry Item01C1; 
    public Entry Item01C2; 
    public Entry Item01C3; 
    public Entry Item01C4; 
    public Entry Item01C5; 
    public Entry Item01C6; 
    public Entry Item01C7; 
    public Entry Item01C8; 
    public Entry Item01C9; 
    public Entry Item01CA; 
    public Entry Item01CB; 
    public Entry Item01CC; 
    public Entry Item01CD; 
    public Entry Item01CE; 
    public Entry Item01CF; 
    
    public Entry Item01D0; 
    public Entry Item01D1; 
    public Entry Item01D2; 
    public Entry Item01D3; 
    public Entry Item01D4; 
    public Entry Item01D5; 
    public Entry Item01D6; 
    public Entry Item01D7; 
    public Entry Item01D8; 
    public Entry Item01D9; 
    public Entry Item01DA; 
    public Entry Item01DB; 
    public Entry Item01DC; 
    public Entry Item01DD; 
    public Entry Item01DE; 
    public Entry Item01DF; 
    
    public Entry Item01E0; 
    public Entry Item01E1; 
    public Entry Item01E2; 
    public Entry Item01E3; 
    public Entry Item01E4; 
    public Entry Item01E5; 
    public Entry Item01E6; 
    public Entry Item01E7; 
    public Entry Item01E8; 
    public Entry Item01E9; 
    public Entry Item01EA; 
    public Entry Item01EB; 
    public Entry Item01EC; 
    public Entry Item01ED; 
    public Entry Item01EE; 
    public Entry Item01EF; 
    
    public Entry Item01F0; 
    public Entry Item01F1; 
    public Entry Item01F2; 
    public Entry Item01F3; 
    public Entry Item01F4; 
    public Entry Item01F5; 
    public Entry Item01F6; 
    public Entry Item01F7; 

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
    public static Page504<A> Construct() =>
        new ();
}