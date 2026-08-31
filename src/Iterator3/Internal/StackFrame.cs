using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
readonly ref struct StackFrame
{
    public readonly ref Fields fields;
        
    [MethodImpl(Optimisations.Default)]
    public StackFrame(ref Fields fields) =>
        this.fields = ref fields;

    public ref Tops tops 
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AsRef(in fields.tops);
    } 
    
    public ref Ops ops  
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AsRef(in fields.ops);
    } 
    
    public ref Globals globals 
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AsRef(in fields.globals);
    } 
    
    public ref Vars vars
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AsRef(in fields.vars);
    }

    [MethodImpl(Optimisations.Default)]
    public bool StartScope() =>
        
        // Create a new scope
        Push();

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

    [MethodImpl(Optimisations.Default)]
    public bool VoidScope() =>
        
        // Pop the current scope
        Pop();
    
    [MethodImpl(Optimisations.Default)]
    public bool Push() =>
        
        // Make sure the tops are in-sync with live object and value stacks; so that we can safely pop later.
        tops.Sync(vars.Snapshot) &&
        
        // Push the current tops onto the stack
        tops.PushFrame();

    [MethodImpl(Optimisations.Default)]
    public bool Pop()
    {
        if (tops.PopFrame())
        {
            vars.Reset(new Vars.State(tops.CurrentObj, tops.CurrentValue));
            return true;
        }
        else
        {
            vars.Reset(new Vars.State(0, 0));
            return false;
        }
    }

    public bool IsVoid
    {
        [MethodImpl(Optimisations.Default)]
        get => tops.IsEmpty;
    }

    public bool IsReturn
    {
        [MethodImpl(Optimisations.Default)]
        get => tops.CurrentPC == ops.Count;
    }
        
    [MethodImpl(Optimisations.Default)]
    public unsafe bool Add(delegate*<ref StackFrame, PullState> f) =>
        ops.Add(f);
        
    [MethodImpl(Optimisations.Default)]
    public unsafe bool Prepend(delegate*<ref StackFrame, PullState> f) =>
        ops.Prepend(f);
}
