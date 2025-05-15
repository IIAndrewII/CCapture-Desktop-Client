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
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtAppPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void btnGetToken_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();
                statusLabel.Text = string.Empty;

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
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message.ToLower().Contains("unauthorized") || ex.Message.Contains("401")
                    ? "Login failed. Please check your credentials and try again."
                    : "Something went wrong. Please try again.";
                statusLabel.ForeColor = Color.Red;
            }
            finally
            {
                btnGetToken.Enabled = true;
            }
        }
    }
}