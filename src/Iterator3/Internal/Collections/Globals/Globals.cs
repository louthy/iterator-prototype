#pragma warning disable CS8618 
#pragma warning disable CS0169
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct Globals
{
    public const int Capacity = 32;
    
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
    readonly ObjStack declaredObjs;
    readonly ByteStack declaredValues;

    [MethodImpl(Optimisations.Default)]
    ref Index Ix(in ushort ix) =>
        ref Unsafe.As<ushort, Index>(ref IxUntyped(in ix));

    [MethodImpl(Optimisations.Default)]
    ref ushort IxUntyped(in ushort ix) =>
        ref Unsafe.Add(ref Unsafe.AsRef(in data00), ix);

    [MethodImpl(Optimisations.Default)]
    public bool ResetAtUnmanaged<A>(ushort ix, out A value)
        where A : unmanaged
    {
        if (ix < Count)
        {
            ref var declared = ref DeclaredAtUnmanaged<A>(ix);
            ref var variable = ref AtUnmanaged<A>(ix);
            variable = declared;
            value = variable;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool ResetAtManaged<A>(ushort ix, out A value)
        where A : class
    {
        if (ix < Count)
        {
            ref var declared = ref DeclaredAtManaged<A>(ix);
            ref var variable = ref AtManaged<A>(ix);
            variable = declared;
            value = variable;
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool ResetAtStruct<A>(ushort ix, out A value)
        where A : struct
    {
        if (ix < Count)
        {
            ref var declared = ref DeclaredAtStruct<A>(ix);
            ref var variable = ref AtStruct<A>(ix);
            variable = declared;
            value = variable;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool ResetAtUnmanaged<A>(ushort ix)
        where A : unmanaged
    {
        if (ix >= Count) return false;
        ref var declared = ref DeclaredAtUnmanaged<A>(ix);
        ref var variable = ref AtUnmanaged<A>(ix);
        variable = declared;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool ResetAtManaged<A>(ushort ix)
        where A : class
    {
        if (ix >= Count) return false;
        ref var declared = ref DeclaredAtManaged<A>(ix);
        ref var variable = ref AtManaged<A>(ix);
        variable = declared;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool ResetAtStruct<A>(ushort ix)
        where A : struct
    {
        if (ix >= Count) return false;
        ref var declared = ref DeclaredAtStruct<A>(ix);
        ref var variable = ref AtStruct<A>(ix);
        variable = declared;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public ref A DeclaredAtUnmanaged<A>(ushort ix)
        where A : unmanaged
    {
        ref var index = ref Ix(in ix);
        ref var vt    = ref Unsafe.AsRef(in declaredValues.Stack);
        return ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref vt, index.Offset));
    }

    [MethodImpl(Optimisations.Default)]
    public ref A DeclaredAtManaged<A>(ushort ix)
        where A : class
    {
        ref var index = ref Ix(in ix);
        ref var ot    = ref Unsafe.AsRef(in declaredObjs.Object00);
        return ref Unsafe.As<object, A>(ref Unsafe.Add(ref ot, index.Offset));
    }

    [MethodImpl(Optimisations.Default)]
    public ref A DeclaredAtStruct<A>(ushort ix)
        where A : struct =>
        ref DeclaredAtManaged<Box<A>>(ix).Ref;

    [MethodImpl(Optimisations.Default)]
    public bool DeclaredAtUnmanaged<A>(ushort ix, out A value)
        where A : unmanaged
    {
        if (ix < Count)
        {
            ref var index = ref Ix(in ix);
            ref var vt    = ref Unsafe.AsRef(in declaredValues.Stack);
            value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref vt, index.Offset));
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool DeclaredAtManaged<A>(ushort ix, out A value)
        where A : class
    {
        if (ix < Count)
        {
            ref var index = ref Ix(in ix);
            ref var ot    = ref Unsafe.AsRef(in declaredObjs.Object00);
            value = Unsafe.As<object, A>(ref Unsafe.Add(ref ot, index.Offset));
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool DeclaredAtStruct<A>(ushort ix, out A value)
        where A : struct
    {
        if (DeclaredAtManaged<Box<A>>(ix, out var box))
        {
            value = box.Ref;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public ref A AtUnmanaged<A>(ushort ix)
        where A : unmanaged
    {
        ref var index = ref Ix(in ix);
        ref var vt = ref Unsafe.AsRef(in values.Stack);
        return ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref vt, index.Offset));
    }

    [MethodImpl(Optimisations.Default)]
    public ref A AtManaged<A>(ushort ix)
        where A : class
    {
        ref var index = ref Ix(in ix);
        ref var ot    = ref Unsafe.AsRef(in objs.Object00);
        return ref Unsafe.As<object, A>(ref Unsafe.Add(ref ot, index.Offset));
    }

    [MethodImpl(Optimisations.Default)]
    public ref A AtStruct<A>(ushort ix)
        where A : struct =>
        ref AtManaged<Box<A>>(ix).Ref;
    
    [MethodImpl(Optimisations.Default)]
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

    [MethodImpl(Optimisations.Default)]
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

    [MethodImpl(Optimisations.Default)]
    public bool AtStruct<A>(in ushort ix, out A value)
        where A : struct
    {
        if (AtManaged<Box<A>>(in ix, out var box))
        {
            value = box.Ref;
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }

    
    [MethodImpl(Optimisations.Default)]
    public bool AddStruct<A>(in A value)
        where A : struct =>
        AddStruct(in value, out _);

    [MethodImpl(Optimisations.Default)]
    public bool AddStruct<A>(in A value, out ushort index)
        where A : struct
    {
        ref var c = ref Unsafe.AsRef(in Count);
        if(c >= Capacity)
        {
            index = 0;
            return false;
        }
        
        var top = objs.Count;
        
        // NOTE: The two box allocations are required because the declared value
        //       is used to reset the original value.  We pass references back, so 
        //       sharing the same box means modifying the variable is also modifying
        //       the original.
        if (objs.Push(new Box<A>(in value)) && declaredObjs.Push(new Box<A>(in value)))
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
    
    [MethodImpl(Optimisations.Default)]
    public bool AddManaged<A>(in A value)
        where A : class =>
        AddManaged(in value, out _);

    [MethodImpl(Optimisations.Default)]
    public bool AddManaged<A>(in A value, out ushort index)
        where A : class
    {
        ref var c = ref Unsafe.AsRef(in Count);
        if(c >= Capacity)
        {
            index = 0;
            return false;
        }

        var top = objs.Count;
        if (objs.Push(in value) && declaredObjs.Push(in value))
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
    
    [MethodImpl(Optimisations.Default)]
    public bool AddUnmanaged<A>(in A value)
        where A : unmanaged =>
        AddUnmanaged(in value, out _);
    
    [MethodImpl(Optimisations.Default)]
    public bool AddUnmanaged<A>(in A value, out ushort index)
        where A : unmanaged
    {
        ref var c = ref Unsafe.AsRef(in Count);
        if(c >= Capacity)
        {
            index = 0;
            return false;
        }

        var top = values.Count;
        if (values.Push(in value) && declaredValues.Push(in value))
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

