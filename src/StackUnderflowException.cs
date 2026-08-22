namespace IteratorPrototype;

public class StackUnderflowException : Exception
{
    public StackUnderflowException() : 
        base("Stack underflow") { }
    
    public StackUnderflowException(string message) : 
        base(message) { }
}