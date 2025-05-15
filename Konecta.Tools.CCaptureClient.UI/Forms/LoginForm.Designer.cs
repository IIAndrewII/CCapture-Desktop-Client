namespace Konecta.Tools.CCaptureClient.UI.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtAppName;
        private System.Windows.Forms.TextBox txtAppLogin;
        private System.Windows.Forms.TextBox txtAppPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnGetToken;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ErrorProvider errorProvider;

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
            errorProvider = new ErrorProvider(components);
            txtAppName = new TextBox();
            txtAppLogin = new TextBox();
            txtAppPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnGetToken = new Button();
            btnCancel = new Button();
            statusLabel = new Label();
            panel = new Panel();
            lblAppName = new Label();
            lblAppLogin = new Label();
            lblAppPassword = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // txtAppName
            // 
            txtAppName.Location = new Point(105, 15);
            txtAppName.Margin = new Padding(3, 2, 3, 2);
            txtAppName.Name = "txtAppName";
            txtAppName.Size = new Size(176, 23);
            txtAppName.TabIndex = 0;
            // 
            // txtAppLogin
            // 
            txtAppLogin.Location = new Point(105, 38);
            txtAppLogin.Margin = new Padding(3, 2, 3, 2);
            txtAppLogin.Name = "txtAppLogin";
            txtAppLogin.Size = new Size(176, 23);
            txtAppLogin.TabIndex = 1;
            // 
            // txtAppPassword
            // 
            txtAppPassword.Location = new Point(105, 60);
            txtAppPassword.Margin = new Padding(3, 2, 3, 2);
            txtAppPassword.Name = "txtAppPassword";
            txtAppPassword.Size = new Size(176, 23);
            txtAppPassword.TabIndex = 2;
            txtAppPassword.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            chkShowPassword.Location = new Point(105, 82);
            chkShowPassword.Margin = new Padding(3, 2, 3, 2);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(70, 21);
            chkShowPassword.TabIndex = 3;
            chkShowPassword.Text = "Show Password";
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnGetToken
            // 
            btnGetToken.Location = new Point(105, 142);
            btnGetToken.Margin = new Padding(3, 2, 3, 2);
            btnGetToken.Name = "btnGetToken";
            btnGetToken.Size = new Size(70, 22);
            btnGetToken.TabIndex = 4;
            btnGetToken.Text = "Login";
            btnGetToken.Click += btnGetToken_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(184, 142);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(70, 22);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // statusLabel
            // 
            statusLabel.ForeColor = SystemColors.ControlText;
            statusLabel.Location = new Point(9, 105);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(262, 30);
            statusLabel.TabIndex = 6;
            // 
            // panel
            // 
            panel.Controls.Add(txtAppName);
            panel.Controls.Add(txtAppLogin);
            panel.Controls.Add(txtAppPassword);
            panel.Controls.Add(chkShowPassword);
            panel.Controls.Add(btnGetToken);
            panel.Controls.Add(btnCancel);
            panel.Controls.Add(statusLabel);
            panel.Controls.Add(lblAppName);
            panel.Controls.Add(lblAppLogin);
            panel.Controls.Add(lblAppPassword);
            panel.Location = new Point(18, 15);
            panel.Margin = new Padding(3, 2, 3, 2);
            panel.Name = "panel";
            panel.Size = new Size(315, 195);
            panel.TabIndex = 0;
            // 
            // lblAppName
            // 
            lblAppName.Location = new Point(9, 15);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(88, 15);
            lblAppName.TabIndex = 7;
            lblAppName.Text = "App Name:";
            // 
            // lblAppLogin
            // 
            lblAppLogin.Location = new Point(9, 38);
            lblAppLogin.Name = "lblAppLogin";
            lblAppLogin.Size = new Size(88, 15);
            lblAppLogin.TabIndex = 8;
            lblAppLogin.Text = "Appl Login:";
            // 
            // lblAppPassword
            // 
            lblAppPassword.Location = new Point(9, 60);
            lblAppPassword.Name = "lblAppPassword";
            lblAppPassword.Size = new Size(88, 15);
            lblAppPassword.TabIndex = 9;
            lblAppPassword.Text = "Password:";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(350, 225);
            Controls.Add(panel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }
        private Panel panel;
        private Label lblAppName;
        private Label lblAppLogin;
        private Label lblAppPassword;
    }
}