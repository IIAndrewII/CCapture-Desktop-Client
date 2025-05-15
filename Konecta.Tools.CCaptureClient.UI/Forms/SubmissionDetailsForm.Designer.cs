namespace Konecta.Tools.CCaptureClient.UI.Forms
{
    partial class SubmissionDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblSubmissionDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label lblBatchClassName;
        private System.Windows.Forms.TextBox txtBatchClassName;
        private System.Windows.Forms.Label lblSourceSystem;
        private System.Windows.Forms.TextBox txtSourceSystem;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.Label lblSessionId;
        private System.Windows.Forms.TextBox txtSessionId;
        private System.Windows.Forms.Label lblMessageId;
        private System.Windows.Forms.TextBox txtMessageId;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Label lblInteractionDateTime;
        private System.Windows.Forms.TextBox txtInteractionDateTime;
        private System.Windows.Forms.Label lblRequestGuid;
        private System.Windows.Forms.TextBox txtRequestGuid;
        private System.Windows.Forms.Label lblSubmittedAt;
        private System.Windows.Forms.TextBox txtSubmittedAt;
        private System.Windows.Forms.Label lblDocuments;
        private System.Windows.Forms.DataGridView dataGridViewDocuments;
        private System.Windows.Forms.Label lblFields;
        private System.Windows.Forms.DataGridView dataGridViewFields;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            lblSubmissionDetails = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblGroupName = new Label();
            txtGroupName = new TextBox();
            lblBatchClassName = new Label();
            txtBatchClassName = new TextBox();
            lblSourceSystem = new Label();
            txtSourceSystem = new TextBox();
            lblChannel = new Label();
            txtChannel = new TextBox();
            lblSessionId = new Label();
            txtSessionId = new TextBox();
            lblMessageId = new Label();
            txtMessageId = new TextBox();
            lblUserCode = new Label();
            txtUserCode = new TextBox();
            lblInteractionDateTime = new Label();
            txtInteractionDateTime = new TextBox();
            lblRequestGuid = new Label();
            txtRequestGuid = new TextBox();
            lblSubmittedAt = new Label();
            txtSubmittedAt = new TextBox();
            lblDocuments = new Label();
            dataGridViewDocuments = new DataGridView();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            lblFields = new Label();
            dataGridViewFields = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            btnClose = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDocuments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFields).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblSubmissionDetails, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(lblDocuments, 0, 2);
            tableLayoutPanel1.Controls.Add(dataGridViewDocuments, 0, 3);
            tableLayoutPanel1.Controls.Add(lblFields, 0, 4);
            tableLayoutPanel1.Controls.Add(dataGridViewFields, 0, 5);
            tableLayoutPanel1.Controls.Add(btnClose, 0, 6);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 331F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.Size = new Size(800, 600);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblSubmissionDetails
            // 
            lblSubmissionDetails.AutoSize = true;
            lblSubmissionDetails.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSubmissionDetails.Location = new Point(3, 0);
            lblSubmissionDetails.Name = "lblSubmissionDetails";
            lblSubmissionDetails.Size = new Size(207, 30);
            lblSubmissionDetails.TabIndex = 0;
            lblSubmissionDetails.Text = "Submission Details";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 5;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(lblGroupName, 0, 0);
            tableLayoutPanel2.Controls.Add(txtGroupName, 0, 1);
            tableLayoutPanel2.Controls.Add(lblBatchClassName, 2, 0);
            tableLayoutPanel2.Controls.Add(txtBatchClassName, 2, 1);
            tableLayoutPanel2.Controls.Add(lblSourceSystem, 4, 0);
            tableLayoutPanel2.Controls.Add(txtSourceSystem, 4, 1);
            tableLayoutPanel2.Controls.Add(lblChannel, 0, 2);
            tableLayoutPanel2.Controls.Add(txtChannel, 0, 3);
            tableLayoutPanel2.Controls.Add(lblSessionId, 2, 2);
            tableLayoutPanel2.Controls.Add(txtSessionId, 2, 3);
            tableLayoutPanel2.Controls.Add(lblMessageId, 4, 2);
            tableLayoutPanel2.Controls.Add(txtMessageId, 4, 3);
            tableLayoutPanel2.Controls.Add(lblUserCode, 0, 4);
            tableLayoutPanel2.Controls.Add(txtUserCode, 0, 5);
            tableLayoutPanel2.Controls.Add(lblInteractionDateTime, 2, 4);
            tableLayoutPanel2.Controls.Add(txtInteractionDateTime, 2, 5);
            tableLayoutPanel2.Controls.Add(lblRequestGuid, 4, 4);
            tableLayoutPanel2.Controls.Add(txtRequestGuid, 4, 5);
            tableLayoutPanel2.Controls.Add(lblSubmittedAt, 0, 6);
            tableLayoutPanel2.Controls.Add(txtSubmittedAt, 0, 7);
            tableLayoutPanel2.Location = new Point(3, 53);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 8;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel2.Size = new Size(794, 325);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // lblGroupName
            // 
            lblGroupName.AutoSize = true;
            lblGroupName.Font = new Font("Segoe UI", 12F);
            lblGroupName.Location = new Point(3, 0);
            lblGroupName.Name = "lblGroupName";
            lblGroupName.Size = new Size(103, 21);
            lblGroupName.TabIndex = 0;
            lblGroupName.Text = "Group Name:";
            // 
            // txtGroupName
            // 
            txtGroupName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGroupName.Font = new Font("Segoe UI", 12F);
            txtGroupName.Location = new Point(3, 43);
            txtGroupName.Name = "txtGroupName";
            txtGroupName.ReadOnly = true;
            txtGroupName.Size = new Size(245, 29);
            txtGroupName.TabIndex = 1;
            // 
            // lblBatchClassName
            // 
            lblBatchClassName.AutoSize = true;
            lblBatchClassName.Font = new Font("Segoe UI", 12F);
            lblBatchClassName.Location = new Point(274, 0);
            lblBatchClassName.Name = "lblBatchClassName";
            lblBatchClassName.Size = new Size(137, 21);
            lblBatchClassName.TabIndex = 2;
            lblBatchClassName.Text = "Batch Class Name:";
            // 
            // txtBatchClassName
            // 
            txtBatchClassName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBatchClassName.Font = new Font("Segoe UI", 12F);
            txtBatchClassName.Location = new Point(274, 43);
            txtBatchClassName.Name = "txtBatchClassName";
            txtBatchClassName.ReadOnly = true;
            txtBatchClassName.Size = new Size(245, 29);
            txtBatchClassName.TabIndex = 2;
            // 
            // lblSourceSystem
            // 
            lblSourceSystem.AutoSize = true;
            lblSourceSystem.Font = new Font("Segoe UI", 12F);
            lblSourceSystem.Location = new Point(545, 0);
            lblSourceSystem.Name = "lblSourceSystem";
            lblSourceSystem.Size = new Size(116, 21);
            lblSourceSystem.TabIndex = 4;
            lblSourceSystem.Text = "Source System:";
            // 
            // txtSourceSystem
            // 
            txtSourceSystem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSourceSystem.Font = new Font("Segoe UI", 12F);
            txtSourceSystem.Location = new Point(545, 43);
            txtSourceSystem.Name = "txtSourceSystem";
            txtSourceSystem.ReadOnly = true;
            txtSourceSystem.Size = new Size(246, 29);
            txtSourceSystem.TabIndex = 3;
            // 
            // lblChannel
            // 
            lblChannel.AutoSize = true;
            lblChannel.Font = new Font("Segoe UI", 12F);
            lblChannel.Location = new Point(3, 80);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(70, 21);
            lblChannel.TabIndex = 6;
            lblChannel.Text = "Channel:";
            // 
            // txtChannel
            // 
            txtChannel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtChannel.Font = new Font("Segoe UI", 12F);
            txtChannel.Location = new Point(3, 123);
            txtChannel.Name = "txtChannel";
            txtChannel.ReadOnly = true;
            txtChannel.Size = new Size(245, 29);
            txtChannel.TabIndex = 4;
            // 
            // lblSessionId
            // 
            lblSessionId.AutoSize = true;
            lblSessionId.Font = new Font("Segoe UI", 12F);
            lblSessionId.Location = new Point(274, 80);
            lblSessionId.Name = "lblSessionId";
            lblSessionId.Size = new Size(85, 21);
            lblSessionId.TabIndex = 8;
            lblSessionId.Text = "Session ID:";
            // 
            // txtSessionId
            // 
            txtSessionId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSessionId.Font = new Font("Segoe UI", 12F);
            txtSessionId.Location = new Point(274, 123);
            txtSessionId.Name = "txtSessionId";
            txtSessionId.ReadOnly = true;
            txtSessionId.Size = new Size(245, 29);
            txtSessionId.TabIndex = 5;
            // 
            // lblMessageId
            // 
            lblMessageId.AutoSize = true;
            lblMessageId.Font = new Font("Segoe UI", 12F);
            lblMessageId.Location = new Point(545, 80);
            lblMessageId.Name = "lblMessageId";
            lblMessageId.Size = new Size(93, 21);
            lblMessageId.TabIndex = 10;
            lblMessageId.Text = "Message ID:";
            // 
            // txtMessageId
            // 
            txtMessageId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtMessageId.Font = new Font("Segoe UI", 12F);
            txtMessageId.Location = new Point(545, 123);
            txtMessageId.Name = "txtMessageId";
            txtMessageId.ReadOnly = true;
            txtMessageId.Size = new Size(246, 29);
            txtMessageId.TabIndex = 6;
            // 
            // lblUserCode
            // 
            lblUserCode.AutoSize = true;
            lblUserCode.Font = new Font("Segoe UI", 12F);
            lblUserCode.Location = new Point(3, 160);
            lblUserCode.Name = "lblUserCode";
            lblUserCode.Size = new Size(64, 21);
            lblUserCode.TabIndex = 12;
            lblUserCode.Text = "User Code:";
            // 
            // txtUserCode
            // 
            txtUserCode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUserCode.Font = new Font("Segoe UI", 12F);
            txtUserCode.Location = new Point(3, 203);
            txtUserCode.Name = "txtUserCode";
            txtUserCode.ReadOnly = true;
            txtUserCode.Size = new Size(245, 29);
            txtUserCode.TabIndex = 7;
            // 
            // lblInteractionDateTime
            // 
            lblInteractionDateTime.AutoSize = true;
            lblInteractionDateTime.Font = new Font("Segoe UI", 12F);
            lblInteractionDateTime.Location = new Point(274, 160);
            lblInteractionDateTime.Name = "lblInteractionDateTime";
            lblInteractionDateTime.Size = new Size(161, 21);
            lblInteractionDateTime.TabIndex = 14;
            lblInteractionDateTime.Text = "Interaction Date Time:";
            // 
            // txtInteractionDateTime
            // 
            txtInteractionDateTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInteractionDateTime.Font = new Font("Segoe UI", 12F);
            txtInteractionDateTime.Location = new Point(274, 203);
            txtInteractionDateTime.Name = "txtInteractionDateTime";
            txtInteractionDateTime.ReadOnly = true;
            txtInteractionDateTime.Size = new Size(245, 29);
            txtInteractionDateTime.TabIndex = 8;
            // 
            // lblRequestGuid
            // 
            lblRequestGuid.AutoSize = true;
            lblRequestGuid.Font = new Font("Segoe UI", 12F);
            lblRequestGuid.Location = new Point(545, 160);
            lblRequestGuid.Name = "lblRequestGuid";
            lblRequestGuid.Size = new Size(110, 21);
            lblRequestGuid.TabIndex = 16;
            lblRequestGuid.Text = "Request GUID:";
            // 
            // txtRequestGuid
            // 
            txtRequestGuid.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtRequestGuid.Font = new Font("Segoe UI", 12F);
            txtRequestGuid.Location = new Point(545, 203);
            txtRequestGuid.Name = "txtRequestGuid";
            txtRequestGuid.ReadOnly = true;
            txtRequestGuid.Size = new Size(246, 29);
            txtRequestGuid.TabIndex = 9;
            // 
            // lblSubmittedAt
            // 
            lblSubmittedAt.AutoSize = true;
            lblSubmittedAt.Font = new Font("Segoe UI", 12F);
            lblSubmittedAt.Location = new Point(3, 240);
            lblSubmittedAt.Name = "lblSubmittedAt";
            lblSubmittedAt.Size = new Size(104, 21);
            lblSubmittedAt.TabIndex = 18;
            lblSubmittedAt.Text = "Submitted At:";
            // 
            // txtSubmittedAt
            // 
            txtSubmittedAt.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSubmittedAt.Font = new Font("Segoe UI", 12F);
            txtSubmittedAt.Location = new Point(3, 285);
            txtSubmittedAt.Name = "txtSubmittedAt";
            txtSubmittedAt.ReadOnly = true;
            txtSubmittedAt.Size = new Size(245, 29);
            txtSubmittedAt.TabIndex = 10;
            // 
            // lblDocuments
            // 
            lblDocuments.AutoSize = true;
            lblDocuments.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblDocuments.Location = new Point(3, 381);
            lblDocuments.Name = "lblDocuments";
            lblDocuments.Size = new Size(130, 30);
            lblDocuments.TabIndex = 2;
            lblDocuments.Text = "Documents";
            // 
            // dataGridViewDocuments
            // 
            dataGridViewDocuments.AllowUserToAddRows = false;
            dataGridViewDocuments.AllowUserToDeleteRows = false;
            dataGridViewDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDocuments.ColumnHeadersHeight = 40;
            dataGridViewDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewDocuments.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8 });
            dataGridViewDocuments.Dock = DockStyle.Fill;
            dataGridViewDocuments.Font = new Font("Segoe UI", 11F);
            dataGridViewDocuments.Location = new Point(3, 416);
            dataGridViewDocuments.Name = "dataGridViewDocuments";
            dataGridViewDocuments.ReadOnly = true;
            dataGridViewDocuments.RowHeadersWidth = 51;
            dataGridViewDocuments.RowTemplate.Height = 30;
            dataGridViewDocuments.Size = new Size(794, 39);
            dataGridViewDocuments.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "File Name";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Page Type";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "File Path";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "Created At";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // lblFields
            // 
            lblFields.AutoSize = true;
            lblFields.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblFields.Location = new Point(3, 458);
            lblFields.Name = "lblFields";
            lblFields.Size = new Size(72, 26);
            lblFields.TabIndex = 4;
            lblFields.Text = "Fields";
            // 
            // dataGridViewFields
            // 
            dataGridViewFields.AllowUserToAddRows = false;
            dataGridViewFields.AllowUserToDeleteRows = false;
            dataGridViewFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewFields.ColumnHeadersHeight = 40;
            dataGridViewFields.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewFields.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dataGridViewFields.Dock = DockStyle.Fill;
            dataGridViewFields.Font = new Font("Segoe UI", 11F);
            dataGridViewFields.Location = new Point(3, 487);
            dataGridViewFields.Name = "dataGridViewFields";
            dataGridViewFields.ReadOnly = true;
            dataGridViewFields.RowHeadersWidth = 51;
            dataGridViewFields.RowTemplate.Height = 30;
            dataGridViewFields.Size = new Size(794, 39);
            dataGridViewFields.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Field Name";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Field Value";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Field Type";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Created At";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Right;
            btnClose.BackColor = Color.RoyalBlue;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 14F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(649, 542);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(148, 45);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // SubmissionDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(800, 600);
            Controls.Add(tableLayoutPanel1);
            Name = "SubmissionDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Submission Details";
            WindowState = FormWindowState.Maximized;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDocuments).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFields).EndInit();
            ResumeLayout(false);
        }
    }
}