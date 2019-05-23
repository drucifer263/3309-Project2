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
    public partial class frmUpdateStudentScores : Form
    {
        //Form updates an existing student record; It is called by StudentScores form (Main form)
        public frmUpdateStudentScores()
        {
            InitializeComponent();
        }

        private string[] scores;

        //Adds a new score to an existing record
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int scoreIndex = lstScores.SelectedIndex, insert = lstScores.Items.Count;
            string newScore = "";
           
            //Declares and instaniates an AddScore form
            frmAddScore newScoreForm = new frmAddScore();

            //Declares and initilizes a custom dialog box from newScoreForm
            DialogResult selectedButton = newScoreForm.ShowDialog();

            try {

                //Validates that there is an item in the list box that is selected
                //  if (ItemPresent(scoreIndex))
                //   {


                //Tests to see if the ok button on the newScoreForm is pressed
                if (selectedButton == DialogResult.OK)
                {
                    //Converts the tag object from newScoreForm into a string
                    newScore = Convert.ToString(newScoreForm.Tag);

                    //Adds the string to the list box
                    lstScores.Items.Add(newScore);

                    //Highlights the first entry in the list box
                    lstScores.SelectedIndex = 0;    
                }
                
                // }
            }

            //Exception catch all
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }

            
        }

        //Updates an existing score
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int scoreIndex = lstScores.SelectedIndex;
            string singleScore ="";

            //Declares and instantiates an UpdateScoreForm
            frmUpdateScore updateSingleScore = new frmUpdateScore();

            //Creates a custom dialog box with a confirm button
            DialogResult selectedButton = updateSingleScore.ShowDialog();

            //Validates there is something in the listbox
            if (ItemPresent(scoreIndex))
            {
                //Ok button confirm
                if (selectedButton == DialogResult.OK)
                {
                    //Converts the tag object sent from UpdateScore form
                    singleScore = Convert.ToString(updateSingleScore.Tag);

                    //Removes the entry at a specific index from list box
                    lstScores.Items.RemoveAt(scoreIndex);

                    //Replaces the entry at a specific index
                    lstScores.Items.Insert(scoreIndex, singleScore);

                    //Changes the focus to the first item in the list 
                    lstScores.SelectedIndex = 0;
                }
            }

        }

        //Removes a score from the list
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int scoreIndex = lstScores.SelectedIndex;
            string item = Convert.ToString(lstScores.SelectedItem);
            string message = "Are you sure you want to delete " + item + "?";
            DialogResult button;

            try {

                if (ItemPresent(scoreIndex))
                {
                    //Custom confirmation message before deletion
                    button = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo);

                    //Test the confirmation 
                    if (button == DialogResult.Yes)
                    {
                        //Removes entry at specific index and redirects focus to first element
                        lstScores.Items.RemoveAt(scoreIndex);
                        lstScores.SelectedIndex = 0;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }

        }

        //Clears scores
        private void btnClearScores_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to clear all scores?";
            DialogResult button;

            //Custom clear confirmation
            button = MessageBox.Show(message, "Confirm Clear", MessageBoxButtons.YesNo);

            //Confirms choice
            if (button == DialogResult.Yes)
            {
                //Clears list box and scores int array
                scores = null;
                lstScores.Items.Clear();
            }
            
        }

        //Confirms changes made to records
        private void btnOk_Click(object sender, EventArgs e)
        {
            int itemCount = lstScores.Items.Count;
            string newScores ="", newRecord ="", name = "";
           
            //Takes the entry in the name txt box and places into name
            name = Convert.ToString(txtName.Text);

            //Places a delimiter at the end of the string
            name = name + "|";

            //Adds all the entries in the list box and places them into a single string 
            for (int i = 0; i < itemCount; i++)
            {
                newScores += lstScores.Items[i] + " ";
            }

            //Concats the name and scores into a single record
            newRecord = name + newScores;
       
            //Places that record into the tag object to be sent back to the StudentScores form
            this.Tag = newRecord;

            //Confirmation
            this.DialogResult = DialogResult.OK;
        }

        //Closes form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Fills the form with data from the StudentScores form when loaded
        private void frmUpdateStudentScores_Load(object sender, EventArgs e)
        {
            FillForm();
        }

        //Fills the txt and list box with the correct data from the StudentScores form
        private void FillForm()
        {
            int scoresIndex, nameIndex;
            string recordUpdate = "", name = "", score = "";

            //Places the static string from StudentScores into a new string
            recordUpdate = frmStudentScores.record;

            try
            {
                nameIndex = recordUpdate.Length;

                //Trims the string
                recordUpdate = recordUpdate.Trim();
                
                //Gets the index of the delimiter in the string
                scoresIndex = recordUpdate.IndexOf("|");

                //Creates a substring of the firt portion of the record and places into name 
                name = recordUpdate.Substring(0, scoresIndex);

                //Places name into txtbox
                txtName.Text = name;

                //Gets the rest of the substring for the scores
                score = recordUpdate.Substring(scoresIndex + 1);

                //Splits the scores into an array of strings
                scores = score.Split();

                //Adds each element of the string array to the list box
                foreach (string s in scores)
                {
                    lstScores.Items.Add(s);
                }

                //Redirects the focus back to the first element
                lstScores.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }
        }

        //Validation
        private bool ItemPresent(int index)
        {
            bool present;

            if(index == -1)
            {
                MessageBox.Show("You must select an Item from the list.");
                present = false;
            }

            else
            {
                present = true;
            }

            return present;
        }
    }
}
