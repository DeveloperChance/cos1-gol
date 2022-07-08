using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cos1_gol
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[5, 5]; 
        bool[,] scratchPad = new bool[5, 5]; // universe for new generations
        
        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < scratchPad.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < scratchPad.GetLength(0); x++)
                {
                    scratchPad[x, y] = universe[x,y];
                }
            }

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                //Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    //int count = CountNeighbor(x,y);

                    // Apply The GOL Rules - Do not turn off in universe
                    // Turn on/off in the scratch pad
                }
            }

            // Copy scratchPad to existing universe by swapping
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            // Invalidate Graphics Panel
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float) graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float) graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = (float) graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                float cellHeight = (float) graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                float x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                float y = e.Y / cellHeight;

                // Toggle the cell's state
                // convert back to int to round 
                universe[(int)x, (int)y] = !universe[(int)x, (int)y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        // Handle File >> Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close Form1.cs Window
            this.Close();
        }


        // Start Ticker - Toolbar
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            timer.Enabled = true; // Run Ticker
        }

        // Start Ticker - Menu: Tools -> Run
        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true; // Run Ticker
        }

        // Pause ticker - Toolbar
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = false; // Pause Ticker
        }


        // Pause ticker - Menu: Tools -> Pause
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false; // Pause Ticker
        }

        // Advance 1 Generation - Toolbar
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            NextGeneration(); // Advance 1 Generation
        }

        // Advance 1 Generation - Menu: Tools -> Next
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextGeneration(); // Advance 1 Generation
        }

        // New Universe - Toolbar
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }

            // Invalidate Graphics Panel
            graphicsPanel1.Invalidate();
        }

        /**
         * Swap NeighborCount Draw Methods
         */
        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set Finite Neighbor Count to True & Toridal Neighbor Count to False
            finiteToolStripMenuItem.Checked = true;
            toroidalToolStripMenuItem.Checked = false;
        }

        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set Finite Neighbor Count to False & Toridal Neighbor Count to True
            toroidalToolStripMenuItem.Checked = true;
            finiteToolStripMenuItem.Checked = false;
        }

        // Switch HUD On/Off
        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hUDToolStripMenuItem.Checked = !hUDToolStripMenuItem.Checked;
        }

        // Switch NeighborCount Drawing On/Off
        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neighborCountToolStripMenuItem.Checked = !neighborCountToolStripMenuItem.Checked;
        }

        // Switch Grid Drawing On/Off
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridToolStripMenuItem.Checked = !gridToolStripMenuItem.Checked;
        }
    }
}
