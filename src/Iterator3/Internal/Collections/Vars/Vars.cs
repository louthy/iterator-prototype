#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS0649 // Field is never assigned to
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct Vars
{
    public const int Capacity = 31;
    
    public readonly ObjStack objs;
    public readonly ByteStack values;
    
    // These flags remember if a value is a co-routine argument, or not, and if so, stops it
    // being popped off the stack (when the `force` flag is `false). That means subsequent 
    // loops through an 'iterable' can use the full stack of co-routine arguments.
    public readonly byte flag0, flag1, flag2, flag3, flag4, flag5, flag6, flag7;
    public readonly byte flag8, flag9, flagA, flagB, flagC, flagD, flagE, flagF;
    public readonly byte flag10, flag11, flag12, flag13, flag14, flag15, flag16, flag17;
    public readonly byte flag18, flag19, flag1A, flag1B, flag1C, flag1D, flag1E /*, flag1F -- we're using this byte for `top` */;
    public readonly byte top;
}
