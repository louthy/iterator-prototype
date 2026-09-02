using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
readonly struct Fields
{
    public readonly Tops tops;
    public readonly Ops ops;
    public readonly Globals globals;
    public readonly Vars vars;
}