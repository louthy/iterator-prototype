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
    
    readonly uint item0;
    readonly uint item1;
    readonly uint item2;
    readonly uint item3;
    readonly uint item4;
    readonly uint item5;
    readonly uint item6;
    readonly uint item7;
    readonly uint item8;
    readonly uint item9;
    readonly uint itemA;
    readonly uint itemB;
    readonly uint itemC;
    readonly uint itemD;
    readonly uint itemE;
    readonly uint itemF;
    readonly uint current;
    readonly uint begin;
    readonly int count;

    [MethodImpl(Optimisations.InliningOnly)]
    public Tops()
    {
        count = 1;
        begin = 0;
        current = 0;
    }
    
    Span<uint> Items
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in item0), count);
    }

    Span<uint> AllItems
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in item0), Capacity);
    }    

    public int Count
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count;
    }

    public bool IsEmpty
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count == 0;
    }

    /// <summary>
    /// This is the current state of the frame
    /// </summary>
    public uint Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current;
    }

    public int PC
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (int)((current & ProgramCounterMask) >> ProgramCounterShift);
    }

    public bool IsSingleton
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => YieldsInFrame == 0;
    }

    public bool HasYielded
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => YieldsInFrame > 0;
    }

    public int YieldsInFrame
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (int)((current & YieldCounterMask) >> YieldCounterShift);
    }

    public int ValuesCount
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (int)((current & ValuesMask) >> ValuesShift);
    }

    public int ObjsCount
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (int)((current & ObjsMask) >> ObjsShift);
    }

    public int VarsCount
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (int)((current & VarsMask) >> VarsShift);
    }
}
