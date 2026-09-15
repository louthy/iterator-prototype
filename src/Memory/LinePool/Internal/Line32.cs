#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory.Internal;

[SkipLocalsInit]
sealed class Line32<A> : 
    Line<Line32<A>, A>, 
    Tr.Constructor<Line32<A>>
{
    const int Size = 32;
    
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
    public static Line32<A> Construct() =>
        new ();
}