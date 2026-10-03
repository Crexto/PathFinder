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

namespace PathFinder
{
    public partial class MainWindow : Window
    {
        private Models.Grid grid = new();

        public MainWindow()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {

            for (int row = 0; row < 25; row++)
            {
                for (int col = 0; col < 40; col++)
                {
                    Button cell = new Button();
                    cell.Tag = new GridNode
                    {
                        Row = row,
                        Column = col
                    };

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
                else if (node.Row == grid.StartRow && node.Column == grid.StartColumn) 
                {
                    child.Background = Brushes.Green;
                } 
                else if (node.Row == grid.EndRow && node.Column == grid.EndColumn)
                {
                    child.Background = Brushes.Red;
                } else
                {
                    child.Background = Brushes.White;
                }

            }
        }

        private void CellClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            GridNode BtnNode = (GridNode)button.Tag;

            if (BtnNode.Row == grid.StartRow && BtnNode.Column == grid.StartColumn)
            {
                grid.EndRow = grid.StartRow;
                grid.EndColumn = grid.StartColumn;
                grid.StartRow = null;
                grid.StartColumn = null;

            }
            else if (BtnNode.Row == grid.EndRow && BtnNode.Column == grid.EndColumn)
            {
                grid.EndRow = null;
                grid.EndColumn = null;
            }
            else if (BtnNode.IsWall) 
            {
                grid.StartRow = BtnNode.Row;
                grid.StartColumn = BtnNode.Column;
                BtnNode.IsWall = false;
            } 
            else
            {
                BtnNode.IsWall = true;
            }

            loadGrid();

        }
    }
}