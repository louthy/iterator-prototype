namespace IteratorPrototype;

public ref struct IteratorStack
{
    public IteratorStack(ref object ta, ref IteratorAction self, ref Space128 space)
    {
        this.ta = ref ta;
        this.self = ref self;
        this.space = ref space;
    }
    
    public ref object ta;
    public ref IteratorAction self;
    public ref Space128 space;
}