using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
readonly ref struct StackFrame
{
    public readonly ref readonly Tops tops;
    public readonly ref readonly Ops ops;
    public readonly ref readonly Globals globals;
    public readonly ref readonly Vars vars;
    public readonly Args args;

    [MethodImpl(Optimisations.Max)]
    public StackFrame(in Fields fields)
    {
        tops = ref fields.tops;
        ops = ref fields.ops;
        globals = ref fields.globals;
        vars = ref fields.vars;
    }
        
    [MethodImpl(Optimisations.Default)]
    public bool StartScope() =>
        
        // Create a new scope
        Push();

    [MethodImpl(Optimisations.Default)]
    public bool StartYieldScope() =>
        
        // Make sure the tops are in-sync with live object
        // and value stacks; so that we can safely pop later.
        vars.SyncTo(in tops) &&
        
        // Push the current tops onto the stack
        tops.PushFrame(1);

    [MethodImpl(Optimisations.Default)]
    public bool EndScope<A>(out A head) =>
        
        // Get the return value
        vars.Pop(out head, false) &&

        // Pop the current scope
        Pop();

    [MethodImpl(Optimisations.Default)]
    public bool ResetFrame<A>(out A result) =>
        
        // Get the return value
        vars.Pop(out result, true) &&

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
        vars.SyncTo(in tops) &&
        
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
        var yielded = tops.YieldsInFrame;
        return $"[pc:{pc}, objs:{objs}/{tops.ObjsCount}, vals:{vals}/{tops.ValuesCount}, tops:{tops.Count}, y:{yielded}, ops:{ops.Count}]";
    }
    
    [MethodImpl(Optimisations.Max)]
    public ref readonly Op Op(int index) =>
        ref ops[index];

    [MethodImpl(Optimisations.Max)]
    public ref readonly Op Op(uint index) =>
        ref ops[index];

    public int OpsRemaining
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ops.Count - tops.PC;
    }

    [MethodImpl(Optimisations.Max)]
    public void NextOp() =>
        tops.NextOp();

    public ref readonly Op CurrentOp
    {
        [MethodImpl(Optimisations.Max)]
        get => ref Op(PC);
    }

    public int PC
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => tops.PC;
    }
}
