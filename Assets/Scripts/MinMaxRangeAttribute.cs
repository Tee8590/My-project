using System;

internal class MinMaxRangeAttribute : Attribute
{
    public int Min { get; }
    public int Max { get; }

    public MinMaxRangeAttribute(int min, int max)
    {
        Min = min;
        Max = max;
    }

}