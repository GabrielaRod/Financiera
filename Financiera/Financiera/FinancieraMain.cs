using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Financiera
{
    public partial class FinancieraMain : Form
    {
        public FinancieraMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activos activos = new Activos();
            activos.Show();
            this.Hide();
        }
    }
}
