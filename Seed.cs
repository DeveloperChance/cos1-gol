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
    public partial class Seed : Form
    {
        public Seed()
        {
            InitializeComponent();
        }

        // Get  & Set the Seed Property
        public int seed
        {
            get { return (int)_seed.Value; }
            set { _seed.Value = value; }
        }

        private void Randomize_Click(object sender, EventArgs e)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            _seed.Value = rand.Next();
        }
    }
}
