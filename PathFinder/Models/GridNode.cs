using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Models
{
    public class GridNode
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsWall { get; set; }
        public double Cost { get; set; } = 1;
        public bool IsVisited { get; set; }
        public GridNode? Parent { get; set; }
    }
}
