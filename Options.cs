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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }


        // Get  & Set the Interval Property for the Ticker Interval
        public int Interval
        {
            get { return (int)TickerInterval.Value; }
            set { TickerInterval.Value = value; }
        }

        // Get & Set the XCell Property for the Universes X Size
        public int XCell
        {
            get { return (int)XCells.Value; }
            set { XCells.Value = value; }
        }

        // Get & Set the XCell Property for the Universes Y Size
        public int YCell
        {
            get { return (int)YCells.Value; }
            set { YCells.Value = value; }
        }

    }
}
