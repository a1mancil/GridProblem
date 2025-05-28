# Grid Problem

## Build Instructions

1. Ensure you have the required dependencies installed:
    - Install the .NET SDK from [Microsoft's .NET website](https://dotnet.microsoft.com/download).

3. Build the project:
    ```bash
    dotnet build
    ```

## Run Instructions

1. Execute the program with any text file e.g.:
    ```bash
    dotnet run -- grid_input.txt
    ```

2. Follow the on-screen instructions to interact with the program.

## Assumptions

- Values to display will always be in the following format: XX.XX
- Alpha is between 0 & 90 degrees
- Rounding alpha to the 1 decimal place, e.g. 4.28 -> 4.3
- Domain and range of the grid are all positive real integers:

$$
\mathbb{Z}^+ = \{ x \in \mathbb{Z} \mid x > 0 \}
$$

## Strategy
1) Calculate the rows using parametric Interpolation; calculate all the values between two known points A & B, then iterate in ascending order with respects to y in the grid. Update points A & B respectively.
2) Build a list of all points while traversing rows. Use this list to print out the columns


