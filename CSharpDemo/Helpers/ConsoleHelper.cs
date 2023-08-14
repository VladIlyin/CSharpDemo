namespace System
{
    public static class ConsoleHelper
    {
        public static void WriteLineCollection<T>(
            IEnumerable<T> list,
            string comment = null)
        {
            if (comment != null) Console.WriteLine($"{comment}\n");

            foreach (T item in list)
            {
                Console.WriteLine(item);
            }

            if (comment != null) Console.WriteLine($"\n");
        }

        public static void WriteLineSpan<T>(
            Span<T> span,
            string comment = null)
        {
            if (comment != null) Console.WriteLine($"{comment}\n");

            foreach (T item in span)
            {
                Console.WriteLine(item);
            }

            if (comment != null) Console.WriteLine($"\n");
        }

        public static void WriteCollection<T>(
            IEnumerable<T> list,
            string separatedWith = " ",
            string comment = null)
        {
            if (comment != null) Console.WriteLine($"{comment}");
            Console.Write($"{string.Join(separatedWith, list)}\n");
        }

        public static void WriteSpan<T>(
            Span<T> span,
            string separatedWith = ", ",
            string comment = null)
        {
            if (comment != null) Console.WriteLine($"{comment}\n");

            Console.Write($"{string.Join(separatedWith, span.ToArray())}\n");
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
