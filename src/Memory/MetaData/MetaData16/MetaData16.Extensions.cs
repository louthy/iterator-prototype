using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

public static partial class MetaDataExtensions
{
    extension(ref MetaData16 metaData)
    {
        /// <summary>
        /// Cast the metadata to another unmanaged
        /// </summary>
        /// <typeparam name="A">Type to cast to</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if the target type is larger than 16 bytes</exception>
        public ref A As<A>()
            where A : unmanaged
        {
            if (Unsafe.SizeOf<A>() <= 16)
            {
                return ref Unsafe.As<MetaData16, A>(ref metaData);
            }
            else
            {
                throw new InvalidOperationException("Cannot cast to a type larger than 16 bytes");
            }
        }
    }
}