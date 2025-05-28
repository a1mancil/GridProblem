using System;
using System.Collections.Generic;

/// <summary>
/// The <c>ClosestFinder</c> class finds the closest value in a sorted list, 
//  that resembles the target value. Used to map the calculated coordinates 
/// to the correct coordinates given in the text file. The class maintains
/// a sorted list of values and efficiently finds the closest value to a given 
/// target via a binary search.
/// </summary>
internal class ClosestFinder
{
    private List<double> sortedList = new List<double>();

    public void Insert(double value)
    {
        int index = sortedList.BinarySearch(value);
        if (index < 0)
        {
            index = ~index; // Get correct insert position
        }
        sortedList.Insert(index, value); // Maintain sorted order
    }

    /// <summary>
    /// Finds the closest value to the specified target in the sorted list.
    /// </summary>
    /// <param name="target">The target value to find the closest match for.</param>
    /// <returns>
    /// The value in the sorted list that is closest to the target. 
    /// If two values are equally close, the smaller value is returned.
    /// </returns>
    public double FindClosest(double target)
    {
        int index = sortedList.BinarySearch(target);

        // If not found, BinarySearch returns (-(insertion point) - 1)
        // It's necessary to negate the index to get the insertion point
        // and then subtract 1 to get the index of the closest value.
        // The ~ operator is a bitwise NOT operator, which flips the bits
        // of the number. For example, if the insertion point is 3,
        // ~3 = -4, so we need to subtract 1 to get the index of the closest value
        // in the sorted list.
        index = ~index;

        bool hasLeft = index > 0;
        bool hasRight = index < sortedList.Count;

        if (!hasLeft)
            return sortedList[index]; // Only right exists
        if (!hasRight)
            return sortedList[index - 1]; // Only left exists

        double left = sortedList[index - 1];
        double right = sortedList[index];

        return Math.Abs(left - target) <= Math.Abs(right - target) ? left : right;
    }
}