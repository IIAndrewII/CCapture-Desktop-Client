using Konecta.Tools.CCaptureClient.Infrastructure;
using Konecta.Tools.CCaptureClient.UI.ViewModels;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Konecta.Tools.CCaptureClient.UI.Forms
{
    public partial class LoginForm : Form
    {
        private readonly MainViewModel _viewModel;
        private readonly ErrorProvider _errorProvider;

        public LoginForm(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            _errorProvider = new ErrorProvider(this) { BlinkStyle = ErrorBlinkStyle.NeverBlink };
            InitializeComponent();
            LoggerHelper.LogInfo("LoginForm initialized"); // Log form initialization
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtAppPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            LoggerHelper.LogInfo($"Show password toggled: {chkShowPassword.Checked}"); // Log password visibility toggle
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            LoggerHelper.LogInfo("Login cancelled"); // Log cancellation
        }

        private async void btnGetToken_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();
                statusLabel.Text = string.Empty;
                LoggerHelper.LogInfo("Attempting login"); // Log login attempt

                if (string.IsNullOrWhiteSpace(txtAppName.Text))
                    _errorProvider.SetError(txtAppName, "Please enter the application name.");
                if (string.IsNullOrWhiteSpace(txtAppLogin.Text))
                    _errorProvider.SetError(txtAppLogin, "Please enter the application login.");
                if (string.IsNullOrWhiteSpace(txtAppPassword.Text))
                    _errorProvider.SetError(txtAppPassword, "Please enter the application password.");

                if (_errorProvider.GetError(txtAppName) != "" ||
                    _errorProvider.GetError(txtAppLogin) != "" ||
                    _errorProvider.GetError(txtAppPassword) != "")
                {
                    statusLabel.Text = "Please fill in all required fields.";
                    statusLabel.ForeColor = Color.Red;
                    LoggerHelper.LogWarning("Login failed: Missing required fields"); // Log warning
                    return;
                }

                btnGetToken.Enabled = false;
                statusLabel.Text = "Logging in...";
                statusLabel.ForeColor = Color.Blue;

                var token = await _viewModel.GetAuthTokenAsync(
                    txtAppName.Text,
                    txtAppLogin.Text,
                    txtAppPassword.Text);

                statusLabel.Text = "You're logged in!";
                statusLabel.ForeColor = Color.Green;
                DialogResult = DialogResult.OK;
                LoggerHelper.LogInfo("Login successful"); // Log successful login
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Login failed. Please check your credentials and try again."
                    : "Something went wrong. Please try again.";
                statusLabel.ForeColor = Color.Red;
                LoggerHelper.LogError("Login failed", ex); // Log error
            }
            finally
            {
                btnGetToken.Enabled = true;
                LoggerHelper.LogDebug("Login process finalized"); // Log process completion
            }
        }
    }
}