namespace Konecta.Tools.CCaptureClient
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
            txtAppName.Location = new Point(120, 20);
            txtAppName.Name = "txtAppName";
            txtAppName.Size = new Size(200, 27);
            txtAppName.TabIndex = 0;
            // 
            // txtAppLogin
            // 
            txtAppLogin.Location = new Point(120, 50);
            txtAppLogin.Name = "txtAppLogin";
            txtAppLogin.Size = new Size(200, 27);
            txtAppLogin.TabIndex = 1;
            // 
            // txtAppPassword
            // 
            txtAppPassword.Location = new Point(120, 80);
            txtAppPassword.Name = "txtAppPassword";
            txtAppPassword.Size = new Size(200, 27);
            txtAppPassword.TabIndex = 2;
            txtAppPassword.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            chkShowPassword.Location = new Point(120, 110);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(100, 20);
            chkShowPassword.TabIndex = 3;
            chkShowPassword.Text = "Show Password";
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnGetToken
            // 
            btnGetToken.Location = new Point(120, 190);
            btnGetToken.Name = "btnGetToken";
            btnGetToken.Size = new Size(80, 30);
            btnGetToken.TabIndex = 4;
            btnGetToken.Text = "Login";
            btnGetToken.Click += btnGetToken_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(210, 190);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 30);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // statusLabel
            // 
            statusLabel.ForeColor = SystemColors.ControlText;
            statusLabel.Location = new Point(10, 140);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(300, 40);
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
            panel.Location = new Point(20, 20);
            panel.Name = "panel";
            panel.Size = new Size(360, 260);
            panel.TabIndex = 0;
            // 
            // lblAppName
            // 
            lblAppName.Location = new Point(10, 20);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(100, 20);
            lblAppName.TabIndex = 7;
            lblAppName.Text = "App Name:";
            // 
            // lblAppLogin
            // 
            lblAppLogin.Location = new Point(10, 50);
            lblAppLogin.Name = "lblAppLogin";
            lblAppLogin.Size = new Size(100, 20);
            lblAppLogin.TabIndex = 8;
            lblAppLogin.Text = "Appl Login:";
            // 
            // lblAppPassword
            // 
            lblAppPassword.Location = new Point(10, 80);
            lblAppPassword.Name = "lblAppPassword";
            lblAppPassword.Size = new Size(100, 20);
            lblAppPassword.TabIndex = 9;
            lblAppPassword.Text = "Password:";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(400, 300);
            Controls.Add(panel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
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