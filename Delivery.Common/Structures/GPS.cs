namespace Delivery.Common.Structures;

public struct GPS
{
    public double North;
    public double East;

    public GPS(double north, double east)
    {
        North = north;
        East = east;
    }
}
