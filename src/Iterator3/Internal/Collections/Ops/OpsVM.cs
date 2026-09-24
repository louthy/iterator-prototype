#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using IteratorPrototype.Types;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
static class OpsVM<A>
{
    static OpsVM()
    {
        switch (Ty<A>.Flavour)
        {
            case TyFlavour.Unmanaged:
            {
                var type = typeof(OpsVMUnmanaged<>).MakeGenericType(typeof(A));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            case TyFlavour.Managed:
            {
                var type = typeof(OpsVMManaged<>).MakeGenericType(typeof(A));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            case TyFlavour.Struct:
            {
                var type = typeof(OpsVMStruct<>).MakeGenericType(typeof(A));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            default:
                throw new Exception("We have a type that apparently isn't managed, unmanaged, or a value-type!");
        }
    }
    
    internal static unsafe delegate*<in StackFrame, out A, bool> run;
    
    public static bool Run(in StackFrame frame, out A head)
    {
        unsafe
        {
            return run(frame, out head);
        }
    }
}
