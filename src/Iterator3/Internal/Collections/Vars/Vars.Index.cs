/*
#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

readonly partial struct Vars
{
    [StructLayout(LayoutKind.Explicit, Size = sizeof(ushort))]
    public readonly struct Index
    {
        internal const ushort IsObjFlag = 0x8000;
        internal const ushort IsObjMask = IsObjFlag - 1;
        
        [FieldOffset(0)]
        internal readonly ushort Value;

        Index(ushort value) =>
            Value = value;

        public bool IsObj
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Value & IsObjFlag) == IsObjFlag;
        }

        public bool IsUnmanaged
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Value & IsObjFlag) == 0;
        }
        
        public int Offset 
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value & IsObjMask;
        } 
        
        public static implicit operator Index(ushort value) =>
            new(value);
        
        public static implicit operator byte(Index index) =>
            index.Value > 255
                ? throw new OverflowException("Casting the Vars.Index to a byte causes an overflow.")
                : (byte)index.Value;
        
        public static implicit operator ushort(Index index) =>
            index.Value;
    }    
}
*/
