using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public ref struct StringMaker(Span<char> buffer)
{
    Span<char> buffer = buffer;
    int pos = 0;

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => pos; 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char c)
    {
        if (pos >= buffer.Length) MoveToHeap();
        buffer[pos++] = c;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string str)
    {
        if(str.Length + pos > buffer.Length) MoveToHeap();
        foreach (var c in str)
        {
            buffer[pos++] = c;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append<A>(A? item) =>
        Append(item?.ToString() ?? "[null]");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Undo(int count) =>
        pos = Math.Max(0, pos - count);
    
    void MoveToHeap()
    {
        var newBuffer = new char[buffer.Length * 2];
        buffer.CopyTo(newBuffer);
        buffer = newBuffer;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() =>
        new (buffer[..pos]);
}