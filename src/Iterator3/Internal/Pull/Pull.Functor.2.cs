using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    public static unsafe IterOp bimap<A, B, C>() =>
        PullGen<A, B, C>.bimap;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedManagedManaged<A, B, C>(in StackFrame frame)
        where A : class
        where B : class
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedManagedManaged<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : class
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructManagedManaged<A, B, C>(in StackFrame frame)
        where A : struct
        where B : class
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }


    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedUnmanagedManaged<A, B, C>(in StackFrame frame)
        where A : class
        where B : unmanaged
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedUnmanagedManaged<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : unmanaged
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructUnmanagedManaged<A, B, C>(in StackFrame frame)
        where A : struct
        where B : unmanaged
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }


    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedStructManaged<A, B, C>(in StackFrame frame)
        where A : class
        where B : struct
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedStructManaged<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : struct
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructStructManaged<A, B, C>(in StackFrame frame)
        where A : struct
        where B : struct
        where C : class
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullManaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }



    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedManagedUnmanaged<A, B, C>(in StackFrame frame)
        where A : class
        where B : class
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedManagedUnmanaged<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : class
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructManagedUnmanaged<A, B, C>(in StackFrame frame)
        where A : struct
        where B : class
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedUnmanagedUnmanaged<A, B, C>(in StackFrame frame)
        where A : class
        where B : unmanaged
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedUnmanagedUnmanaged<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructUnmanagedUnmanaged<A, B, C>(in StackFrame frame)
        where A : struct
        where B : unmanaged
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedStructUnmanaged<A, B, C>(in StackFrame frame)
        where A : class
        where B : struct
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedStructUnmanaged<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : struct
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructStructUnmanaged<A, B, C>(in StackFrame frame)
        where A : struct
        where B : struct
        where C : unmanaged
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullUnmanaged.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }



    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedManagedStruct<A, B, C>(in StackFrame frame)
        where A : class
        where B : class
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedManagedStruct<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : class
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructManagedStruct<A, B, C>(in StackFrame frame)
        where A : struct
        where B : class
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }


    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedUnmanagedStruct<A, B, C>(in StackFrame frame)
        where A : class
        where B : unmanaged
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedUnmanagedStruct<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : unmanaged
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructUnmanagedStruct<A, B, C>(in StackFrame frame)
        where A : struct
        where B : unmanaged
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return pop<A, B>(in frame, out var a, out var b) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }


    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManagedStructStruct<A, B, C>(in StackFrame frame)
        where A : class
        where B : struct
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return PullStruct.pop<B>(in frame, out var b)  &&
               PullManaged.pop<A>(in frame, out var a) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanagedStructStruct<A, B, C>(in StackFrame frame)
        where A : unmanaged
        where B : struct
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return PullStruct.pop<B>(in frame, out var b)    &&
               PullUnmanaged.pop<A>(in frame, out var a) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStructStructStruct<A, B, C>(in StackFrame frame)
        where A : struct
        where B : struct
        where C : struct
    {
        var f = PullManaged.arg1<Func<A, B, C>>(in frame);
        return PullStruct.pop<B>(in frame, out var b) &&
               PullStruct.pop<A>(in frame, out var a) &&
               PullStruct.@return(in frame, f(a, b))
                   ? PullState.Continue
                   : PullState.Void;
    }


    /*

     Unoptimised reference implementations

    public static int bimap<A, B, C>(in StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C>>(in frame, out var f) &&

        // Take the value off the stack
        pop<B>(in frame, out var b) &&

        // Take the value off the stack
        pop<A>(in frame, out var a) &&

        // Push the mapped value on the stack
        @return(in frame, f(a, b))

            ? @continue(in frame)
            : empty(in frame);

    public static int bimap1<A, B, C>(in StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C>>(in frame, out var f) &&

        // Take the value off the stack
        pop<(A, B)>(in frame, out var ab) &&

        // Push the mapped value on the stack
        @return(in frame, f(ab.Item1, ab.Item2))

            ? @continue(in frame)
            : empty(in frame);
            */

}