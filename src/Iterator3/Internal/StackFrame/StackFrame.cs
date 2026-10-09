using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
readonly ref struct StackFrame
{
    public readonly ref TopsMutable tops;
    public readonly ref OpsMutable ops;
    public readonly ref GlobalsMutable globals;
    public readonly ref VarsMutable vars;
    public readonly Args args;

    [MethodImpl(Optimisations.Max)]
    public StackFrame(in Fields fields)
    {
        tops = ref Unsafe.As<Tops, TopsMutable>(ref Unsafe.AsRef(in fields.tops));
        ops = ref Unsafe.As<Ops, OpsMutable>(ref Unsafe.AsRef(in fields.ops));
        globals = ref Unsafe.As<Globals, GlobalsMutable>(ref Unsafe.AsRef(in fields.globals));
        vars = ref Unsafe.As<Vars, VarsMutable>(ref Unsafe.AsRef(in fields.vars));
    }

    [MethodImpl(Optimisations.Default)]
    public void StartScope() =>

        // Create a new scope
        Push();

    [MethodImpl(Optimisations.Default)]
    public void StartYieldScope()
    {
        // Make sure the tops are in-sync with live object
        // and value stacks; so that we can safely pop later.
        vars.SyncTo(ref tops);

        // Push the current tops onto the stack
        tops.PushFrame(1);
    }

    [MethodImpl(Optimisations.Default)]
    public void EndScope<A>(out A head)
    {
        // Get the return value
        vars.Pop(out head, false);

        // Pop the current scope
        Pop();
    }

    [MethodImpl(Optimisations.Default)]
    public void ResetFrame<A>(out A result)
    {
        // Get the return value
        vars.Pop(out result, true);

        // Pop the current tops
        tops.ResetFrame();
    }

    [MethodImpl(Optimisations.Default)]
    public void ResetFrameManaged<A>(out A result)
        where A : class
    {
        // Get the return value
        vars.PopManaged(out result, true);

        // Pop the current tops
        tops.ResetFrame();
    }

    [MethodImpl(Optimisations.Default)]
    public void ResetFrameUnmanaged<A>(out A result)
        where A : unmanaged
    {
        // Get the return value
        vars.PopUnmanaged(out result, true);

        // Pop the current tops
        tops.ResetFrame();
    }

    [MethodImpl(Optimisations.Default)]
    public void ResetFrameStruct<A>(out A result)
        where A : struct
    {
        // Get the return value
        vars.PopStruct(out result, true);

        // Pop the current tops
        tops.ResetFrame();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool VoidScope() =>
        
        // Pop the current scope
        Pop();

    [MethodImpl(Optimisations.InliningOnly)]
    public void Push()
    {
        // Make sure the tops are in-sync with live object
        // and value stacks; so that we can safely pop later.
        vars.SyncTo(ref tops);

        // Push the current tops onto the stack
        tops.PushFrame(0);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Pop()
    {
        if (tops.PopFrame())
        {
            vars.SyncFrom(in tops);
            return true;
        }
        else
        {
            vars.Zero();
            return false;
        }
    }

    public bool IsVoid
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => tops.IsEmpty;
    }
    
    public bool IsRunnable
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ops.IsRunnable;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public unsafe bool Add(
        IterOp f, 
        int varBytesIn, 
        int varBytesOut, 
        int varObjsIn, 
        int varObjsOut, 
        OpReturn @return) =>
        ops.Add(f, varBytesIn, varBytesOut, varObjsIn, varObjsOut, @return);

    [MethodImpl(Optimisations.InliningOnly)]
    public unsafe bool Prepend(
        IterOp f, 
        int varBytesIn, 
        int varBytesOut, 
        int varObjsIn, 
        int varObjsOut, 
        OpReturn @return) =>
        ops.Prepend(f, varBytesIn, varBytesOut, varObjsIn, varObjsOut, @return);

    public override string ToString()
    {
        var pc      = tops.PC;
        var objs    = vars.ObjsCount;
        var vals    = vars.ValuesCount;
        var yielded = tops.YieldsInFrame;
        return $"[pc:{pc}, objs:{objs}/{tops.ObjsCount}, vals:{vals}/{tops.ValuesCount}, tops:{tops.Count}, y:{yielded}, ops:{ops.Count}]";
    }
}
