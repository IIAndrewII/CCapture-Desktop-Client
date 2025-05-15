using System.Drawing;

namespace Konecta.Tools.CCaptureClient
{
    partial class SubmitForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel submitPanel;
        private System.Windows.Forms.Panel metadataPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayout2;
        private System.Windows.Forms.TextBox txtMessageID;
        private System.Windows.Forms.Label lblMessageID;
        private System.Windows.Forms.TextBox txtSessionID;
        private System.Windows.Forms.Label lblSessionID;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.TextBox txtSourceSystem;
        private System.Windows.Forms.Label lblSourceSystem;
        private System.Windows.Forms.ComboBox cboBatchClassName;
        private System.Windows.Forms.Label lblBatchClassName;
        private System.Windows.Forms.DataGridView dataGridViewGroups;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel2;
        private System.Windows.Forms.DataGridView dataGridViewDocuments;
        private System.Windows.Forms.DataGridView dataGridViewFields;
        private System.Windows.Forms.Label lblDocuments;
        private System.Windows.Forms.Label lblFields;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnSubmitDocument;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Button btnRemoveFile;
        private System.Windows.Forms.Label lblInteractionDate;
        private System.Windows.Forms.DateTimePicker pickerInteractionDateTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemoveField;
        private System.Windows.Forms.TextBox txtApiUrl;
        private System.Windows.Forms.Label lblApiUrl;

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
            components = new System.ComponentModel.Container();
            submitPanel = new Panel();
            metadataPanel = new Panel();
            tableLayout2 = new TableLayoutPanel();
            cboBatchClassName = new ComboBox();
            lblBatchClassName = new Label();
            txtSourceSystem = new TextBox();
            lblSourceSystem = new Label();
            txtChannel = new TextBox();
            lblChannel = new Label();
            txtUserCode = new TextBox();
            lblUserCode = new Label();
            txtSessionID = new TextBox();
            lblSessionID = new Label();
            txtMessageID = new TextBox();
            lblMessageID = new Label();
            lblInteractionDate = new Label();
            pickerInteractionDateTime = new DateTimePicker();
            lblApiUrl = new Label();
            txtApiUrl = new TextBox();
            lblDocuments = new Label();
            dataGridViewGroups = new DataGridView();
            Submit = new DataGridViewCheckBoxColumn();
            GroupName = new DataGridViewTextBoxColumn();
            tableLayoutPanel4 = new TableLayoutPanel();
            btnAddGroup = new Button();
            btnRemoveGroup = new Button();
            label1 = new Label();
            dataGridViewDocuments = new DataGridView();
            FilePath = new DataGridViewTextBoxColumn();
            PageType = new DataGridViewComboBoxColumn();
            tableLayoutPanel3 = new TableLayoutPanel();
            btnBrowseFile = new Button();
            btnRemoveFile = new Button();
            lblFields = new Label();
            dataGridViewFields = new DataGridView();
            FieldName = new DataGridViewComboBoxColumn();
            FieldValue = new DataGridViewTextBoxColumn();
            FieldType = new DataGridViewTextBoxColumn();
            btnSubmitDocument = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnAddField = new Button();
            btnRemoveField = new Button();
            statusStrip2 = new StatusStrip();
            statusLabel2 = new ToolStripStatusLabel();
            errorProvider = new ErrorProvider(components);
            submitPanel.SuspendLayout();
            metadataPanel.SuspendLayout();
            tableLayout2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGroups).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDocuments).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFields).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // submitPanel
            // 
            submitPanel.Controls.Add(metadataPanel);
            submitPanel.Controls.Add(statusStrip2);
            submitPanel.Dock = DockStyle.Fill;
            submitPanel.Font = new Font("Segoe UI", 12F);
            submitPanel.Location = new Point(0, 0);
            submitPanel.Margin = new Padding(3, 4, 3, 4);
            submitPanel.Name = "submitPanel";
            submitPanel.Size = new Size(1375, 733);
            submitPanel.TabIndex = 1;
            // 
            // metadataPanel
            // 
            metadataPanel.Controls.Add(tableLayout2);
            metadataPanel.Dock = DockStyle.Fill;
            metadataPanel.Location = new Point(0, 0);
            metadataPanel.Margin = new Padding(3, 4, 3, 4);
            metadataPanel.Name = "metadataPanel";
            metadataPanel.Size = new Size(1375, 709);
            metadataPanel.TabIndex = 9;
            // 
            // tableLayout2
            // 
            tableLayout2.ColumnCount = 6;
            tableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
            tableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
            tableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26F));
            tableLayout2.Controls.Add(cboBatchClassName, 0, 1);
            tableLayout2.Controls.Add(lblBatchClassName, 0, 0);
            tableLayout2.Controls.Add(txtSourceSystem, 2, 1);
            tableLayout2.Controls.Add(lblSourceSystem, 2, 0);
            tableLayout2.Controls.Add(txtChannel, 4, 1);
            tableLayout2.Controls.Add(lblChannel, 4, 0);
            tableLayout2.Controls.Add(txtUserCode, 0, 3);
            tableLayout2.Controls.Add(lblUserCode, 0, 2);
            tableLayout2.Controls.Add(txtSessionID, 2, 3);
            tableLayout2.Controls.Add(lblSessionID, 2, 2);
            tableLayout2.Controls.Add(txtMessageID, 4, 3);
            tableLayout2.Controls.Add(lblMessageID, 4, 2);
            tableLayout2.Controls.Add(lblInteractionDate, 0, 4);
            tableLayout2.Controls.Add(pickerInteractionDateTime, 0, 5);
            tableLayout2.Controls.Add(lblApiUrl, 2, 4);
            tableLayout2.Controls.Add(txtApiUrl, 2, 5);
            tableLayout2.Controls.Add(lblDocuments, 0, 6);
            tableLayout2.Controls.Add(dataGridViewGroups, 0, 7);
            tableLayout2.Controls.Add(tableLayoutPanel4, 0, 8);
            tableLayout2.Controls.Add(label1, 2, 6);
            tableLayout2.Controls.Add(dataGridViewDocuments, 2, 7);
            tableLayout2.Controls.Add(tableLayoutPanel3, 2, 8);
            tableLayout2.Controls.Add(lblFields, 4, 6);
            tableLayout2.Controls.Add(dataGridViewFields, 4, 7);
            tableLayout2.Controls.Add(btnSubmitDocument, 4, 9);
            tableLayout2.Controls.Add(tableLayoutPanel1, 4, 8);
            tableLayout2.Dock = DockStyle.Fill;
            tableLayout2.Location = new Point(0, 0);
            tableLayout2.Margin = new Padding(3, 4, 3, 4);
            tableLayout2.Name = "tableLayout2";
            tableLayout2.RowCount = 10;
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 5.398111F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 7.152497F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 5.398111F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 7.152497F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 5.398111F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 7.152497F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 5.398111F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 40.48583F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.232119F));
            tableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 8.232119F));
            tableLayout2.Size = new Size(1375, 709);
            tableLayout2.TabIndex = 0;
            // 
            // cboBatchClassName
            // 
            cboBatchClassName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboBatchClassName.Font = new Font("Segoe UI", 12F);
            cboBatchClassName.FormattingEnabled = true;
            cboBatchClassName.Location = new Point(3, 45);
            cboBatchClassName.Margin = new Padding(3, 4, 3, 4);
            cboBatchClassName.Name = "cboBatchClassName";
            cboBatchClassName.Size = new Size(428, 36);
            cboBatchClassName.TabIndex = 0;
            // 
            // lblBatchClassName
            // 
            lblBatchClassName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblBatchClassName.AutoSize = true;
            lblBatchClassName.Font = new Font("Segoe UI", 12F);
            lblBatchClassName.Location = new Point(3, 10);
            lblBatchClassName.Name = "lblBatchClassName";
            lblBatchClassName.Size = new Size(112, 28);
            lblBatchClassName.TabIndex = 0;
            lblBatchClassName.Text = "Batch Class:";
            // 
            // txtSourceSystem
            // 
            txtSourceSystem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSourceSystem.Font = new Font("Segoe UI", 12F);
            txtSourceSystem.Location = new Point(460, 46);
            txtSourceSystem.Margin = new Padding(3, 4, 3, 4);
            txtSourceSystem.Name = "txtSourceSystem";
            txtSourceSystem.Size = new Size(428, 34);
            txtSourceSystem.TabIndex = 1;
            // 
            // lblSourceSystem
            // 
            lblSourceSystem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSourceSystem.AutoSize = true;
            lblSourceSystem.Font = new Font("Segoe UI", 12F);
            lblSourceSystem.Location = new Point(460, 10);
            lblSourceSystem.Name = "lblSourceSystem";
            lblSourceSystem.Size = new Size(143, 28);
            lblSourceSystem.TabIndex = 0;
            lblSourceSystem.Text = "Source System:";
            // 
            // txtChannel
            // 
            txtChannel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtChannel.Font = new Font("Segoe UI", 12F);
            txtChannel.Location = new Point(917, 46);
            txtChannel.Margin = new Padding(3, 4, 3, 4);
            txtChannel.Name = "txtChannel";
            txtChannel.Size = new Size(428, 34);
            txtChannel.TabIndex = 2;
            // 
            // lblChannel
            // 
            lblChannel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblChannel.AutoSize = true;
            lblChannel.Font = new Font("Segoe UI", 12F);
            lblChannel.Location = new Point(917, 10);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(86, 28);
            lblChannel.TabIndex = 0;
            lblChannel.Text = "Channel:";
            // 
            // txtUserCode
            // 
            txtUserCode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUserCode.Font = new Font("Segoe UI", 12F);
            txtUserCode.Location = new Point(3, 134);
            txtUserCode.Margin = new Padding(3, 4, 3, 4);
            txtUserCode.Name = "txtUserCode";
            txtUserCode.Size = new Size(428, 34);
            txtUserCode.TabIndex = 3;
            // 
            // lblUserCode
            // 
            lblUserCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblUserCode.AutoSize = true;
            lblUserCode.Font = new Font("Segoe UI", 12F);
            lblUserCode.Location = new Point(3, 98);
            lblUserCode.Name = "lblUserCode";
            lblUserCode.Size = new Size(79, 28);
            lblUserCode.TabIndex = 0;
            lblUserCode.Text = "User ID:";
            // 
            // txtSessionID
            // 
            txtSessionID.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSessionID.Font = new Font("Segoe UI", 12F);
            txtSessionID.Location = new Point(460, 134);
            txtSessionID.Margin = new Padding(3, 4, 3, 4);
            txtSessionID.Name = "txtSessionID";
            txtSessionID.Size = new Size(428, 34);
            txtSessionID.TabIndex = 4;
            // 
            // lblSessionID
            // 
            lblSessionID.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSessionID.AutoSize = true;
            lblSessionID.Font = new Font("Segoe UI", 12F);
            lblSessionID.Location = new Point(460, 98);
            lblSessionID.Name = "lblSessionID";
            lblSessionID.Size = new Size(105, 28);
            lblSessionID.TabIndex = 0;
            lblSessionID.Text = "Session ID:";
            // 
            // txtMessageID
            // 
            txtMessageID.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtMessageID.Font = new Font("Segoe UI", 12F);
            txtMessageID.Location = new Point(917, 134);
            txtMessageID.Margin = new Padding(3, 4, 3, 4);
            txtMessageID.Name = "txtMessageID";
            txtMessageID.Size = new Size(428, 34);
            txtMessageID.TabIndex = 5;
            // 
            // lblMessageID
            // 
            lblMessageID.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblMessageID.AutoSize = true;
            lblMessageID.Font = new Font("Segoe UI", 12F);
            lblMessageID.Location = new Point(917, 98);
            lblMessageID.Name = "lblMessageID";
            lblMessageID.Size = new Size(116, 28);
            lblMessageID.TabIndex = 0;
            lblMessageID.Text = "Message ID:";
            // 
            // lblInteractionDate
            // 
            lblInteractionDate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblInteractionDate.AutoSize = true;
            lblInteractionDate.Font = new Font("Segoe UI", 12F);
            lblInteractionDate.Location = new Point(3, 186);
            lblInteractionDate.Name = "lblInteractionDate";
            lblInteractionDate.Size = new Size(203, 28);
            lblInteractionDate.TabIndex = 24;
            lblInteractionDate.Text = "Interaction Date Time:";
            // 
            // pickerInteractionDateTime
            // 
            pickerInteractionDateTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pickerInteractionDateTime.CustomFormat = "ddd, dd MMM yyyy hh:mm tt";
            pickerInteractionDateTime.Font = new Font("Segoe UI", 12F);
            pickerInteractionDateTime.Format = DateTimePickerFormat.Custom;
            pickerInteractionDateTime.Location = new Point(3, 222);
            pickerInteractionDateTime.Margin = new Padding(3, 4, 3, 4);
            pickerInteractionDateTime.Name = "pickerInteractionDateTime";
            pickerInteractionDateTime.Size = new Size(428, 34);
            pickerInteractionDateTime.TabIndex = 25;
            // 
            // lblApiUrl
            // 
            lblApiUrl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblApiUrl.AutoSize = true;
            lblApiUrl.Font = new Font("Segoe UI", 12F);
            lblApiUrl.Location = new Point(460, 186);
            lblApiUrl.Name = "lblApiUrl";
            lblApiUrl.Size = new Size(85, 28);
            lblApiUrl.TabIndex = 30;
            lblApiUrl.Text = "API URL:";
            // 
            // txtApiUrl
            // 
            txtApiUrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtApiUrl.Font = new Font("Segoe UI", 12F);
            txtApiUrl.Location = new Point(460, 222);
            txtApiUrl.Margin = new Padding(3, 4, 3, 4);
            txtApiUrl.Name = "txtApiUrl";
            txtApiUrl.Size = new Size(428, 34);
            txtApiUrl.TabIndex = 29;
            // 
            // lblDocuments
            // 
            lblDocuments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblDocuments.AutoSize = true;
            lblDocuments.Font = new Font("Segoe UI", 12F);
            lblDocuments.Location = new Point(3, 274);
            lblDocuments.Name = "lblDocuments";
            lblDocuments.Size = new Size(176, 28);
            lblDocuments.TabIndex = 12;
            lblDocuments.Text = "Document Groups:";
            // 
            // dataGridViewGroups
            // 
            dataGridViewGroups.AllowUserToAddRows = false;
            dataGridViewGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGroups.Columns.AddRange(new DataGridViewColumn[] { Submit, GroupName });
            dataGridViewGroups.Dock = DockStyle.Fill;
            dataGridViewGroups.Font = new Font("Segoe UI", 12F);
            dataGridViewGroups.Location = new Point(3, 306);
            dataGridViewGroups.Margin = new Padding(3, 4, 3, 4);
            dataGridViewGroups.Name = "dataGridViewGroups";
            dataGridViewGroups.RowHeadersWidth = 51;
            dataGridViewGroups.Size = new Size(428, 279);
            dataGridViewGroups.TabIndex = 11;
            // 
            // Submit
            // 
            Submit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Submit.FillWeight = 40.60914F;
            Submit.HeaderText = "Submit";
            Submit.MinimumWidth = 6;
            Submit.Name = "Submit";
            Submit.Resizable = DataGridViewTriState.True;
            Submit.SortMode = DataGridViewColumnSortMode.Automatic;
            Submit.Width = 104;
            // 
            // GroupName
            // 
            GroupName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GroupName.FillWeight = 159.390869F;
            GroupName.HeaderText = "Group Name";
            GroupName.MinimumWidth = 6;
            GroupName.Name = "GroupName";
            GroupName.ReadOnly = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(btnAddGroup, 0, 0);
            tableLayoutPanel4.Controls.Add(btnRemoveGroup, 1, 0);
            tableLayoutPanel4.Location = new Point(3, 595);
            tableLayoutPanel4.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(428, 45);
            tableLayoutPanel4.TabIndex = 26;
            // 
            // btnAddGroup
            // 
            btnAddGroup.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAddGroup.BackColor = Color.Green;
            btnAddGroup.FlatStyle = FlatStyle.Flat;
            btnAddGroup.Font = new Font("Segoe UI", 12F);
            btnAddGroup.ForeColor = Color.White;
            btnAddGroup.Location = new Point(3, 4);
            btnAddGroup.Margin = new Padding(3, 4, 3, 4);
            btnAddGroup.Name = "btnAddGroup";
            btnAddGroup.Size = new Size(208, 37);
            btnAddGroup.TabIndex = 20;
            btnAddGroup.Text = "Add Group";
            btnAddGroup.UseVisualStyleBackColor = false;
            // 
            // btnRemoveGroup
            // 
            btnRemoveGroup.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveGroup.BackColor = Color.FromArgb(220, 53, 69);
            btnRemoveGroup.FlatStyle = FlatStyle.Flat;
            btnRemoveGroup.Font = new Font("Segoe UI", 12F);
            btnRemoveGroup.ForeColor = Color.White;
            btnRemoveGroup.Location = new Point(217, 4);
            btnRemoveGroup.Margin = new Padding(3, 4, 3, 4);
            btnRemoveGroup.Name = "btnRemoveGroup";
            btnRemoveGroup.Size = new Size(208, 37);
            btnRemoveGroup.TabIndex = 12;
            btnRemoveGroup.Text = "Remove Group";
            btnRemoveGroup.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(460, 274);
            label1.Name = "label1";
            label1.Size = new Size(115, 28);
            label1.TabIndex = 27;
            label1.Text = "Documents:";
            // 
            // dataGridViewDocuments
            // 
            dataGridViewDocuments.AllowUserToAddRows = false;
            dataGridViewDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDocuments.Columns.AddRange(new DataGridViewColumn[] { FilePath, PageType });
            dataGridViewDocuments.Dock = DockStyle.Fill;
            dataGridViewDocuments.Font = new Font("Segoe UI", 12F);
            dataGridViewDocuments.Location = new Point(460, 306);
            dataGridViewDocuments.Margin = new Padding(3, 4, 3, 4);
            dataGridViewDocuments.Name = "dataGridViewDocuments";
            dataGridViewDocuments.RowHeadersWidth = 51;
            dataGridViewDocuments.Size = new Size(428, 279);
            dataGridViewDocuments.TabIndex = 13;
            // 
            // FilePath
            // 
            FilePath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FilePath.HeaderText = "File Path";
            FilePath.MinimumWidth = 6;
            FilePath.Name = "FilePath";
            // 
            // PageType
            // 
            PageType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            PageType.FlatStyle = FlatStyle.Flat;
            PageType.HeaderText = "Page Type";
            PageType.MinimumWidth = 6;
            PageType.Name = "PageType";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(btnBrowseFile, 0, 0);
            tableLayoutPanel3.Controls.Add(btnRemoveFile, 1, 0);
            tableLayoutPanel3.Location = new Point(460, 595);
            tableLayoutPanel3.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(428, 45);
            tableLayoutPanel3.TabIndex = 23;
            // 
            // btnBrowseFile
            // 
            btnBrowseFile.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnBrowseFile.BackColor = Color.Green;
            btnBrowseFile.FlatStyle = FlatStyle.Flat;
            btnBrowseFile.Font = new Font("Segoe UI", 12F);
            btnBrowseFile.ForeColor = Color.White;
            btnBrowseFile.Location = new Point(3, 4);
            btnBrowseFile.Margin = new Padding(3, 4, 3, 4);
            btnBrowseFile.Name = "btnBrowseFile";
            btnBrowseFile.Size = new Size(208, 37);
            btnBrowseFile.TabIndex = 15;
            btnBrowseFile.Text = "Add Files";
            btnBrowseFile.UseVisualStyleBackColor = false;
            // 
            // btnRemoveFile
            // 
            btnRemoveFile.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveFile.BackColor = Color.FromArgb(220, 53, 69);
            btnRemoveFile.FlatStyle = FlatStyle.Flat;
            btnRemoveFile.Font = new Font("Segoe UI", 12F);
            btnRemoveFile.ForeColor = Color.White;
            btnRemoveFile.Location = new Point(217, 4);
            btnRemoveFile.Margin = new Padding(3, 4, 3, 4);
            btnRemoveFile.Name = "btnRemoveFile";
            btnRemoveFile.Size = new Size(208, 37);
            btnRemoveFile.TabIndex = 16;
            btnRemoveFile.Text = "Remove File";
            btnRemoveFile.UseVisualStyleBackColor = false;
            // 
            // lblFields
            // 
            lblFields.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFields.AutoSize = true;
            lblFields.Font = new Font("Segoe UI", 12F);
            lblFields.Location = new Point(917, 274);
            lblFields.Name = "lblFields";
            lblFields.Size = new Size(104, 28);
            lblFields.TabIndex = 11;
            lblFields.Text = "Field Data:";
            // 
            // dataGridViewFields
            // 
            dataGridViewFields.AllowUserToAddRows = false;
            dataGridViewFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewFields.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewFields.Columns.AddRange(new DataGridViewColumn[] { FieldName, FieldValue, FieldType });
            dataGridViewFields.Dock = DockStyle.Fill;
            dataGridViewFields.Font = new Font("Segoe UI", 12F);
            dataGridViewFields.Location = new Point(917, 306);
            dataGridViewFields.Margin = new Padding(3, 4, 3, 4);
            dataGridViewFields.Name = "dataGridViewFields";
            dataGridViewFields.RowHeadersWidth = 51;
            dataGridViewFields.Size = new Size(428, 279);
            dataGridViewFields.TabIndex = 14;
            // 
            // FieldName
            // 
            FieldName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FieldName.FillWeight = 97.8416F;
            FieldName.FlatStyle = FlatStyle.Flat;
            FieldName.HeaderText = "Field Name";
            FieldName.MinimumWidth = 6;
            FieldName.Name = "FieldName";
            // 
            // FieldValue
            // 
            FieldValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FieldValue.FillWeight = 103.17363F;
            FieldValue.HeaderText = "Field Value";
            FieldValue.MinimumWidth = 6;
            FieldValue.Name = "FieldValue";
            // 
            // FieldType
            // 
            FieldType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FieldType.FillWeight = 98.98477F;
            FieldType.HeaderText = "Field Type";
            FieldType.MinimumWidth = 6;
            FieldType.Name = "FieldType";
            FieldType.ReadOnly = true;
            // 
            // btnSubmitDocument
            // 
            btnSubmitDocument.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnSubmitDocument.BackColor = Color.RoyalBlue;
            btnSubmitDocument.FlatStyle = FlatStyle.Flat;
            btnSubmitDocument.Font = new Font("Segoe UI", 12F);
            btnSubmitDocument.ForeColor = Color.White;
            btnSubmitDocument.Location = new Point(917, 651);
            btnSubmitDocument.Margin = new Padding(3, 4, 3, 4);
            btnSubmitDocument.Name = "btnSubmitDocument";
            btnSubmitDocument.Size = new Size(428, 53);
            btnSubmitDocument.TabIndex = 17;
            btnSubmitDocument.Text = "Submit Groups";
            btnSubmitDocument.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.0588226F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.9411774F));
            tableLayoutPanel1.Controls.Add(btnAddField, 0, 0);
            tableLayoutPanel1.Controls.Add(btnRemoveField, 1, 0);
            tableLayoutPanel1.Location = new Point(917, 595);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(428, 45);
            tableLayoutPanel1.TabIndex = 31;
            // 
            // btnAddField
            // 
            btnAddField.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAddField.BackColor = Color.Green;
            btnAddField.FlatStyle = FlatStyle.Flat;
            btnAddField.Font = new Font("Segoe UI", 12F);
            btnAddField.ForeColor = Color.White;
            btnAddField.Location = new Point(3, 4);
            btnAddField.Margin = new Padding(3, 4, 3, 4);
            btnAddField.Name = "btnAddField";
            btnAddField.Size = new Size(195, 37);
            btnAddField.TabIndex = 29;
            btnAddField.Text = "Add Field";
            btnAddField.UseVisualStyleBackColor = false;
            // 
            // btnRemoveField
            // 
            btnRemoveField.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveField.BackColor = Color.FromArgb(220, 53, 69);
            btnRemoveField.FlatStyle = FlatStyle.Flat;
            btnRemoveField.Font = new Font("Segoe UI", 12F);
            btnRemoveField.ForeColor = Color.White;
            btnRemoveField.Location = new Point(204, 4);
            btnRemoveField.Margin = new Padding(3, 4, 3, 4);
            btnRemoveField.Name = "btnRemoveField";
            btnRemoveField.Size = new Size(221, 37);
            btnRemoveField.TabIndex = 28;
            btnRemoveField.Text = "Remove Field";
            btnRemoveField.UseVisualStyleBackColor = false;
            // 
            // statusStrip2
            // 
            statusStrip2.Font = new Font("Segoe UI", 10F);
            statusStrip2.ImageScalingSize = new Size(20, 20);
            statusStrip2.Items.AddRange(new ToolStripItem[] { statusLabel2 });
            statusStrip2.Location = new Point(0, 709);
            statusStrip2.Name = "statusStrip2";
            statusStrip2.Padding = new Padding(1, 0, 16, 0);
            statusStrip2.Size = new Size(1375, 24);
            statusStrip2.TabIndex = 11;
            // 
            // statusLabel2
            // 
            statusLabel2.Name = "statusLabel2";
            statusLabel2.Size = new Size(0, 18);
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // SubmitForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1375, 733);
            Controls.Add(submitPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            Name = "SubmitForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Submit Document";
            submitPanel.ResumeLayout(false);
            submitPanel.PerformLayout();
            metadataPanel.ResumeLayout(false);
            tableLayout2.ResumeLayout(false);
            tableLayout2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGroups).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewDocuments).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewFields).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            statusStrip2.ResumeLayout(false);
            statusStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnAddField;
        private DataGridViewTextBoxColumn FilePath;
        private DataGridViewComboBoxColumn PageType;
        private DataGridViewCheckBoxColumn Submit;
        private DataGridViewTextBoxColumn GroupName;
        private DataGridViewComboBoxColumn FieldName;
        private DataGridViewTextBoxColumn FieldValue;
        private DataGridViewTextBoxColumn FieldType;
    }
}