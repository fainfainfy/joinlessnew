using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FyColocation
{
    public partial class mainForm : Form
    {
       
        public Form_joinlessbegin form_joinlessbegin = null;
        public mainForm()
        {
            InitializeComponent();
         
        }

     

       

        private void Form1_Load(object sender, EventArgs e)
        {

             
                    form_joinlessbegin = new Form_joinlessbegin();
                    form_joinlessbegin.ShowDialog(this);
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                    SetVisibleCore(false); 
                
 
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);
        }
 

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

            this.Hide();
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
 
        }
    }
}
