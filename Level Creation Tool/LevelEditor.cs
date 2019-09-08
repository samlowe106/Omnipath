using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Omnipath;

namespace Level_Creation_Tool
{
    public partial class LevelEditor : Form
    {
        #region Fields
        private int specifiedX;
        private int specifiedY;
        private int startX;         // The leftmost possible X value that can be occupied
        private int startY;         // The northernmost possible Y value that can be occupied
        private int endY;           // The southernmost possible Y value that can be occupied
        private int height;         // The total distance from startY to endX
        private int pixelBuffer;    // Just makes the rightmost side of the form look nice
        private bool areUnsavedChanges;
        private int pictureBoxDimensions;   // The X and Y dimensions fo the picture boxes (the "pixels")
        private PictureBox[,] pictureBoxes;
        private Map map;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that does not accept a filename
        /// </summary>
        /// <param name="specifiedX"></param>
        /// <param name="specifiedY"></param>
        public LevelEditor(int specifiedX, int specifiedY)
        {
            InitializeComponent();
            
            this.specifiedX = specifiedX;
            this.specifiedY = specifiedY;

            startX = 7;
            startY = 20;
            endY = 438;
            height = endY - startY; // 425
            pixelBuffer = 20;

            pictureBoxDimensions = height / specifiedX;

            levelGroupBox.Size = new Size((specifiedY * pictureBoxDimensions) + pixelBuffer, 425);

            areUnsavedChanges = false;

            this.Size = new Size(levelGroupBox.Size.Width + 120, 500);

            // Customize the openFileDialog and saveFileDialog
            openFileDialog.Title = "Open a level file";
            openFileDialog.Filter = "Level Files| *.level";

            saveFileDialog.Title = "Choose your save location";
            saveFileDialog.Filter = "Level Files| *.level";

            // Make the specified number of picture boxes, put them in position, and ensure they change position on MouseOver
            pictureBoxes = new PictureBox[specifiedX, specifiedY];
            for (int yPosition = 0; yPosition < specifiedY; ++yPosition)
            {
                for (int xPosition = 0; xPosition < specifiedX; ++xPosition)
                {
                    // Make a new square picture box at the current X/Y postion
                    PictureBox pixel = new PictureBox();
                    pixel.Size = new Size(pictureBoxDimensions, pictureBoxDimensions);
                    pixel.Location = new Point(startX + xPosition * pictureBoxDimensions, startY + yPosition * pictureBoxDimensions);

                    // Make the pictureBox white
                    pixel.BackColor = Color.White;

                    // Make sure the pictureBox changes color when clicked
                    pixel.Click += pictureBoxPixel_Click;

                    // Add the pictureBox to the array
                    pictureBoxes[xPosition % specifiedX, yPosition % specifiedY] = pixel;

                    // Add the pictureBox to the levelGroupBox
                    this.levelGroupBox.Controls.Add(pixel);
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parameterized constructor that accepts a filename to be opened
        /// </summary>
        /// <param name="specifiedX"></param>
        /// <param name="specifiedY"></param>
        /// <param name="fileName"></param>
        public LevelEditor(string fileName)
        {
            InitializeComponent();

            startX = 7;
            startY = 20;
            endY = 438;
            height = endY - startY; // 425
            pixelBuffer = 20;

            // Customize the openFileDialog and saveFileDialog
            openFileDialog.Title = "Open a level file";
            openFileDialog.Filter = "Level Files| *.level";

            saveFileDialog.Title = "Choose your save location";
            saveFileDialog.Filter = "Level Files| *.level";
            // Load the map from the specified file
            this.map = new Map(fileName, null, width, height, width, height, 64, textures[]);
        }

        // Methods

        /// <summary>
        /// Changes the current color to the selected color
        /// </summary>
        private void pictureBoxRed_Click(object sender, EventArgs e)
        {
            pictureBoxCurrent.BackColor = pictureBoxRed.BackColor;
        }

        /// <summary>
        /// Changes the current color to the selected color
        /// </summary>
        private void pictureBoxBlue_Click(object sender, EventArgs e)
        {
            pictureBoxCurrent.BackColor = pictureBoxBlue.BackColor;
        }

        /// <summary>
        /// Changes the current color to the selected color
        /// </summary>
        private void pictureBoxGreen_Click(object sender, EventArgs e)
        {
            pictureBoxCurrent.BackColor = pictureBoxGreen.BackColor;
        }

        /// <summary>
        /// Changes the current color to the selected color
        /// </summary>
        private void pictureBoxOrange_Click(object sender, EventArgs e)
        {
            pictureBoxCurrent.BackColor = pictureBoxOrange.BackColor;
        }

        /// <summary>
        /// Changes the current color to the selected color
        /// </summary>
        private void pictureBoxGray_Click(object sender, EventArgs e)
        {
            pictureBoxCurrent.BackColor = pictureBoxGray.BackColor;
        }

        /// <summary>
        /// Changes the current color to the selected color
        /// </summary>
        private void pictureBoxBlack_Click(object sender, EventArgs e)
        {
            pictureBoxCurrent.BackColor = pictureBoxBlack.BackColor;
        }

        /// <summary>
        /// Colors the current pixel to the current color
        /// </summary>
        /// <param name="sender"> The picturebox that the user is currently clicking to paint</param>
        /// <param name="e"></param>
        private void pictureBoxPixel_Click(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = pictureBoxCurrent.BackColor;

            // If there weren't already unsaved changes, mark unsaved changes add an * to the form text
            if (!areUnsavedChanges)
            {
                areUnsavedChanges = true;
                this.Text += " *";
            }
        }

        /// <summary>
        /// Save the map to the specified location when the user clicks the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Open the save file dialog; if the user chose not to save the file, just return
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // Establish the stream and reader as null
            FileStream inStream = null;
            BinaryWriter writer = null;
            // Try reading from the file
            try
            {
                inStream = File.OpenWrite(saveFileDialog.FileName);
                writer = new BinaryWriter(inStream);

                // Write a the picture's dimensions
                writer.Write(specifiedX);
                writer.Write(specifiedY);

                // Write each box's dimensions, location X & Y, and its color (as an int)
                foreach (PictureBox box in pictureBoxes)
                {
                    writer.Write(box.Size.Width);
                    writer.Write(box.Location.X);
                    writer.Write(box.Location.Y);
                    writer.Write(box.BackColor.ToArgb());
                }
                // Inform the user that the saved correctly
                MessageBox.Show("File saved", "File saved");
                areUnsavedChanges = false;
                // Change the form name to the current file name
                this.Text = saveFileDialog.FileName;
            }
            // Catch any exceptions and print their error message (e already defined elsewhere)
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            // Finally, close the file if it was opened
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }  
        }

        /// <summary>
        /// Calls LoadMap if the user selects a file from the file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            // If the user selected a file, hide this form and open the level in the editor
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadMap(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Load the map from a file and display it
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadMap(string fileName)
        {
            // If they exist, unload all the previous pictureBoxes
            if (pictureBoxes != null)
            {
                foreach (PictureBox box in pictureBoxes)
                {
                    box.Hide();
                    this.levelGroupBox.Controls.Remove(box);
                }
            }

            // Establish the stream and reader as null
            FileStream inStream = null;
            BinaryReader reader = null;
            // Try reading from the file
            try
            {
                inStream = File.OpenRead(fileName);
                reader = new BinaryReader(inStream);

                // Reading the X and Y dimensions of the map
                specifiedX = reader.ReadInt32();
                specifiedY = reader.ReadInt32();

                // Establish the pictureBox dimensions and form width accordingly
                pictureBoxDimensions = height / specifiedX;

                levelGroupBox.Size = new Size((specifiedY * pictureBoxDimensions) + pixelBuffer, 425);

                areUnsavedChanges = false;

                this.Size = new Size(levelGroupBox.Size.Width + 120, 500);

                // (Re-) Initialize the array of pictureboxes with new dimensions
                pictureBoxes = new PictureBox[specifiedX, specifiedY];

                for (int i = 0; i < specifiedX * specifiedY; ++i)
                {
                    PictureBox pictureBox = new PictureBox();

                    pictureBoxDimensions = reader.ReadInt32();

                    pictureBox.Size = new Size(pictureBoxDimensions, pictureBoxDimensions);
                    pictureBox.Location = new Point(reader.ReadInt32(), reader.ReadInt32());
                    pictureBox.BackColor = Color.FromArgb(reader.ReadInt32());

                    // Ensure the box reacts correctly to being clicked
                    pictureBox.Click += pictureBoxPixel_Click;

                    // Add the box to the array
                    pictureBoxes[i / specifiedX, i % specifiedY] = pictureBox;

                    // Add the pictureBox to the form
                    this.levelGroupBox.Controls.Add(pictureBox);
                }

                // Inform the user that the file loaded correctly
                MessageBox.Show("File loaded correctly", "File loaded");
                // Change the form name to the file's name
                this.Text = fileName;
            }
            // Catch any exceptions and print their error message
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            // Finally, close the file if it was opened
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private void levelGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void LevelEditor_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// If there are unsaved changes, ask the user if they're sure; close the form if they are
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevelEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If there are unsaved changes, ask the user if they're sure; close if they are
            if (areUnsavedChanges)
            {
                DialogResult result = MessageBox.Show("There are unsaved changes. Are you sure you want to quit?",
                    "Unsaved changes",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion
    }
}
