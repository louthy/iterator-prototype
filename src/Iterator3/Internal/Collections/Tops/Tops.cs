#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct Tops
{
    public const int Capacity = 16;
    
    // Program counter: Bits 0 to 7  (8 bits)
    // Values top:      Bits 8 to 15 (8 bits)
    // Objects top:     Bits 16 - 21 (6 bits)
    // Vars top:        Bits 22, 23, 24, 25, 26, 27 (6 bits)
    // Yield counter:   Bits 28 - 31 (4 bits)

    public const uint ProgramCounterMask = 0b00000000_00000000_00000000_11111111;
    public const uint ValuesMask         = 0b00000000_00000000_11111111_00000000;
    public const uint ObjsMask           = 0b00000000_00111111_00000000_00000000;
    public const uint VarsMask           = 0b00001111_11000000_00000000_00000000;
    public const uint YieldCounterMask   = 0b11110000_00000000_00000000_00000000;

    public const uint NotProgramCounterMask = ~ProgramCounterMask;
    public const uint NotValuesMask         = ~ValuesMask;
    public const uint NotObjsMask           = ~ObjsMask;
    public const uint NotVarsMask           = ~VarsMask;
    public const uint NotYieldCounterMask   = ~YieldCounterMask;

    public const int ProgramCounterShift = 0;
    public const int ValuesShift         = 8;
    public const int ObjsShift           = 16;
    public const int VarsShift           = 22;
    public const int YieldCounterShift   = 28;
    
    public readonly uint item0;
    public readonly uint item1;
    public readonly uint item2;
    public readonly uint item3;
    public readonly uint item4;
    public readonly uint item5;
    public readonly uint item6;
    public readonly uint item7;
    public readonly uint item8;
    public readonly uint item9;
    public readonly uint itemA;
    public readonly uint itemB;
    public readonly uint itemC;
    public readonly uint itemD;
    public readonly uint itemE;
    public readonly uint itemF;
    public readonly uint Current;
    public readonly uint Begin;
    public readonly int Count;

    [MethodImpl(Optimisations.InliningOnly)]
    public Tops()
    {
        Count = 1;
        Begin = 0;
        Current = 0;
    }
}
