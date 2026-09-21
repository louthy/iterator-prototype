namespace IteratorPrototype.Types;

public class TyPretty
{
    public static string Make(Type type) =>
        (type.Namespace, type.Name) switch
        {
            ("System", "Char")                                   => "char",
            ("System", "Boolean")                                => "bool",
            ("System", "Byte")                                   => "byte",
            ("System", "SByte")                                  => "sbyte",
            ("System", ".Int16")                                 => "short",
            ("System", "UInt16")                                 => "ushort",
            ("System", "Int32")                                  => "int",
            ("System", "UInt32")                                 => "uint",
            ("System", "Int64")                                  => "long",
            ("System", "UInt64")                                 => "ulong",
            ("System", "IntPtr")                                 => "nint",
            ("System", "UIntPtr")                                => "nuint",
            ("System", "String")                                 => "string",
            ("System", "Object")                                 => "object",
            ("LanguageExt", "Unit")                              => "unit",
            ("System", "Float")                                  => "float",
            ("System", "Double")                                 => "double",
            ("System", "IO.Console")                             => "console",
            ("System", "ConsoleColor")                           => "colour",
            ("System", "Decimal")                                => "decimal",
            ("System", "Guid")                                   => "guid",
            var (_, n) when type.GenericTypeArguments.Length > 0 => MakeGeneric(n, type.GenericTypeArguments),
            var (_, n)                                           => MakePretty(n)
        };

    static string MakePretty(string name) =>
        name;
    
    static string MakeGeneric(string name, ReadOnlySpan<Type> args) =>
        MakeGenericPretty(name) switch
        {
            "Func" => MakeFunc(args),
            var n  => $"{n}<{MakeCommas(args)}>"
        };

    static string MakeGenericPretty(string name) =>
        name.LastIndexOf('`') switch
        {
            < 0   => name,
            var n => name[..n]
        };

    static string MakeCommas(ReadOnlySpan<Type> args) =>
        args switch
        {
            []                      => "",
            [var head]              => Make(head),
            [var head, .. var tail] => $"{Make(head)}, {MakeCommas(tail)}"
        };

    static string MakeFunc(ReadOnlySpan<Type> args) =>
        args switch
        {
            []                      => "",
            [var head]              => Make(head),
            [var head, .. var tail] => $"{Make(head)} -> {MakeFunc(tail)}"
        };
}