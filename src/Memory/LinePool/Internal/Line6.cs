#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory.Internal;

[SkipLocalsInit]
sealed class Line6<A> : 
    Line<Line6<A>, A>, 
    Tr.Constructor<Line6<A>>
{
    const int Size = 6;
    
    public Entry Item0000;
    public Entry Item0001;
    public Entry Item0002;
    public Entry Item0003;
    public Entry Item0004;
    public Entry Item0005;

    
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
    public static Line6<A> Construct() =>
        new ();
}