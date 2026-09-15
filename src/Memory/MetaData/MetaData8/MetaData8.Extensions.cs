using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

public static partial class MetaDataExtensions
{
    extension(ref MetaData8 metaData)
    {
        /// <summary>
        /// Cast the metadata to another unmanaged
        /// </summary>
        /// <typeparam name="A">Type to cast to</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if the target type is larger than 8 bytes</exception>
        public ref A As<A>()
            where A : unmanaged
        {
            if (Unsafe.SizeOf<A>() <= 8)
            {
                return ref Unsafe.As<MetaData8, A>(ref metaData);
            }
            else
            {
                throw new InvalidOperationException("Cannot cast to a type larger than 8 bytes");
            }
        }
    }
}