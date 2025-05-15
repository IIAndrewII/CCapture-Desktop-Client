using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Konecta.Tools.CCaptureClient.Infrastructure.Services;
using Konecta.Tools.CCaptureClient.UI.ViewModels;
using Konecta.Tools.CCaptureClient.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konecta.Tools.CCaptureClient.Core.DbEntities;

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
        }

        public CheckStatusForm(IApiDatabaseService apiDatabaseService, IDatabaseService databaseService, IConfiguration configuration, MainViewModel viewModel)
            : this()
        {
            _apiDatabaseService = apiDatabaseService;
            _databaseService = databaseService;
            _configuration = configuration;
            _viewModel = viewModel;

            txtApiUrl.Text = _configuration["ApiUrl"];
            ConfigureDataGridViewRequests();
            ConfigureTreeView();
            AttachEventHandlers();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            var appName = _configuration["AppName"];
            var appLogin = _configuration["AppLogin"];
            var appPassword = _configuration["AppPassword"];
            await loginAsync(appName, appLogin, appPassword);
            await PopulateUncheckedGuidsAsync();
            UpdateButtonStates(false); // Set initial button states
        }

        private async Task PopulateUncheckedGuidsAsync()
        {
            try
            {
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
                        dataGridViewRequests.Rows.Add(false, guid);
                        existingGuids.Add(guid);
                    }
                }

                dataGridViewRequests.Refresh();
                UpdateButtonStates(false); // Update button states after adding rows
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading unchecked GUIDs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridViewRequests()
        {
            // Configure DataGridView as needed
        }

        private void ConfigureTreeView()
        {
            VerificationStatusTree.Nodes.Clear();
            VerificationStatusTree.Font = new Font("Segoe UI", 12F);
            VerificationStatusTree.ShowLines = true;
            VerificationStatusTree.ShowPlusMinus = true;
            VerificationStatusTree.CollapseAll();
        }

        private void AttachEventHandlers()
        {
            btnCheckStatus.Click += btnCheckStatus_Click;
            btnExpandAll.Click += btnExpandAll_Click;
            btnCollapseAll.Click += btnCollapseAll_Click;
            btnCheckAll.Click += btnCheckAll_Click;
            btnUncheckAll.Click += btnUncheckAll_Click;
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
        }

        private void UpdateButtonStates(bool isProcessing)
        {
            // Determine button states when not processing
            bool canCheckStatus = !isProcessing && dataGridViewRequests.Rows
                .Cast<DataGridViewRow>()
                .Any(row => row.Cells["Select"].Value is true &&
                            !string.IsNullOrWhiteSpace(row.Cells["RequestGuid"].Value?.ToString()) &&
                            Guid.TryParse(row.Cells["RequestGuid"].Value?.ToString(), out _));

            bool hasRows = !isProcessing && dataGridViewRequests.Rows.Count > 0;
            bool canCheckAll = hasRows && dataGridViewRequests.Rows
                .Cast<DataGridViewRow>()
                .Any(row => row.Cells["Select"].Value is not true);
            bool canUncheckAll = hasRows && dataGridViewRequests.Rows
                .Cast<DataGridViewRow>()
                .Any(row => row.Cells["Select"].Value is true);
            bool hasTreeNodes = !isProcessing && VerificationStatusTree.Nodes.Count > 0;

            // Apply enabled states
            btnCheckStatus.Enabled = canCheckStatus;
            btnCheckAll.Enabled = canCheckAll;
            btnUncheckAll.Enabled = canUncheckAll;
            btnExpandAll.Enabled = hasTreeNodes;
            btnCollapseAll.Enabled = hasTreeNodes;
            txtApiUrl.Enabled = !isProcessing;
            txtSourceSystem.Enabled = !isProcessing;
            txtChannel.Enabled = !isProcessing;
            txtSessionID.Enabled = !isProcessing;
            txtMessageID.Enabled = !isProcessing;
            txtUserCode.Enabled = !isProcessing;
            pickerInteractionDateTime.Enabled = !isProcessing;
            dataGridViewRequests.Enabled = !isProcessing;

            // Update visual styles for buttons and controls
            foreach (Control control in new Control[] {
                btnCheckStatus,
                btnExpandAll,
                btnCollapseAll,
                btnCheckAll,
                btnUncheckAll,
                txtApiUrl,
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
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRequests.Rows)
            {
                row.Cells["Select"].Value = true;
            }
            UpdateButtonStates(false); // Update button states after checking all
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRequests.Rows)
            {
                row.Cells["Select"].Value = false;
            }
            UpdateButtonStates(false); // Update button states after unchecking all
        }

        private async void DataGridViewRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewRequests.Columns["Details"].Index && e.RowIndex >= 0)
            {
                var requestGuid = dataGridViewRequests.Rows[e.RowIndex].Cells["RequestGuid"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(requestGuid) && Guid.TryParse(requestGuid, out _))
                {
                    try
                    {
                        var details = await _databaseService.GetSubmissionDetailsAsync(requestGuid);
                        using (var detailsForm = new SubmissionDetailsForm(details))
                        {
                            detailsForm.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            VerificationStatusTree.ExpandAll();
            UpdateButtonStates(false); // Update button states after expanding
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            VerificationStatusTree.CollapseAll();
            UpdateButtonStates(false); // Update button states after collapsing
        }

        private async Task loginAsync(string appName, string appLogin, string appPassword)
        {
            try
            {
                _errorProvider.Clear();
                statusLabel3.Text = "Logging in...";
                statusLabel3.ForeColor = Color.Blue;

                if (string.IsNullOrEmpty(appName) || string.IsNullOrEmpty(appLogin) || string.IsNullOrEmpty(appPassword))
                {
                    statusLabel3.Text = "Configuration settings are missing.";
                    statusLabel3.ForeColor = Color.Red;
                    MessageBox.Show("Configuration settings are missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowLoginForm();
                    return;
                }

                var token = await _viewModel.GetAuthTokenAsync(appName, appLogin, appPassword);
                statusLabel3.Text = "You're logged in!";
                statusLabel3.ForeColor = Color.Green;
                checkStatusPanel.Visible = true;
            }
            catch (Exception ex)
            {
                statusLabel3.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Unauthorized configuration settings."
                    : $"Login failed: {ex.Message}";
                statusLabel3.ForeColor = Color.Red;
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
                }
                else
                {
                    statusLabel3.Text = "Login failed. Please try again.";
                    statusLabel3.ForeColor = Color.Red;
                    MessageBox.Show("Login failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateButtonStates(false); // Update button states after login attempt
        }

        private async void btnCheckStatus_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();
                UpdateButtonStates(true); // Disable buttons during processing

                // Validate metadata inputs
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

                // Collect selected RequestGuids
                var requestGuids = dataGridViewRequests.Rows
                    .Cast<DataGridViewRow>()
                    .Where(row => row.Cells["Select"].Value is true)
                    .Select(row => row.Cells["RequestGuid"].Value?.ToString())
                    .Where(guid => !string.IsNullOrWhiteSpace(guid) && Guid.TryParse(guid, out _))
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
                    MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateButtonStates(false); // Re-enable buttons
                    return;
                }

                if (!requestGuids.Any())
                {
                    statusLabel3.Text = "Please select at least one valid Request Guid.";
                    statusLabel3.ForeColor = Color.Red;
                    MessageBox.Show("Please select at least one valid Request Guid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateButtonStates(false); // Re-enable buttons
                    return;
                }

                var apiUrl = string.IsNullOrWhiteSpace(txtApiUrl.Text) ? _configuration["ApiUrl"] : txtApiUrl.Text;
                if (string.IsNullOrEmpty(apiUrl))
                {
                    throw new InvalidOperationException("API URL is not configured in appsettings.json or provided in the textbox");
                }
                var apiService = new ApiService(apiUrl);
                _viewModel.UpdateApiService(apiService);

                statusLabel3.Text = "Checking status...";
                statusLabel3.ForeColor = Color.Blue;
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Maximum = requestGuids.Count;
                VerificationStatusTree.Nodes.Clear();

                foreach (var requestGuid in requestGuids)
                {
                    TreeNode requestNode = null;
                    try
                    {
                        statusLabel3.Text = $"Checking status for {requestGuid}...";
                        var responseJson = await _viewModel.CheckVerificationStatusAsync(
                            requestGuid,
                            txtSourceSystem.Text,
                            txtChannel.Text,
                            txtSessionID.Text,
                            txtMessageID.Text,
                            txtUserCode.Text,
                            pickerInteractionDateTime.Value.ToString("o"));

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

                        var updateResult = await _databaseService.UpdateCheckedGuidAsync(requestGuid);
                        if (!updateResult)
                        {
                            requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                            requestNode.ForeColor = Color.Black;
                            var errorNode = requestNode.Nodes.Add("Error: Failed to update Checked_GUID (submission not found)");
                            errorNode.ForeColor = Color.Red;
                            toolStripProgressBar1.Value++;
                            continue;
                        }

                        requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                        requestNode.ForeColor = Color.Black;

                        var statusNode = requestNode.Nodes.Add($"Status: {response.Status}");
                        statusNode.ForeColor = response.Status == 0 ? Color.Green : Color.Red;

                        var executionDateNode = requestNode.Nodes.Add($"Execution Date: {response.ExecutionDate:yyyy-MM-dd HH:mm:ss}");
                        executionDateNode.ForeColor = Color.Black;

                        if (!string.IsNullOrEmpty(response.ErrorMessage))
                        {
                            var errorNode = requestNode.Nodes.Add($"Error Message: {response.ErrorMessage}");
                            errorNode.ForeColor = Color.Red;
                        }

                        if (response.Batch != null)
                        {
                            var batchNode = requestNode.Nodes.Add("Batch");
                            batchNode.ForeColor = Color.Black;

                            batchNode.Nodes.Add($"Id: {response.Batch.BatchId}").ForeColor = Color.Black;
                            batchNode.Nodes.Add($"Name: {response.Batch.Name}").ForeColor = Color.Black;
                            batchNode.Nodes.Add($"Creation Date: {response.Batch.CreationDate:yyyy-MM-dd HH:mm:ss}").ForeColor = Color.Black;
                            batchNode.Nodes.Add($"Close Date: {response.Batch.CloseDate:yyyy-MM-dd HH:mm:ss}").ForeColor = Color.Black;

                            if (response.Batch.BatchClass != null)
                            {
                                var batchClassNode = batchNode.Nodes.Add($"Batch Class: {response.Batch.BatchClass.Name}");
                                batchClassNode.ForeColor = Color.Black;
                            }

                            if (response.Batch.BatchFields?.Any() == true)
                            {
                                var fieldsNode = batchNode.Nodes.Add("Batch Fields");
                                fieldsNode.ForeColor = Color.Black;
                                foreach (var field in response.Batch.BatchFields)
                                {
                                    var fieldNode = fieldsNode.Nodes.Add($"Field: {field.Name}");
                                    fieldNode.ForeColor = Color.Black;
                                    fieldNode.Nodes.Add($"Value: {field.Value}").ForeColor = Color.Black;
                                    fieldNode.Nodes.Add($"Confidence: {field.Confidence}").ForeColor = Color.Black;
                                }
                            }

                            if (response.Batch.VerificationDocuments?.Any() == true)
                            {
                                var docsNode = batchNode.Nodes.Add("Documents");
                                docsNode.ForeColor = Color.Black;
                                foreach (var doc in response.Batch.VerificationDocuments)
                                {
                                    var docNode = docsNode.Nodes.Add($"Document: {doc.Name}");
                                    docNode.ForeColor = Color.Black;

                                    if (doc.DocumentClass != null)
                                    {
                                        docNode.Nodes.Add($"Document Class: {doc.DocumentClass.Name}").ForeColor = Color.Black;
                                    }

                                    if (doc.Pages?.Any() == true)
                                    {
                                        var pagesNode = docNode.Nodes.Add("Pages");
                                        pagesNode.ForeColor = Color.Black;
                                        foreach (var page in doc.Pages)
                                        {
                                            var pageNode = pagesNode.Nodes.Add($"Page: {page.FileName}");
                                            pageNode.ForeColor = Color.Black;

                                            if (page.PageTypes?.Any() == true)
                                            {
                                                var pageTypesNode = pageNode.Nodes.Add("Page Types");
                                                pageTypesNode.ForeColor = Color.Black;
                                                foreach (var pageType in page.PageTypes)
                                                {
                                                    var pageTypeNode = pageTypesNode.Nodes.Add($"Type: {pageType.Name}");
                                                    pageTypeNode.ForeColor = Color.Black;
                                                    pageTypeNode.Nodes.Add($"Confidence: {pageType.Confidence}").ForeColor = Color.Black;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (response.Batch.BatchStates?.Any() == true)
                            {
                                var statesNode = batchNode.Nodes.Add("Batch States");
                                statesNode.ForeColor = Color.Black;
                                foreach (var state in response.Batch.BatchStates)
                                {
                                    var stateNode = statesNode.Nodes.Add($"State: {state.Value}");
                                    stateNode.ForeColor = Color.Black;
                                    stateNode.Nodes.Add($"Track Date: {state.TrackDate:yyyy-MM-dd HH:mm:ss}").ForeColor = Color.Black;
                                    stateNode.Nodes.Add($"Workstation: {state.Workstation}").ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        requestNode = requestNode ?? VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                        requestNode.ForeColor = Color.Black;
                        var errorNode = requestNode.Nodes.Add($"Error: {ex.Message}");
                        errorNode.ForeColor = Color.Red;
                    }
                    toolStripProgressBar1.Value++;
                    Application.DoEvents();
                }

                VerificationStatusTree.CollapseAll();
                toolStripProgressBar1.Visible = false;
                statusLabel3.Text = "Status check completed.";
                statusLabel3.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                toolStripProgressBar1.Visible = false;
                statusLabel3.Text = $"Error: {ex.Message}";
                statusLabel3.ForeColor = Color.Red;
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401"))
                    ShowLoginForm();
            }
            finally
            {
                UpdateButtonStates(false); // Re-enable buttons
            }
        }

        private void DataGridViewRequests_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewRequests.Columns["RequestGuid"].Index)
            {
                dataGridViewRequests.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                e.Cancel = true;
            }
        }
    }
}