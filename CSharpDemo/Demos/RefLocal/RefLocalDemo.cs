using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.RefLocal
{
    public class RefLocalDemo : DemoRunner<RefLocalDemo>
    {
        [DemoCaption("Ref local: change array element with local ref")]
        public void Demo1()
        {
            var arrPoint = new Point[] {
                new() { X = 0, Y = 0 },
                new() { X = 1, Y = 1 },
                new() { X = 2, Y = 2 },
            };

            var p0 = arrPoint[0];
            p0.X++;

            Console.WriteLine(arrPoint[0].X); // 0

            ref var p00 = ref arrPoint[0];
            p00.X++;

            Console.WriteLine(arrPoint[0].X); // 1
        }

        struct Point
        {
            public int X;
            public int Y;

            public void Reset()
            {
                X = 0;
                Y = 0;
            }
        }

        [DemoCaption("Ref local: change all zero matrix elements")]
        public void Demo2()
        {
            var mx = new [,]
            {
                { 0, 1, 0 },
                { 1, 0, 1 },
                { 0, 1, 0 },
            };

            ConsoleHelper.WriteArray(mx);

            var found = true;
            while (found)
            {
                /*
                 * Чтобы вызывающий объект имел возможность изменять состояние объекта,
                 * возвращаемое ссылочное значение должно храниться в переменной,
                 * которая явно определена как ссылочная локальная переменная
                 */
                ref var num = ref Find(mx, (n) => n == 0, out found);
                num = 1;
            }

            ConsoleHelper.WriteArray(mx);


            /*
             * Возвращаемые ссылочные значения — это значения, которые метод
             * возвращает вызывающему объекту по ссылке.
             * Это значит, что вызывающий объект может изменять значение,
             * возвращаемое методом, и это изменение будет отражаться
             * в состоянии объекта в вызывающем методе.
             */
            ref int Find(int[,] matrix, Func<int, bool> predicate, out bool found)
            {
                for (var i = 0; i < matrix.GetLength(0); i++)
                {
                    for (var j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (predicate(matrix[i, j]))
                        {
                            found = true;
                            return ref matrix[i, j];
                        }
                    }
                }

                found = false;
                return ref new[] { 0 }[0];
            }
        }
    }
}
