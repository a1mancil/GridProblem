/// <summary>
/// The <c>PrintGrid</c> class is responsible for printing the grid of coordinates
/// based on the processed data from a text file. It calculates the rows and columns
/// of the grid, maps the calculated coordinates to the closest values in the
/// provided coordinate sets, and prints the results to the console. It also
/// calculates and prints the angle (alpha) between the maximum x-coordinate and
/// the minimum y-coordinate.
/// </summary>
internal class PrintGrid
{
    public PrintGrid(string[] linesParam)
    {
        lines = linesParam;
        dimension = Math.Sqrt(lines.Length);
        coordinateList = new List<Tuple<double, double>>();
    }
    // Use List to store columns
    private List<Tuple<double, double>> coordinateList;
    private string[] lines;
    // Grid is NxN = number of coordinates
    private double dimension;

    /// <summary>
    /// This method prints the base case grid.
    /// </summary>
    public void printBaseCaseGrid()
    {
        string[] tokens = new string[2];
        foreach (string line in lines)
        {
            tokens = line.Trim().Split(',');

            if( double.TryParse(tokens[0], out double xVal) == false ||
                double.TryParse(tokens[1], out double yVal) == false)
            {
                throw new FormatException(Constants.ERR_INVALID_COOR_FORMAT + line);
            }
            coordinateList.Add(new Tuple<double, double>(xVal, yVal));
        }

        // ----- Sort and organize into grid -----
        var sortedX = coordinateList.Select(p => p.Item1)
            .Distinct().OrderBy(x => x).ToList(); 

        var sortedY = coordinateList.Select(p => p.Item2)
            .Distinct().OrderByDescending(y => y).ToList(); 

        var grid = new Tuple<double, double>[(int)dimension, (int)dimension];

        foreach (var coor in coordinateList)
        {
            int row = sortedY.IndexOf(coor.Item2); // Y = rows (top to bottom)
            int col = sortedX.IndexOf(coor.Item1); // X = cols (left to right)
            grid[row, col] = coor;
        }

        // ----- Output rows -----
        for (int row = 0; row < dimension; row++)
        {
            Console.Write($"Row {row}: ");
            for (int col = 0; col < dimension; col++)
            {
                var point = grid[row, col];
                Console.Write($"{point.Item1},{point.Item2}");
                if (col < dimension - 1)
                {
                    Console.Write(Constants.DASH);
                }
            }
            Console.WriteLine("");
        }

        // ----- Output columns -----
        for (int col = 0; col < dimension; col++)
        {
            Console.Write($"Col {col}: ");
            for (int row = 0; row < dimension; row++)
            {
                var point = grid[row, col];
                Console.Write($"{point.Item1},{point.Item2}");
                if (row < dimension - 1)
                {
                    Console.Write(Constants.DASH);
                }
            }
            Console.WriteLine("");
        }

        Console.WriteLine($"Alpha=0.0 degrees");
    }

    /// <summary>
    /// This method prints the rows and columns of the grid.
    /// </summary>
    /// <param name="processFile">The processed data of the text file</param>
    public void printRowsAndColumns(ProcessTextFile processFile)
    {
        // ----- Print rows using parametric interpolation -----
        Tuple<double, double> newPointA = 
            new Tuple<double, double>(
                processFile.getMinX().Item1, processFile.getMinX().Item2
            );
        Tuple<double, double> newPointB = 
            new Tuple<double, double>(
                processFile.getMaxY().Item1, processFile.getMaxY().Item2
            );
        
        for (int i = 1; i < dimension+1; i++)
        {
            Console.Write($"Row {i-1}: ");
            for (int j = 0; j < dimension; j++)
            {
                // Note: ".Item1" = x value, ".Item2" = y value
                double xValue = 
                    newPointA.Item1 
                        + (j / (dimension - 1)) * ((newPointB.Item1 - newPointA.Item1));
                double yValue = 
                    newPointA.Item2
                        + (j / (dimension - 1)) * ((newPointB.Item2 - newPointA.Item2));
                
                xValue = Utils.MapValue(
                    xValue, processFile.getHashSetX(), processFile.getFinderX()
                );
                yValue = Utils.MapValue(
                    yValue, processFile.getHashSetY(), processFile.getFinderY()
                );

                // Add the x and y values to the list of columns
                coordinateList.Add(new Tuple<double, double>(xValue, yValue));

                Console.Write($"{xValue},{yValue}");

                if(j < dimension - 1){
                    Console.Write(Constants.DASH);
                }
                else{
                    Console.WriteLine("");
                }
            }
            if(i != dimension){
                // Find new point A between xMin and yMin
                double currXValue = processFile.getMinX().Item1 + 
                    (i / (dimension - 1)) * 
                    ((processFile.getMinY().Item1 - processFile.getMinX().Item1));

                double currYValue = processFile.getMinX().Item2 + 
                    (i / (dimension - 1)) * 
                    ((processFile.getMinY().Item2 - processFile.getMinX().Item2));

                newPointA = new Tuple<double, double>(currXValue, currYValue);

                // Find new point B between xMax and yMax
                currXValue = processFile.getMaxY().Item1 + 
                    (i / (dimension - 1)) * 
                    ((processFile.getMaxX().Item1 - processFile.getMaxY().Item1));
                    
                currYValue = processFile.getMaxY().Item2 + 
                    (i / (dimension - 1)) * 
                    ((processFile.getMaxX().Item2 - processFile.getMaxY().Item2));

                newPointB = new Tuple<double, double>(currXValue, currYValue);
            }        
        }
        // ----- Print columns from list -----
        printColumns();
    }

    /// <summary>
    /// This method prints the columns of the grid.
    /// </summary>
    private void printColumns()
    {
        int count = 0;
        for(int i = 0; i < dimension; i++)
        {                        
            Console.Write($"Column {i}: ");
            for(int k = i; k < coordinateList.Count; k++)
            {   
                if(k % dimension == i)
                {
                    Console.Write($"{coordinateList[k].Item1},{coordinateList[k].Item2}");
                    ++count;
                    if(count < dimension){
                        Console.Write(Constants.DASH);
                    }
                    else{
                        Console.WriteLine("");
                    }
                }
            }
            count = 0;
        }
    }

    /// <summary>
    /// This method prints the alpha value.
    /// </summary>  
    /// <param name="alpha">The alpha value to print</param>
    public void printAlpha(Tuple<double, double> xMax, Tuple<double,double> yMin)
    {
        Console.WriteLine($"Alpha={Utils.FindAlpha(xMax, yMin)} degrees");
    }
}