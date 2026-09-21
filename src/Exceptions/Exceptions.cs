using IteratorPrototype.Types;

namespace IteratorPrototype;

public class CollectionIsFullException<CollectionType>() : Exception($"{Ty<CollectionType>.Pretty} is full")
{
}