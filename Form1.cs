using EGIS.ShapeFileLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace The_BEST_US_Swingometer
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            ShapeFile test = new ShapeFile("..\\..\\shapefile\\test\\cb_2018_us_state_500k.shp");

            

        }

        private void button2_Click(object sender, EventArgs e) // clears textboxes
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProgramLogic logic = new ProgramLogic();
            double inputD;
            double inputR;
            double inputI;

            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && comboBox1.Text.Length > 0)
            {
                try
                {
                    inputD = Convert.ToDouble(textBox2.Text);
                    inputR = Convert.ToDouble(textBox1.Text);
                    inputI = Convert.ToDouble(textBox3.Text);
                }
                catch
                {
                    MessageBox.Show("Something went wrong with the input. That's all I know.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();

                    return;
                }

                logic.inputD = inputD;
                logic.inputR = inputR;
                logic.inputI = inputI;

                logic.Setup();

                logic.GetOutput(comboBox1.SelectedIndex);

            }
            else
            {
                MessageBox.Show("Something went wrong with the input. That's all I know.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                return;
            }

            MapHandler theMap = null;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    theMap = new MapHandler(sfMap1, "..\\..\\shapefile\\test\\cb_2018_us_state_500k.shp", logic.OutputAreas);
                    break;
                case 2:
                    theMap = new MapHandler(sfMap1, "..\\..\\shapefile\\test\\cb_2018_us_state_500k.shp", logic.OutputAreas);
                    break;
                case 1:
                    theMap = new MapHandler(sfMap1, "..\\..\\shapefile\\house\\congress.shp", logic.OutputAreas);
                    break;
            }
            
            //theMap.Setup();
            theMap.Colouring();
        }


    }
}
