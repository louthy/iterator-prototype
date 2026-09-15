using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Memory;

static class Objs<A>
    where A : class, new()
{
    static readonly ObjPool<A>[] objs;

    [MethodImpl(Optimisations.InliningOnly)]
    static Objs()
    {
        var count = Environment.ProcessorCount;
        objs = new ObjPool<A>[count];
        for(var i = 0; i < count; i++)
        {
            objs[i] = new ObjPool<A>();
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Obj<A> Alloc()
    {
        var pid = Thread.GetCurrentProcessorId();
        return objs[pid].Alloc();
    }
}
