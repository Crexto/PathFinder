namespace PathFinder.Models
{
    public class Grid
    {
        public GridNode[,] Nodes { get; set; }

        public GridNode? Start { get; set; }
        public GridNode? End { get; set; }

        public Grid(int rows, int columns)
        {
            Nodes = new GridNode[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Nodes[row, col] = new GridNode
                    {
                        Row = row,
                        Column = col
                    };
                }
            }
        }
    }
}