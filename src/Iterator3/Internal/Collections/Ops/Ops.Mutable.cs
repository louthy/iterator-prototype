// ReSharper disable UnassignedReadonlyField
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
struct OpsMutable
{
    public short VarBytes;
    public short MaxVarBytes;
    public byte VarObjs;
    public byte MaxVarObjs;
    public byte Frames;
    public byte BlockSize;
    public bool IsRunnable;
    public int Count;
    
    public Op Fun00, Fun01, Fun02, Fun03, Fun04, Fun05, Fun06, Fun07,
              Fun08, Fun09, Fun0A, Fun0B, Fun0C, Fun0D, Fun0E, Fun0F,
              Fun10, Fun11, Fun12, Fun13, Fun14, Fun15, Fun16, Fun17,
              Fun18, Fun19, Fun1A, Fun1B, Fun1C, Fun1D, Fun1E, Fun1F; 
    
    public byte Blk00, Blk01, Blk02, Blk03, Blk04, Blk05, Blk06, Blk07, 
                Blk08, Blk09, Blk0A, Blk0B, Blk0C, Blk0D, Blk0E, Blk0F,
                Blk10, Blk11, Blk12, Blk13, Blk14, Blk15, Blk16, Blk17, 
                Blk18, Blk19, Blk1A, Blk1B, Blk1C, Blk1D, Blk1E, Blk1F;
}
