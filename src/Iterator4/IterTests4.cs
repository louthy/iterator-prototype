using System.Diagnostics;
using System.Runtime.CompilerServices;
using IteratorPrototype.Memory;

namespace IteratorPrototype.Iterator4;

public class IterTests4
{
    public static void Tests()
    {
        SizeOfTest();
        TreeListTest1();
        TreeListTest2();
        TreeListTest3();
        TreeListTest4();
        BlockAlloc();
        FillBlock();
        TestSeq();
    }
    
    public static void SizeOfTest()
    {
        var sizeOfBlockRef = Unsafe.SizeOf<PageRef<int>>();
        Console.WriteLine($"Size of BlockRef<int>: {sizeOfBlockRef} bytes");
        Debug.Assert(sizeOfBlockRef <= 8);
        
        var sizeOfBlockList = Unsafe.SizeOf<PageRef<int>>();
        Console.WriteLine($"Size of BlockList<int>: {sizeOfBlockList} bytes");
        Debug.Assert(sizeOfBlockList <= 16);
        
        var sizeOfBlockLists = Unsafe.SizeOf<PageRef<int>>();
        Console.WriteLine($"Size of BlockLists<int>: {sizeOfBlockLists} bytes");
        Debug.Assert(sizeOfBlockLists <= 24);
        
        var sizeOfSq = Unsafe.SizeOf<Sq<int>>();
        Console.WriteLine($"Size of Sq<int>: {sizeOfSq} bytes");
        Debug.Assert(sizeOfSq <= 64);
    }
    
    public static void TestSeq()
    {
        Sq<int> seq = default;

        for (var i = 0; i < 1000000; i++)
        {
            seq = seq.Add(i);
        }

        for (var i = 0; i < 1000000; i++)
        {
            Debug.Assert(seq[i] == i);
        }
        
        //Console.WriteLine(seq);
    }

    public static void BlockAlloc()
    {
        using var blist = PageList<int>.Alloc();
        
        blist.Add(100, out var blist1);
        blist1.Add(200, out var blist2);
        blist2.Add(300, out var blist3);
        blist3.Add(400, out var blist4);
        blist4.Add(500, out var blist5);
        
        Console.WriteLine(blist5);
    }
    
    public static void FillBlock()
    {
        var initial = PageList<int>.Alloc();
        var blist   = initial;
        var first   = initial;

        for (var i = 0; i < 2000; i++)
        {
            var current = blist;
            if (!blist.Add(i, out blist))
            {
                first = current;
                Console.WriteLine($"Full at {i}");
            }
        }
        
        //Console.WriteLine(initial);
        //Console.WriteLine(first);
        //Console.WriteLine(blist);
        
        initial.Dispose();
        blist.Dispose();
    }

    public static void TreeListTest1()
    {
        var list = TreeList<int>.Empty;
        
        var list0 = list.Add(100);
        var list1 = list0.Add(200);
        var list2 = list1.Add(300);
        var list3 = list2.Add(400);
        var list4 = list3.Add(500);
        var list5 = list4.Add(600);
        var list6 = list5.Add(700);
        var list7 = list6.Add(800);
        var list8 = list7.Add(900);
        var list9 = list8.Add(1000);

        Console.WriteLine(list9);
    }

    public static void TreeListTest2()
    {
        var initial = TreeList<int>.Empty;
        var blist   = initial;

        for (var i = 0; i < 2000; i++)
        {
            blist = blist.Add(i);
        }
        
        //Console.WriteLine(initial);
        //Console.WriteLine(blist);
        
        initial.Dispose();
        blist.Dispose();
    }

    public static void TreeListTest3()
    {
        var list = TreeList<int>.Empty;

        for (var i = 0; i < 1000000; i++)
        {
            list = list.Add(i);
        }

        for (var i = 0; i < 1000000; i++)
        {
            Debug.Assert(list[i] == i);
        }
        
        //Console.WriteLine(list);
    }

    public static void TreeListTest4()
    {
    }

}