# Escape Mines

A console pplication which reads an intial game settings file containing one or more sequences of moves. For each move
sequence, the application will output whether the sequence leads to the success or failure


Made with .NET Core v3.1

## File Format

The file must be in the Betsson.EscapeMines.App **root directory** as **gameSetting.txt**

**First line**: board size (column row) in tiles separated by space.

**Second line**: list of mines locations (row, column) separated by space. Each row/column pair separated by comma.

**Third line**: exit point location (column row) separated by space.

**Fourth line**: turtle starting position (column row direction) separated by space. Direction can be N, S, E or W.

**Fifth line and greater**: sequence of commands to be executed. These commands can be M, R or L. M as move, R as rotate 90 degrees to the right and L as rotate 90 degrees to the left.

**Example**:

    5 4
    1,1 1,3 3,3
    4 2
    0 1 N
    M R M M M R M
    M R M M M M R M M

## Tests

To run the unit tests with test coverage, navigate to the test project and run the following command in the console: 

    dotnet test --collect "Code Coverage"


