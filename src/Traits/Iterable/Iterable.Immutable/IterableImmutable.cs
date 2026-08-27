using System.Runtime.CompilerServices;
using LanguageExt;
using LanguageExt.Traits;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Traits;

/// <summary>
/// Apply this to an instance-type of an `IterableK`
/// </summary>
public interface IterableImmutable<T, IS> : Iterable<T>
    where IS : unmanaged
    where T : IterableImmutable<T, IS>
{
    static abstract IS SetupImmutable<A>(in K<T, A> ta); 
    static abstract bool StepImmutable<A>(in K<T, A> ta, in IS state, out A head, out IS tail);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static IEnumerable<A> Iterable<T>.AsEnumerable<A>(K<T, A> ta) =>
        new IterableImmutableEnumerable<T, IS, A>(ta);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Iterator<A> Iterable<T>.Forward<A>(K<T, A> ta) =>
        Iterator.fromWeak<T, IS, A>(in ta);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Unit Iterable<T>.ToWriter<A>(K<T, A> ta, ref ArrayWriter<A> writer)
    {
        var ts = T.SetupImmutable(ta);
        while (T.StepImmutable(ta, in ts, out var x, out ts))
        {
            ArrayWriter<A>.Add(ref writer, x);
        }
        return default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Unit Iterable<T>.ToWriter<A, B>(K<T, A> ta, Func<A, B> f, ref ArrayWriter<B> writer)
    {
        var ts = T.SetupImmutable(ta);
        while (T.StepImmutable(ta, in ts, out var x, out ts))
        {
            ArrayWriter<B>.Add(ref writer, f(x));
        }
        return default;
    }
    
    static string Iterable<T>.ToString<A>(K<T, A> ta, string separator)
    {
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.SetupImmutable(ta);
        var ix = 0;

        while (T.StepImmutable(ta, in ts, out var x, out ts))
        {
            sm.Append(x);
            ix++;

            if (ix == 50)
            {
                sm.Append("...  ");
                break;
            }
            else
            {
                sm.Append(separator);
            }
        }

        if (sm.Length > 0)
        {
            sm.Undo(2); // Remove the last separator
        }

        return sm.ToString();
    }
    
    static string Iterable<T>.ToArrayString<A>(K<T, A> ta, string separator)
    {
        var sm   = new StringMaker(stackalloc char[1024]);
        var ts   = T.SetupImmutable(ta);
        var ix   = 0;

        sm.Append('[');
        while (T.StepImmutable(ta, in ts, out var x, out ts))
        {
            sm.Append(x);
            ix++;

            if (ix == 50)
            {
                sm.Append("...  ");
                break;
            }
            else
            {
                sm.Append(separator);
            }
        }

        if (sm.Length > 1)
        {
            sm.Undo(separator.Length); // Remove the last separator
        }

        sm.Append(']');

        return sm.ToString();
    }

    static string Iterable<T>.ToFullString<A>(K<T, A> ta, string separator)
    {
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.SetupImmutable(ta);

        while (T.StepImmutable(ta, in ts, out var x, out ts))
        {
            sm.Append(x);
            sm.Append(separator);
        }
        if (sm.Length > 0)
        {
            sm.Undo(separator.Length); // Remove the last separator
        }

        return sm.ToString();
    }

    static string Iterable<T>.ToFullArrayString<A>(K<T, A> ta, string separator)
    {
        var sm   = new StringMaker(stackalloc char[1024]);
        var ts   = T.SetupImmutable(ta);

        sm.Append('[');
        while (T.StepImmutable(ta, in ts, out var x, out ts))
        {
            sm.Append(x);
            sm.Append(separator);
        }
        if (sm.Length > 1)
        {
            sm.Undo(separator.Length); // Remove the last separator
        }
        sm.Append(']');

        return sm.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static K<T, A> Iterable<T>.Do<A>(K<T, A> ta, Action<A> f)
    {
        var ts = T.SetupImmutable(ta);
        while (T.StepImmutable(ta, in ts, out var x, out ts))
        {
            f(x);
        }
        return ta;
    }
}