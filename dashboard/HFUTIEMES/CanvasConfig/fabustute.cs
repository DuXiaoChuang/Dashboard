using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Data.Sql;
using System.Data.SqlClient;


namespace HFUTIEMES
{
    public partial class fabustute : Form
    {
        public string h;
        public fabustute()
        {
            InitializeComponent();
        }

        private void fabustute_Load(object sender, EventArgs e)
        {
            comboBox1.Text = h;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.CloseOutput = false;
            settings.OmitXmlDeclaration = false;
            string outfilename = Application.StartupPath + "\\fabu.xml";
            XmlWriter writer = XmlWriter.Create(outfilename, settings);
            writer.WriteStartDocument();
            writer.WriteElementString("data", comboBox1.Text.ToString());
            writer.Flush();
            writer.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
