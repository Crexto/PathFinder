using PathFinder.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PathFinder.Algorithms
{
    class DFS
    {
        public List<GridNode> getNeighbors(GridNode[,] Nodes, GridNode node)
        {
            List<GridNode> neighbors = new();

            int row = node.Row;
            int col = node.Column;

            if (row > 0)
                neighbors.Add(Nodes[row - 1, col]);

            if (row < Nodes.GetLength(0) - 1)
                neighbors.Add(Nodes[row + 1, col]);

            if (col > 0)
                neighbors.Add(Nodes[row, col - 1]);

            if (col < Nodes.GetLength(1) - 1)
                neighbors.Add(Nodes[row, col + 1]);

            return neighbors;
        }

        public GridNode getPath(Models.Grid grid)
        {
            Stack<GridNode> stack = new();
            stack.Push(grid.Start);

            while (stack.Count > 0) { 
                GridNode current = stack.Pop();

                if (current == grid.End)
                {
                    return current;
                }

                current.IsVisited = true;
                
                foreach (GridNode node in getNeighbors(grid.Nodes, current))
                {
                    if (node.IsVisited)
                    {
                        continue;
                    }
                    else if (node.IsWall)
                    {
                        continue;
                    }
                    else
                    {
                        node.Parent = current;
                        node.IsVisited = true;
                        stack.Push(node);
                    }
                }
            }
            return null;
        }
    }
    
}
