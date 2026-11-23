#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly record struct IterBind<A, B>(Func<A, Iter<B>> f);
