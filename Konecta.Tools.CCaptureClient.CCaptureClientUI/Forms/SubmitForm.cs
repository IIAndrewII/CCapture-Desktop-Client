using Konecta.Tools.CCaptureClient.Core.ApiEntities;
using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Konecta.Tools.CCaptureClient.CCaptureClientUI.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Konecta.Tools.CCaptureClient.Infrastructure.Services;

namespace Konecta.Tools.CCaptureClient
{
    public partial class SubmitForm : Form
    {
        private readonly MainViewModel _viewModel;
        private readonly IConfiguration _configuration;
        private readonly IApiDatabaseService _apiDatabaseService;
        private readonly IDatabaseService _databaseService;
        private readonly ErrorProvider _errorProvider;
        private Dictionary<string, GroupData> _groups;
        private bool _isSubmitting = false;
        private ToolStripProgressBar _progressBar; // Progress bar

        private class GroupData
        {
            public List<Document_Row> Documents { get; set; } = new List<Document_Row>();
            public List<Field> Fields { get; set; } = new List<Field>();
        }

        public SubmitForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            _errorProvider = new ErrorProvider(this) { BlinkStyle = ErrorBlinkStyle.NeverBlink };
            this.Shown += SubmitForm_Shown;

            // Initialize the progress bar
            _progressBar = new ToolStripProgressBar
            {
                Name = "toolStripProgressBar1",
                Size = new Size(200, 16),
                Visible = false,
                Alignment = ToolStripItemAlignment.Right // Align to the right of statusStrip2
            };
            statusStrip2.Items.Add(_progressBar); // Add to statusStrip2
        }

        public SubmitForm(IApiDatabaseService apiDatabaseService, IDatabaseService databaseService, IConfiguration configuration, MainViewModel viewModel)
            : this()
        {
            _apiDatabaseService = apiDatabaseService;
            _databaseService = databaseService;
            _configuration = configuration;
            _viewModel = viewModel;

            _groups = new Dictionary<string, GroupData>();
            txtApiUrl.Text = _configuration["ApiUrl"];
            ConfigureDataGridViewGroupsColumns();
            ConfigureDataGridViewColumns();
            AttachEventHandlers();
            UpdateControlStates();
            InitializeAsync();
        }

        private void SubmitForm_Shown(object sender, EventArgs e)
        {
            tableLayout2.PerformLayout();
            metadataPanel.PerformLayout();
            submitPanel.PerformLayout();
            tableLayout2.Invalidate();
            tableLayout2.Refresh();
            float dpiScale = CreateGraphics().DpiX / 96f;
            if (dpiScale != 1.0f)
            {
                tableLayout2.Width = (int)(tableLayout2.Width * dpiScale);
                tableLayout2.Height = (int)(tableLayout2.Height * dpiScale);
            }
        }

        private async void InitializeAsync()
        {
            await PopulateBatchClassNamesAsync();
            var appName = _configuration["AppName"];
            var appLogin = _configuration["AppLogin"];
            var appPassword = _configuration["AppPassword"];
            await loginAsync(appName, appLogin, appPassword);
        }

        private void ConfigureDataGridViewGroupsColumns()
        {
            dataGridViewGroups.Columns.Clear();
            dataGridViewGroups.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Submit",
                Name = "Submit",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridViewGroups.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Group Name",
                Name = "GroupName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });
            dataGridViewGroups.MultiSelect = false;
            dataGridViewGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigureDataGridViewColumns()
        {
            dataGridViewDocuments.Columns.Clear();
            dataGridViewDocuments.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "File Path",
                Name = "FilePath",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            var pageTypeColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Page Type",
                Name = "PageType",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FlatStyle = FlatStyle.Flat
            };
            dataGridViewDocuments.Columns.Add(pageTypeColumn);

            dataGridViewFields.Columns.Clear();
            var fieldNameColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Field Name",
                Name = "FieldName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FlatStyle = FlatStyle.Flat
            };
            dataGridViewFields.Columns.Add(fieldNameColumn);
            dataGridViewFields.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Field Value",
                Name = "FieldValue",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridViewFields.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Field Type",
                Name = "FieldType",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });
        }

        private void AttachEventHandlers()
        {
            dataGridViewDocuments.DataError += DataGridViewDocuments_DataError;
            dataGridViewFields.DataError += DataGridViewFields_DataError;
            btnBrowseFile.Click += btnBrowseFile_Click;
            btnRemoveFile.Click += btnRemoveFile_Click;
            btnSubmitDocument.Click += btnSubmitDocument_Click;
            btnAddGroup.Click += btnAddGroup_Click;
            btnRemoveGroup.Click += btnRemoveGroup_Click;
            btnRemoveField.Click += btnRemoveField_Click;
            btnAddField.Click += btnAddField_Click;
            dataGridViewGroups.SelectionChanged += dataGridViewGroups_SelectionChanged;
            dataGridViewGroups.CellValueChanged += dataGridViewGroups_CellValueChanged;
            cboBatchClassName.SelectedIndexChanged += cboBatchClassName_SelectedIndexChanged;
            dataGridViewDocuments.CellValueChanged += dataGridViewDocuments_CellValueChanged;
            dataGridViewFields.CellValueChanged += dataGridViewFields_CellValueChanged;
            dataGridViewGroups.RowsRemoved += (s, e) => UpdateControlStates();
            dataGridViewGroups.RowsAdded += (s, e) => UpdateControlStates();
            dataGridViewDocuments.RowsRemoved += (s, e) => UpdateControlStates();
            dataGridViewDocuments.RowsAdded += (s, e) => UpdateControlStates();
            dataGridViewFields.RowsRemoved += (s, e) => UpdateControlStates();
            dataGridViewFields.RowsAdded += (s, e) => UpdateControlStates();

            btnRemoveField.EnabledChanged += Control_EnabledChanged;
            btnAddField.EnabledChanged += Control_EnabledChanged;
            btnAddGroup.EnabledChanged += Control_EnabledChanged;
            btnRemoveGroup.EnabledChanged += Control_EnabledChanged;
            txtApiUrl.EnabledChanged += Control_EnabledChanged;
            txtSourceSystem.EnabledChanged += Control_EnabledChanged;
            txtChannel.EnabledChanged += Control_EnabledChanged;
            txtUserCode.EnabledChanged += Control_EnabledChanged;
            txtSessionID.EnabledChanged += Control_EnabledChanged;
            txtMessageID.EnabledChanged += Control_EnabledChanged;
            cboBatchClassName.EnabledChanged += Control_EnabledChanged;
            dataGridViewGroups.EnabledChanged += Control_EnabledChanged;
            dataGridViewDocuments.EnabledChanged += Control_EnabledChanged;
            dataGridViewFields.EnabledChanged += Control_EnabledChanged;
            pickerInteractionDateTime.EnabledChanged += Control_EnabledChanged;
        }

        private void Control_EnabledChanged(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                if (control.Enabled)
                {
                    control.BackColor = SystemColors.Window;
                    if (control is Button button)
                    {
                        button.ForeColor = Color.White;
                        button.BackColor = button == btnSubmitDocument ? Color.RoyalBlue :
                                          button == btnBrowseFile || button == btnAddGroup || button == btnAddField ? Color.Green :
                                          Color.FromArgb(220, 53, 69);
                    }
                    if (control is TextBox || control is ComboBox)
                    {
                        control.ForeColor = SystemColors.WindowText;
                    }
                }
                else
                {
                    control.BackColor = Color.FromArgb(230, 230, 230);
                    if (control is Button button)
                    {
                        button.ForeColor = Color.DarkGray;
                        button.BackColor = Color.FromArgb(200, 200, 200);
                    }
                    if (control is TextBox || control is ComboBox)
                    {
                        control.ForeColor = Color.DimGray;
                    }
                }
            }
        }

        private void UpdateControlStates()
        {
            bool hasGroups = _groups.Any();
            bool hasDocuments = dataGridViewDocuments.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow);
            bool hasFields = dataGridViewFields.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow);

            btnSubmitDocument.Enabled = hasGroups && !_isSubmitting;
            btnBrowseFile.Enabled = hasGroups && !_isSubmitting;
            btnRemoveFile.Enabled = hasGroups && hasDocuments && !_isSubmitting;
            btnRemoveField.Enabled = hasGroups && hasFields && !_isSubmitting;
            btnAddField.Enabled = hasGroups && !_isSubmitting;
            btnRemoveGroup.Enabled = hasGroups && !_isSubmitting;
            btnAddGroup.Enabled = !_isSubmitting;
            dataGridViewDocuments.Enabled = hasGroups && !_isSubmitting;
            dataGridViewFields.Enabled = hasGroups && !_isSubmitting;
            dataGridViewGroups.Enabled = !_isSubmitting;
            cboBatchClassName.Enabled = !_isSubmitting;
            pickerInteractionDateTime.Enabled = !_isSubmitting;

            txtApiUrl.Enabled = !_isSubmitting;
            txtSourceSystem.Enabled = !_isSubmitting;
            txtChannel.Enabled = !_isSubmitting;
            txtUserCode.Enabled = !_isSubmitting;
            txtSessionID.Enabled = !_isSubmitting;
            txtMessageID.Enabled = !_isSubmitting;

            // Update visual styles for all controls
            foreach (Control control in new Control[] {
                btnSubmitDocument,
                btnBrowseFile,
                btnRemoveFile,
                btnRemoveField,
                btnAddField,
                btnAddGroup,
                btnRemoveGroup,
                txtApiUrl,
                txtSourceSystem,
                txtChannel,
                txtUserCode,
                txtSessionID,
                txtMessageID,
                cboBatchClassName,
                dataGridViewGroups,
                dataGridViewDocuments,
                dataGridViewFields,
                pickerInteractionDateTime
            })
            {
                Control_EnabledChanged(control, EventArgs.Empty);
            }
        }

        private void AddNewGroup()
        {
            using (var form = new Form())
            {
                form.Text = "Create New Group";
                form.Size = new Size(300, 180);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.StartPosition = FormStartPosition.CenterParent;

                var tableLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    Padding = new Padding(10)
                };
                tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                var label = new Label
                {
                    Text = "Enter Group Name:",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12F)
                };

                var textBox = new TextBox
                {
                    Size = new Size(260, 20),
                    Font = new Font("Segoe UI", 12F)
                };

                // Set default group name with counter
                int counter = _groups.Count + 1;
                string defaultGroupName;
                do
                {
                    defaultGroupName = $"Group {counter}";
                    counter++;
                } while (_groups.ContainsKey(defaultGroupName));
                textBox.Text = defaultGroupName;

                var buttonPanel = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight
                };

                var okButton = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Font = new Font("Segoe UI", 12F),
                    Size = new Size(75, 35),
                    Margin = new Padding(5)
                };

                var cancelButton = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 12F),
                    Size = new Size(75, 35),
                    Margin = new Padding(5)
                };

                buttonPanel.Controls.Add(okButton);
                buttonPanel.Controls.Add(cancelButton);

                tableLayout.Controls.Add(label, 0, 0);
                tableLayout.Controls.Add(textBox, 0, 1);
                tableLayout.Controls.Add(buttonPanel, 0, 2);

                form.Controls.Add(tableLayout);

                form.AcceptButton = okButton;
                form.CancelButton = cancelButton;

                form.Shown += (s, e) =>
                {
                    form.Refresh();
                };

                if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    string groupName = textBox.Text.Trim();
                    if (_groups.ContainsKey(groupName))
                    {
                        MessageBox.Show("Group name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _groups.Add(groupName, new GroupData());
                    int rowIndex = dataGridViewGroups.Rows.Add(true, groupName);
                    dataGridViewGroups.ClearSelection();
                    dataGridViewGroups.Rows[rowIndex].Selected = true;
                    UpdateControlStates();
                }
            }
        }

        private async void btnAddField_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a group first.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
            var fieldNameColumn = (DataGridViewComboBoxColumn)dataGridViewFields.Columns["FieldName"];
            if (fieldNameColumn.Items.Count == 0)
            {
                MessageBox.Show("No field names available. Please select a batch class first.", "No Fields Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Find the first available field name not already used in the group
            string defaultFieldName = fieldNameColumn.Items.Cast<string>()
                .FirstOrDefault(name => !_groups[selectedGroup].Fields.Any(f => f.FieldName == name));
            if (defaultFieldName == null)
            {
                MessageBox.Show("All available field names are already used.", "No Fields Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newField = new Field
            {
                FieldName = defaultFieldName,
                FieldValue = string.Empty
            };

            try
            {
                var fieldType = await _apiDatabaseService.GetFieldTypeAsync(defaultFieldName);
                _groups[selectedGroup].Fields.Add(newField);
                int rowIndex = dataGridViewFields.Rows.Add(defaultFieldName, string.Empty, fieldType);
                dataGridViewFields.Rows[rowIndex].Tag = defaultFieldName; // Set the Tag for the new row
                dataGridViewFields.Rows[rowIndex].Selected = true;

                // Update dropdown items to remove the selected FieldName
                UpdateFieldNameDropdown(selectedGroup);
                UpdateControlStates();
            }
            catch (Exception ex)
            {
                statusLabel2.Text = $"Failed to add field: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                MessageBox.Show($"Failed to add field: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PopulateBatchClassNamesAsync()
        {
            try
            {
                var batchClassNames = await _apiDatabaseService.GetBatchClassNamesAsync();
                cboBatchClassName.Items.Clear();
                cboBatchClassName.Items.AddRange(batchClassNames.ToArray());
                if (cboBatchClassName.Items.Count > 0)
                    cboBatchClassName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                statusLabel2.Text = $"Failed to load Batch Class Names: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                MessageBox.Show($"Failed to load Batch Class Names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cboBatchClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBatchClassName.SelectedIndex != -1)
            {
                _errorProvider.SetError(cboBatchClassName, "");
                string selectedBatchClass = cboBatchClassName.SelectedItem.ToString();
                try
                {
                    var pageTypes = await _apiDatabaseService.GetPageTypesAsync(selectedBatchClass);
                    var fieldNames = await _apiDatabaseService.GetFieldNamesAsync(selectedBatchClass);

                    var pageTypeColumn = (DataGridViewComboBoxColumn)dataGridViewDocuments.Columns["PageType"];
                    pageTypeColumn.Items.Clear();
                    pageTypeColumn.Items.AddRange(pageTypes.ToArray());

                    var fieldNameColumn = (DataGridViewComboBoxColumn)dataGridViewFields.Columns["FieldName"];
                    fieldNameColumn.Items.Clear();
                    fieldNameColumn.Items.AddRange(fieldNames.ToArray());

                    UpdateDocumentAndFieldGrid();
                }
                catch (Exception ex)
                {
                    statusLabel2.Text = $"Failed to load page types or field names: {ex.Message}";
                    statusLabel2.ForeColor = Color.Red;
                    MessageBox.Show($"Failed to load page types or field names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewGroups_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDocumentAndFieldGrid();
            UpdateControlStates();
        }

        private void dataGridViewGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewGroups.Columns["Submit"].Index && e.RowIndex >= 0)
                UpdateDocumentAndFieldGrid();
        }

        private void UpdateDocumentAndFieldGrid()
        {
            dataGridViewDocuments.Rows.Clear();
            dataGridViewFields.Rows.Clear();
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow != null)
            {
                string selectedGroup = selectedRow.Cells["GroupName"].Value?.ToString();
                if (selectedGroup != null && _groups.ContainsKey(selectedGroup))
                {
                    var pageTypeColumn = (DataGridViewComboBoxColumn)dataGridViewDocuments.Columns["PageType"];
                    foreach (var doc in _groups[selectedGroup].Documents)
                    {
                        int rowIndex = dataGridViewDocuments.Rows.Add(doc.FilePath, null);
                        if (!string.IsNullOrEmpty(doc.PageType) && pageTypeColumn.Items.Contains(doc.PageType))
                            dataGridViewDocuments.Rows[rowIndex].Cells["PageType"].Value = doc.PageType;
                    }

                    var fieldNameColumn = (DataGridViewComboBoxColumn)dataGridViewFields.Columns["FieldName"];
                    foreach (var field in _groups[selectedGroup].Fields)
                    {
                        int rowIndex = dataGridViewFields.Rows.Add(null, field.FieldValue);
                        if (!string.IsNullOrEmpty(field.FieldName) && fieldNameColumn.Items.Contains(field.FieldName))
                        {
                            dataGridViewFields.Rows[rowIndex].Cells["FieldName"].Value = field.FieldName;
                            dataGridViewFields.Rows[rowIndex].Tag = field.FieldName; // Set Tag for existing fields
                            if (string.IsNullOrEmpty(dataGridViewFields.Rows[rowIndex].Cells["FieldType"].Value?.ToString()))
                            {
                                try
                                {
                                    var fieldType = _apiDatabaseService.GetFieldTypeAsync(field.FieldName).Result;
                                    dataGridViewFields.Rows[rowIndex].Cells["FieldType"].Value = fieldType;
                                }
                                catch (Exception)
                                {
                                    // Ignore errors to avoid blocking UI
                                }
                            }
                        }
                    }

                    // Update dropdown items based on used FieldNames
                    UpdateFieldNameDropdown(selectedGroup);
                }
            }
            UpdateControlStates();
        }

        private void UpdateFieldNameDropdown(string selectedGroup)
        {
            var fieldNameColumn = (DataGridViewComboBoxColumn)dataGridViewFields.Columns["FieldName"];
            var originalItems = fieldNameColumn.Items.Cast<string>().ToList();
            var usedFieldNames = _groups[selectedGroup].Fields.Select(f => f.FieldName).ToList();

            // Update Items for each cell's ComboBox
            foreach (DataGridViewRow row in dataGridViewFields.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cell = (DataGridViewComboBoxCell)row.Cells["FieldName"];
                    var currentValue = cell.Value?.ToString();
                    cell.Items.Clear();
                    // Add all original items except those used by other fields
                    foreach (var item in originalItems)
                    {
                        if (!usedFieldNames.Contains(item) || item == currentValue)
                            cell.Items.Add(item);
                    }
                }
            }
        }

        private async Task loginAsync(string appName, string appLogin, string appPassword)
        {
            try
            {
                _errorProvider.Clear();
                statusLabel2.Text = "Logging in...";
                statusLabel2.ForeColor = Color.Blue;

                if (string.IsNullOrEmpty(appName) || string.IsNullOrEmpty(appLogin) || string.IsNullOrEmpty(appPassword))
                {
                    statusLabel2.Text = "Configuration settings are missing.";
                    statusLabel2.ForeColor = Color.Red;
                    MessageBox.Show("Configuration settings are missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowLoginForm();
                    return;
                }

                var token = await _viewModel.GetAuthTokenAsync(appName, appLogin, appPassword);
                statusLabel2.Text = "You're logged in!";
                statusLabel2.ForeColor = Color.Green;
                submitPanel.Visible = true;
            }
            catch (Exception ex)
            {
                statusLabel2.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Unauthorized configuration settings."
                    : $"Login failed: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                MessageBox.Show(statusLabel2.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLoginForm();
            }
        }

        private void ShowLoginForm()
        {
            submitPanel.Visible = false;
            using (var loginForm = new LoginForm(_viewModel))
            {
                loginForm.Shown += (s, e) =>
                {
                    loginForm.PerformLayout();
                    loginForm.Invalidate();
                    loginForm.Refresh();
                };
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    statusLabel2.Text = "You're logged in!";
                    statusLabel2.ForeColor = Color.Green;
                    submitPanel.Visible = true;
                    SubmitForm_Shown(this, EventArgs.Empty);
                }
                else
                {
                    statusLabel2.Text = "Login failed. Please try again.";
                    statusLabel2.ForeColor = Color.Red;
                    MessageBox.Show("Login failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSubmitDocument_Click(object sender, EventArgs e)
        {
            try
            {
                _isSubmitting = true;
                UpdateControlStates();
                _errorProvider.Clear();

                if (cboBatchClassName.SelectedIndex == -1)
                    _errorProvider.SetError(cboBatchClassName, "Please select a batch category.");
                if (string.IsNullOrWhiteSpace(txtSourceSystem.Text))
                    _errorProvider.SetError(txtSourceSystem, "Please enter the source system.");
                if (string.IsNullOrWhiteSpace(txtChannel.Text))
                    _errorProvider.SetError(txtChannel, "Please enter the channel.");
                if (string.IsNullOrWhiteSpace(txtSessionID.Text))
                    _errorProvider.SetError(txtSessionID, "Please enter the session ID.");
                if (string.IsNullOrWhiteSpace(txtMessageID.Text))
                    _errorProvider.SetError(txtMessageID, "Please enter the message ID.");
                if (string.IsNullOrWhiteSpace(txtUserCode.Text))
                    _errorProvider.SetError(txtUserCode, "Please enter the user ID.");

                var checkedGroups = dataGridViewGroups.Rows.Cast<DataGridViewRow>()
                    .Where(row => (bool?)row.Cells["Submit"].Value == true)
                    .Select(row => row.Cells["GroupName"].Value.ToString())
                    .ToList();

                if (_errorProvider.GetError(cboBatchClassName) != "" ||
                    _errorProvider.GetError(txtSourceSystem) != "" ||
                    _errorProvider.GetError(txtChannel) != "" ||
                    _errorProvider.GetError(txtSessionID) != "" ||
                    _errorProvider.GetError(txtMessageID) != "" ||
                    _errorProvider.GetError(txtUserCode) != "")
                {
                    statusLabel2.Text = "Please enter all needed data.";
                    statusLabel2.ForeColor = Color.Red;
                    MessageBox.Show("Please enter all needed data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _progressBar.Visible = false;
                    return;
                }

                if (!checkedGroups.Any())
                {
                    statusLabel2.Text = "Please check at least one group to submit.";
                    statusLabel2.ForeColor = Color.Red;
                    MessageBox.Show("Please check at least one group to submit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _progressBar.Visible = false;
                    return;
                }

                var emptyGroups = checkedGroups.Where(group => !_groups[group].Documents.Any()).ToList();
                if (emptyGroups.Any())
                {
                    statusLabel2.Text = $"The following groups have no documents: {string.Join(", ", emptyGroups)}";
                    statusLabel2.ForeColor = Color.Red;
                    MessageBox.Show($"The following groups have no documents: {string.Join(", ", emptyGroups)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _progressBar.Visible = false;
                    return;
                }

                var apiUrl = string.IsNullOrWhiteSpace(txtApiUrl.Text) ? _configuration["ApiUrl"] : txtApiUrl.Text;
                if (string.IsNullOrEmpty(apiUrl))
                {
                    throw new InvalidOperationException("API URL is not configured in appsettings.json or provided in the textbox");
                }
                var apiService = new ApiService(apiUrl);
                _viewModel.UpdateApiService(apiService);

                // Initialize progress bar
                _progressBar.Visible = true;
                _progressBar.Value = 0;
                _progressBar.Maximum = checkedGroups.Count;

                foreach (string group in checkedGroups.ToList())
                {
                    var gridFields = dataGridViewFields.Rows.Cast<DataGridViewRow>()
                        .Where(row => !row.IsNewRow)
                        .Select(row => new
                        {
                            FieldName = row.Cells["FieldName"].Value?.ToString(),
                            FieldValue = row.Cells["FieldValue"].Value?.ToString()
                        })
                        .Where(f => !string.IsNullOrEmpty(f.FieldName))
                        .ToList();

                    var emptyFields = gridFields
                        .Where(f => string.IsNullOrWhiteSpace(f.FieldValue))
                        .Select(f => f.FieldName)
                        .ToList();
                    if (emptyFields.Any())
                    {
                        statusLabel2.Text = $"Group '{group}' has fields with empty values: {string.Join(", ", emptyFields)}";
                        statusLabel2.ForeColor = Color.Red;
                        MessageBox.Show($"Group '{group}' has fields with empty values: {string.Join(", ", emptyFields)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _progressBar.Visible = false;
                        return;
                    }

                    var groupData = _groups[group];
                    statusLabel2.Text = $"Submitting {group} documents...";
                    statusLabel2.ForeColor = Color.Blue;

                    var requestGuid = await _viewModel.SubmitDocumentAsync(
                        cboBatchClassName.SelectedItem.ToString(),
                        txtSourceSystem.Text,
                        txtChannel.Text,
                        txtSessionID.Text,
                        txtMessageID.Text,
                        txtUserCode.Text,
                        pickerInteractionDateTime.Value.ToString("o"),
                        groupData.Fields,
                        group,
                        groupData.Documents);

                    statusLabel2.Text = $"Documents for {group} submitted! Request Guid: {requestGuid}";
                    statusLabel2.ForeColor = Color.Green;

                    _groups.Remove(group);
                    var rowToRemove = dataGridViewGroups.Rows.Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["GroupName"].Value?.ToString() == group);
                    if (rowToRemove != null)
                        dataGridViewGroups.Rows.Remove(rowToRemove);

                    _progressBar.Value++;
                    Application.DoEvents();
                }

                UpdateDocumentAndFieldGrid();
                _progressBar.Visible = false;
                statusLabel2.Text = "Submission completed.";
                statusLabel2.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                _progressBar.Visible = false;
                statusLabel2.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Unauthorized. Please log in again."
                    : $"Submission failed: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                MessageBox.Show(statusLabel2.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401"))
                    ShowLoginForm();
            }
            finally
            {
                _isSubmitting = false;
                UpdateControlStates();
                _progressBar.Visible = false;
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a group first.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "PDF and Image Files (*.pdf;*.jpg;*.jpeg;*.png;*.bmp)|*.pdf;*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select PDF or Image Files"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var filePath in openFileDialog.FileNames)
                {
                    var doc = new Document_Row { FilePath = filePath, PageType = string.Empty };
                    _groups[selectedGroup].Documents.Add(doc);
                    if (dataGridViewGroups.SelectedRows[0].Cells["GroupName"].Value.ToString() == selectedGroup)
                        dataGridViewDocuments.Rows.Add(filePath, string.Empty);
                }
                UpdateControlStates();
            }
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a group first.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
            foreach (DataGridViewRow row in dataGridViewDocuments.SelectedRows.Cast<DataGridViewRow>().ToList())
            {
                if (!row.IsNewRow)
                {
                    var filePath = row.Cells["FilePath"].Value?.ToString();
                    _groups[selectedGroup].Documents.RemoveAll(doc => doc.FilePath == filePath);
                    dataGridViewDocuments.Rows.Remove(row);
                }
            }
            UpdateControlStates();
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            AddNewGroup();
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow != null)
            {
                var selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
                _groups.Remove(selectedGroup);
                dataGridViewGroups.Rows.Remove(selectedRow);
                UpdateDocumentAndFieldGrid();
            }
        }

        private void btnRemoveField_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a group first.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
            foreach (DataGridViewRow row in dataGridViewFields.SelectedRows.Cast<DataGridViewRow>().ToList())
            {
                if (!row.IsNewRow)
                {
                    var fieldName = row.Cells["FieldName"].Value?.ToString();
                    _groups[selectedGroup].Fields.RemoveAll(f => f.FieldName == fieldName);
                    dataGridViewFields.Rows.Remove(row);
                }
            }
            UpdateFieldNameDropdown(selectedGroup);
            UpdateControlStates();
        }

        private void dataGridViewDocuments_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewDocuments.Columns["PageType"].Index && e.RowIndex >= 0)
            {
                var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
                if (selectedRow != null)
                {
                    string selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
                    var filePath = dataGridViewDocuments.Rows[e.RowIndex].Cells["FilePath"].Value?.ToString();
                    var pageType = dataGridViewDocuments.Rows[e.RowIndex].Cells["PageType"].Value?.ToString();
                    var doc = _groups[selectedGroup].Documents.FirstOrDefault(d => d.FilePath == filePath);
                    if (doc != null)
                        doc.PageType = pageType ?? string.Empty;
                }
            }
        }

        private void DataGridViewDocuments_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewDocuments.Columns["PageType"].Index)
            {
                dataGridViewDocuments.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                e.Cancel = true;
            }
        }

        private void DataGridViewFields_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewFields.Columns["FieldName"].Index)
            {
                dataGridViewFields.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                dataGridViewFields.Rows[e.RowIndex].Cells["FieldType"].Value = string.Empty;
                e.Cancel = true;
            }
        }

        private async void dataGridViewFields_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header row

            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                statusLabel2.Text = "Please select a group before editing fields.";
                statusLabel2.ForeColor = Color.Red;
                MessageBox.Show("Please select a group before editing fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
            var fieldName = dataGridViewFields.Rows[e.RowIndex].Cells["FieldName"].Value?.ToString();
            var fieldValue = dataGridViewFields.Rows[e.RowIndex].Cells["FieldValue"].Value?.ToString();
            var oldFieldName = dataGridViewFields.Rows[e.RowIndex].Tag as string;

            if (e.ColumnIndex == dataGridViewFields.Columns["FieldName"].Index)
            {
                if (!string.IsNullOrEmpty(oldFieldName) && oldFieldName != fieldName)
                {
                    _groups[selectedGroup].Fields.RemoveAll(f => f.FieldName == oldFieldName);
                }

                if (string.IsNullOrWhiteSpace(fieldName))
                {
                    dataGridViewFields.Rows[e.RowIndex].Cells["FieldType"].Value = string.Empty;
                }
                else
                {
                    try
                    {
                        var fieldType = await _apiDatabaseService.GetFieldTypeAsync(fieldName);
                        dataGridViewFields.Rows[e.RowIndex].Cells["FieldType"].Value = fieldType;

                        var existingField = _groups[selectedGroup].Fields.FirstOrDefault(f => f.FieldName == fieldName);
                        if (existingField != null)
                        {
                            existingField.FieldName = fieldName;
                            existingField.FieldValue = fieldValue ?? string.Empty;
                        }
                        else
                        {
                            _groups[selectedGroup].Fields.Add(new Field
                            {
                                FieldName = fieldName,
                                FieldValue = fieldValue ?? string.Empty
                            });
                        }

                        dataGridViewFields.Rows[e.RowIndex].Tag = fieldName;

                        // Commit the edit and refresh the grid
                        dataGridViewFields.EndEdit();
                        dataGridViewFields.Refresh();
                    }
                    catch (Exception ex)
                    {
                        statusLabel2.Text = $"Failed to load field type: {ex.Message}";
                        statusLabel2.ForeColor = Color.Red;
                        MessageBox.Show($"Failed to load field type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                UpdateFieldNameDropdown(selectedGroup);
            }
            else if (e.ColumnIndex == dataGridViewFields.Columns["FieldValue"].Index)
            {
                if (!string.IsNullOrWhiteSpace(fieldName))
                {
                    var existingField = _groups[selectedGroup].Fields.FirstOrDefault(f => f.FieldName == fieldName);
                    if (existingField != null)
                    {
                        existingField.FieldValue = fieldName;
                        existingField.FieldValue = fieldValue ?? string.Empty;
                    }
                    else
                    {
                        _groups[selectedGroup].Fields.Add(new Field
                        {
                            FieldName = fieldName,
                            FieldValue = fieldValue ?? string.Empty
                        });

                        try
                        {
                            var fieldType = await _apiDatabaseService.GetFieldTypeAsync(fieldName);
                            dataGridViewFields.Rows[e.RowIndex].Cells["FieldType"].Value = fieldType;
                            dataGridViewFields.Rows[e.RowIndex].Tag = fieldName;
                            dataGridViewFields.EndEdit();
                            dataGridViewFields.Refresh();
                        }
                        catch (Exception ex)
                        {
                            statusLabel2.Text = $"Failed to load field type: {ex.Message}";
                            statusLabel2.ForeColor = Color.Red;
                            MessageBox.Show($"Failed to load field type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            UpdateControlStates();
        }
    }
}