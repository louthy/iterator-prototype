#pragma warning disable CS8618 
#pragma warning disable CS0169
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct Globals
{
    const int Capacity = 32;
    
    [StructLayout(LayoutKind.Explicit, Size = sizeof(ushort))]
    readonly ref struct Index
    {
        public const ushort IsObjFlag = 0x8000;
        public const ushort IsObjMask = IsObjFlag - 1;
        
        [FieldOffset(0)]
        readonly ushort Value;
        
        public Index(ushort value) =>
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
    }
    
    readonly ushort data00, data01, data02, data03, data04, data05, data06, data07;
    readonly ushort data08, data09, data0A, data0B, data0C, data0D, data0E, data0F;
    readonly ushort data10, data11, data12, data13, data14, data15, data16, data17;
    readonly ushort data18, data19, data1A, data1B, data1C, data1D, data1E, data1F;
    public readonly ushort Count;
    readonly ObjStack objs;
    readonly ByteStack values;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    ref Index Ix(in ushort ix) =>
        ref Unsafe.As<ushort, Index>(ref IxUntyped(in ix));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    ref ushort IxUntyped(in ushort ix) =>
        ref Unsafe.Add(ref Unsafe.AsRef(in data00), ix);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A AtUnmanaged<A>(ushort ix)
        where A : unmanaged
    {
        ref var index = ref Ix(in ix);
        ref var vt = ref Unsafe.AsRef(in values.Stack);
        return ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref vt, index.Offset));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A AtManaged<A>(ushort ix)
        where A : class
    {
        ref var index = ref Ix(in ix);
        ref var ot    = ref Unsafe.AsRef(in objs.Object00);
        return ref Unsafe.As<object, A>(ref Unsafe.Add(ref ot, index.Offset));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A AtStruct<A>(ushort ix)
        where A : struct
    {
        ref var index = ref Ix(in ix);
        ref var ot    = ref Unsafe.AsRef(in objs.Object00);
        ref var box   = ref Unsafe.As<object, Box<A>>(ref Unsafe.Add(ref ot, index.Offset));
        return ref box.Ref;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AtUnmanaged<A>(in ushort ix, out A value)
        where A : unmanaged
    {
        if (ix < Count)
        {
            ref var index = ref Ix(in ix);
            ref var vt    = ref Unsafe.AsRef(in values.Stack);
            ref var val   = ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref vt, index.Offset));
            value = val;
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AtManaged<A>(in ushort ix, out A value)
        where A : class
    {
        if (ix < Count)
        {
            ref var index = ref Ix(in ix);
            ref var ot    = ref Unsafe.AsRef(in objs.Object00);
            ref var val   = ref Unsafe.As<object, A>(ref Unsafe.Add(ref ot, index.Offset));
            value = val;
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AtStruct<A>(in ushort ix, out A value)
        where A : struct
    {
        if (ix < Count)
        {
            ref var index = ref Ix(in ix);
            ref var ot    = ref Unsafe.AsRef(in objs.Object00);
            ref var box   = ref Unsafe.As<object, Box<A>>(ref Unsafe.Add(ref ot, index.Offset));
            value = box.Ref;
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AddStruct<A>(in A value)
        where A : struct =>
        AddManaged(new Box<A>(in value), out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AddStruct<A>(in A value, out ushort index)
        where A : struct =>
        AddManaged(new Box<A>(in value), out index);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AddManaged<A>(in A value)
        where A : class =>
        AddManaged(in value, out _);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AddManaged<A>(in A value, out ushort index)
        where A : class
    {
        ref var c = ref Unsafe.AsRef(in Count);
        if(c >= Capacity)
        {
            index = 0;
            return false;
        }

        var top = objs.Top;
        if (objs.Push(in value))
        {
            ref var ix = ref IxUntyped(c);
            ix = (ushort)(top | Index.IsObjFlag);
            index = c;
            c++;
            return true;
        }
        else
        {
            index = 0;
            return false;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AddUnmanaged<A>(in A value)
        where A : unmanaged =>
        AddUnmanaged(in value, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool AddUnmanaged<A>(in A value, out ushort index)
        where A : unmanaged
    {
        ref var c = ref Unsafe.AsRef(in Count);
        if(c >= Capacity)
        {
            index = 0;
            return false;
        }

        var top = values.Top;
        if (values.Push(in value))
        {
            ref var ix = ref IxUntyped(c);
            ix = (ushort)(top & Index.IsObjMask);
            index = c;
            c++;
            return true;
        }
        else
        {
            index = 0;
            return false;
        }
    }
}

