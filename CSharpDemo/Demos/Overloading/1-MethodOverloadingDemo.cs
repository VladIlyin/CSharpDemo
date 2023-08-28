using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Overloading;

public partial class OverloadingDemo : DemoRunner<OverloadingDemo>
{
    public class Message { }

    public class MessageProcessor
    {
        public void Process(Message order) { Console.WriteLine("Process(Message message) call"); }
        public void Process(object order) { Console.WriteLine("Process(object message) call"); }
    }

    [DemoCaption("Method overloading")]
    public void Demo1()
    {
        MessageProcessor processor = new();

        processor.Process(new Message());        
        processor.Process(new Message() as object);
    }
}