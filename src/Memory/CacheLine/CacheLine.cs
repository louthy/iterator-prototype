// MIT License
// 
// Copyright (c) 2017 Nick Strupat
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

/// <summary>
/// Simple cache line size utility.
/// </summary>
/// <remarks>
/// Thank you to Nick Strupat for the original implementation.
/// Which can be found here: <see cref="https://github.com/NickStrupat/CacheLineSize.NET"/>
/// </remarks>
public static partial class CacheLine
{
    /// <summary>
    /// Size of a cache line.
    /// </summary>
    public static readonly int Size = GetCacheLineSize();

    static Int32 GetCacheLineSize()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return Windows.GetSize();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return Linux.GetSize();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return OSX.GetSize();
        throw new Exception("Unrecognized OS platform.");
    }
}