#pragma warning disable CS8618 
#pragma warning disable CS0169
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct Globals
{
    readonly ObjStack2 objs;
    readonly ByteList2 values;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool ResetAtUnmanaged<A>(in ushort ix, out A value)
        where A : unmanaged =>
        values.RestoreAt(in ix, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool ResetAtManaged<A>(ushort ix, out A value)
        where A : class =>
        objs.RestoreAt(ix, out value);

    [MethodImpl(Optimisations.Default)]
    public bool ResetAtStruct<A>(ushort ix, out A value)
        where A : struct
    {
        if (ix < objs.Count)
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

    [MethodImpl(Optimisations.InliningOnly)]
    public bool ResetAtUnmanaged<A>(ushort ix)
        where A : unmanaged =>
        values.RestoreAt<A>(ix, out _);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool ResetAtManaged<A>(ushort ix)
        where A : class =>
        objs.RestoreAt(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool ResetAtStruct<A>(ushort ix)
        where A : struct
    {
        if (ix >= objs.Count) return false;
        ref var declared = ref DeclaredAtStruct<A>(ix);
        ref var variable = ref AtStruct<A>(ix);
        variable = declared;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A DeclaredAtUnmanaged<A>(ushort ix)
        where A : unmanaged =>
        ref values.DeclaredAt<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A DeclaredAtManaged<A>(ushort ix)
        where A : class =>
        ref objs.DeclaredAt<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A DeclaredAtStruct<A>(ushort ix)
        where A : struct =>
        ref DeclaredAtManaged<Box<A>>(ix).Ref;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool DeclaredAtUnmanaged<A>(ushort ix, out A value)
        where A : unmanaged =>
        values.DeclaredAt(ix, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool DeclaredAtManaged<A>(ushort ix, out A value)
        where A : class =>
        objs.DeclaredAt(ix, out value);

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

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A AtUnmanaged<A>(ushort ix)
        where A : unmanaged =>
        ref values.At<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A AtManaged<A>(ushort ix)
        where A : class =>
        ref objs.At<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A AtStruct<A>(ushort ix)
        where A : struct =>
        ref AtManaged<Box<A>>(ix).Ref;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool AtUnmanaged<A>(in ushort ix, out A value)
        where A : unmanaged =>
        values.At(in ix, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool AtManaged<A>(in ushort ix, out A value)
        where A : class =>
        objs.At(ix, out value);

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
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool AddStruct<A>(in A value)
        where A : struct =>
        AddStruct(in value, out _);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool AddStruct<A>(in A value, out ushort index)
        where A : struct =>
        objs.Push(Boxes.alloc(in value), Boxes.alloc(in value), out index);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool AddManaged<A>(in A value)
        where A : class =>
        AddManaged(in value, out _);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool AddManaged<A>(in A value, out ushort index)
        where A : class =>
        objs.Push(value, out index);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool AddUnmanaged<A>(in A value)
        where A : unmanaged =>
        values.Add(in value);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool AddUnmanaged<A>(in A value, out ushort index)
        where A : unmanaged =>
        values.Add(in value, out index);

    [MethodImpl(Optimisations.Default)]
    public bool AtEndStruct<A>(in ushort ix, out Global<A> global)
        where A : struct
    {
        var count = objs.Count;
        if (ix <= count)
        {
            global = new Global<A>((ushort)(count - ix));
            return true;
        }
        else
        {
            global = default;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool AtEndManaged<A>(in ushort ix, out Global<A> global)
        where A : class
    {
        var count = objs.Count;
        if (ix <= count)
        {
            global = new Global<A>((ushort)(count - ix));
            return true;
        }
        else
        {
            global = default;
            return false;
        }
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool AtEndUnmanaged<A>(in ushort ix, out Global<A> global)
        where A : unmanaged
    {
        var count = values.Count;
        if (ix <= count)
        {
            global = new Global<A>((ushort)(count - ix));
            return true;
        }
        else
        {
            global = default;
            return false;
        }
    }
}

