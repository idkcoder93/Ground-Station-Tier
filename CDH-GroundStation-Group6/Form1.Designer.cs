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
            SignInPanel.Location = new Point(475, 255);
            SignInPanel.Margin = new Padding(4);
            SignInPanel.Name = "SignInPanel";
            SignInPanel.Padding = new Padding(13);
            SignInPanel.Size = new Size(650, 768);
            SignInPanel.TabIndex = 0;
            // 
            // errorMessageLabel
            // 
            errorMessageLabel.AutoSize = true;
            errorMessageLabel.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            errorMessageLabel.ForeColor = Color.Red;
            errorMessageLabel.Location = new Point(112, 635);
            errorMessageLabel.Name = "errorMessageLabel";
            errorMessageLabel.Size = new Size(0, 27);
            errorMessageLabel.TabIndex = 5;
            // 
            // signInLabel
            // 
            signInLabel.AutoSize = true;
            signInLabel.Dock = DockStyle.Top;
            signInLabel.Font = new Font("Arial", 18F, FontStyle.Bold);
            signInLabel.Location = new Point(13, 13);
            signInLabel.Margin = new Padding(4, 0, 4, 0);
            signInLabel.Name = "signInLabel";
            signInLabel.Size = new Size(571, 56);
            signInLabel.TabIndex = 0;
            signInLabel.Text = "Ground Station - Sign In";
            signInLabel.Click += signInLabel_Click;
            // 
            // enterpriseLogo
            // 
            enterpriseLogo.Image = Properties.Resources.mission_control__2_;
            enterpriseLogo.Location = new Point(65, 102);
            enterpriseLogo.Margin = new Padding(4);
            enterpriseLogo.Name = "enterpriseLogo";
            enterpriseLogo.Size = new Size(520, 192);
            enterpriseLogo.SizeMode = PictureBoxSizeMode.Zoom;
            enterpriseLogo.TabIndex = 1;
            enterpriseLogo.TabStop = false;
            // 
            // userTextBox
            // 
            userTextBox.Font = new Font("Arial", 14F);
            userTextBox.Location = new Point(65, 333);
            userTextBox.Margin = new Padding(4);
            userTextBox.Name = "userTextBox";
            userTextBox.PlaceholderText = "Username";
            userTextBox.Size = new Size(519, 50);
            userTextBox.TabIndex = 2;
            userTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // passTextBox
            // 
            passTextBox.Font = new Font("Arial", 14F);
            passTextBox.Location = new Point(65, 410);
            passTextBox.Margin = new Padding(4);
            passTextBox.Name = "passTextBox";
            passTextBox.PasswordChar = '*';
            passTextBox.PlaceholderText = "Password";
            passTextBox.Size = new Size(519, 50);
            passTextBox.TabIndex = 3;
            passTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // signInButton
            // 
            signInButton.BackColor = Color.DodgerBlue;
            signInButton.Font = new Font("Arial", 14F, FontStyle.Bold);
            signInButton.ForeColor = Color.White;
            signInButton.Location = new Point(195, 512);
            signInButton.Margin = new Padding(4);
            signInButton.Name = "signInButton";
            signInButton.Size = new Size(260, 64);
            signInButton.TabIndex = 4;
            signInButton.Text = "Sign In";
            signInButton.UseVisualStyleBackColor = false;
            signInButton.Click += signInLabel_Click;
            // 
            // SignInPage
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1537, 1222);
            Controls.Add(SignInPanel);
            Margin = new Padding(4);
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
