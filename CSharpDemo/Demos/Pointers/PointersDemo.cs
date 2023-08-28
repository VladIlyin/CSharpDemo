using System.Runtime.InteropServices;
using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Pointers;

public class PointersDemo : DemoRunner<PointersDemo>
{
    [DemoCaption("Change array via pointer")]
    public void Demo1()
    {
        var arr = new Byte[10];
        Array.Fill<byte>(arr, 1);

        ConsoleHelper.WriteCollection(arr, comment: "Initial array:");

        unsafe
        {
            fixed (byte* p = arr)
            {
                for (var i = 0; i < 10; i++) 
                {
                    p[i]++;
                }
            }
        }

        ConsoleHelper.WriteCollection(arr, comment: "Changed array:");
    }

    [DemoCaption("Address-of operator &")]
    public void Demo2()
    {
        unsafe
        {
            var number = 27;
            var pointerToNumber = &number;

            Console.WriteLine($"Value of the variable: {number}");
            Console.WriteLine($"Address of the variable: {(long)pointerToNumber:X}");
        }
    }
}