namespace CDH_GroundStation_Group6
{
    partial class SignInPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SignInPanel = new Panel();
            companyLogo = new PictureBox();
            userID = new TextBox();
            userPassword = new TextBox();
            SignInLabel = new Label();
            SignInPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)companyLogo).BeginInit();
            SuspendLayout();
            // 
            // SignInPanel
            // 
            SignInPanel.Controls.Add(companyLogo);
            SignInPanel.Controls.Add(userID);
            SignInPanel.Controls.Add(userPassword);
            SignInPanel.Controls.Add(SignInLabel);
            SignInPanel.Location = new Point(472, 200);
            SignInPanel.Name = "SignInPanel";
            SignInPanel.Size = new Size(296, 300);
            SignInPanel.TabIndex = 0;
            SignInPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            // 
            // companyLogo
            // 
            companyLogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            companyLogo.Image = Properties.Resources.mission_control__2_;
            companyLogo.Location = new Point(83, 41);
            companyLogo.Name = "companyLogo";
            companyLogo.Size = new Size(125, 205);
            companyLogo.TabIndex = 4;
            companyLogo.TabStop = false;
            // 
            // userID
            // 
            userID.Dock = DockStyle.Bottom;
            userID.Location = new Point(0, 246);
            userID.Name = "userID";
            userID.PlaceholderText = "User ID";
            userID.Size = new Size(296, 27);
            userID.TabIndex = 3;
            userID.TextAlign = HorizontalAlignment.Center;
            // 
            // userPassword
            // 
            userPassword.Dock = DockStyle.Bottom;
            userPassword.Location = new Point(0, 273);
            userPassword.Name = "userPassword";
            userPassword.PlaceholderText = "Password";
            userPassword.Size = new Size(296, 27);
            userPassword.TabIndex = 2;
            userPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // SignInLabel
            // 
            SignInLabel.Dock = DockStyle.Top;
            SignInLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SignInLabel.Location = new Point(0, 0);
            SignInLabel.Name = "SignInLabel";
            SignInLabel.Size = new Size(296, 313);
            SignInLabel.TabIndex = 0;
            SignInLabel.Text = "Sign In";
            SignInLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // SignInPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            ClientSize = new Size(1255, 681);
            Controls.Add(SignInPanel);
            Name = "SignInPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Welcome to Ground Control - Sign In";
            Load += SignInPage_Load;
            SignInPanel.ResumeLayout(false);
            SignInPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)companyLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel SignInPanel;
        private Label SignInLabel;
        private TextBox userPassword;
        private TextBox userID;
        private PictureBox companyLogo;
    }
}
