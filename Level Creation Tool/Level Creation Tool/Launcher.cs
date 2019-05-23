using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Level_Creation_Tool
{
    public partial class MainPage : Form
    {

        // Fields
        LevelEditor levelEditorForm;

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lets the user open the leveleditor from a specific file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            // Open the file dialog and let the user choose a file to open and edit
            openFileDialog.Title = "Open a level file";
            openFileDialog.Filter = "Level Files| *.level";
            
            // If the user selected a file, hide this form and open the level in the editor
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Hide();
                levelEditorForm = new LevelEditor(openFileDialog.FileName);
                levelEditorForm.ShowDialog();
                // Close this form when the next form is closed
                this.Close();
            }
        }

        /// <summary>
        /// Checks if the user's parameters are correct and creates a new level in the editor if they are
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click(object sender, EventArgs e)
        {
            // Establish if width is an integer in a valid range
            bool widthIsInt = Int32.TryParse(widthTextBox.Text, out int width);
            bool widthValueValid = widthIsInt && (10 <= width && width <= 30);
            // Establish if height is an integer in a valid range
            bool heightIsInt = Int32.TryParse(heightTextBox.Text, out int height);
            bool heightValueValid = heightIsInt && (10 <= height && height <= 30);

            // If both width and height are valid, create a new level, hide this form, and open the editor
            if (widthValueValid && heightValueValid)
            {
                this.Hide();
                levelEditorForm = new LevelEditor(width, height);
                levelEditorForm.ShowDialog();
                // Close this form when the next form is closed
                this.Close();
            }

            // Otherwise, just print out any error messages
            string errorMessage = "Errors:\n";

            // If the width isn't an integer, prompt the user for a valid input
            if (!widthIsInt)
            {
                errorMessage += " -" + "Width isn't a valid number! Please input an integer from 10 to 30\n";
            }
            // If width just isn't in a valid range, prompt the user for a valid int
            else if (!widthValueValid)
            {
                errorMessage += " -" + "Please input an integer from 10 to 30 for width\n";
            }
            // If the width isn't an integer, prompt the user for a valid input
            if (!heightIsInt)
            {
                errorMessage += " -" + "Height isn't a valid number! Please input an integer from 10 to 30";
            }
            // If width just isn't in a valid range, prompt the user for a valid int
            else if (!heightValueValid)
            {
                errorMessage += " -" + "Please input an integer from 10 to 30 for height";
            }

            // Display the errors in a MessageBox
            MessageBox.Show(errorMessage);
        }
    }
}
