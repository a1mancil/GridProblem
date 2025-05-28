/// <summary>
/// The <c>Utils</c> class provides utility methods for working with 
/// numerical values and coordinate sets. It includes functionality 
/// to map a given value to the closest value in a set of coordinates, 
/// which can be useful in grid-based or spatial computations.
/// </summary>
internal class Utils
{
    /// <summary>
    /// This method checks each value and determines 
    /// what value this value is closest to in a set
    /// or list of coordinates.
    /// </summary>
    /// <param val="value"> X or y value to be checked</param>
    /// <param coorSet="coordinates"> HashSet of x or y values</param>
    /// <returns>
    /// Value that is closest to the value in a hashset of coordinates
    /// </returns>
    public static double MapValue(double val, HashSet<double> coorSet, ClosestFinder finder)
    {
        if (coorSet.Contains(val))
        {
            return val;
        }
        return finder.FindClosest(val);
    }

    /// <summary>
    /// This method calculates the angle (alpha) between two points
    /// in a 2D plane using the arctangent function.
    /// </summary>
    /// <param name="xMax">The maximum x-coordinate</param>
    /// <param name="yMin">The minimum y-coordinate</param>
    /// <returns>
    /// The angle in degrees between the two points.
    /// </returns>
    /// <remarks>
    /// The angle is calculated using the arctangent of the ratio of the
    /// difference in y-coordinates to the difference in x-coordinates.
    /// The result is rounded to one decimal place.
    /// </remarks>
    public static double FindAlpha(Tuple<double, double> xMax, Tuple<double,double> yMin)
    {
        // Note: ".Item1" = x value, ".Item2" = y value
        double opposite = xMax.Item2 - yMin.Item2;
        double adjacent = xMax.Item1 - yMin.Item1;

        double angleRadians = Math.Atan(opposite / adjacent);
        // Convert radians to degrees. 
        // 1 radian = 180/π degrees
        double angleDegrees = angleRadians * (180.0 / Math.PI);

        // Return the angle in degrees
        return Math.Round(angleDegrees, 1);
    }
}