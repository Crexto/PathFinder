using PathFinder.Algorithms;
using PathFinder.Models;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PathFinder
{
    public partial class MainWindow : Window
    {
        private Models.Grid grid = new(ROWS, COLUMNS);
        private DFS dfs = new();
        private BFS bfs = new();
        const int ROWS = 10; 
        const int COLUMNS = 10;

        public MainWindow()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            GridContainer.Children.Clear();
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    Button cell = new Button();

                    GridNode node = grid.Nodes[row, col];

                    cell.Tag = node;
                    cell.Click += CellClick;

                    GridContainer.Children.Add(cell);
                }
            }
        }

        private void loadGrid()
        {
            foreach (Button child in GridContainer.Children)
            {
                var node = (GridNode)child.Tag;

                if (node.IsWall)
                {
                    child.Background = Brushes.Black;
                } 
                else if (node == grid.Start) 
                {
                    child.Background = Brushes.Green;
                } 
                else if (node == grid.End)
                {
                    child.Background = Brushes.Red;
                }
                else if (node.shortest)
                {
                    child.Background = Brushes.LightGreen;
                }
                else if (node.IsVisited)
                {
                    child.Background = Brushes.LightBlue;
                }
                else
                {
                    child.Background = Brushes.White;
                }

            }
        }

        private void CellClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            GridNode BtnNode = (GridNode)button.Tag;

            if (BtnNode == grid.Start)
            {
                grid.End = BtnNode;
                grid.Start = null;
            }
            else if (BtnNode == grid.End)
            {
                grid.End = null;
            }
            else if (BtnNode.IsWall) 
            {
                BtnNode.IsWall = false;
                grid.Nodes[BtnNode.Row, BtnNode.Column].IsWall = false;
                grid.Start = BtnNode;
            } 
            else
            {
                BtnNode.IsWall = true;
                grid.Nodes[BtnNode.Row, BtnNode.Column].IsWall = true;
            }

            loadGrid();

        }

        private void shortestPathFinder(GridNode node)
        {
            if (node != null)
            {
                while (node.Parent != null)
                {
                    node.shortest = true;
                    node = node.Parent;
                }
            }
        }

        private void Run(object sender, RoutedEventArgs e)
        {
            if (grid.Start == null || grid.End == null)
            {
                MessageBox.Show("Start and end points must be available");
                return;
            }

            switch (CmbAlgo.SelectedIndex)
            {
                case 0:
                    shortestPathFinder(dfs.getPath(grid));
                    loadGrid();
                    break;
                case 1:
                    shortestPathFinder(bfs.getPath(grid));
                    loadGrid();
                    break;

            }

        }
    }
}