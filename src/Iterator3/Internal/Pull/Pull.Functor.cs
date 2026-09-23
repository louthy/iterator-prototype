using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe int map<A, B>(in StackFrame frame) =>
        PullGen<A, B>.map(in frame);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapManagedManaged<A, B>(in StackFrame frame)
        where A : class
        where B : class
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullManaged.pop<A>(in frame, out var x) &&
               PullManaged.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapManagedUnmanaged<A, B>(in StackFrame frame)
        where A : class
        where B : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullManaged.pop<A>(in frame, out var x) &&
               PullUnmanaged.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapManagedStruct<A, B>(in StackFrame frame)
        where A : class
        where B : struct
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullManaged.pop<A>(in frame, out var x) &&
               PullStruct.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapUnmanagedManaged<A, B>(in StackFrame frame)
        where A : unmanaged
        where B : class
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullUnmanaged.pop<A>(in frame, out var x) &&
               PullManaged.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapUnmanagedUnmanaged<A, B>(in StackFrame frame)
        where A : unmanaged
        where B : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullUnmanaged.pop<A>(in frame, out var x) &&
               PullUnmanaged.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapUnmanagedStruct<A, B>(in StackFrame frame)
        where A : unmanaged
        where B : struct
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullUnmanaged.pop<A>(in frame, out var x) &&
               PullStruct.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapStructManaged<A, B>(in StackFrame frame)
        where A : struct
        where B : class
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullStruct.pop<A>(in frame, out var x) &&
               PullManaged.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapStructUnmanaged<A, B>(in StackFrame frame)
        where A : struct
        where B : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullStruct.pop<A>(in frame, out var x) &&
               PullUnmanaged.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int mapStructStruct<A, B>(in StackFrame frame)
        where A : struct
        where B : struct
    {
        var f = PullManaged.arg1<Func<A, B>>(in frame);
        return PullStruct.pop<A>(in frame, out var x) &&
               PullStruct.@return(in frame, f(x))
                   ? PullState.Continue
                   : PullState.Void;
    }


       
    /*
     Unoptimised reference
     
    public static int map<A, B>(in StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B>>(in frame, out var f) &&

        // Take the value off the stack
        pop<A>(in frame, out var a) &&

        // Push the mapped value on the stack
        @return(in frame, f(a)) 

            ? @continue(in frame)
            : empty(in frame);
            */
}