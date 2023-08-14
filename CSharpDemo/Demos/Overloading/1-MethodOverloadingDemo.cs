using CSharpDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.Demos.Overloading
{
    public class MethodOverloadingDemo : DemoRunner<MethodOverloadingDemo>
    {
        public class Order { }

        public class OrderProcessor
        {
            public void Process(Order order) { Console.WriteLine("Process(Order order) call"); }
            public void Process(object order) { Console.WriteLine("Process(object order) call"); }
        }

        [DemoCaption("Method overloading")]
        public void Demo1()
        {
            OrderProcessor processor = new();

            processor.Process(new Order());             // call Process(Order order) overloading
            processor.Process(new Order() as object);   // call Process(object order) overloading
        }
    }
}
