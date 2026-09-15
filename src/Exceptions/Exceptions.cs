namespace IteratorPrototype;

public class CollectionIsFullException<CollectionType>() : Exception($"{typeof(CollectionType).Name} is full")
{
}