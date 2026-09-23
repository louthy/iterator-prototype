using System.Runtime.CompilerServices;
using IteratorPrototype.Types;

namespace IteratorPrototype.Iterator3.Internal;

static class Log
{
    public static string ty<A>() => 
        Ty<A>.Pretty;
    
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

    public static void stackInfo(in StackFrame frame) =>
        Console.Write(frame.ToString());

    public static void stackLine(in StackFrame frame) =>
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

    static int write(string msg, ConsoleColor colour, in StackFrame frame)
    {
        if (!enabled) return PullState.Continue;
        var c = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(indent);
        Console.Write(msg);
        Console.Write(' ');
        Console.ForegroundColor = ConsoleColor.Gray;
        stackInfo(in frame);
        Console.ForegroundColor = c;
        Console.WriteLine();
        return PullState.Continue;
    }

    public static void stack(in StackFrame frame) =>
        write("", ConsoleColor.DarkBlue, in frame);

    public static int function(string message, in StackFrame frame) =>
        write(message, ConsoleColor.Green, in frame);

    public static int function(string message) =>
        write(message, ConsoleColor.Green);

    public static int coroutine(string message, in StackFrame frame) =>
        write(message, ConsoleColor.Cyan, in frame);

    public static int coroutine(string message) =>
        write(message, ConsoleColor.Cyan);

    public static int value(string message, in StackFrame frame) =>
        write(message, ConsoleColor.Magenta, in frame);

    public static int value(string message) =>
        write(message, ConsoleColor.Magenta);

    public static int terminator(string message, in StackFrame frame) =>
        write(message, ConsoleColor.Blue, in frame);

    public static int terminator(string message) =>
        write(message, ConsoleColor.Blue);

    public static int msg(string message, in StackFrame frame) =>
        write(message, ConsoleColor.White, in frame);

    public static int msg(string message) =>
        write(message, ConsoleColor.White);

    public static int warn(string message, in StackFrame frame) =>
        write(message, ConsoleColor.Yellow, in frame);

    public static int warn(string message) =>
        write(message, ConsoleColor.Yellow);

    public static int err(string message, in StackFrame frame) =>
        write(message, ConsoleColor.Red, in frame);

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
    public static string stackInfo(in StackFrame frame)
    {
        return default!;
    }
    
    [MethodImpl(Optimisations.Default)]
    public static string stackInfo()
    {
        return default!;
    }

    [MethodImpl(Optimisations.Default)]
    static int write(string msg, ConsoleColor colour, in StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    static int write(string msg, ConsoleColor colour)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int function(string message, in StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int function(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int coroutine(string message, in StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int coroutine(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int value(string message, in StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int value(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int terminator(string message, in StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int terminator(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int msg(string message, in StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int msg(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int warn(string message, in StackFrame frame)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int warn(string message)
    {
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.Default)]
    public static int err(string message, in StackFrame frame)
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