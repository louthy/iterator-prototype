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

    public ref Vars yields
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in fields.yields);
    } 
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref Fields fields) =>
        this.fields = ref fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool StartCoRoutine<A>() =>
        
        // Get the result type off the stack
        vars.Pop<A>(out var x) &&

        // Create a new scope
        Push() &&

        // Push the input type into the new co-routine scope
        vars.Push(in x);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool StartNoArgCoRoutine() =>
        
        // Create a new scope
        Push();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool StartYield<A>() =>

        // Get the result type off the stack
        vars.Pop<A>(out var x) &&

        // Create a new scope
        Push() &&

        // Yield
        yields.Push(in x);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool StartYield<A>(in A value) =>
        
        // Create a new scope
        Push() &&
        
        // Yield
        yields.Push(in value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool EndCoRoutine<A>() =>
        
        // Get the return value
        vars.Pop(out A result) &&
        
        // Pop the current scope
        Pop() &&
        
        // Push the result
        vars.Push(in result);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool VoidCoRoutine() =>
        
        // Pop the current scope
        Pop();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push() =>
        
        // Make sure the tops are in-sync with live object and value stacks; so that we can safely pop later.
        tops.Sync(in vars.objs, in vars.values) &&
        
        // Push the current tops onto the stack
        tops.Push();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop()
    {
        if (tops.Pop())
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
    public unsafe bool Prepend(delegate*<ref StackFrame, PullState> f) =>
        ops.Prepend(f);
}
