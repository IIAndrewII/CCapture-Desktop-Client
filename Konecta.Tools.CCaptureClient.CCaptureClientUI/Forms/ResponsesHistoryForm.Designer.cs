namespace Konecta.Tools.CCaptureClient
{
    partial class ResponsesHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label lblVerificationResponses;
        private System.Windows.Forms.DataGridView dataGridViewResponses;
        private System.Windows.Forms.Label lblVerificationStatus;
        private System.Windows.Forms.TreeView VerificationStatusTree;
        private System.Windows.Forms.TableLayoutPanel treeButtonLayout;
        private System.Windows.Forms.Button btnExpandAll;
        private System.Windows.Forms.Button btnCollapseAll;
        private System.Windows.Forms.Button btnApplyFilters;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.TableLayoutPanel filterLayout;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker datePickerStart;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label lblSourceSystem;
        private System.Windows.Forms.TextBox txtSourceSystem;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.TextBox txtChannel;

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
            mainPanel = new Panel();
            tableLayout = new TableLayoutPanel();
            lblVerificationResponses = new Label();
            dataGridViewResponses = new DataGridView();
            lblVerificationStatus = new Label();
            VerificationStatusTree = new TreeView();
            treeButtonLayout = new TableLayoutPanel();
            btnExpandAll = new Button();
            btnCollapseAll = new Button();
            filterLayout = new TableLayoutPanel();
            lblStartDate = new Label();
            lblEndDate = new Label();
            lblStatus = new Label();
            lblSourceSystem = new Label();
            lblChannel = new Label();
            btnApplyFilters = new Button();
            txtChannel = new TextBox();
            txtSourceSystem = new TextBox();
            comboBoxStatus = new ComboBox();
            datePickerEnd = new DateTimePicker();
            datePickerStart = new DateTimePicker();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            btnClean = new Button();
            mainPanel.SuspendLayout();
            tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResponses).BeginInit();
            treeButtonLayout.SuspendLayout();
            filterLayout.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(tableLayout);
            mainPanel.Controls.Add(statusStrip);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Font = new Font("Segoe UI", 12F);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1200, 800);
            mainPanel.TabIndex = 0;
            // 
            // tableLayout
            // 
            tableLayout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayout.ColumnCount = 1;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.Controls.Add(lblVerificationResponses, 0, 1);
            tableLayout.Controls.Add(dataGridViewResponses, 0, 2);
            tableLayout.Controls.Add(lblVerificationStatus, 0, 3);
            tableLayout.Controls.Add(VerificationStatusTree, 0, 4);
            tableLayout.Controls.Add(treeButtonLayout, 0, 5);
            tableLayout.Controls.Add(filterLayout, 0, 0);
            tableLayout.Location = new Point(3, 12);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 6;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 6F));
            tableLayout.Size = new Size(1193, 756);
            tableLayout.TabIndex = 0;
            // 
            // lblVerificationResponses
            // 
            lblVerificationResponses.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblVerificationResponses.AutoSize = true;
            lblVerificationResponses.Font = new Font("Segoe UI", 12F);
            lblVerificationResponses.Location = new Point(3, 77);
            lblVerificationResponses.Name = "lblVerificationResponses";
            lblVerificationResponses.Size = new Size(208, 28);
            lblVerificationResponses.TabIndex = 0;
            lblVerificationResponses.Text = "Check Status Requests:";
            // 
            // dataGridViewResponses
            // 
            dataGridViewResponses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewResponses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResponses.Font = new Font("Segoe UI", 12F);
            dataGridViewResponses.Location = new Point(3, 108);
            dataGridViewResponses.Name = "dataGridViewResponses";
            dataGridViewResponses.RowHeadersWidth = 51;
            dataGridViewResponses.Size = new Size(1187, 281);
            dataGridViewResponses.TabIndex = 1;
            // 
            // lblVerificationStatus
            // 
            lblVerificationStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblVerificationStatus.AutoSize = true;
            lblVerificationStatus.Font = new Font("Segoe UI", 12F);
            lblVerificationStatus.Location = new Point(3, 394);
            lblVerificationStatus.Name = "lblVerificationStatus";
            lblVerificationStatus.Size = new Size(172, 28);
            lblVerificationStatus.TabIndex = 2;
            lblVerificationStatus.Text = "Verification Status:";
            // 
            // VerificationStatusTree
            // 
            VerificationStatusTree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            VerificationStatusTree.Font = new Font("Segoe UI", 12F);
            VerificationStatusTree.Location = new Point(3, 425);
            VerificationStatusTree.Name = "VerificationStatusTree";
            VerificationStatusTree.Size = new Size(1187, 281);
            VerificationStatusTree.TabIndex = 3;
            // 
            // treeButtonLayout
            // 
            treeButtonLayout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            treeButtonLayout.ColumnCount = 4;
            treeButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            treeButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            treeButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            treeButtonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            treeButtonLayout.Controls.Add(btnExpandAll, 2, 0);
            treeButtonLayout.Controls.Add(btnCollapseAll, 3, 0);
            treeButtonLayout.Location = new Point(3, 712);
            treeButtonLayout.Name = "treeButtonLayout";
            treeButtonLayout.RowCount = 1;
            treeButtonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            treeButtonLayout.Size = new Size(1187, 41);
            treeButtonLayout.TabIndex = 4;
            // 
            // btnExpandAll
            // 
            btnExpandAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnExpandAll.BackColor = Color.RoyalBlue;
            btnExpandAll.FlatStyle = FlatStyle.Flat;
            btnExpandAll.Font = new Font("Segoe UI", 12F);
            btnExpandAll.ForeColor = Color.White;
            btnExpandAll.Location = new Point(595, 3);
            btnExpandAll.Name = "btnExpandAll";
            btnExpandAll.Size = new Size(290, 35);
            btnExpandAll.TabIndex = 0;
            btnExpandAll.Text = "Expand All";
            btnExpandAll.UseVisualStyleBackColor = false;
            // 
            // btnCollapseAll
            // 
            btnCollapseAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCollapseAll.BackColor = Color.RoyalBlue;
            btnCollapseAll.FlatStyle = FlatStyle.Flat;
            btnCollapseAll.Font = new Font("Segoe UI", 12F);
            btnCollapseAll.ForeColor = Color.White;
            btnCollapseAll.Location = new Point(891, 3);
            btnCollapseAll.Name = "btnCollapseAll";
            btnCollapseAll.Size = new Size(293, 35);
            btnCollapseAll.TabIndex = 1;
            btnCollapseAll.Text = "Collapse All";
            btnCollapseAll.UseVisualStyleBackColor = false;
            // 
            // filterLayout
            // 
            filterLayout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filterLayout.ColumnCount = 12;
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.987009F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.329004F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.9870119F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.329004F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.9870119F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.329004F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.9870119F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.329004F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.658008F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.76190472F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.658008F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.658008F));
            filterLayout.Controls.Add(lblStartDate, 0, 0);
            filterLayout.Controls.Add(lblEndDate, 2, 0);
            filterLayout.Controls.Add(lblStatus, 4, 0);
            filterLayout.Controls.Add(lblSourceSystem, 6, 0);
            filterLayout.Controls.Add(lblChannel, 8, 0);
            filterLayout.Controls.Add(btnApplyFilters, 11, 1);
            filterLayout.Controls.Add(txtChannel, 8, 1);
            filterLayout.Controls.Add(txtSourceSystem, 6, 1);
            filterLayout.Controls.Add(comboBoxStatus, 4, 1);
            filterLayout.Controls.Add(datePickerEnd, 2, 1);
            filterLayout.Controls.Add(datePickerStart, 0, 1);
            filterLayout.Controls.Add(btnClean, 10, 1);
            filterLayout.Location = new Point(3, 3);
            filterLayout.Name = "filterLayout";
            filterLayout.RowCount = 2;
            filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            filterLayout.Size = new Size(1187, 69);
            filterLayout.TabIndex = 5;
            // 
            // lblStartDate
            // 
            lblStartDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblStartDate.AutoSize = true;
            lblStartDate.Font = new Font("Segoe UI", 12F);
            lblStartDate.Location = new Point(3, 0);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(148, 27);
            lblStartDate.TabIndex = 0;
            lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            lblEndDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEndDate.AutoSize = true;
            lblEndDate.Font = new Font("Segoe UI", 12F);
            lblEndDate.Location = new Point(208, 0);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(148, 27);
            lblEndDate.TabIndex = 2;
            lblEndDate.Text = "End:";
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 12F);
            lblStatus.Location = new Point(413, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(148, 27);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status:";
            // 
            // lblSourceSystem
            // 
            lblSourceSystem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSourceSystem.AutoSize = true;
            lblSourceSystem.Font = new Font("Segoe UI", 12F);
            lblSourceSystem.Location = new Point(618, 0);
            lblSourceSystem.Name = "lblSourceSystem";
            lblSourceSystem.Size = new Size(148, 27);
            lblSourceSystem.TabIndex = 6;
            lblSourceSystem.Text = "Source System:";
            // 
            // lblChannel
            // 
            lblChannel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblChannel.AutoSize = true;
            lblChannel.Font = new Font("Segoe UI", 12F);
            lblChannel.Location = new Point(823, 0);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(96, 27);
            lblChannel.TabIndex = 8;
            lblChannel.Text = "Channel:";
            // 
            // btnApplyFilters
            // 
            btnApplyFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnApplyFilters.BackColor = Color.ForestGreen;
            btnApplyFilters.FlatStyle = FlatStyle.Flat;
            btnApplyFilters.Font = new Font("Segoe UI", 12F);
            btnApplyFilters.ForeColor = Color.White;
            btnApplyFilters.Location = new Point(1083, 30);
            btnApplyFilters.Name = "btnApplyFilters";
            btnApplyFilters.Size = new Size(101, 36);
            btnApplyFilters.TabIndex = 10;
            btnApplyFilters.Text = "Apply Filters";
            btnApplyFilters.UseVisualStyleBackColor = false;
            // 
            // txtChannel
            // 
            txtChannel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtChannel.Font = new Font("Segoe UI", 12F);
            txtChannel.Location = new Point(823, 31);
            txtChannel.Name = "txtChannel";
            txtChannel.Size = new Size(96, 34);
            txtChannel.TabIndex = 9;
            // 
            // txtSourceSystem
            // 
            txtSourceSystem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSourceSystem.Font = new Font("Segoe UI", 12F);
            txtSourceSystem.Location = new Point(618, 31);
            txtSourceSystem.Name = "txtSourceSystem";
            txtSourceSystem.Size = new Size(148, 34);
            txtSourceSystem.TabIndex = 7;
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBoxStatus.Font = new Font("Segoe UI", 12F);
            comboBoxStatus.FormattingEnabled = true;
            comboBoxStatus.Location = new Point(413, 30);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(148, 36);
            comboBoxStatus.TabIndex = 5;
            // 
            // datePickerEnd
            // 
            datePickerEnd.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            datePickerEnd.Font = new Font("Segoe UI", 12F);
            datePickerEnd.Format = DateTimePickerFormat.Short;
            datePickerEnd.Location = new Point(208, 31);
            datePickerEnd.Name = "datePickerEnd";
            datePickerEnd.Size = new Size(148, 34);
            datePickerEnd.TabIndex = 3;
            // 
            // datePickerStart
            // 
            datePickerStart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            datePickerStart.Font = new Font("Segoe UI", 12F);
            datePickerStart.Format = DateTimePickerFormat.Short;
            datePickerStart.Location = new Point(3, 31);
            datePickerStart.Name = "datePickerStart";
            datePickerStart.Size = new Size(148, 34);
            datePickerStart.TabIndex = 1;
            // 
            // statusStrip
            // 
            statusStrip.Font = new Font("Segoe UI", 10F);
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 778);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1200, 22);
            statusStrip.TabIndex = 1;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 16);
            // 
            // btnClean
            // 
            btnClean.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnClean.BackColor = Color.FromArgb(192, 192, 0);
            btnClean.FlatStyle = FlatStyle.Flat;
            btnClean.Font = new Font("Segoe UI", 12F);
            btnClean.ForeColor = Color.White;
            btnClean.Location = new Point(981, 30);
            btnClean.Name = "btnClean";
            btnClean.Size = new Size(96, 35);
            btnClean.TabIndex = 11;
            btnClean.Text = "Clean";
            btnClean.UseVisualStyleBackColor = false;
            // 
            // ResponsesHistoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1200, 800);
            Controls.Add(mainPanel);
            Name = "ResponsesHistoryForm";
            Text = "Responses History";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResponses).EndInit();
            treeButtonLayout.ResumeLayout(false);
            filterLayout.ResumeLayout(false);
            filterLayout.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
        }
        private Button btnClean;
    }
}