using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal;

static class Log
{
    public static string ty<A>() => 
        typeof(A).Name;
    
#if DEBUG

    static bool enabled = true;
    internal static string indent = "";

    public static PullState scope()
    {
        indent += "  ";
        return PullState.Continue;
    }

    public static PullState descope()
    {
        
        indent = indent.Length > 1 ? indent[..^2] : indent;
        return PullState.Continue;
    }
    
    public static PullState enable()
    {
        enabled = true;
        return PullState.Continue;
    }
    
    public static PullState disable()
    {
        enabled = false;
        return PullState.Continue;
    }
    
    public static string stackInfo(ref StackFrame frame) =>
        $"[pc:{frame.tops.CurrentPC}, objs:{frame.vars.objs.Top}, vals:{frame.vars.values.Top}, tops:{frame.tops.Count}, y:{frame.tops.CurrentYield}, len: {frame.tops.Count}]";

    static PullState write(string msg, ConsoleColor colour, ref StackFrame frame)
    {
        if (!enabled) return PullState.Continue;
        var c = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(indent);
        Console.Write(msg);
        Console.Write(' ');
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(stackInfo(ref frame));
        Console.ForegroundColor = c;
        Console.WriteLine();
        return PullState.Continue;
    }

    public static PullState function(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Green, ref frame);

    public static PullState coroutine(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Cyan, ref frame);

    public static PullState value(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Magenta, ref frame);

    public static PullState terminator(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Blue, ref frame);

    public static PullState msg(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.White, ref frame);

    public static PullState warn(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Yellow, ref frame);

    public static PullState err(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Red, ref frame);

#else


    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void scope()
    {
    }
            
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void descope()
    { 
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static string stackInfo(ref StackFrame frame)
    {
        return default!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState write(string msg, ConsoleColor colour, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState function(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState coroutine(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState value(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState terminator(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState msg(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState warn(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState err(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState enable()
    {
        return PullState.Continue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState disable()
    {
        return PullState.Continue;
    }
    
#endif
}