using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public ref struct StringMaker(Span<char> buffer)
{
    Span<char> buffer = buffer;
    int pos = 0;

    public int Length
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => pos; 
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Append(char c)
    {
        if (pos >= buffer.Length) MoveToHeap(buffer.Length + 1);
        buffer[pos++] = c;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Append(string str)
    {
        if(pos + str.Length > buffer.Length) MoveToHeap(pos + str.Length);
        foreach (var c in str)
        {
            buffer[pos++] = c;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Append<A>(A? item) =>
        Append(item?.ToString() ?? "[null]");

    [MethodImpl(Optimisations.InliningOnly)]
    public void Undo(int count) =>
        pos = Math.Max(0, pos - count);
    
    [MethodImpl(Optimisations.InliningOnly)]
    void MoveToHeap(int needed)
    {
        var newSize   = PowerOf2(needed);
        if (newSize - needed < 512) newSize <<= 1;
        var newBuffer = new char[newSize];
        buffer.CopyTo(newBuffer);
        buffer = newBuffer;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        new (buffer[..pos]);
    
    static long PowerOf2(long size)
    {
        size--;
        size |= size >> 1;
        size |= size >> 2;
        size |= size >> 4;
        size |= size >> 8;
        size |= size >> 16;
        size |= size >> 32;
        size++;
        return size;
    }
}