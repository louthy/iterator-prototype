using System.Runtime.CompilerServices;
using LanguageExt;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

/// <summary>
/// Apply this to an instance-type of an `IterableK`
/// </summary>
public interface IterableMutable<T, IS, MS> : IterableImmutable<T, IS>
    where T : IterableMutable<T, IS, MS>
    where IS : struct
    where MS : allows ref struct
{
    static abstract MS SetupMutable<A>(K<T, A> ta);
    static abstract bool StepMutable<A>(K<T, A> ta, ref MS ts, out A value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static IEnumerable<A> Iterable<T>.AsEnumerable<A>(K<T, A> ta) =>
        new IterableMutableEnumerable<T, IS, MS, A>(ta);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Unit Iterable<T>.ToWriter<A>(K<T, A> ta, ref ArrayWriter<A> writer)
    {
        var ts = T.SetupMutable(ta);
        while (T.StepMutable(ta, ref ts, out var x))
        {
            ArrayWriter<A>.Add(ref writer, x);
        }

        return default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Unit Iterable<T>.ToWriter<A, B>(K<T, A> ta, Func<A, B> f, ref ArrayWriter<B> writer)
    {
        var ts = T.SetupMutable(ta);
        while (T.StepMutable(ta, ref ts, out var x))
        {
            ArrayWriter<B>.Add(ref writer, f(x));
        }
        return default;
    }    

    static string Iterable<T>.ToString<A>(K<T, A> ta, string separator)
    {
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.SetupMutable(ta);
        var ix = 0;

        while (T.StepMutable(ta, ref ts, out var x))
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
            sm.Undo(separator.Length); // Remove the last separator
        }

        return sm.ToString();
    }

    static string Iterable<T>.ToArrayString<A>(K<T, A> ta, string separator)
    {
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.SetupMutable(ta);
        var ix = 0;

        sm.Append('[');
        while (T.StepMutable(ta, ref ts, out var x))
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
        var ts = T.SetupMutable(ta);

        while (T.StepMutable(ta, ref ts, out var x))
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
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.SetupMutable(ta);

        sm.Append('[');
        while (T.StepMutable(ta, ref ts, out var x))
        {
            sm.Append(x);
            sm.Append(separator);
        }

        sm.Append(']');

        if (sm.Length > 0)
        {
            sm.Undo(separator.Length); // Remove the last separator
        }

        return sm.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static K<T, A> Iterable<T>.Do<A>(K<T, A> ta, Action<A> f)
    {
        var ts = T.SetupMutable(ta);
        while (T.StepMutable(ta, ref ts, out var x))
        {
            f(x);
        }
        return ta;
    }
    
}