namespace IteratorPrototype;

public static class Assert
{
    public static void True(bool condition) =>
        True(condition, "Assertion failed");
    
    public static void True(bool condition, string message)
    {
        if (!condition)
        {
            throw new Exception(message);
        }
    }
    
    public static void False(bool condition) =>
        False(condition, "Assertion failed");
    
    public static void False(bool condition, string message)
    {
        if (condition)
        {
            throw new Exception(message);
        }
    }
}