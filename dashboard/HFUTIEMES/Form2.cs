using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HFUTIEMES
{
    public partial class Form2 : Form
    {
        public ClassMediaPlay player = new ClassMediaPlay();
        string mediaPath = Application.StartupPath + @"/Alert/AlertMediaPath.ini";
        string mediaFileName = Application.StartupPath + @"/Alert/alert.wav";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            player.fileName = mediaFileName;
            player.PlayMedia();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.Stop();
        }
    }
}
