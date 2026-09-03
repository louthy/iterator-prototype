using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal;

static class Log
{
    public static string ty<A>() => 
        typeof(A).Name;
    
#if DEBUG

    static bool enabled = true;
    internal static string indent = "";

    public static int scope()
    {
        indent += "  ";
        return PullState.Continue;
    }

    public static int descope()
    {
        
        indent = indent.Length > 1 ? indent[..^2] : indent;
        return PullState.Continue;
    }
    
    public static int enable()
    {
        enabled = true;
        return PullState.Continue;
    }
    
    public static int disable()
    {
        enabled = false;
        return PullState.Continue;
    }

    public static void stackInfo(ref StackFrame frame) =>
        Console.Write(frame.ToString());

    public static void stackLine(ref StackFrame frame) =>
        Console.WriteLine(frame.ToString());

    static int write(string msg, ConsoleColor colour)
    {
        if (!enabled) return PullState.Continue;
        var c = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(indent);
        Console.Write(msg);
        Console.ForegroundColor = c;
        Console.WriteLine();
        return PullState.Continue;
    }

    static int write(string msg, ConsoleColor colour, ref StackFrame frame)
    {
        if (!enabled) return PullState.Continue;
        var c = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(indent);
        Console.Write(msg);
        Console.Write(' ');
        Console.ForegroundColor = ConsoleColor.Gray;
        stackInfo(ref frame);
        Console.ForegroundColor = c;
        Console.WriteLine();
        return PullState.Continue;
    }

    public static int function(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Green, ref frame);

    public static int function(string message) =>
        write(message, ConsoleColor.Green);

    public static int coroutine(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Cyan, ref frame);

    public static int coroutine(string message) =>
        write(message, ConsoleColor.Cyan);

    public static int value(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Magenta, ref frame);

    public static int value(string message) =>
        write(message, ConsoleColor.Magenta);

    public static int terminator(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Blue, ref frame);

    public static int terminator(string message) =>
        write(message, ConsoleColor.Blue);

    public static int msg(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.White, ref frame);

    public static int msg(string message) =>
        write(message, ConsoleColor.White);

    public static int warn(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Yellow, ref frame);

    public static int warn(string message) =>
        write(message, ConsoleColor.Yellow);

    public static int err(string message, ref StackFrame frame) =>
        write(message, ConsoleColor.Red, ref frame);

    public static int err(string message) =>
        write(message, ConsoleColor.Red);

#else


    [MethodImpl(Optimisations.Default)]
    public static void scope()
    {
    }
            
    [MethodImpl(Optimisations.Default)]
    public static void descope()
    { 
    }
    
    [MethodImpl(Optimisations.Default)]
    public static string stackInfo(ref StackFrame frame)
    {
        return default!;
    }
    
    [MethodImpl(Optimisations.Default)]
    public static string stackInfo()
    {
        return default!;
    }

    [MethodImpl(Optimisations.Default)]
    static int write(string msg, ConsoleColor colour, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    static int write(string msg, ConsoleColor colour)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int function(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int function(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int coroutine(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int coroutine(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int value(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int value(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int terminator(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int terminator(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int msg(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int msg(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int warn(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int warn(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int err(string message, ref StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int err(string message)
    {
        return PullState.Continue;
    }
    
    [MethodImpl(Optimisations.Default)]
    public static int enable()
    {
        return PullState.Continue;
    }
    
    [MethodImpl(Optimisations.Default)]
    public static int disable()
    {
        return PullState.Continue;
    }
    
#endif
}