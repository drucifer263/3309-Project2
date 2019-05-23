using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_2_2
{
    public partial class frmUpdateScore : Form
    {
        //Form updates an existing score for an existing student record; It is called by UpdateStudentScores form (Secondary form)
        public frmUpdateScore()
        {
            InitializeComponent();
        }

        //Updates scores
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int score = 0;

            //Validates data
            if (IsValidEntry())
            {
                //Takes the info from the score textbox, converts it to an int and places it in score
                score = Convert.ToInt32(txtScore.Text);

                //Places score into the Tag object to be passed back to UpdateStudentScores form
                this.Tag = score;

                //Confirms the Ok button press in the form
                this.DialogResult = DialogResult.OK;
            }
        }

        //Makes sure txt box isnt empty
        public bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        //Makes sure txt box has an integer
        public bool IsInt(TextBox textBox, string name)
        {
            int number = 0;

            if (Int32.TryParse(textBox.Text, out number))
            {
                return true;
            }

            else
            {
                MessageBox.Show(name + " must be an integer number.", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        //Makes sure that the numbers in txtbox are in range 0-100  
        public bool IsWithinRange(TextBox textBox, string name,
            int min, int max)
        {
            int number = Convert.ToInt32(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(name + " must be between " + min +
                    " and " + max + ".", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        //Data validation
        public bool IsValidEntry()
        {
            return
                IsPresent(txtScore, "Score") &&
                IsInt(txtScore, "Score") &&
                IsWithinRange(txtScore, "Score", 0, 100);
        }

        //Closes form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
