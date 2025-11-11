using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Drawing;
using System.IO; 
using System.Windows.Forms;

namespace CitiConnect
{
    public partial class ReportIssuesForm : Form
    {
        // Appropiate for data structure for storing categories
        // List of categories
        private List<string> categories = new List<string>
        {
            "Crime & Safety",
            "Environmental Concerns",
            "Roads",
            "Sanitation",
            "Utilities",
            "Other"
        };

        // Store the selected file path
        private string attachedFilePath = string.Empty;

        // To track form completion
        private bool locationEntered = false;
        private bool categorySelected = false;
        private bool descriptionEntered = false;
        private bool fileAttached = false;

        public ReportIssuesForm()
        {
            InitializeComponent();

            // Populate the ComboBox with the list of categories
            cmbCategory.Items.AddRange(categories.ToArray());

            // Initialize progress bar
            progressBar1.Value = 0; // Start progress at 0%
            lblProgressStatus.Text = "Form Incomplete"; 
            lblProgressStatus.ForeColor = Color.Red; // Set to red initially for incomplete status

            // Set initial event handlers
            txtLocation.TextChanged += new EventHandler(this.FieldsChanged);
            cmbCategory.SelectedIndexChanged += new EventHandler(this.FieldsChanged);
            rtbDescription.TextChanged += new EventHandler(this.FieldsChanged);
        }

        private void ReportIssuesForm_Load(object sender, EventArgs e)
        {

        }

        // This method was adapted from AL John Villareal and modified by ChatGPT
        // YouTube video: Progress Bar - C# Windows Forms Controls
        // Link to YouTube video: https://www.google.com/search?sca_esv=c029e4d275f10b11&sca_upv=1&rlz=1C1CHBF_enZA1016ZA1016&sxsrf=ADLYWIJrgvbEKN0DuiJZrVEqn9dxXu1nNA:1726081716839&q=progress+bar+for+achievements+in+windows+forms+c%23&tbm=vid&source=lnms&fbs=AEQNm0CFDpRHaDHkXm_YXueHTfHtrgIXUKlluACpCix4T5ZoUSz6e3GWv4zN_09JkP2cR-DwqD-ER1CSuTjXzdXNKT0Wma9mNyum3oWLzhs1xt8u5GJdCx58_F0ZS8HPQnKQz9WgsdJpThHV1BWzodOO8E_BatEjT0dIL3U5ECu78v7bqTooU6tI88M-Qrvjqh0VwmagWSscc3JaO_0BmvDToqXs55uEqQ&sa=X&ved=2ahUKEwiVpNe8y7uIAxXOV0EAHQ-WHtMQ0pQJegQICBAB&biw=767&bih=730&dpr=1.25#fpstate=ive&vld=cid:9e5a16d8,vid:fz98QdT5YJA,st:0
        // Link to YouTube channel: https://www.youtube.com/@aljohnvillareal1267
        // Link to ChatGPT: https://chatgpt.com
        // This method checks the status of each field and updates the progress bar
        private void UpdateProgressBar()
        {
            int progress = 0;

            // Check if each field is filled and increment progress accordingly
            if (locationEntered) progress += 25;
            if (categorySelected) progress += 25;
            if (descriptionEntered) progress += 25;
            if (fileAttached) progress += 25;

            // Update the progress bar value
            progressBar1.Value = progress;

            // Update progress status label and its color based on completion
            if (progress == 100)
            {
                lblProgressStatus.Text = "Form Completed!";
                lblProgressStatus.ForeColor = Color.Green; // Change to green when form is completed
                label7.Text = "Click the Submit button below to submit your issue";
            }
            else
            {
                lblProgressStatus.Text = "Form Incomplete";
                lblProgressStatus.ForeColor = Color.Red; // Keep the color red if the form is incomplete
            }
        }

        // Event handler for text changes or combo box selection
        private void FieldsChanged(object sender, EventArgs e)
        {
            // Track if each field has valid input
            locationEntered = !string.IsNullOrEmpty(txtLocation.Text);
            categorySelected = cmbCategory.SelectedItem != null;
            descriptionEntered = !string.IsNullOrEmpty(rtbDescription.Text);

            // Update the progress bar based on the current state
            UpdateProgressBar();
        }


        // This code was adapted from The Software Creators 
        // YouTube video: How To Make a Open File Dialog Window | Visual Studio | Visual Basic
        // YouTube video link: https://www.youtube.com/watch?v=-yMo8apiMdM
        // Link to the YouTube Channel: https://www.youtube.com/@TheSoftwareCreators
        // The button to attach a media file and display the file name 
        private void btnAttachMedia_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Opens the File dialog 

            // Filters which files to look for 
            openFileDialog.Filter = "Image or Document Files|*.jpg;*.jpeg;*.png;*.pdf;*.docx;*.xlsx|All Files|*.*"; 
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Store the selected file path
                attachedFilePath = openFileDialog.FileName;

                // Display the file name and an icon next to it
                FileInfo fileInfo = new FileInfo(attachedFilePath);
                Icon icon = Icon.ExtractAssociatedIcon(attachedFilePath); // Extract the file's associated icon
                pictureBoxAttachment.Image = icon.ToBitmap(); // Show the file's icon
                lblAttachment.Text = fileInfo.Name; // Display the file name

                fileAttached = true; // Mark that the file is attached

                // Update the progress bar
                UpdateProgressBar();

                // Enable click event to open the file externally
                lblAttachment.Click += new EventHandler(OpenAttachment);
                pictureBoxAttachment.Click += new EventHandler(OpenAttachment); // Clicking the icon also opens the file
            }
        }

        // This code was taken from ChatGPT 
        // Link to ChatGPT: https://chatgpt.com
        // To open an attachment externally 
        private void OpenAttachment(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(attachedFilePath))
            {
                // Open the attached file with its default application
                try
                {
                    Process.Start(new ProcessStartInfo(attachedFilePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to open the file. Error: " + ex.Message);
                }
            }
        }

        // The button to submit a form 
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Collect data from the form
            string location = txtLocation.Text;
            string category = cmbCategory.SelectedItem?.ToString(); // Null-conditional operator to avoid crash if not selected
            string description = rtbDescription.Text;

            // Validate that all fields are filled
            if (string.IsNullOrEmpty(location))
            {
                MessageBox.Show("Please enter a location.");
                return;
            }

            if (string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please enter a description.");
                return;
            }

            // Store the report in a list
            ReportIssue(location, category, description, attachedFilePath);

            MessageBox.Show("Issue reported successfully!");

            // Clear the form
            ClearForm();
        }

        // This code was adapted from C# Corner
        // Title: How To Create A List Of Objects In C#?
        // Link: https://www.c-sharpcorner.com/UploadFile/mahesh/create-a-list-of-objects-in-C-Sharp/
        // Author: Mahesh Chand 
        // Link to author's page: https://www.c-sharpcorner.com/members/mahesh-chand
        private void ReportIssue(string location, string category, string description, string attachment)
        {
            // Data structure for storing issues 
            List<Issue> issues = new List<Issue>();
            issues.Add(new Issue
            {
                Location = location,
                Category = category,
                Description = description,
                Attachment = attachment
            });

            // Feedback to user
            lblFeedback.Text = "Your report is being processed...";
        }

        // Issue object 
        public class Issue
        {
            public string Location { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string Attachment { get; set; }
        }

        // Back button to close the form and go back to the main menu
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.OpenChildForm(new Home(), sender);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Method to clear the form and reset everything
        private void ClearForm()
        {
            txtLocation.Clear();
            rtbDescription.Clear();
            lblAttachment.Text = string.Empty;
            pictureBoxAttachment.Image = null; // Clear the attachment icon
            attachedFilePath = string.Empty; // Clear the file path

            // Reset progress and feedback
            locationEntered = false;
            categorySelected = false;
            descriptionEntered = false;
            fileAttached = false;
            UpdateProgressBar(); // Reset the progress bar

            lblFeedback.Text = string.Empty; // Clear the feedback label
        }
    }
}
