#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly record struct IterMap<A, B>(Func<A, B> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C>(Func<A, B, C> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D>(Func<A, B, C, D> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E>(Func<A, B, C, D, E> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f);
