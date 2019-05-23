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
    public partial class frmAddNewStudent : Form
    {
        //Form creates a new student record with a name and scores in it; It is called by StudentScores form (main form)
        public frmAddNewStudent()
        {
            InitializeComponent();
        }

        //Class variables 
        string scores="";
        
        //Adds scores to student record
        private void btnAddScore_Click(object sender, EventArgs e)
        {
            int score = 0;

            //Validation
            if (IsValidData())
            {
                //Converts txtBox text to an int and places into score
                score = Convert.ToInt32(txtScore.Text);
                
                //Adds each score with a space to a string called scores 
                scores += score + " ";

                //Places the scores into the txtBox, I just convert it here just in case
                txtScores.Text = Convert.ToString(scores);
            }
        }

        //Clears scores
        private void btnClearScores_Click(object sender, EventArgs e)
        {
            txtScores.Text = "";
            scores = "";
        }

        //Confirms the creation of a new record
        private void btnOk_Click(object sender, EventArgs e)
        {
            string name = "", record ="";

            //Data validation
            if (IsValidEntry())
            {
                //Unneccessary step but I just convert what is in the txtbox and place it into name
                name = Convert.ToString(txtName.Text);

                //Concats the name and score strings into a single  record with a delimiter
                record = name + "|" + scores;

                //Places record into tag object to be sent back to StudentScores form
                this.Tag = record;

                //Confirms ok of form
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
                MessageBox.Show(name + " must be an Integer number.", "Entry Error");
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

        //Data validation for string data
        public bool IsValidEntry()
        {
            return
                IsPresent(txtName, "Name");
        }

        //Data validation for numerical data
        public bool IsValidData()
        {
            return
                IsPresent(txtScore, "Score")&&
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
