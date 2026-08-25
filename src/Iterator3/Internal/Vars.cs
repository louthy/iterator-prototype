using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly struct Vars
{
    public readonly ObjStack objs;
    public readonly ByteStack values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Vars(in ObjStack objs, in ByteStack values)
    {
        this.objs = objs;
        this.values = values;
    }
}
