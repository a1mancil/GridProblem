using System;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // ----- Check for errors in the command line arguments -----
            if (args.Length == 0)
            {
                throw new ArgumentException(Constants.ERR_PROVIDE_FILE_PATH);
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(Constants.ERR_FILE_NOT_FOUND);
            }

            // ----- Read the file and process the coordinates -----
            string[] lines = File.ReadAllLines(filePath);

            ProcessTextFile processFile = new ProcessTextFile(lines);

            bool isDuplicates = processFile.parseInfoCheckDuplicates();

            // ----- Print Rows & Columns -----
            PrintGrid printGrid = new PrintGrid(lines);

            if(isDuplicates == true)
            {
                printGrid.printBaseCaseGrid();
            }
            else
            {
                printGrid.printRowsAndColumns(processFile);
                // ----- Print Alpha: -----
                printGrid.printAlpha(processFile.getMaxX(), processFile.getMinY());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing the file: {ex.Message}");
        }
    }
}