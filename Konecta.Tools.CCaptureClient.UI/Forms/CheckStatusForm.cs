using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Konecta.Tools.CCaptureClient.Infrastructure.Services;
using Konecta.Tools.CCaptureClient.UI.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konecta.Tools.CCaptureClient.Core.DbEntities;
using Konecta.Tools.CCaptureClient.Infrastructure;

namespace Konecta.Tools.CCaptureClient.UI.Forms
{
    public partial class CheckStatusForm : Form
    {
        private readonly IApiDatabaseService _apiDatabaseService;
        private readonly IDatabaseService _databaseService;
        private readonly IConfiguration _configuration;
        private readonly MainViewModel _viewModel;
        private readonly ErrorProvider _errorProvider;

        public CheckStatusForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            _errorProvider = new ErrorProvider(this) { BlinkStyle = ErrorBlinkStyle.NeverBlink };
            LoggerHelper.LogInfo("CheckStatusForm initialized"); // Log form initialization
        }

        public CheckStatusForm(IApiDatabaseService apiDatabaseService, IDatabaseService databaseService, IConfiguration configuration, MainViewModel viewModel)
            : this()
        {
            _apiDatabaseService = apiDatabaseService;
            _databaseService = databaseService;
            _configuration = configuration;
            _viewModel = viewModel;

            ConfigureDataGridViewRequests();
            ConfigureTreeView();
            AttachEventHandlers();
            LoggerHelper.LogInfo("CheckStatusForm constructor called with dependencies"); // Log constructor
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            LoggerHelper.LogDebug("Initializing CheckStatusForm asynchronously");
            var appName = _configuration["AppName"];
            var appLogin = _configuration["AppLogin"];
            var appPassword = _configuration["AppPassword"];
            await loginAsync(appName, appLogin, appPassword);
            await PopulateUncheckedGuidsAsync();
            UpdateButtonStates(false);
            LoggerHelper.LogInfo("CheckStatusForm initialization completed");
        }

        private async Task PopulateUncheckedGuidsAsync()
        {
            try
            {
                LoggerHelper.LogDebug("Populating unchecked Request GUIDs");
                var existingGuids = dataGridViewRequests.Rows
                    .Cast<DataGridViewRow>()
                    .Select(row => row.Cells["RequestGuid"].Value?.ToString())
                    .Where(guid => !string.IsNullOrWhiteSpace(guid))
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                var uncheckedGuids = await _databaseService.GetUncheckedRequestGuidsAsync();

                foreach (var guid in uncheckedGuids)
                {
                    if (!existingGuids.Contains(guid))
                    {
                        // Assuming _databaseService has a method to get submission date
                        var submissionDate = await _databaseService.GetSubmissionDateAsync(guid);
                        dataGridViewRequests.Rows.Add(false, guid, submissionDate?.ToString("yyyy-MM-dd HH:mm:ss"));
                        existingGuids.Add(guid);
                    }
                }

                dataGridViewRequests.Refresh();
                UpdateButtonStates(false);
                LoggerHelper.LogInfo($"Loaded {uncheckedGuids.Count} unchecked Request GUIDs");
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Failed to load unchecked Request GUIDs", ex);
                MessageBox.Show($"Error loading unchecked GUIDs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridViewRequests()
        {
            //dataGridViewRequests.Columns.Clear();
            //dataGridViewRequests.Columns.Add(new DataGridViewCheckBoxColumn
            //{
            //    Name = "Select",
            //    HeaderText = "Select",
            //    Width = 50
            //});
            //dataGridViewRequests.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "RequestGuid",
            //    HeaderText = "Request GUID",
            //    Width = 200
            //});
            //dataGridViewRequests.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "SubmissionDate",
            //    HeaderText = "Submission Date",
            //    Width = 150
            //});
            //dataGridViewRequests.Columns.Add(new DataGridViewButtonColumn
            //{
            //    Name = "Details",
            //    HeaderText = "Details",
            //    Text = "View",
            //    UseColumnTextForButtonValue = true,
            //    Width = 80
            //});
            LoggerHelper.LogDebug("Configured DataGridViewRequests");
        }

        private void ConfigureTreeView()
        {
            VerificationStatusTree.Nodes.Clear();
            VerificationStatusTree.Font = new Font("Segoe UI", 12F);
            VerificationStatusTree.ShowLines = true;
            VerificationStatusTree.ShowPlusMinus = true;
            VerificationStatusTree.CollapseAll();
            LoggerHelper.LogDebug("Configured VerificationStatusTree");
        }

        private void AttachEventHandlers()
        {
            btnCheckStatus.Click += btnCheckStatus_Click;
            btnExpandAll.Click += btnExpandAll_Click;
            btnCollapseAll.Click += btnCollapseAll_Click;
            btnCheckAll.Click += btnCheckAll_Click;
            btnUncheckAll.Click += btnUncheckAll_Click;
            btnDeleteSelected.Click += btnDeleteSelected_Click;
            dataGridViewRequests.DataError += DataGridViewRequests_DataError;
            dataGridViewRequests.CellContentClick += DataGridViewRequests_CellContentClick;

            // DataGridView events for checkbox changes
            dataGridViewRequests.CellValueChanged += (s, e) => UpdateButtonStates(false);
            dataGridViewRequests.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dataGridViewRequests.IsCurrentCellDirty)
                {
                    dataGridViewRequests.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };

            // TreeView events for node changes
            VerificationStatusTree.AfterExpand += (s, e) => UpdateButtonStates(false);
            VerificationStatusTree.AfterCollapse += (s, e) => UpdateButtonStates(false);
            LoggerHelper.LogDebug("Attached event handlers to controls");
        }

        private void UpdateButtonStates(bool isProcessing)
        {
            bool canCheckStatus = !isProcessing && dataGridViewRequests.Rows
                .Cast<DataGridViewRow>()
                .Any(row => row.Cells["Select"].Value is true &&
                            !string.IsNullOrWhiteSpace(row.Cells["RequestGuid"].Value?.ToString()));

            bool hasRows = !isProcessing && dataGridViewRequests.Rows.Count > 0;
            bool canCheckAll = hasRows && dataGridViewRequests.Rows
                .Cast<DataGridViewRow>()
                .Any(row => row.Cells["Select"].Value is not true);
            bool canUncheckAll = hasRows && dataGridViewRequests.Rows
                .Cast<DataGridViewRow>()
                .Any(row => row.Cells["Select"].Value is true);
            bool canDelete = canUncheckAll; // Delete button enabled if any row is selected
            bool hasTreeNodes = !isProcessing && VerificationStatusTree.Nodes.Count > 0;

            btnCheckStatus.Enabled = canCheckStatus;
            btnCheckAll.Enabled = canCheckAll;
            btnUncheckAll.Enabled = canUncheckAll;
            btnDeleteSelected.Enabled = canDelete; // Enable delete button based on selection
            btnExpandAll.Enabled = hasTreeNodes;
            btnCollapseAll.Enabled = hasTreeNodes;
            txtSourceSystem.Enabled = !isProcessing;
            txtChannel.Enabled = !isProcessing;
            txtSessionID.Enabled = !isProcessing;
            txtMessageID.Enabled = !isProcessing;
            txtUserCode.Enabled = !isProcessing;
            pickerInteractionDateTime.Enabled = !isProcessing;
            dataGridViewRequests.Enabled = !isProcessing;

            foreach (Control control in new Control[] {
                btnCheckStatus,
                btnExpandAll,
                btnCollapseAll,
                btnCheckAll,
                btnUncheckAll,
                btnDeleteSelected,
                txtSourceSystem,
                txtChannel,
                txtSessionID,
                txtMessageID,
                txtUserCode,
                pickerInteractionDateTime,
                dataGridViewRequests
            })
            {
                if (control.Enabled)
                {
                    control.BackColor = SystemColors.Window;
                    if (control is Button button)
                        button.BackColor = Color.FromArgb(0, 122, 204); // RoyalBlue
                    if (control is TextBox || control is DateTimePicker)
                        control.ForeColor = SystemColors.WindowText;
                }
                else
                {
                    control.BackColor = Color.FromArgb(230, 230, 230);
                    if (control is Button button)
                        button.BackColor = Color.FromArgb(200, 200, 200);
                    if (control is TextBox || control is DateTimePicker)
                        control.ForeColor = Color.DimGray;
                }
            }
            LoggerHelper.LogDebug($"Updated button states, isProcessing: {isProcessing}");
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRequests.Rows)
            {
                row.Cells["Select"].Value = true;
            }
            UpdateButtonStates(false);
            LoggerHelper.LogInfo("Checked all Request GUIDs");
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRequests.Rows)
            {
                row.Cells["Select"].Value = false;
            }
            UpdateButtonStates(false);
            LoggerHelper.LogInfo("Unchecked all Request GUIDs");
        }

        private async void btnDeleteSelected_Click(object? sender, EventArgs e)
        {
            try
            {
            var selectedRows = dataGridViewRequests.Rows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells["Select"].Value is true)
                .ToList();

            if (!selectedRows.Any())
            {
                    MessageBox.Show("No rows selected for deletion.", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoggerHelper.LogWarning("Delete selected rows failed: No rows selected.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete {selectedRows.Count} selected row(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var row in selectedRows)
                {
                        var requestGuid = row.Cells["RequestGuid"].Value?.ToString();
                        if (!string.IsNullOrWhiteSpace(requestGuid))
                        {
                            var updateResult = await _databaseService.UpdateCheckedGuidAsync(requestGuid);
                            if (updateResult)
                            {
                    dataGridViewRequests.Rows.Remove(row);
                }
                            else
                            {
                                LoggerHelper.LogWarning($"Failed to mark Request GUID as checked in database: {requestGuid}");
                                MessageBox.Show($"Failed to mark Request GUID {requestGuid} as checked in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                UpdateButtonStates(false);
                    LoggerHelper.LogInfo($"Processed {selectedRows.Count} selected rows for deletion from DataGridViewRequests");
            }
        }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Failed to process deletion of selected rows", ex);
                MessageBox.Show($"Error during deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DataGridViewRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewRequests.Columns["Details"].Index && e.RowIndex >= 0)
            {
                var requestGuid = dataGridViewRequests.Rows[e.RowIndex].Cells["RequestGuid"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(requestGuid))
                {
                    try
                    {
                        LoggerHelper.LogInfo($"Loading submission details for Request GUID: {requestGuid}");
                        var details = await _databaseService.GetSubmissionDetailsAsync(requestGuid);
                        using (var detailsForm = new SubmissionDetailsForm(details))
                        {
                            detailsForm.ShowDialog();
                        }
                        LoggerHelper.LogDebug($"Displayed submission details for Request GUID: {requestGuid}");
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.LogError($"Failed to load submission details for Request GUID: {requestGuid}", ex);
                        MessageBox.Show($"Error loading details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    LoggerHelper.LogWarning($"Invalid Request GUID for details: {requestGuid}");
                }
            }
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            VerificationStatusTree.ExpandAll();
            UpdateButtonStates(false);
            LoggerHelper.LogInfo("Expanded all TreeView nodes");
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            VerificationStatusTree.CollapseAll();
            UpdateButtonStates(false);
            LoggerHelper.LogInfo("Collapsed all TreeView nodes");
        }

        private async Task loginAsync(string appName, string appLogin, string appPassword)
        {
            try
            {
                _errorProvider.Clear();
                statusLabel3.Text = "Logging in...";
                statusLabel3.ForeColor = Color.Blue;
                LoggerHelper.LogInfo("Attempting login");

                if (string.IsNullOrEmpty(appName) || string.IsNullOrEmpty(appLogin) || string.IsNullOrEmpty(appPassword))
                {
                    statusLabel3.Text = "Configuration settings are missing.";
                    statusLabel3.ForeColor = Color.Red;
                    LoggerHelper.LogError("Login failed: Configuration settings are missing");
                    MessageBox.Show("Configuration settings are missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowLoginForm();
                    return;
                }

                var token = await _viewModel.GetAuthTokenAsync(appName, appLogin, appPassword);
                statusLabel3.Text = "You're logged in!";
                statusLabel3.ForeColor = Color.Green;
                checkStatusPanel.Visible = true;
                LoggerHelper.LogInfo("Login successful");
            }
            catch (Exception ex)
            {
                statusLabel3.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Unauthorized configuration settings."
                    : $"Login failed: {ex.Message}";
                statusLabel3.ForeColor = Color.Red;
                LoggerHelper.LogError("Login failed", ex);
                MessageBox.Show(statusLabel3.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLoginForm();
            }
        }

        private void ShowLoginForm()
        {
            checkStatusPanel.Visible = false;
            using (var loginForm = new LoginForm(_viewModel))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    statusLabel3.Text = "You're logged in!";
                    statusLabel3.ForeColor = Color.Green;
                    checkStatusPanel.Visible = true;
                    LoggerHelper.LogInfo("Login successful via login form");
                }
                else
                {
                    statusLabel3.Text = "Login failed. Please try again.";
                    statusLabel3.ForeColor = Color.Red;
                    LoggerHelper.LogWarning("Login failed via login form");
                    MessageBox.Show("Login failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateButtonStates(false);
        }

        private async void btnCheckStatus_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();
                UpdateButtonStates(true);
                LoggerHelper.LogInfo("Starting status check");

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

                var requestGuids = dataGridViewRequests.Rows
                    .Cast<DataGridViewRow>()
                    .Where(row => row.Cells["Select"].Value is true)
                    .Select(row => row.Cells["RequestGuid"].Value?.ToString())
                    .Where(guid => !string.IsNullOrWhiteSpace(guid))
                    .Distinct()
                    .ToList();

                if (_errorProvider.GetError(txtSourceSystem) != "" ||
                    _errorProvider.GetError(txtChannel) != "" ||
                    _errorProvider.GetError(txtSessionID) != "" ||
                    _errorProvider.GetError(txtMessageID) != "" ||
                    _errorProvider.GetError(txtUserCode) != "")
                {
                    statusLabel3.Text = "Please fill in all required fields.";
                    statusLabel3.ForeColor = Color.Red;
                    LoggerHelper.LogWarning("Status check failed: Missing required fields");
                    MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateButtonStates(false); // Re-enable buttons
                    return;
                }

                if (!requestGuids.Any())
                {
                    statusLabel3.Text = "Please select at least one valid Request Guid.";
                    statusLabel3.ForeColor = Color.Red;
                    LoggerHelper.LogWarning("Status check failed: No valid Request GUIDs selected");
                    MessageBox.Show("Please select at least one valid Request Guid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateButtonStates(false); // Re-enable buttons
                    return;
                }

                var apiUrl = _configuration["ApiUrl"];
                if (string.IsNullOrEmpty(apiUrl))
                {
                    LoggerHelper.LogError("Status check failed: API URL is not configured");
                    throw new InvalidOperationException("API URL is not configured in appsettings.json or provided in the textbox");
                }
                var apiService = new ApiService(apiUrl);
                _viewModel.UpdateApiService(apiService);
                LoggerHelper.LogDebug($"API service updated with URL: {apiUrl}");

                statusLabel3.Text = "Checking status...";
                statusLabel3.ForeColor = Color.Blue;
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Maximum = requestGuids.Count;
                VerificationStatusTree.Nodes.Clear();
                LoggerHelper.LogInfo($"Checking status for {requestGuids.Count} Request GUIDs");

                foreach (var requestGuid in requestGuids)
                {
                    TreeNode requestNode = null;
                    try
                    {
                        statusLabel3.Text = $"Checking status for {requestGuid}";
                        LoggerHelper.LogInfo($"Checking status for Request GUID: {requestGuid}");
                        var responseJson = await _viewModel.CheckVerificationStatusAsync(
                            requestGuid,
                            txtSourceSystem.Text,
                            txtChannel.Text,
                            txtSessionID.Text,
                            txtMessageID.Text,
                            txtUserCode.Text,
                            pickerInteractionDateTime.Value.ToString("o"));

                        // Parse the JSON response
                        var jsonNode = JsonNode.Parse(responseJson);
                        if (jsonNode == null)
                        {
                            throw new JsonException("Failed to parse JSON response");
                        }

                        // Still deserialize for saving to database
                        var response = JsonSerializer.Deserialize<VerificationResponse>(responseJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        response.RequestGuid = requestGuid;
                        response.SourceSystem = txtSourceSystem.Text;
                        response.Channel = txtChannel.Text;
                        response.SessionId = txtSessionID.Text;
                        response.MessageId = txtMessageID.Text;
                        response.UserCode = txtUserCode.Text;
                        response.InteractionDateTime = pickerInteractionDateTime.Value;
                        response.ResponseJson = responseJson;

                        await _databaseService.SaveVerificationResponseAsync(response);
                        LoggerHelper.LogDebug($"Saved verification response for Request GUID: {requestGuid}");

                        var updateResult = await _databaseService.UpdateCheckedGuidAsync(requestGuid);
                        if (!updateResult)
                        {
                            requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                            requestNode.ForeColor = Color.Black;
                            var errorNode = requestNode.Nodes.Add("Error: Failed to update Checked_GUID (submission not found)");
                            errorNode.ForeColor = Color.Red;
                            toolStripProgressBar1.Value++;
                            LoggerHelper.LogWarning($"Failed to update Checked_GUID for Request GUID: {requestGuid}");
                            continue;
                        }

                        requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                        requestNode.ForeColor = Color.Black;

                        // Build tree from JSON
                        BuildTreeFromJson(jsonNode, requestNode);

                        LoggerHelper.LogInfo($"Status check completed for Request GUID: {requestGuid}");
                    }
                    catch (Exception ex)
                    {
                        requestNode = requestNode ?? VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                        requestNode.ForeColor = Color.Black;
                        var errorNode = requestNode.Nodes.Add($"Error: {ex.Message}");
                        errorNode.ForeColor = Color.Red;
                        LoggerHelper.LogError($"Failed to check status for Request GUID: {requestGuid}", ex);
                    }
                    toolStripProgressBar1.Value++;
                    Application.DoEvents();
                }

                VerificationStatusTree.CollapseAll();
                toolStripProgressBar1.Visible = false;
                statusLabel3.Text = "Status check completed.";
                statusLabel3.ForeColor = Color.Green;
                LoggerHelper.LogInfo("Status check process completed successfully");
            }
            catch (Exception ex)
            {
                toolStripProgressBar1.Visible = false;
                statusLabel3.Text = $"Error: {ex.Message}";
                statusLabel3.ForeColor = Color.Red;
                LoggerHelper.LogError("Status check process failed", ex);
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401"))
                    ShowLoginForm();
            }
            finally
            {
                UpdateButtonStates(false);
                LoggerHelper.LogDebug("Status check process finalized");
            }
        }

        private void BuildTreeFromJson(JsonNode node, TreeNode parentNode)
        {
            if (node == null) return;

            switch (node)
            {
                case JsonObject obj:
                    foreach (var property in obj)
                    {
                        var key = property.Key;
                        var value = property.Value;

                        if (value == null)
                        {
                            var childNode = parentNode.Nodes.Add($"{key}: null");
                            childNode.ForeColor = Color.Black;
                            continue;
                        }

                        if (value is JsonArray)
                        {
                            var arrayNode = parentNode.Nodes.Add(key);
                            arrayNode.ForeColor = Color.Black;
                            int index2 = 0;
                            foreach (var item in value.AsArray())
                            {
                                var itemNode = arrayNode.Nodes.Add($"{key} [{index2}]");
                                itemNode.ForeColor = Color.Black;
                                BuildTreeFromJson(item, itemNode);
                                index2++;
                            }
                        }
                        else if (value is JsonObject)
                        {
                            var objectNode = parentNode.Nodes.Add(key);
                            objectNode.ForeColor = Color.Black;
                            BuildTreeFromJson(value, objectNode);
                        }
                        else
                        {
                            var valueString2 = value.ToString();
                            string displayString;

                            // Special handling for Status key
                            if (key == "Status")
                            {
                                displayString = valueString2 == "0" ? "OK" : "KO";
                                var childNode = parentNode.Nodes.Add($"{key}: {displayString}");
                                childNode.ForeColor = valueString2 == "0" ? Color.Green : Color.Red;
                            }
                            else
                            {
                                // Format dates if they look like ISO 8601
                                if (DateTime.TryParse(valueString2, out var dateTime2))
                                {
                                    valueString2 = dateTime2.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                var childNode = parentNode.Nodes.Add($"{key}: {valueString2}");
                                childNode.ForeColor = key == "ErrorMessage" && !string.IsNullOrEmpty(valueString2) ? Color.Red : Color.Black;
                            }
                        }
                    }
                    break;

                case JsonArray array:
                    int index = 0;
                    foreach (var item in array)
                    {
                        var itemNode = parentNode.Nodes.Add($"Item [{index}]");
                        itemNode.ForeColor = Color.Black;
                        BuildTreeFromJson(item, itemNode);
                        index++;
                    }
                    break;

                case JsonValue jsonValue:
                    var valueString = jsonValue.ToString();
                    if (DateTime.TryParse(valueString, out var dateTime))
                    {
                        valueString = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    var valueNode = parentNode.Nodes.Add($"Value: {valueString}");
                    valueNode.ForeColor = Color.Black;
                    break;
            }
        }

        private void DataGridViewRequests_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewRequests.Columns["RequestGuid"].Index ||
                e.ColumnIndex == dataGridViewRequests.Columns["SubmissionDate"].Index)
            {
                dataGridViewRequests.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                e.Cancel = true;
                LoggerHelper.LogError($"Data error in DataGridViewRequests at row {e.RowIndex}, column {dataGridViewRequests.Columns[e.ColumnIndex].Name}", e.Exception);
            }
        }
    }
}