using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
readonly ref struct StackFrame
{
    public readonly ref Fields fields;

    public ref Tops tops 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in fields.tops);
    } 
    
    public ref Ops ops  
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in fields.ops);
    } 
    
    public ref Globals globals 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in fields.globals);
    } 
    
    public ref Vars vars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in fields.vars);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Unwind()
    {
        // TODO: Some more efficient way to unwind the stack.

        while (Pop())
        {
            // unwind 
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref Fields fields) =>
        this.fields = ref fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool SetArg<A>(ushort argIndex, A value)
    {
        ref var g = ref globals.At<A>(argIndex);
        g = value;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool ClearArg<A>(ushort argIndex)
    {
        ref var g = ref globals.At<A>(argIndex);
        g = default!;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool StartScope()
    {
        // Create a new scope
        var success = Push();
        
        var self = this;
        Log.coroutine("{", ref self);
        Log.scope();
        
        return success;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool EndScope<A>(out A head)
    {
        // Get the return value
        if (!vars.Pop(out head)) return false;
        
        // Pop the current scope
        var success = Pop();
                
        var self = this;
        Log.descope();
        Log.coroutine("}", ref self);

        return success;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool ResetFrame<A>(out A result)
    {
        // Get the return value
        vars.Pop(out result);

        var self = this;
        Log.coroutine("end-frame", ref self);

        // Pop the current tops
        var success = tops.ResetFrame();
        
        Log.coroutine("reset-frame", ref self);
        return success;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool VoidScope()
    {
        var self = this;
        Log.descope();
        Log.coroutine("}", ref self);
        
        // Pop the current scope
        var success = Pop();

        Log.coroutine("end", ref self);

        return success;
    }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push() =>
        
        // Make sure the tops are in-sync with live object and value stacks; so that we can safely pop later.
        tops.Sync(in vars.objs, in vars.values) &&
        
        // Push the current tops onto the stack
        tops.PushFrame();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop()
    {
        if (tops.PopFrame())
        {
            vars.objs.PopToTop(tops.CurrentObj);
            vars.values.PopToTop(tops.CurrentValue);
            return true;
        }
        else
        {
            vars.objs.PopToTop(0);
            vars.values.PopToTop(0);
            return false;
        }
    }

    public bool IsVoid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => tops.IsEmpty;
    }

    public bool IsReturn
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => tops.CurrentPC == ops.Count;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe bool Add(delegate*<ref StackFrame, PullState> f) =>
        ops.Add(f);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe bool Add(delegate*<ref StackFrame, PullState> f, delegate*<ref StackFrame, PullState> c) =>
        ops.Add(f, c);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe bool Prepend(delegate*<ref StackFrame, PullState> f) =>
        ops.Prepend(f);
}
