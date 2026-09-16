using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe int map<A, B>(ref StackFrame frame) =>
        PullGen<A, B>.map(ref frame);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapManagedManaged<A, B>(ref StackFrame frame)
        where A : class
        where B : class
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullManaged.pop<A>(ref frame, out var x) &&
               PullManaged.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapManagedUnmanaged<A, B>(ref StackFrame frame)
        where A : class
        where B : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullManaged.pop<A>(ref frame, out var x) &&
               PullUnmanaged.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapManagedStruct<A, B>(ref StackFrame frame)
        where A : class
        where B : struct
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullManaged.pop<A>(ref frame, out var x) &&
               PullStruct.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapUnmanagedManaged<A, B>(ref StackFrame frame)
        where A : unmanaged
        where B : class
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullUnmanaged.pop<A>(ref frame, out var x) &&
               PullManaged.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapUnmanagedUnmanaged<A, B>(ref StackFrame frame)
        where A : unmanaged
        where B : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullUnmanaged.pop<A>(ref frame, out var x) &&
               PullUnmanaged.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapUnmanagedStruct<A, B>(ref StackFrame frame)
        where A : unmanaged
        where B : struct
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullUnmanaged.pop<A>(ref frame, out var x) &&
               PullStruct.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapStructManaged<A, B>(ref StackFrame frame)
        where A : struct
        where B : class
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullStruct.pop<A>(ref frame, out var x) &&
               PullManaged.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapStructUnmanaged<A, B>(ref StackFrame frame)
        where A : struct
        where B : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullStruct.pop<A>(ref frame, out var x) &&
               PullUnmanaged.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapStructStruct<A, B>(ref StackFrame frame)
        where A : struct
        where B : struct
    {
        var f = PullManaged.arg1<Func<A, B>>(ref frame);
        return PullStruct.pop<A>(ref frame, out var x) &&
               PullStruct.@return(ref frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }


       
    /*
     Unoptimised reference
     
    public static int map<A, B>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a)) 

            ? @continue(ref frame)
            : empty(ref frame);
            */
}