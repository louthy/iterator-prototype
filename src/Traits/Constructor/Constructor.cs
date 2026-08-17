namespace IteratorPrototype.Traits;

public interface Constructor<out SELF, ARG>
    where SELF : Constructor<SELF, ARG>
    where ARG : notnull, allows ref struct 
{
    static abstract SELF Construct(in ARG value);
}

public interface Constructor<out SELF, ARG1, ARG2>
    where SELF : Constructor<SELF, ARG1, ARG2>
    where ARG1 : notnull, allows ref struct 
    where ARG2 : notnull, allows ref struct 
{
    static abstract SELF Construct(in ARG1 value1, in ARG2 value2);
}

public interface Constructor<out SELF, ARG1, ARG2, ARG3>
    where SELF : Constructor<SELF, ARG1, ARG2, ARG3>
    where ARG1 : notnull, allows ref struct 
    where ARG2 : notnull, allows ref struct 
    where ARG3 : notnull, allows ref struct 
{
    static abstract SELF Construct(in ARG1 value1, in ARG2 value2, in ARG3 value3);
}

public interface Constructor<out SELF, ARG1, ARG2, ARG3, ARG4>
    where SELF : Constructor<SELF, ARG1, ARG2, ARG3, ARG4>
    where ARG1 : notnull, allows ref struct 
    where ARG2 : notnull, allows ref struct 
    where ARG3 : notnull, allows ref struct 
    where ARG4 : notnull, allows ref struct 
{
    static abstract SELF Construct(in ARG1 value1, in ARG2 value2, in ARG3 value3, in ARG4 value4);
}