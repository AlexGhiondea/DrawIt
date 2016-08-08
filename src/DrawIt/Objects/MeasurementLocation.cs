using System;

namespace DrawIt{
    [Flags]
    public enum MeasurementLocation
    {
        Left = 1,
        Right = 2,
        Above = 4,
        Below = 8
    }
}
