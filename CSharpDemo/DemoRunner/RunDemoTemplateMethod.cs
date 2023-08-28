namespace CSharpDemo.DemoRunner;

internal class RunDemoTemplateMethod
{
    public static void YesNoLoop(Action action)
    {
        var quit = 'y';
        while (quit == 'y')
        {
            action();

            Console.WriteLine("\nContinue? (y/n)\n");
            quit = Console.ReadKey(true).KeyChar;
        }
    }
}