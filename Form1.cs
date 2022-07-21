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
        private static int universeX = 30;
        private static int universeY = 30;
        bool[,] universe = new bool[universeX, universeY]; 
        bool[,] scratchPad = new bool[universeX, universeY]; // universe for new generations
        
        // Drawing colors
        Color deadColor = Color.White;
        Color cellColor = Color.RoyalBlue;
        Color gridColor = Color.SlateGray;
        Color byTenColor = Color.Black;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        // Current Seed
        int seed = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = Properties.Settings.Default.IntervalTicker; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running

            // Initialize Settings
            deadColor = Properties.Settings.Default.DeadCellColor;
            cellColor = Properties.Settings.Default.AliveCellColor;
            gridColor = Properties.Settings.Default.GridColor;
            byTenColor = Properties.Settings.Default.By10Color;

            // Initialize Universe Size
            universeX = Properties.Settings.Default.XCells;
            universeY = Properties.Settings.Default.YCells;

            // Create new Univese
            universe = new bool[universeX, universeY];
            scratchPad = new bool[universeX, universeY];

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
                    bool cell = universe[x, y];
                    int count;

                    // Run Count Alg based off of setting option
                    if (finiteToolStripMenuItem.Checked) count = CountNeighborsFinite(x,y);
                    else count = CountNeighborsToroidal(x,y);

                    // Apply The GOL Rules - Turn On/Off in Scratch Pad
                    /** RULES
                     * Living cells with less than 2 living neighbors die in the next generation.
                     * Living cells with more than 3 living neighbors die in the next generation.
                     * Living cells with 2 or 3 living neighbors live in the next generation.
                     * Dead cells with exactly 3 living neighbors live in the next generation.
                    */

                    if (cell){
                        // Cell is alive
                        if (count < 2) scratchPad[x, y] = false;
                        if (count > 3) scratchPad[x, y] = false;
                    } else {
                        // Cell is dead
                        if (count == 3) scratchPad[x, y] = true;
                    }
                }
            }

            // Copy scratchPad to existing universe by swapping
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            // Increment generation count
            generations++;

            // Update Strip Info
            UpdateStripInfo();


            // Invalidate Graphics Panel
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration(); // Run The Next Generation
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

            // A penf or drawing the 10x10 grid lines (color, width)
            Pen byTenPen = new Pen(byTenColor, 2);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // A Brush for filling dead cells interiors (color)
            Brush backBrush = new SolidBrush(deadColor);

            // Font Setup
            Font font = new Font("Arial", 8f);

            // String Setup
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

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
                    if (universe[x, y]) e.Graphics.FillRectangle(cellBrush, cellRect); // fill live cell
                    else e.Graphics.FillRectangle(backBrush, cellRect); // fill dead cell


                    // Outline the cell with a pen if enabled
                    if(gridToolStripMenuItem.Checked) e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    // If the 10x10 grid is enabled, draw
                    if (x10GridToolStripMenuItem.Checked) {
                        // Draw 10x10 Horizontal - Does not draw if the y is 0 (at the top of the screen)
                        if (y % 10 == 0 && y != 0) e.Graphics.DrawRectangle(byTenPen, cellRect.X, cellRect.Y, cellRect.Width, 1);

                        // Draw 10x10 Vertical - Does not draw if the x is 0 (at the left of the screen)
                        if(x % 10 == 0 && x != 0) e.Graphics.DrawRectangle(byTenPen, cellRect.X, cellRect.Y, 1, cellRect.Height);
                    }


                    // Draw Neighbor Count If enabled
                    if (neighborCountToolStripMenuItem.Checked){
                        int neighbors;
                        // Run Count Alg based off of setting option
                        if (finiteToolStripMenuItem.Checked) neighbors = CountNeighborsFinite(x, y);
                        else neighbors = CountNeighborsToroidal(x, y);

                        if(neighbors > 0)
                        {
                            if (universe[x, y]) e.Graphics.DrawString(neighbors.ToString(), font, Brushes.White, cellRect, stringFormat); // fill live cell
                            else e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Black, cellRect, stringFormat); // fill dead cell
                        }
                    }
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            byTenPen.Dispose();
            cellBrush.Dispose();
            backBrush.Dispose();
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

                // Update Status Strip Alive
                int aliveCount = 0;
                for (int y2 = 0; y2 < universe.GetLength(1); y2++)
                    for (int x2 = 0; x2 < universe.GetLength(0); x2++)
                        if (universe[x2, y2]) aliveCount++;
                toolStripStatusLabel1.Text = "Alive = " + aliveCount.ToString();

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
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Switch NeighborCount Drawing On/Off
        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neighborCountToolStripMenuItem.Checked = !neighborCountToolStripMenuItem.Checked;
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Switch Grid Drawing On/Off
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridToolStripMenuItem.Checked = !gridToolStripMenuItem.Checked;
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void x10GridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x10GridToolStripMenuItem.Checked = !x10GridToolStripMenuItem.Checked;
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }


        // Finite Neighbor Counting
        private int CountNeighborsFinite(int x, int y){
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++){
                for (int xOffset = -1; xOffset <= 1; xOffset++){
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0) continue;
                    // if xCheck/yCheck is less than 0 then continue
                    if (xCheck < 0 || yCheck < 0) continue;
                    // if xCheck/yCheck is greater than or equal too xLen/yLen then continue
                    if (xCheck >= xLen || yCheck >= yLen) continue;

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }

        // Toroidal Neighbor Counting
        private int CountNeighborsToroidal(int x, int y){
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++){
                for (int xOffset = -1; xOffset <= 1; xOffset++){
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0) continue;
                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0) xCheck = xLen - 1;
                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0) yCheck = yLen - 1;
                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen) xCheck = 0;
                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen) yCheck = 0;

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }

        // Menu - Settings -> Options
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options dlg = new Options();

            // Set Current Interval & Universe Size Values
            dlg.Interval = timer.Interval;
            dlg.XCell = universeX;
            dlg.YCell = universeY;

            // Show Dialog Box to User & Determine Result
            if (DialogResult.OK == dlg.ShowDialog())
            {

                // Set New Interval
                timer.Interval = dlg.Interval;


                // Only reset universe size if size has changed
                if (universeX != dlg.XCell || universeY != dlg.YCell)
                {
                    // Set new Universe Sizes
                    universeX = dlg.XCell;
                    universeY = dlg.YCell;

                    // Create new Univese
                    universe = new bool[universeX, universeY];
                    scratchPad = new bool[universeX, universeY];
                }

                // Invalidate Graphics Panel
                graphicsPanel1.Invalidate();
            }
        }

        // Menu - Settings -> Customization -> Dead Cell Color
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create Instance of Dialog Box
            ColorDialog dlg = new ColorDialog();

            // Construct the Dialog Box
            dlg.Color = deadColor;

            // Show Dialog Box to User & Determine Result
            if (DialogResult.OK == dlg.ShowDialog()) {

                // Update Color to Chosen Color
                deadColor = dlg.Color; 
                
                // Invalidate Graphics Panel
                graphicsPanel1.Invalidate();
            }
        }

        // Menu - Settings -> Customization -> Alive Cell Color
        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create Instance of Dialog Box
            ColorDialog dlg = new ColorDialog();

            // Construct the Dialog Box
            dlg.Color = cellColor;

            // Show Dialog Box to User & Determine Result
            if (DialogResult.OK == dlg.ShowDialog())
            {

                // Update Color to Chosen Color
                cellColor = dlg.Color;

                // Invalidate Graphics Panel
                graphicsPanel1.Invalidate();
            }
        }

        // Menu - Settings -> Customization -> Grid Color
        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create Instance of Dialog Box
            ColorDialog dlg = new ColorDialog();

            // Construct the Dialog Box
            dlg.Color = gridColor;

            // Show Dialog Box to User & Determine Result
            if (DialogResult.OK == dlg.ShowDialog())
            {

                // Update Color to Chosen Color
                gridColor = dlg.Color;

                // Invalidate Graphics Panel
                graphicsPanel1.Invalidate();
            }
        }

        // Menu - Settings -> Customization -> Grid x10 Color
        private void gridX10ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create Instance of Dialog Box
            ColorDialog dlg = new ColorDialog();

            // Construct the Dialog Box
            dlg.Color = byTenColor;

            // Show Dialog Box to User & Determine Result
            if (DialogResult.OK == dlg.ShowDialog())
            {

                // Update Color to Chosen Color
                byTenColor = dlg.Color;

                // Invalidate Graphics Panel
                graphicsPanel1.Invalidate();
            }
        }

        // Handle Form Once Closed
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Update Settings Properties
            Properties.Settings.Default.IntervalTicker = timer.Interval;
            Properties.Settings.Default.XCells = universeX;
            Properties.Settings.Default.YCells = universeY;
            Properties.Settings.Default.DeadCellColor = deadColor;
            Properties.Settings.Default.AliveCellColor = cellColor;
            Properties.Settings.Default.GridColor = gridColor;
            Properties.Settings.Default.By10Color = byTenColor;


            // Save the new properties
            Properties.Settings.Default.Save();
        }

        // Menu - Settings -> Reset Settings
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset Settings Properties
            Properties.Settings.Default.Reset();

            // Initialize Settings
            timer.Interval = Properties.Settings.Default.IntervalTicker; // milliseconds
            deadColor = Properties.Settings.Default.DeadCellColor;
            cellColor = Properties.Settings.Default.AliveCellColor;
            gridColor = Properties.Settings.Default.GridColor;
            byTenColor = Properties.Settings.Default.By10Color;

            // Initialize Universe Size
            universeX = Properties.Settings.Default.XCells;
            universeY = Properties.Settings.Default.YCells;

            // Create new Univese
            universe = new bool[universeX, universeY];
            scratchPad = new bool[universeX, universeY];

            // Invalidate Graphics Panel
            graphicsPanel1.Invalidate();
        }

        // Menu - Settings -> Reload Settings
        private void reloadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Update Settings Properties
            Properties.Settings.Default.Reload();

            // Initialize Settings
            timer.Interval = Properties.Settings.Default.IntervalTicker; // milliseconds
            deadColor = Properties.Settings.Default.DeadCellColor;
            cellColor = Properties.Settings.Default.AliveCellColor;
            gridColor = Properties.Settings.Default.GridColor;
            byTenColor = Properties.Settings.Default.By10Color;

            // Initialize Universe Size
            universeX = Properties.Settings.Default.XCells;
            universeY = Properties.Settings.Default.YCells;

            // Create new Univese
            universe = new bool[universeX, universeY];
            scratchPad = new bool[universeX, universeY];

            // Invalidate Graphics Panel
            graphicsPanel1.Invalidate();
        }

        // Menu - Randomize -> From Seed
        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Seed dlg = new Seed();

            // Set Current Seed
            dlg.seed = seed;
         

            // Show Dialog Box to User & Determine Result
            if (DialogResult.OK == dlg.ShowDialog())
            {
                seed = dlg.seed;
                Randomize();
            }
        }

        // Menu - Randomize -> From Current Seed
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Randomize();
        }

        // Menu - Randomize -> From Time
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            seed = (int)DateTime.Now.Ticks;
            Randomize();
        }

        // Randomize GOL
        private void Randomize()
        {
            Random rand = new Random(seed); // Init Rand Variable

            // Clear ScratchPad
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }

            // Iterate through the universe in the x, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the y, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int num = rand.Next(0,2);

                    // if number is 0, turn cell on
                    if (num == 0) scratchPad[x, y] = true;
                }
            }

            // Copy scratchPad to existing universe by swapping
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            // Update Strip Info
            UpdateStripInfo();

            // Invalidate Graphics Panel
            graphicsPanel1.Invalidate();
        }

        public void UpdateStripInfo()
        {
            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            // Update Status Strip Alive
            int aliveCount = 0; // Total count for alive cells
            for (int y = 0; y < universe.GetLength(1); y++)
                for (int x = 0; x < universe.GetLength(0); x++)
                    if (universe[x, y]) aliveCount++; // Add alive cell to count
            toolStripStatusLabel1.Text = "Alive = " + aliveCount.ToString(); // Write alive count to status

            // Update Seed Strip
            toolStripStatusLabel2.Text = "Seed = " + seed; // Write alive count to status
        }

    }
}
