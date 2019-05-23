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
    public partial class frmStudentScores : Form
    {
        //Form allows you to add, update, or delete a student record; It is the Main form
        public frmStudentScores()
        {
            InitializeComponent();
        }

        //Public static string allows UpdateStudentScores form to access records from StudentScores
        public static string record = "";

        //Adds a new student record
        private void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            int studentIndex = lstStudent.SelectedIndex;

            //Instantiates and declares an AddNewStudent form
            frmAddNewStudent newStudentForm = new frmAddNewStudent();

            DialogResult selectedButton = newStudentForm.ShowDialog();

            try
            {
                //  if (StudentPresent(studentIndex))
                // {

                if (selectedButton == DialogResult.OK)
                {
                    //Converts tag object from AddNewStudent form to a string
                    record = Convert.ToString(newStudentForm.Tag);

                    //Places string record into list box and redirects focus back to first entry
                    lstStudent.Items.Add(record);
                    lstStudent.SelectedIndex = 0;

                }
            }

                // }
            //Exception catch all
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }
            
        }

        //Updates the  scores of the student record
        private void btnUpdateStudentScores_Click(object sender, EventArgs e)
        {
            //Casts the object from the list box into a string
            record = (string)lstStudent.SelectedItem;

            //Instantiates and declares an UpdateStudentScores form
            frmUpdateStudentScores updateScores = new frmUpdateStudentScores();
            DialogResult selectedButton = updateScores.ShowDialog();

            //Gets the index of selected item in list box
            int studentIndex = lstStudent.SelectedIndex;

            try {

                if (StudentPresent(studentIndex))
                {

                    if (selectedButton == DialogResult.OK)
                    {
                        //Converts tag object from AddNewStudent form to a string
                        record = Convert.ToString(updateScores.Tag);

                        //Removes entry at specific index
                        lstStudent.Items.RemoveAt(studentIndex);

                        //Places string record into list box where the previous record was deleted and redirects focus back to first entry
                        lstStudent.Items.Insert(studentIndex, record);
                        lstStudent.SelectedIndex = 0;
                    }


                }
            }

            //Exception catch all
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }
        }

        //Deletes a record
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int studentIndex = lstStudent.SelectedIndex;
            string item = Convert.ToString(lstStudent.SelectedItem);
            string message = "Are you sure you want to delete " + item + "?";
            DialogResult button;

            try {

                if (StudentPresent(studentIndex))
                {
                    //Custom confirmation message before deletion
                    button = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo);

                    //Test the confirmation 
                    if (button == DialogResult.Yes)
                    {
                        //Removes a record and redirects forcus back to first entry
                        lstStudent.Items.RemoveAt(studentIndex);
                        lstStudent.SelectedIndex = 0;
                    }
                }
            }

            //Exception catch all
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }
        }

        //Checks to see if there is an entry selected
        private bool StudentPresent(int index)
        {
            bool present;

            if (index == -1)
            {
                MessageBox.Show("You must select a Student from the list or create one.");
                present = false;
            }

            else
            {
                present = true;
            }

            return present;
        }

        //Check that the txtbox is empty or not
        private bool EntryPresent(TextBox textBox)
        {
            bool notPresent;

            if (textBox.Text == " ")
            {
                notPresent = true;
            }

            else
            {
                MessageBox.Show("Something is in the txtbox.");
                notPresent = false;
            }

            return notPresent;
        }

        //Closes form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Load default records into list box when form loads
        private void frmStudentScores_Load(object sender, EventArgs e)
        {
            FillDefaults();

        }

        //Defaults loaded into list box
        private void FillDefaults()
        {
            record = "Joel Murach|34 23 23";
            lstStudent.Items.Add(record);

            record = "Karry Murach|100 33 0";
            lstStudent.Items.Add(record);

            record = "Joel Burach|99 3 0";
            lstStudent.Items.Add(record);

            lstStudent.SelectedIndex = 0;
        }

        //Calculates scores of an entry
        private void GetScores()
        {
            string studentRecord = "", name ="",stringScores="";
            string[] converts;
            int[] intScores;
            int length = 0, number = 0, scoreTotal = 0, scoreCount = 0;
            double average =0;

            //Gets the item from list box
            studentRecord =Convert.ToString(lstStudent.SelectedItem);

            int scoresIndex, nameIndex;
           

            try
            {
                if (IsPresent(lstStudent))
                {
                    //Gets the length of the string
                    nameIndex = studentRecord.Length;

                    //Trims record
                    studentRecord = studentRecord.Trim();
                    
                    //Finds index of delimiter
                    scoresIndex = studentRecord.IndexOf("|");
                    
                    //Dont really use it but put the first half of the record into name
                    name = studentRecord.Substring(0, scoresIndex);
                    
                    //Gets the second half of the string, the scores, and places them into a string
                    stringScores = studentRecord.Substring(scoresIndex + 1);

                    //Splits string by a space and places each element into a string array
                    converts = stringScores.Split(' ');

                    //Gets the length of the string array
                    length = converts.Length;

                    //Initilizes the int array with the length of the string array
                    intScores = new int[length];

                    

                    //Checks to see if scores is null
                    if (converts != null)
                    {
                        //Converts each element in the string array to an int,then places each int into an int array
                        for (int i = 0; i < length; i++)
                        {
                            number = Convert.ToInt32(converts[i]);
                            intScores[i] = number;
                        }
                    }

                    //If scores null/empty then all scores converted to zero
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            number = 0;
                            intScores[i] = number;
                        }
                    }
                    

                    //Gets the elements in the array just incase
                    scoreCount = intScores.Length;

                    //Displays the score count
                    txtScoreCount.Text = Convert.ToString(scoreCount);

                    //Adds up each element in the array and gets a total of the scores
                    foreach (int s in intScores)
                    {
                        scoreTotal += s;
                    }

                    //Displays the scores
                    txtScoreTotal.Text = Convert.ToString(scoreTotal);

                    //Calculates the average and displays it
                    average = scoreTotal / scoreCount;
                    txtAverage.Text = Convert.ToString(average);

                }
            }

            //Exception catch all
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }
        }

        //Clears total, average, and count txt box if nothing is in list box
        public bool IsPresent(ListBox listBox)
        {
            if (listBox.Text == "")
            {

                txtAverage.Text ="";
                txtScoreTotal.Text = "";
                txtScoreCount.Text = "";
                return false;

            }

            return true;
            
        }

        //When a different item is select the scores change
        private void lstStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetScores();
        }
    }
}
