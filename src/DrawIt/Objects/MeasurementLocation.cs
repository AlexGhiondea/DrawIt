using System;

namespace DrawIt{
    [Flags]
    public enum MeasurementLocation
    {
        Left = 1, // Not used
        Right = 2, // Not used
        Above = 4,
        Below = 8
    }
}
