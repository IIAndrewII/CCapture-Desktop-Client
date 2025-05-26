using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using Konecta.Tools.CCaptureClient.UI.ViewModels;
using Konecta.Tools.CCaptureClient.Core.Models;
using Konecta.Tools.CCaptureClient.Core.DbEntities;
using System.Windows.Forms.VisualStyles;
using Konecta.Tools.CCaptureClient.Infrastructure;
using System.Text.Json.Nodes;

namespace Konecta.Tools.CCaptureClient.UI.Forms
{
    public partial class ResponsesHistoryForm : Form
    {
        private readonly IDatabaseService _databaseService;
        private readonly IConfiguration _configuration;
        private List<VerificationResponseModel> _verificationResponses;

        public ResponsesHistoryForm(IDatabaseService databaseService, IConfiguration configuration)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _configuration = configuration;
            _verificationResponses = new List<VerificationResponseModel>();

            ConfigureDataGridViewResponses();
            ConfigureTreeView();
            ConfigureFilterControls();
            AttachEventHandlers();
            //LoadVerificationResponsesAsync();
            LoggerHelper.LogInfo("ResponsesHistoryForm initialized"); // Log form initialization
        }

        private void ConfigureDataGridViewResponses()
        {
            dataGridViewResponses.AllowUserToAddRows = false;
            dataGridViewResponses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResponses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoggerHelper.LogDebug("Configured DataGridViewResponses"); // Log configuration
        }

        private void ConfigureTreeView()
        {
            VerificationStatusTree.Nodes.Clear();
            VerificationStatusTree.Font = new Font("Segoe UI", 12F);
            VerificationStatusTree.ShowLines = true;
            VerificationStatusTree.ShowPlusMinus = true;
            VerificationStatusTree.CollapseAll();
            LoggerHelper.LogDebug("Configured VerificationStatusTree"); // Log configuration
        }

        private void ConfigureFilterControls()
        {
            // Populate status combo box
            comboBoxStatus.Items.AddRange(new[] { "All", "OK", "KO" });
            comboBoxStatus.SelectedIndex = 0;

            // Set default date range (e.g., last 30 days)
            datePickerStart.Value = DateTime.Now.AddDays(-30);
            datePickerEnd.Value = DateTime.Now;
            LoggerHelper.LogDebug("Configured filter controls"); // Log configuration
        }

        private void AttachEventHandlers()
        {
            btnExpandAll.Click += btnExpandAll_Click;
            btnCollapseAll.Click += btnCollapseAll_Click;
            btnClean.Click += btnClean_Click;
            btnApplyFilters.Click += btnApplyFilters_Click;
            dataGridViewResponses.SelectionChanged += DataGridViewResponses_SelectionChanged;
            LoggerHelper.LogDebug("Attached event handlers to controls"); // Log event handler attachment
        }

        private async void LoadVerificationResponsesAsync()
        {
            try
            {
                statusLabel.Text = "Loading verification responses...";
                statusLabel.ForeColor = Color.Blue;
                LoggerHelper.LogInfo("Loading verification responses"); // Log start of loading

                // Get filter parameters
                DateTime? startDate = datePickerStart.Checked ? datePickerStart.Value : null;
                DateTime? endDate = datePickerEnd.Checked ? datePickerEnd.Value : null;
                int? status = comboBoxStatus.SelectedIndex switch
                {
                    1 => 0, // OK
                    2 => 1, // KO
                    _ => null // All
                };
                string sourceSystem = string.IsNullOrWhiteSpace(txtSourceSystem.Text) ? null : txtSourceSystem.Text;
                string channel = string.IsNullOrWhiteSpace(txtChannel.Text) ? null : txtChannel.Text;
                string requestGuid = string.IsNullOrWhiteSpace(txtRequestGuid.Text) ? null : txtRequestGuid.Text;
                string batchClassName = string.IsNullOrWhiteSpace(txtBatchClassName.Text) ? null : txtBatchClassName.Text;
                string sessionId = string.IsNullOrWhiteSpace(txtSessionId.Text) ? null : txtSessionId.Text;
                string messageId = string.IsNullOrWhiteSpace(txtMessageId.Text) ? null : txtMessageId.Text;
                string userCode = string.IsNullOrWhiteSpace(txtUserCode.Text) ? null : txtUserCode.Text;

                _verificationResponses = await _databaseService.GetFilteredVerificationResponses(
                    startDate, endDate, status, sourceSystem, channel,
                    requestGuid, batchClassName, sessionId, messageId, userCode);

                dataGridViewResponses.DataSource = _verificationResponses.Select(r => new
                {
                    r.InteractionDateTime,
                    r.RequestGuid,
                    r.Status,
                    r.BatchClassName,
                    r.SourceSystem,
                    r.Channel,
                    r.SessionId,
                    r.MessageId,
                    r.UserCode
                }).ToList();

                statusLabel.Text = $"Loaded {_verificationResponses.Count} responses.";
                statusLabel.ForeColor = Color.Green;
                LoggerHelper.LogInfo($"Loaded {_verificationResponses.Count} verification responses"); // Log successful load
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Error loading responses: {ex.Message}";
                statusLabel.ForeColor = Color.Red;
                LoggerHelper.LogError("Failed to load verification responses", ex); // Log error
            }
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            LoggerHelper.LogInfo("Applying filters"); // Log filter application
            LoadVerificationResponsesAsync();
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            VerificationStatusTree.ExpandAll();
            LoggerHelper.LogInfo("Expanded all TreeView nodes"); // Log expand action
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            VerificationStatusTree.CollapseAll();
            LoggerHelper.LogInfo("Collapsed all TreeView nodes"); // Log collapse action
        }

        private void DataGridViewResponses_SelectionChanged(object sender, EventArgs e)
        {
            VerificationStatusTree.Nodes.Clear();

            if (dataGridViewResponses.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewResponses.SelectedRows[0];
                var requestGuid = selectedRow.Cells["RequestGuid"].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(requestGuid))
                {
                    try
                    {
                        LoggerHelper.LogInfo($"Selected response with Request GUID: {requestGuid}"); // Log selection
                        var response = _verificationResponses.FirstOrDefault(r => r.RequestGuid == requestGuid);

                        if (response != null && !string.IsNullOrEmpty(response.ResponseJson))
                        {
                            // Parse the JSON string into a JsonNode
                            var jsonNode = JsonNode.Parse(response.ResponseJson);
                            if (jsonNode != null)
                            {
                                // Create a root node for the request GUID
                                var rootNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                                rootNode.ForeColor = Color.Black;

                                // Build the tree using BuildTreeFromJson
                                BuildTreeFromJson(jsonNode, rootNode);
                                LoggerHelper.LogDebug($"Populated TreeView for Request GUID: {requestGuid}"); // Log successful population
                            }
                            else
                            {
                                var requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                                requestNode.ForeColor = Color.Black;
                                var errorNode = requestNode.Nodes.Add("Error: Failed to parse JSON response");
                                errorNode.ForeColor = Color.Red;
                                LoggerHelper.LogWarning($"Failed to parse JSON response for Request GUID: {requestGuid}"); // Log warning
                            }
                        }
                        else
                        {
                            var requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                            requestNode.ForeColor = Color.Black;
                            var errorNode = requestNode.Nodes.Add("Error: No JSON response available");
                            errorNode.ForeColor = Color.Red;
                            LoggerHelper.LogWarning($"No JSON response available for Request GUID: {requestGuid}"); // Log warning
                        }
                    }
                    catch (Exception ex)
                    {
                        var requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
                        requestNode.ForeColor = Color.Black;
                        var errorNode = requestNode.Nodes.Add($"Error: {ex.Message}");
                        errorNode.ForeColor = Color.Red;
                        LoggerHelper.LogError($"Failed to populate TreeView for Request GUID: {requestGuid}", ex); // Log error
                    }
                }
                else
                {
                    LoggerHelper.LogWarning("Selected row has no valid Request GUID"); // Log warning for invalid GUID
                }
            }
            else
            {
                LoggerHelper.LogDebug("No row selected in DataGridViewResponses"); // Log no selection
            }

            VerificationStatusTree.CollapseAll();
        }

        private void PopulateTreeView(string responseJson, string requestGuid)
        {
            var requestNode = VerificationStatusTree.Nodes.Add($"Request Guid: {requestGuid}");
            requestNode.ForeColor = Color.Black;

            try
            {
                if (!string.IsNullOrEmpty(responseJson))
                {
                    // Parse the JSON string into a JsonNode
                    var jsonNode = JsonNode.Parse(responseJson);
                    if (jsonNode != null)
                    {
                        // Build the tree using BuildTreeFromJson
                        BuildTreeFromJson(jsonNode, requestNode);
                        LoggerHelper.LogDebug($"Populated TreeView nodes for Request GUID: {requestGuid}"); // Log TreeView population
                    }
                    else
                    {
                        var errorNode = requestNode.Nodes.Add("Error: Failed to parse JSON response");
                        errorNode.ForeColor = Color.Red;
                        LoggerHelper.LogWarning($"Failed to parse JSON response for Request GUID: {requestGuid}"); // Log warning
                    }
                }
                else
                {
                    var errorNode = requestNode.Nodes.Add("Error: No JSON response available");
                    errorNode.ForeColor = Color.Red;
                    LoggerHelper.LogWarning($"No JSON response available for Request GUID: {requestGuid}"); // Log warning
                }
            }
            catch (Exception ex)
            {
                var errorNode = requestNode.Nodes.Add($"Error: {ex.Message}");
                errorNode.ForeColor = Color.Red;
                LoggerHelper.LogError($"Failed to populate TreeView for Request GUID: {requestGuid}", ex); // Log error
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

        private void btnClean_Click(object sender, EventArgs e)
        {
            // Clear the internal data collection
            _verificationResponses.Clear();

            // Reset DataGridView
            dataGridViewResponses.DataSource = null;

            // Reset TreeView
            VerificationStatusTree.Nodes.Clear();

            // Reset filter controls to default values
            comboBoxStatus.SelectedIndex = 0; // Select "All"
            datePickerStart.Value = DateTime.Now.AddDays(-30); // Default to last 30 days
            datePickerEnd.Value = DateTime.Now; // Default to today
            datePickerStart.Checked = true; // Enable start date
            datePickerEnd.Checked = true; // Enable end date
            txtSourceSystem.Text = string.Empty; // Clear source system
            txtChannel.Text = string.Empty; // Clear channel

            // Reset status label
            statusLabel.Text = "Ready";
            statusLabel.ForeColor = Color.Black;

            // Reconfigure TreeView to ensure it matches initial setup
            ConfigureTreeView();
            LoggerHelper.LogInfo("Cleared filters and reset form"); // Log clean action
        }
    }
}