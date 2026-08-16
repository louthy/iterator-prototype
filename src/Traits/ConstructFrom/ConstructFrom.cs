namespace IteratorPrototype.Traits;

public interface ConstructFrom<out SELF, ARG>
    where SELF : ConstructFrom<SELF, ARG>
    where ARG : notnull, allows ref struct 
{
    static abstract SELF Construct(in ARG value);
}