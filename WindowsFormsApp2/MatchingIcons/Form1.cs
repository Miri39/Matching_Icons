using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MatchingIcons
{
    public partial class Form1 : Form
    {
        private Label firstClicked = null;
        private Label secondClicked = null;
        private Random random = new Random();
        private List<string> icons = new List<string>()
        {
            "!","!",
            "A","A",
            "C","C",
            "S","S",
            "T","T",
            "V","V",
            "M","M",
            "E","E"
        };
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
                SetRandomIcon(control as Label);
        }

        private void SetRandomIcon(Label iconLabel)
        {
            if (iconLabel != null)
            {
                int randomNumber = random.Next(icons.Count);
                iconLabel.Text = icons[randomNumber];
                iconLabel.ForeColor = iconLabel.BackColor;
                icons.RemoveAt(randomNumber);
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled == true)
                return;
            
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                if (clickedLabel == firstClicked)
                {
                    firstClicked.ForeColor = firstClicked.BackColor;
                    firstClicked = null;
                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    CheckForWinner();
                    return;
                }
                timer1.Start();
            }
        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    if(iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Le-ai dat pe toate","Bravo ba");
            Close();
        }
    }
}