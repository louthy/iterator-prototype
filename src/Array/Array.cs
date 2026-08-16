using LanguageExt.Traits;

namespace IteratorPrototype;

public record Array<A>(A[] Items) : K<Array, A>;
