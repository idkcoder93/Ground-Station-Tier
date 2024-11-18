namespace CDH_GroundStation_Group6
{
    partial class SignInPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            SignInPanel = new Panel();
            errorMessageLabel = new Label();
            signInLabel = new Label();
            enterpriseLogo = new PictureBox();
            userTextBox = new TextBox();
            passTextBox = new TextBox();
            signInButton = new Button();
            SignInPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)enterpriseLogo).BeginInit();
            SuspendLayout();
            // 
            // SignInPanel
            // 
            SignInPanel.Anchor = AnchorStyles.None;
            SignInPanel.Controls.Add(errorMessageLabel);
            SignInPanel.Controls.Add(signInLabel);
            SignInPanel.Controls.Add(enterpriseLogo);
            SignInPanel.Controls.Add(userTextBox);
            SignInPanel.Controls.Add(passTextBox);
            SignInPanel.Controls.Add(signInButton);
            SignInPanel.Location = new Point(365, 199);
            SignInPanel.Name = "SignInPanel";
            SignInPanel.Padding = new Padding(10);
            SignInPanel.Size = new Size(500, 600);
            SignInPanel.TabIndex = 0;
            // 
            // errorMessageLabel
            // 
            errorMessageLabel.AutoSize = true;
            errorMessageLabel.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            errorMessageLabel.ForeColor = Color.Red;
            errorMessageLabel.Location = new Point(86, 496);
            errorMessageLabel.Margin = new Padding(2, 0, 2, 0);
            errorMessageLabel.Name = "errorMessageLabel";
            errorMessageLabel.Size = new Size(0, 21);
            errorMessageLabel.TabIndex = 5;
            // 
            // signInLabel
            // 
            signInLabel.AutoSize = true;
            signInLabel.Dock = DockStyle.Top;
            signInLabel.Font = new Font("Arial", 18F, FontStyle.Bold);
            signInLabel.Location = new Point(10, 10);
            signInLabel.Name = "signInLabel";
            signInLabel.Size = new Size(426, 43);
            signInLabel.TabIndex = 0;
            signInLabel.Text = "Ground Station - Sign In";
            signInLabel.Click += signInLabel_Click;
            // 
            // enterpriseLogo
            // 
            enterpriseLogo.Image = Properties.Resources.mission_control__2_;
            enterpriseLogo.Location = new Point(50, 80);
            enterpriseLogo.Name = "enterpriseLogo";
            enterpriseLogo.Size = new Size(400, 150);
            enterpriseLogo.SizeMode = PictureBoxSizeMode.Zoom;
            enterpriseLogo.TabIndex = 1;
            enterpriseLogo.TabStop = false;
            // 
            // userTextBox
            // 
            userTextBox.Font = new Font("Arial", 14F);
            userTextBox.Location = new Point(50, 260);
            userTextBox.Name = "userTextBox";
            userTextBox.PlaceholderText = "Username";
            userTextBox.Size = new Size(400, 40);
            userTextBox.TabIndex = 2;
            userTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // passTextBox
            // 
            passTextBox.Font = new Font("Arial", 14F);
            passTextBox.Location = new Point(50, 320);
            passTextBox.Name = "passTextBox";
            passTextBox.PasswordChar = '*';
            passTextBox.PlaceholderText = "Password";
            passTextBox.Size = new Size(400, 40);
            passTextBox.TabIndex = 3;
            passTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // signInButton
            // 
            signInButton.BackColor = Color.DodgerBlue;
            signInButton.Font = new Font("Arial", 14F, FontStyle.Bold);
            signInButton.ForeColor = Color.White;
            signInButton.Location = new Point(150, 400);
            signInButton.Name = "signInButton";
            signInButton.Size = new Size(200, 50);
            signInButton.TabIndex = 4;
            signInButton.Text = "Sign In";
            signInButton.UseVisualStyleBackColor = false;
            signInButton.Click += signInLabel_Click;
            // 
            // SignInPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1182, 955);
            Controls.Add(SignInPanel);
            Name = "SignInPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ground Station - Sign In";
            SignInPanel.ResumeLayout(false);
            SignInPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)enterpriseLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel SignInPanel;
        private TextBox passTextBox;
        private Button signInButton;
        private TextBox userTextBox;
        private Label signInLabel;
        private PictureBox enterpriseLogo;
        private Label errorMessageLabel;
    }
}
