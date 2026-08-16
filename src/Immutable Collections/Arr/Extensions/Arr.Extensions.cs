using System.Diagnostics.Contracts;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class ArrExtensions
{
    extension<A>(K<Arr, Arr<A>> mma)
    {
        /// <summary>
        /// Monadic join
        /// </summary>
        [Pure]
        public Arr<A> Flatten()
        {
            var w  = LE.ArrayWriter<A>.Init();
            var ts = mma.SetupMutable<Arr, ArrState, ArrStateRef, Arr<A>>();
            while (mma.StepMutable<Arr, ArrState, ArrStateRef, Arr<A>>(ref ts, out var ma))
            {
                w.AddRange(ma.AsSpan());
            }
            return w.ToArr();
        }
    }

    /*
     TODO
     
    extension<OrdA, A>(K<Arr, A> ma)
        where OrdA : Ord<A>
    {
        /// <summary>
        /// Provide a sorted Arr
        /// </summary>
        [Pure]
        public Arr<A> Sort()  =>
            ma.As().OrderBy(x => x, LE.OrdComparer<OrdA, A>.Default).ToArr();
        
        [Pure]
        public Arr<A> Filter(Func<A, bool> f)
        {
            var writer = LE.ArrayWriter<A>.Init(ma.Count);
        
            var state = ma.StepSetup<Arr, Arr.FoldState, A>();
            while (ma.Step(ref state, out var a))
            {
                if(f(a)) writer.Add(a);
            }
            return writer.ToArr();
        }

        [Pure]
        public Arr<B> Map<B>(Func<A, B> f) 
        {
            var writer = LE.ArrayWriter<B>.Init(ma.Count);
        
            var state = ma.StepSetup<Arr, Arr.FoldState, A>();
            while (ma.Step(ref state, out var a))
            {
                writer.Add(f(a));
            }
            return writer.ToArr();
        }

        [Pure]
        public Arr<B> Bind<B>(Func<A, Arr<B>> f)
        {
            var writer = ArrayWriterRef<B>.Init();
        
            var astate = ma.StepSetup<Arr, Arr.FoldState, A>();
            while (ma.Step(ref astate, out var a))
            {
                var mb     = +f(a);
                var bstate = mb.StepSetup<Arr, Arr.FoldState, B>();
                while (mb.Step(ref bstate, out var b))
                {
                    writer.Add(b);
                }
            }
            return writer.ToArr();
        }
    }*/    
}
