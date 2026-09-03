using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
readonly ref struct StackFrame
{
    public readonly ref Tops tops;
    public readonly ref Ops ops;
    public readonly ref Globals globals;
    public readonly ref Vars vars;
    public readonly Args args;

    [MethodImpl(Optimisations.Max)]
    public StackFrame(ref Fields fields)
    {
        tops = ref Unsafe.AsRef(in fields.tops);
        ops = ref Unsafe.AsRef(in fields.ops);
        globals = ref Unsafe.AsRef(in fields.globals);
        vars = ref Unsafe.AsRef(in fields.vars);
    }

    [MethodImpl(Optimisations.Default)]
    public bool StartScope() =>
        
        // Create a new scope
        Push();

    [MethodImpl(Optimisations.Default)]
    public bool StartYieldScope() =>
        
        // Make sure the tops are in-sync with live object
        // and value stacks; so that we can safely pop later.
        vars.SyncTo(ref tops) &&
        
        // Push the current tops onto the stack
        tops.PushFrame(1);

    [MethodImpl(Optimisations.Default)]
    public bool EndScope<A>(out A head) =>
        
        // Get the return value
        vars.Pop(out head) &&

        // Pop the current scope
        Pop();

    [MethodImpl(Optimisations.Default)]
    public bool ResetFrame<A>(out A result) =>
        
        // Get the return value
        vars.Pop(out result) &&

        // Pop the current tops
        tops.ResetFrame();

    [MethodImpl(Optimisations.InliningOnly)]
    public bool VoidScope() =>
        
        // Pop the current scope
        Pop();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool Push() =>
        
        // Make sure the tops are in-sync with live object
        // and value stacks; so that we can safely pop later.
        vars.SyncTo(ref tops) &&
        
        // Push the current tops onto the stack
        tops.PushFrame(0);

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

    public bool IsReturn
    {
        [MethodImpl(Optimisations.Default)]
        get => tops.PC == ops.Count;
    }
        
    [MethodImpl(Optimisations.Default)]
    public unsafe bool Add(IterOp f) =>
        ops.Add(f);
        
    [MethodImpl(Optimisations.Default)]
    public unsafe bool Prepend(IterOp f) =>
        ops.Prepend(f);

    public override string ToString()
    {
        var pc      = tops.Current & 0xff;
        var objs    = vars.ObjsCount;
        var vals    = vars.ValuesCount;
        var yielded = tops.HasYielded.ToString().ToLower();
        return $"[pc:{pc}, objs:{objs}, vals:{vals}, tops:{tops.Count}, y:{yielded}, ops:{ops.Count}]";
    }
}
