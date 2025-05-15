using Konecta.Tools.CCaptureClient.Core.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics; // Required for Process.Start
using System.IO; // Required for File.Exists

namespace Konecta.Tools.CCaptureClient.UI.Forms
{
    public partial class SubmissionDetailsForm : Form
    {
        public SubmissionDetailsForm(SubmissionDetailsModel details)
        {
            InitializeComponent();
            PopulateDetails(details);
            // Register the double-click event handler for dataGridViewDocuments
            dataGridViewDocuments.CellDoubleClick += DataGridViewDocuments_CellDoubleClick;
        }

        private void PopulateDetails(SubmissionDetailsModel details)
        {
            if (details == null)
            {
                MessageBox.Show("No submission details found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Submission Details
            txtGroupName.Text = details.GroupName;
            txtBatchClassName.Text = details.Submission.BatchClassName;
            txtSourceSystem.Text = details.Submission.SourceSystem;
            txtChannel.Text = details.Submission.Channel;
            txtSessionId.Text = details.Submission.SessionId;
            txtMessageId.Text = details.Submission.MessageId;
            txtUserCode.Text = details.Submission.UserCode;
            txtInteractionDateTime.Text = details.Submission.InteractionDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            txtRequestGuid.Text = details.Submission.RequestGuid;
            txtSubmittedAt.Text = details.Submission.SubmittedAt?.ToString("yyyy-MM-dd HH:mm:ss");

            // Documents
            dataGridViewDocuments.Rows.Clear();
            foreach (var doc in details.Documents)
            {
                dataGridViewDocuments.Rows.Add(
                    doc.FileName,
                    doc.PageType,
                    doc.FilePath,
                    doc.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss")
                );
            }

            // Fields
            dataGridViewFields.Rows.Clear();
            foreach (var field in details.Fields)
            {
                dataGridViewFields.Rows.Add(
                    field.FieldName,
                    field.FieldValue,
                    field.FieldType,
                    field.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss")
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridViewDocuments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is on a valid row (not the header)
            if (e.RowIndex >= 0)
            {
                // Get the file path from the FilePath column (index 2)
                string filePath = dataGridViewDocuments.Rows[e.RowIndex].Cells[2].Value?.ToString();

                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("No file path specified for this document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // Check if the file exists
                    if (File.Exists(filePath))
                    {
                        // Open the file with the default application
                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                    }
                    else
                    {
                        MessageBox.Show($"The file '{filePath}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}