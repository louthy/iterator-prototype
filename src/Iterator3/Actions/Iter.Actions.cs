#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly struct IterEmpty;

[SkipLocalsInit]
public readonly struct IterPure;

[SkipLocalsInit]
public readonly struct IterScope;

[SkipLocalsInit]
public readonly record struct IterTake(int amount);

[SkipLocalsInit]
public readonly record struct IterSkip(int amount);

[SkipLocalsInit]
public readonly record struct IterDistinct;

[SkipLocalsInit]public readonly record struct IterIndex;