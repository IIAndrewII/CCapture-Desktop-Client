using Konecta.Tools.CCaptureClient.Core.ApiEntities;
using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Konecta.Tools.CCaptureClient.UI.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Konecta.Tools.CCaptureClient.Infrastructure.Services;
using Konecta.Tools.CCaptureClient.Infrastructure;

namespace Konecta.Tools.CCaptureClient.UI.Forms
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
            statusStrip2.Items.Add(_progressBar);
            LoggerHelper.LogInfo("SubmitForm initialized"); // Log form initialization
        }

        public SubmitForm(IApiDatabaseService apiDatabaseService, IDatabaseService databaseService, IConfiguration configuration, MainViewModel viewModel)
            : this()
        {
            _apiDatabaseService = apiDatabaseService;
            _databaseService = databaseService;
            _configuration = configuration;
            _viewModel = viewModel;

            _groups = new Dictionary<string, GroupData>();
            ConfigureDataGridViewGroupsColumns();
            ConfigureDataGridViewColumns();
            AttachEventHandlers();
            UpdateControlStates();
            LoggerHelper.LogInfo("SubmitForm constructor called with dependencies"); // Log constructor
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
            LoggerHelper.LogInfo("SubmitForm shown and layout adjusted"); // Log form shown
        }

        private async void InitializeAsync()
        {
            LoggerHelper.LogDebug("Initializing SubmitForm asynchronously");
            await PopulateBatchClassNamesAsync();
            var appName = _configuration["AppName"];
            var appLogin = _configuration["AppLogin"];
            var appPassword = _configuration["AppPassword"];
            await loginAsync(appName, appLogin, appPassword);
            LoggerHelper.LogInfo("SubmitForm initialization completed");
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
            LoggerHelper.LogDebug("Configured DataGridViewGroups columns");
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
            LoggerHelper.LogDebug("Configured DataGridViewDocuments and DataGridViewFields columns");
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
            btnAssignToNewGroup.Click += btnAssignToNewGroup_Click;
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
            btnAssignToNewGroup.EnabledChanged += Control_EnabledChanged;
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
            LoggerHelper.LogDebug("Attached event handlers to controls");
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
                                          button == btnBrowseFile || button == btnAddGroup || button == btnAddField || button == btnAssignToNewGroup ? Color.Green :
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
            btnAssignToNewGroup.Enabled = !_isSubmitting; // Always enabled unless submitting
            btnRemoveField.Enabled = hasGroups && hasFields && !_isSubmitting;
            btnAddField.Enabled = hasGroups && !_isSubmitting;
            btnRemoveGroup.Enabled = hasGroups && !_isSubmitting;
            btnAddGroup.Enabled = !_isSubmitting;
            dataGridViewDocuments.Enabled = hasGroups && !_isSubmitting;
            dataGridViewFields.Enabled = hasGroups && !_isSubmitting;
            dataGridViewGroups.Enabled = !_isSubmitting;
            cboBatchClassName.Enabled = !_isSubmitting;
            pickerInteractionDateTime.Enabled = !_isSubmitting;

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
                btnAssignToNewGroup,
                btnRemoveField,
                btnAddField,
                btnAddGroup,
                btnRemoveGroup,
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
            LoggerHelper.LogDebug("Updated control states");
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
                        LoggerHelper.LogWarning($"Attempted to add duplicate group name: {groupName}");
                        MessageBox.Show("Group name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _groups.Add(groupName, new GroupData());
                    int rowIndex = dataGridViewGroups.Rows.Add(true, groupName);
                    dataGridViewGroups.ClearSelection();
                    dataGridViewGroups.Rows[rowIndex].Selected = true;
                    UpdateControlStates();
                    LoggerHelper.LogInfo($"Added new group: {groupName}");
                }
                else
                {
                    LoggerHelper.LogDebug("Group creation cancelled or invalid group name");
                }
            }
        }

        private void btnAssignToNewGroup_Click(object sender, EventArgs e)
        {
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
                    // Generate unique group name
                    int counter = _groups.Count + 1;
                    string newGroupName;
                    do
                    {
                        newGroupName = $"Group {counter}";
                        counter++;
                    } while (_groups.ContainsKey(newGroupName));

                    // Create new group
                    _groups.Add(newGroupName, new GroupData());

                    // Add single document to new group
                    var doc = new Document_Row { FilePath = filePath, PageType = string.Empty };
                    _groups[newGroupName].Documents.Add(doc);

                    // Add new group to grid
                    int rowIndex = dataGridViewGroups.Rows.Add(true, newGroupName);
                    dataGridViewGroups.ClearSelection();
                    dataGridViewGroups.Rows[rowIndex].Selected = true;
                    LoggerHelper.LogInfo($"Created new group {newGroupName} with file {filePath}");
                }

                // Update grids and controls
                UpdateDocumentAndFieldGrid();
                UpdateControlStates();
            }
            else
            {
                LoggerHelper.LogDebug("File selection cancelled for assigning to new groups");
            }
        }

        private async void btnAddField_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                LoggerHelper.LogWarning("Attempted to add field without selecting a group");
                MessageBox.Show("Please select a group first.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedGroup = selectedRow.Cells["GroupName"].Value.ToString();
            var fieldNameColumn = (DataGridViewComboBoxColumn)dataGridViewFields.Columns["FieldName"];
            if (fieldNameColumn.Items.Count == 0)
            {
                LoggerHelper.LogWarning("No field names available for adding field");
                MessageBox.Show("No field names available. Please select a batch class first.", "No Fields Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Find the first available field name not already used in the group
            string defaultFieldName = fieldNameColumn.Items.Cast<string>()
                .FirstOrDefault(name => !_groups[selectedGroup].Fields.Any(f => f.FieldName == name));
            if (defaultFieldName == null)
            {
                LoggerHelper.LogWarning("All available field names are already used");
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
                var fieldType = await _databaseService.GetFieldTypeAsync(defaultFieldName);
                _groups[selectedGroup].Fields.Add(newField);
                int rowIndex = dataGridViewFields.Rows.Add(defaultFieldName, string.Empty, fieldType);
                dataGridViewFields.Rows[rowIndex].Tag = defaultFieldName; // Set the Tag for the new row
                dataGridViewFields.Rows[rowIndex].Selected = true;

                // Update dropdown items to remove the selected FieldName
                UpdateFieldNameDropdown(selectedGroup);
                UpdateControlStates();
                LoggerHelper.LogInfo($"Added field {defaultFieldName} to group {selectedGroup}");
            }
            catch (Exception ex)
            {
                statusLabel2.Text = $"Failed to add field: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                LoggerHelper.LogError($"Failed to add field {defaultFieldName} to group {selectedGroup}", ex);
                MessageBox.Show($"Failed to add field: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PopulateBatchClassNamesAsync()
        {
            try
            {
                LoggerHelper.LogDebug("Populating batch class names");
                var batchClassNames = await _databaseService.GetBatchClassNamesAsync();
                cboBatchClassName.Items.Clear();
                cboBatchClassName.Items.AddRange(batchClassNames.ToArray());
                if (cboBatchClassName.Items.Count > 0)
                    cboBatchClassName.SelectedIndex = 0;
                LoggerHelper.LogInfo($"Loaded {batchClassNames.Count} batch class names");
            }
            catch (Exception ex)
            {
                statusLabel2.Text = $"Failed to load Batch Class Names: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                LoggerHelper.LogError("Failed to load batch class names", ex);
                MessageBox.Show($"Failed to load Batch Class Names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cboBatchClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBatchClassName.SelectedIndex != -1)
            {
                _errorProvider.SetError(cboBatchClassName, "");
                string selectedBatchClass = cboBatchClassName.SelectedItem.ToString();
                LoggerHelper.LogInfo($"Selected batch class: {selectedBatchClass}");
                try
                {
                    var pageTypes = await _databaseService.GetPageTypesAsync(selectedBatchClass);
                    var fieldNames = await _databaseService.GetFieldNamesAsync(selectedBatchClass);

                    var pageTypeColumn = (DataGridViewComboBoxColumn)dataGridViewDocuments.Columns["PageType"];
                    pageTypeColumn.Items.Clear();
                    pageTypeColumn.Items.AddRange(pageTypes.ToArray());

                    var fieldNameColumn = (DataGridViewComboBoxColumn)dataGridViewFields.Columns["FieldName"];
                    fieldNameColumn.Items.Clear();
                    fieldNameColumn.Items.AddRange(fieldNames.ToArray());

                    UpdateDocumentAndFieldGrid();
                    LoggerHelper.LogDebug($"Loaded {pageTypes.Count} page types and {fieldNames.Count} field names for batch class {selectedBatchClass}");
                }
                catch (Exception ex)
                {
                    statusLabel2.Text = $"Failed to load page types or field names: {ex.Message}";
                    statusLabel2.ForeColor = Color.Red;
                    LoggerHelper.LogError($"Failed to load page types or field names for batch class {selectedBatchClass}", ex);
                    MessageBox.Show($"Failed to load page types or field names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewGroups_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDocumentAndFieldGrid();
            UpdateControlStates();
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow != null)
            {
                string selectedGroup = selectedRow.Cells["GroupName"].Value?.ToString();
                LoggerHelper.LogDebug($"Selected group: {selectedGroup}");
            }
        }

        private void dataGridViewGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewGroups.Columns["Submit"].Index && e.RowIndex >= 0)
            {
                UpdateDocumentAndFieldGrid();
                var groupName = dataGridViewGroups.Rows[e.RowIndex].Cells["GroupName"].Value?.ToString();
                var submitValue = (bool?)dataGridViewGroups.Rows[e.RowIndex].Cells["Submit"].Value;
                LoggerHelper.LogDebug($"Submit value changed for group {groupName} to {submitValue}");
            }
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
                                    var fieldType = _databaseService.GetFieldTypeAsync(field.FieldName).Result;
                                    dataGridViewFields.Rows[rowIndex].Cells["FieldType"].Value = fieldType;
                                }
                                catch (Exception ex)
                                {
                                    LoggerHelper.LogError($"Failed to load field type for field {field.FieldName}", ex);
                                }
                            }
                        }
                    }

                    // Update dropdown items based on used FieldNames
                    UpdateFieldNameDropdown(selectedGroup);
                    LoggerHelper.LogDebug($"Updated document and field grids for group {selectedGroup}");
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
            LoggerHelper.LogDebug($"Updated field name dropdown for group {selectedGroup}");
        }

        private async Task loginAsync(string appName, string appLogin, string appPassword)
        {
            try
            {
                _errorProvider.Clear();
                statusLabel2.Text = "Logging in...";
                statusLabel2.ForeColor = Color.Blue;
                LoggerHelper.LogInfo("Attempting login");

                if (string.IsNullOrEmpty(appName) || string.IsNullOrEmpty(appLogin) || string.IsNullOrEmpty(appPassword))
                {
                    statusLabel2.Text = "Configuration settings are missing.";
                    statusLabel2.ForeColor = Color.Red;
                    LoggerHelper.LogError("Login failed: Configuration settings are missing");
                    MessageBox.Show("Configuration settings are missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowLoginForm();
                    return;
                }

                var token = await _viewModel.GetAuthTokenAsync(appName, appLogin, appPassword);
                statusLabel2.Text = "You're logged in!";
                statusLabel2.ForeColor = Color.Green;
                submitPanel.Visible = true;
                LoggerHelper.LogInfo("Login successful");
            }
            catch (Exception ex)
            {
                statusLabel2.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Unauthorized configuration settings."
                    : $"Login failed: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                LoggerHelper.LogError("Login failed", ex);
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
                    LoggerHelper.LogInfo("Login successful via login form");
                }
                else
                {
                    statusLabel2.Text = "Login failed. Please try again.";
                    statusLabel2.ForeColor = Color.Red;
                    LoggerHelper.LogWarning("Login failed via login form");
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
                LoggerHelper.LogInfo("Starting document submission");

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
                    LoggerHelper.LogWarning("Document submission failed: Missing required data");
                    MessageBox.Show("Please enter all needed data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _progressBar.Visible = false;
                    return;
                }

                if (!checkedGroups.Any())
                {
                    statusLabel2.Text = "Please check at least one group to submit.";
                    statusLabel2.ForeColor = Color.Red;
                    LoggerHelper.LogWarning("Document submission failed: No groups selected");
                    MessageBox.Show("Please check at least one group to submit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _progressBar.Visible = false;
                    return;
                }

                var emptyGroups = checkedGroups.Where(group => !_groups[group].Documents.Any()).ToList();
                if (emptyGroups.Any())
                {
                    statusLabel2.Text = $"The following groups have no documents: {string.Join(", ", emptyGroups)}";
                    statusLabel2.ForeColor = Color.Red;
                    LoggerHelper.LogWarning($"Document submission failed: Empty groups detected: {string.Join(", ", emptyGroups)}");
                    MessageBox.Show($"The following groups have no documents: {string.Join(", ", emptyGroups)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _progressBar.Visible = false;
                    return;
                }

                var apiUrl = _configuration["ApiUrl"];
                if (string.IsNullOrEmpty(apiUrl))
                {
                    LoggerHelper.LogError("Document submission failed: API URL is not configured");
                    throw new InvalidOperationException("API URL is not configured in appsettings.json or provided in the textbox");
                }
                var apiService = new ApiService(apiUrl);
                _viewModel.UpdateApiService(apiService);
                LoggerHelper.LogDebug($"API service updated with URL: {apiUrl}");

                // Initialize progress bar
                _progressBar.Visible = true;
                _progressBar.Value = 0;
                _progressBar.Maximum = checkedGroups.Count;
                statusLabel2.Text = "";
                LoggerHelper.LogInfo($"Submitting {checkedGroups.Count} groups");

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
                        LoggerHelper.LogWarning($"Document submission failed for group {group}: Empty fields: {string.Join(", ", emptyFields)}");
                        MessageBox.Show($"Group '{group}' has fields with empty values: {string.Join(", ", emptyFields)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _progressBar.Visible = false;
                        return;
                    }

                    var groupData = _groups[group];
                    // Append "Submitting {group} documents" to the status label
                    statusLabel2.Text = statusLabel2.Text.Length > 0
                        ? $"{statusLabel2.Text}  ......  Submitting {group}"
                        : $"Submitting {group} documents";
                    statusLabel2.ForeColor = Color.Blue;
                    Application.DoEvents();
                    LoggerHelper.LogInfo($"Submitting documents for group: {group}");

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
                        groupData.Documents,
                        apiUrl);

                    // Append submission confirmation and next group submission message
                    statusLabel2.Text = $"Documents for {group} submitted, Request Guid: {requestGuid}";
                    Application.DoEvents();
                    LoggerHelper.LogInfo($"Documents submitted for group {group}, Request Guid: {requestGuid}");

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
                statusLabel2.Text = $"{statusLabel2.Text}  ......  Submitting completed.";
                statusLabel2.ForeColor = Color.Green;
                LoggerHelper.LogInfo("Document submission completed successfully");
            }
            catch (Exception ex)
            {
                _progressBar.Visible = false;
                statusLabel2.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Unauthorized. Please log in again."
                    : $"Submission failed: {ex.Message}";
                statusLabel2.ForeColor = Color.Red;
                LoggerHelper.LogError("Document submission failed", ex);
                MessageBox.Show(statusLabel2.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401"))
                    ShowLoginForm();
            }
            finally
            {
                _isSubmitting = false;
                UpdateControlStates();
                _progressBar.Visible = false;
                LoggerHelper.LogDebug("Submission process completed");
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                LoggerHelper.LogWarning("Attempted to browse files without selecting a group");
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
                    LoggerHelper.LogInfo($"Added file {filePath} to group {selectedGroup}");
                }
                UpdateControlStates();
            }
            else
            {
                LoggerHelper.LogDebug("File browsing cancelled");
            }
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                LoggerHelper.LogWarning("Attempted to remove file without selecting a group");
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
                    LoggerHelper.LogInfo($"Removed file {filePath} from group {selectedGroup}");
                }
            }
            UpdateControlStates();
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            LoggerHelper.LogDebug("Adding new group");
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
                LoggerHelper.LogInfo($"Removed group: {selectedGroup}");
            }
            else
            {
                LoggerHelper.LogWarning("Attempted to remove group without selecting one");
            }
        }

        private void btnRemoveField_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewGroups.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (selectedRow == null)
            {
                LoggerHelper.LogWarning("Attempted to remove field without selecting a group");
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
                    LoggerHelper.LogInfo($"Removed field {fieldName} from group {selectedGroup}");
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
                    {
                        doc.PageType = pageType ?? string.Empty;
                        LoggerHelper.LogDebug($"Updated page type to {pageType} for document {filePath} in group {selectedGroup}");
                    }
                }
            }
        }

        private void DataGridViewDocuments_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewDocuments.Columns["PageType"].Index)
            {
                dataGridViewDocuments.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                e.Cancel = true;
                LoggerHelper.LogError($"Data error in DataGridViewDocuments at row {e.RowIndex}, column PageType", e.Exception);
            }
        }

        private void DataGridViewFields_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewFields.Columns["FieldName"].Index)
            {
                dataGridViewFields.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                dataGridViewFields.Rows[e.RowIndex].Cells["FieldType"].Value = string.Empty;
                e.Cancel = true;
                LoggerHelper.LogError($"Data error in DataGridViewFields at row {e.RowIndex}, column FieldName", e.Exception);
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
                LoggerHelper.LogWarning("Attempted to edit field without selecting a group");
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
                    LoggerHelper.LogDebug($"Removed old field {oldFieldName} from group {selectedGroup}");
                }

                if (string.IsNullOrWhiteSpace(fieldName))
                {
                    dataGridViewFields.Rows[e.RowIndex].Cells["FieldType"].Value = string.Empty;
                    LoggerHelper.LogDebug($"Cleared field type for empty field name in group {selectedGroup}");
                }
                else
                {
                    try
                    {
                        var fieldType = await _databaseService.GetFieldTypeAsync(fieldName);
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
                        LoggerHelper.LogInfo($"Updated field {fieldName} in group {selectedGroup} with value {fieldValue}");
                    }
                    catch (Exception ex)
                    {
                        statusLabel2.Text = $"Failed to load field type: {ex.Message}";
                        statusLabel2.ForeColor = Color.Red;
                        LoggerHelper.LogError($"Failed to load field type for {fieldName} in group {selectedGroup}", ex);
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
                            var fieldType = await _databaseService.GetFieldTypeAsync(fieldName);
                            dataGridViewFields.Rows[e.RowIndex].Cells["FieldType"].Value = fieldType;
                            dataGridViewFields.Rows[e.RowIndex].Tag = fieldName;
                            dataGridViewFields.EndEdit();
                            dataGridViewFields.Refresh();
                            LoggerHelper.LogInfo($"Added new field {fieldName} in group {selectedGroup} with value {fieldValue}");
                        }
                        catch (Exception ex)
                        {
                            statusLabel2.Text = $"Failed to load field type: {ex.Message}";
                            statusLabel2.ForeColor = Color.Red;
                            LoggerHelper.LogError($"Failed to load field type for {fieldName} in group {selectedGroup}", ex);
                            MessageBox.Show($"Failed to load field type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            UpdateControlStates();
        }
    }
}