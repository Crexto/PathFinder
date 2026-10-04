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

namespace PathFinder.Views
{
    public partial class MainWindow : Window
    {
        private Models.Grid? maze = null;
        private DFS dfs = new();
        private BFS bfs = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        //private void shortestPathFinder(GridNode node)
        //{
        //    if (node != null)
        //    {
        //        while (node.Parent != null)
        //        {
        //            node.shortest = true;
        //            node = node.Parent;
        //        }
        //    }
        //}

        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }


        private void NumberOnlyPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));

                if (!text.All(char.IsDigit))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


        private void BtnEditMaze(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtRows.Text) || string.IsNullOrWhiteSpace(TxtColumns.Text))
            {
                MessageBox.Show("Rows and Columns cannot be empty");
                return;
            }

            int.TryParse(TxtRows.Text, out int rows);
            int.TryParse(TxtColumns.Text, out int cols);

            if (maze == null || maze.Columns != cols || maze.Rows != rows)
            {
                maze = new(rows, cols);
            }
            
            GridEdit editWindow = new(maze, CmbPathAlgo.SelectedIndex);
            if (editWindow.ShowDialog() == true)
            {
                maze = editWindow.grid;
            }
        }


        private void BtnResetMaze(object sender, RoutedEventArgs e)
        {
            maze = null;
        }


        private void BtnRun(object sender, RoutedEventArgs e)
        {
            if (maze == null || maze.Start == null || maze.End == null)
            {
                MessageBox.Show("Start and end points must be available");
                return;
            }

            //switch (CmbAlgo.SelectedIndex)
            //{
            //    case 0:
            //        shortestPathFinder(dfs.getPath(grid));
            //        loadGrid();
            //        break;
            //    case 1:
            //        shortestPathFinder(bfs.getPath(grid));
            //        loadGrid();
            //        break;

            //}

        }
    }
}