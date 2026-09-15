#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory.Internal;

[SkipLocalsInit]
sealed class Line2<A> : 
    Line<Line2<A>, A>, 
    Tr.Constructor<Line2<A>>
{
    const int Size = 2;
    
    public Entry Item0000;
    public Entry Item0001;
    
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
    public static Line2<A> Construct() =>
        new ();
}