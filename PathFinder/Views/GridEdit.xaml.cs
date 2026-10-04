using PathFinder.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PathFinder.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GridEdit : Window
    {
        public Models.Grid grid;
        private int alg;
        public GridEdit(Models.Grid maze, int algo)
        {
            InitializeComponent();
            GridContainer.Rows = maze.Rows;
            GridContainer.Columns = maze.Columns;
            grid = maze;
            alg = algo;
            load();
            loadGrid();
        }

        private void load()
        {
            GridContainer.Children.Clear();
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Columns; col++)
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

        private void BtnSave(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }


    }
}
