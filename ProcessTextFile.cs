/// <summary>
/// The <c>ProcessTextFile</c> class processes a text file containing
/// coordinates, calculates the minimum and maximum values for both x and y
/// coordinates, and provides methods to retrieve these values and the closest
/// values in a sorted list. It also maintains hash sets for unique x and y
/// coordinates, and uses a <c>ClosestFinder</c> class to efficiently find
/// the closest values to a given target in the sorted lists of x and y
/// coordinates.
/// </summary>
internal class ProcessTextFile{
    // ----- Initialize temporary tokens array as well as other variables -----
    // Delcare outside of loop to mimimize memory allocation 
    // & garbage collection.
    // Assuming the file contains only two values (one coordinate) per line.
    private string[] tokens = new string[2];
    private string[] lines;

    public ProcessTextFile(string[] linesParam){
        lines = linesParam;

        // Remove any leading and trailing whitespace characters from a string.
        // Split the first line into two parts using a comma as the delimiter.
        tokens = lines[0].Trim().Split(',');

        string firstXstr = tokens[0];
        string firstYstr = tokens[1];

        if( double.TryParse(firstXstr, out double firstXVal) == false || 
            double.TryParse(firstYstr, out double firstYVal) == false)
        {
            throw new FormatException(Constants.ERR_INVALID_FIRST_LINE);
        }
        // ----- Calculate the min & max values i.e. 4 corners of the grid -----
        // Set the min & max value to the first coordinate
        Tuple<double, double> firstCoor = 
                            new Tuple<double, double>(firstXVal, firstYVal);

        // The values below represent the four corners of the grid
        xMax = firstCoor;
        xMin = firstCoor;            
        yMax = firstCoor;
        yMin = firstCoor;
    }

    private HashSet<double> hashSetX = new HashSet<double>();
    private HashSet<double> hashSetY = new HashSet<double>();
    private ClosestFinder finderX = new ClosestFinder();
    private ClosestFinder finderY = new ClosestFinder();

    private Tuple<double, double> xMax;
    private Tuple<double, double> xMin;            
    private Tuple<double, double> yMax;
    private Tuple<double, double> yMin;

    public HashSet<double> getHashSetX(){
        return hashSetX;
    }
    public HashSet<double> getHashSetY(){
        return hashSetY;
    }
    public ClosestFinder getFinderX(){
        return finderX;
    }
    public ClosestFinder getFinderY(){
        return finderY;
    }

    public Tuple<double, double> getMaxX(){
        return xMax;
    }
    public Tuple<double, double> getMinX(){
        return xMin;
    }
    public Tuple<double, double> getMaxY(){
        return yMax;
    }
    public Tuple<double, double> getMinY(){
        return yMin;
    }

    public bool parseInfoCheckDuplicates(){
        // Process each line of the file.
        foreach (string line in lines)
        {
            tokens = line.Trim().Split(',');

            if( double.TryParse(tokens[0], out double xVal) == false ||
                double.TryParse(tokens[1], out double yVal) == false)
            {
                throw new FormatException(Constants.ERR_INVALID_COOR_FORMAT + line);
            }
            // Note: ".Item1" = x value, ".Item2" = y value
            Tuple<double, double> xAndY = new Tuple<double, double>(xVal, yVal);

            // ----- Populate hash set & sorted list -----
            // Add the x and y values to the hash sets
            bool addedX = hashSetX.Add(xAndY.Item1);
            if (addedX == false)
            {
                return true; // Duplicate found
            }
            bool addedY = hashSetY.Add(xAndY.Item2);
            if (addedX == false)
            {
                return true; // Duplicate found
            }

            // Add the x and y values to the ClosestFinder objects
            finderX.Insert(xAndY.Item1);
            finderY.Insert(xAndY.Item2);

            // Update the min and max values
            xMin = (xAndY.Item1 < xMin.Item1)? xAndY: xMin;
            xMax = (xAndY.Item1 > xMax.Item1)? xAndY: xMax;
            yMin = (xAndY.Item2 < yMin.Item2)? xAndY: yMin;
            yMax = (xAndY.Item2 > yMax.Item2)? xAndY: yMax;
        }
        return false;
    }
}