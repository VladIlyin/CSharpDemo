using CSharpDemo.Demos.GetHashCode;
using CSharpDemo.Helpers;

namespace CSharpDemo
{
    public class GetHashCodeDemo : DemoRunner<GetHashCodeDemo>
    {
        [DemoCaption("Demo 1")]
        public void Demo1()
        {
            var point = new Point() { X = 1, Y = 1, data = new PointData() { Color = "red" } };
            var point2 = new Point() { X = 1, Y = 1, data = new PointData() { Color = "red" } };

            Console.WriteLine(point.GetHashCode());
            Console.WriteLine(point2.GetHashCode());

            point.data.Color = "green";

            Console.WriteLine(point.GetHashCode());
            Console.WriteLine(point.data.GetHashCode());


            var pointData = new PointData() { Color = "white" };
            var pointData2 = new PointData() { Color = "white" };

            Console.WriteLine(pointData.GetHashCode());
            Console.WriteLine(pointData2.GetHashCode());

            pointData.Color = "black";

            Console.WriteLine(pointData.GetHashCode());

            pointData.Color = "black";

            Console.WriteLine(pointData.GetHashCode());


            var simplePoint1 = new SimplePoint() { X = 1, Y = 1 };
            var simplePoint2 = new SimplePoint() { X = 1, Y = 1 };

            Console.WriteLine(simplePoint1.GetHashCode());
            Console.WriteLine(simplePoint2.GetHashCode());

            var ph1 = new PointHolder() { Value = new SimplePoint { X = 1, Y = 1 } };
            var ph2 = new PointHolder() { Value = new SimplePoint { X = 1, Y = 1 } };

            Console.WriteLine(ph1.GetHashCode());
            Console.WriteLine(ph2.GetHashCode());

            ph1.Value = new SimplePoint { X = 10, Y = 10 };
            Console.WriteLine(ph1.GetHashCode());

            var p1 = new Person { Name = "Vlad" };
            Console.WriteLine(p1.GetHashCode());

            var p2 = new Person { Name = "Vlad" };
            Console.WriteLine(p2.GetHashCode());

            p1.Name = "Anna";
            Console.WriteLine(p1.GetHashCode());

            var n = 1;
            Interlocked.CompareExchange(ref n, 111, 1);
            Console.WriteLine(n);

            var list = new List<int>();
            for (var i = 0; i < 2146500000; i++)
            {
                list.Add(i);
            }

            Console.WriteLine(list[2146499999]);

            foreach (var i in list)
            {
                Thread.Sleep(500);
                Console.WriteLine(i);
            }
        }

        [DemoCaption("Demo 2")]
        public void Demo2()
        {
            var t1 = new Thread(() => Console.WriteLine("Thread"), 1000);
            var t2 = new Thread(() => Console.WriteLine("Thread"), 1000);

            Console.WriteLine(t1.ManagedThreadId);
            Console.WriteLine(t1.GetHashCode());
            Console.WriteLine(t2.GetHashCode());

            PersonRef p1 = new() { Age = 30, Name = "Vlad", Email = "email@mail.com" };
            PersonRef p2 = new() { Age = 30, Name = "Vlad", Email = "email@mail.com" };

            HashSet<PersonRef> set = new();
            set.Add(p1);
            set.Add(p2);

            Console.WriteLine($"HashSet has {set.Count} items");



            Dictionary<PersonRef, int> dict = new();

            dict.TryAdd(p1, 1);
            dict.TryAdd(p2, 2); // false cause p1 equals p2

            Console.WriteLine($"Dictionary has {set.Count} items");


            // How calculating hash code in tuple

            var p11 = new PersonRef() { Age = 30, Name = "Vlad", Email = "someemail@abc" };
            var p22 = new PersonRef() { Age = 30, Name = "Vlad", Email = "someemail@abc" };
            var p33 = new PersonRef() { Age = 30, Name = "Vlad", Email = "someemail@abc" };
            var p44 = new PersonRef() { Age = 10, Name = "Mike", Email = "someemailMike@abc" };

            Console.WriteLine($"Hash codes of two person: {p11.GetHashCode()} - {p22.GetHashCode()}");
            Console.WriteLine($"Hash code of tuple: {(p11, p22).GetHashCode()}");

            HashSet<(PersonRef, PersonRef)> set11 = new();
            set11.Add((p11, p22));
            set11.Add((p11, p22));
            set11.Add((p11, p33));
            set11.Add((p33, p11));

            set11.Add((p44, p11));
            set11.Add((p11, p44));

            Console.WriteLine((p11, p33).GetHashCode());
            Console.WriteLine((p33, p11).GetHashCode());

            Console.WriteLine((p11, p44).GetHashCode());
            Console.WriteLine((p44, p11).GetHashCode());

            Console.WriteLine(set11.Count);

            HashSet<(int, int, int)> set111 = new();
            set111.Add((1, 2, 3));
            set111.Add((3, 1, 2));
            set111.Add((2, 3, 1));

            Console.WriteLine(set111.Count);

            Console.WriteLine(dict.TryGetValue(p1, out var value1) ? value1 : null);
            Console.WriteLine(dict.TryGetValue(p2, out var value2) ? value2 : null);

            PersonValue pv1 = new() { Age = 30, Name = "Vlad", Person = p1 };
            PersonValue pv2 = new() { Age = 30, Name = "Vlad", Person = p2 };

            Console.WriteLine(pv1.Equals(pv2));

            pv1.Age = 31;

            Console.WriteLine($"{pv1.GetHashCode()} {pv2.GetHashCode()}");


            HashSet<PersonValue> setStruct = new();
            setStruct.Add(pv1);
            setStruct.Add(pv2);

            Dictionary<PersonValue, int> dictStruct = new();

            dictStruct.Add(pv1, 1);
            dictStruct.Add(pv2, 2);

            Console.WriteLine(dictStruct.Count());
            //Console.WriteLine(dictStruct.TryGetValue(ps1, out int value1) ? value1 : null);
            //Console.WriteLine(dictStruct.TryGetValue(ps2, out int value2) ? value2 : null);
        }

        [DemoCaption("GetHashCode demo: Tuple")]
        public void Demo3()
        {
            var tuple1 = (0, 1);
            var tuple2 = (0, 1);

            // Identical hash code for both tuples
            Console.WriteLine(tuple1.GetHashCode());
            Console.WriteLine(tuple2.GetHashCode());

            Dictionary<(int, int), string> dictTupleKey = new();

            dictTupleKey.TryAdd((0, 1), "01");
            dictTupleKey.TryAdd((0, 1), "01");

            Console.WriteLine(dictTupleKey.Count); // 1
        }

        struct Point
        {
            public PointData data;
            public int X;
            public int Y;
        }

        struct SimplePoint
        {
            public int X;
            public int Y;
        }

        class PointData
        {
            public string Color;
        }

        class PointHolder
        {
            public SimplePoint Value;
        }

        class Person
        {
            public string Name;
        }
    }
}
