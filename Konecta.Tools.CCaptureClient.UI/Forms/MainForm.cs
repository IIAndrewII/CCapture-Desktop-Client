using Konecta.Tools.CCaptureClient.Core.Interfaces;
using Konecta.Tools.CCaptureClient.Infrastructure.Services;
using Konecta.Tools.CCaptureClient.UI.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using System.Linq;
using Konecta.Tools.CCaptureClient.Core.ApiEntities;

namespace Konecta.Tools.CCaptureClient.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly IConfiguration _configuration;
        private readonly IApiDatabaseService _apiDatabaseService;
        private readonly IDatabaseService _databaseService;
        private readonly MainViewModel _viewModel;

        public MainForm(IApiDatabaseService apiDatabaseService, IDatabaseService databaseService, IConfiguration configuration)
        {
            _apiDatabaseService = apiDatabaseService;
            _databaseService = databaseService;
            _configuration = configuration;

            // Set the form to maximized state before initializing components
            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();

            // Initialize MainViewModel
            var fileService = new FileService();
            var apiUrl = _configuration["ApiUrl"];
            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new InvalidOperationException("ApiUrl is not configured in appsettings.json");
            }
            var apiService = new ApiService(apiUrl);
            _viewModel = new MainViewModel(apiService, fileService, _apiDatabaseService, _databaseService, _configuration);

            // Set up MDI
            this.IsMdiContainer = true;

            // Attach menu event handlers
            submitDocumentToolStripMenuItem.Click += (s, e) => OpenSubmitForm();
            checkStatusToolStripMenuItem.Click += (s, e) => OpenCheckStatusForm();
            viewVerificationResponsesToolStripMenuItem.Click += (s, e) => OpenVerificationResponseForm();
        }

        private void OpenSubmitForm()
        {
            var existingForm = this.MdiChildren.OfType<SubmitForm>().FirstOrDefault();
            if (existingForm == null)
            {
                var submitForm = new SubmitForm(_apiDatabaseService, _databaseService, _configuration, _viewModel)
                {
                    MdiParent = this,
                    WindowState = FormWindowState.Maximized
                };
                submitForm.Show();
            }
            else
            {
                existingForm.Activate();
            }
        }

        private void OpenCheckStatusForm()
        {
            var existingForm = this.MdiChildren.OfType<CheckStatusForm>().FirstOrDefault();
            if (existingForm == null)
            {
                var checkStatusForm = new CheckStatusForm(_apiDatabaseService, _databaseService, _configuration, _viewModel)
                {
                    MdiParent = this,
                    WindowState = FormWindowState.Maximized
                };
                checkStatusForm.Show();
            }
            else
            {
                existingForm.Activate();
            }
        }

        private void OpenVerificationResponseForm()
        {
            var existingForm = this.MdiChildren.OfType<ResponsesHistoryForm>().FirstOrDefault();
            if (existingForm == null)
            {
                var verificationResponseForm = new ResponsesHistoryForm(_databaseService, _configuration)
                {
                    MdiParent = this,
                    WindowState = FormWindowState.Maximized
                };
                verificationResponseForm.Show();
            }
            else
            {
                existingForm.Activate();
            }
        }
    }
}