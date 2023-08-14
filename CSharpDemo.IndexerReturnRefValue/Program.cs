

var pLock = new Point();

static void UseRefLocalVariable()
{
    var arrPoint = new Point[] {
        new Point { X = 0, Y = 0 }
    };

    var p0 = arrPoint[0];
    p0.X++;

    Console.WriteLine(arrPoint[0].X); // 0

    ref Point p00 = ref arrPoint[0];
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