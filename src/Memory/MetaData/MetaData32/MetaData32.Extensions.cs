using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

public static partial class MetaDataExtensions
{
    extension(ref MetaData32 metaData)
    {
        /// <summary>
        /// Cast the metadata to another unmanaged
        /// </summary>
        /// <typeparam name="A">Type to cast to</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if the target type is larger than 32 bytes</exception>
        [MethodImpl(Optimisations.InliningOnly)]
        public ref A As<A>()
            where A : unmanaged
        {
            if (Unsafe.SizeOf<A>() <= 32)
            {
                return ref Unsafe.As<MetaData32, A>(ref metaData);
            }
            else
            {
                throw new InvalidOperationException("Cannot cast to a type larger than 32 bytes");
            }
        }
    }
}