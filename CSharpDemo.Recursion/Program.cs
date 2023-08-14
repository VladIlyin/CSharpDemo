
var vh = new ValueHolder();
Console.WriteLine(ReturnIfOver1000(vh).Value);

ValueHolder ReturnIfOver1000(ValueHolder valueHolder)
{
    valueHolder.Value++;

    if (valueHolder.Value >= 1000) return valueHolder;

    return ReturnIfOver1000(valueHolder);
}

public class ValueHolder
{
    public int Value;
}
