namespace Dashboard
{
    partial class Dashboard
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
            statusPanel = new Panel();
            tempSatTextBox = new TextBox();
            speedSatTextBox = new TextBox();
            altSatTextBox = new TextBox();
            longSatTextBox = new TextBox();
            latSatTextBox = new TextBox();
            tempLabel = new Label();
            velLabel = new Label();
            altLabel = new Label();
            longLabel = new Label();
            latLabel = new Label();
            statusLabel = new Label();
            controlPanel = new Panel();
            textBox4 = new TextBox();
            label5 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            panel1 = new Panel();
            signInButton = new Button();
            autoRadioButton = new RadioButton();
            centreRadioButton = new RadioButton();
            satRadioButton = new RadioButton();
            visualPanel = new Panel();
            consoleTextBox = new TextBox();
            panel3 = new Panel();
            latencyTextBox = new TextBox();
            pktTextBox = new TextBox();
            bandTextBox = new TextBox();
            latencyLabel = new Label();
            dropPacketLabel = new Label();
            latencyRateLabel = new Label();
            packetRateLabel = new Label();
            bandWSpeedLabel = new Label();
            bandLabel = new Label();
            panel2 = new Panel();
            label6 = new Label();
            verticalProgressBar = new VerticalProgressBar();
            visualEarthPanel = new Panel();
            visualLabelSection = new Label();
            statusPanel.SuspendLayout();
            controlPanel.SuspendLayout();
            panel1.SuspendLayout();
            visualPanel.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // statusPanel
            // 
            statusPanel.BackColor = Color.Silver;
            statusPanel.BorderStyle = BorderStyle.Fixed3D;
            statusPanel.Controls.Add(tempSatTextBox);
            statusPanel.Controls.Add(speedSatTextBox);
            statusPanel.Controls.Add(altSatTextBox);
            statusPanel.Controls.Add(longSatTextBox);
            statusPanel.Controls.Add(latSatTextBox);
            statusPanel.Controls.Add(tempLabel);
            statusPanel.Controls.Add(velLabel);
            statusPanel.Controls.Add(altLabel);
            statusPanel.Controls.Add(longLabel);
            statusPanel.Controls.Add(latLabel);
            statusPanel.Controls.Add(statusLabel);
            statusPanel.Location = new Point(87, 47);
            statusPanel.Name = "statusPanel";
            statusPanel.Size = new Size(651, 1573);
            statusPanel.TabIndex = 0;
            // 
            // tempSatTextBox
            // 
            tempSatTextBox.Location = new Point(36, 927);
            tempSatTextBox.Name = "tempSatTextBox";
            tempSatTextBox.ReadOnly = true;
            tempSatTextBox.Size = new Size(461, 39);
            tempSatTextBox.TabIndex = 21;
            // 
            // speedSatTextBox
            // 
            speedSatTextBox.Location = new Point(38, 736);
            speedSatTextBox.Name = "speedSatTextBox";
            speedSatTextBox.ReadOnly = true;
            speedSatTextBox.Size = new Size(461, 39);
            speedSatTextBox.TabIndex = 20;
            // 
            // altSatTextBox
            // 
            altSatTextBox.Location = new Point(36, 549);
            altSatTextBox.Name = "altSatTextBox";
            altSatTextBox.ReadOnly = true;
            altSatTextBox.Size = new Size(461, 39);
            altSatTextBox.TabIndex = 19;
            // 
            // longSatTextBox
            // 
            longSatTextBox.Location = new Point(36, 377);
            longSatTextBox.Name = "longSatTextBox";
            longSatTextBox.ReadOnly = true;
            longSatTextBox.Size = new Size(461, 39);
            longSatTextBox.TabIndex = 18;
            // 
            // latSatTextBox
            // 
            latSatTextBox.Location = new Point(38, 204);
            latSatTextBox.Name = "latSatTextBox";
            latSatTextBox.ReadOnly = true;
            latSatTextBox.Size = new Size(461, 39);
            latSatTextBox.TabIndex = 17;
            // 
            // tempLabel
            // 
            tempLabel.AutoSize = true;
            tempLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tempLabel.Location = new Point(36, 850);
            tempLabel.Name = "tempLabel";
            tempLabel.Size = new Size(270, 32);
            tempLabel.TabIndex = 9;
            tempLabel.Text = "Temperature (Celsius):";
            // 
            // velLabel
            // 
            velLabel.AutoSize = true;
            velLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            velLabel.Location = new Point(36, 649);
            velLabel.Name = "velLabel";
            velLabel.Size = new Size(196, 32);
            velLabel.TabIndex = 7;
            velLabel.Text = "Velocity (km/h):";
            // 
            // altLabel
            // 
            altLabel.AutoSize = true;
            altLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            altLabel.Location = new Point(38, 475);
            altLabel.Name = "altLabel";
            altLabel.Size = new Size(113, 32);
            altLabel.TabIndex = 5;
            altLabel.Text = "Altitude:";
            // 
            // longLabel
            // 
            longLabel.AutoSize = true;
            longLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            longLabel.Location = new Point(38, 306);
            longLabel.Name = "longLabel";
            longLabel.Size = new Size(146, 32);
            longLabel.TabIndex = 3;
            longLabel.Text = "Longtitude:";
            // 
            // latLabel
            // 
            latLabel.AutoSize = true;
            latLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            latLabel.Location = new Point(38, 159);
            latLabel.Name = "latLabel";
            latLabel.Size = new Size(123, 32);
            latLabel.TabIndex = 2;
            latLabel.Text = "Lattitude:";
            latLabel.Click += label1_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Arial", 19.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusLabel.Location = new Point(29, 29);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(352, 60);
            statusLabel.TabIndex = 0;
            statusLabel.Text = "Satellite Label";
            // 
            // controlPanel
            // 
            controlPanel.BackColor = Color.Silver;
            controlPanel.BorderStyle = BorderStyle.Fixed3D;
            controlPanel.Controls.Add(textBox4);
            controlPanel.Controls.Add(label5);
            controlPanel.Controls.Add(textBox3);
            controlPanel.Controls.Add(label4);
            controlPanel.Controls.Add(textBox2);
            controlPanel.Controls.Add(label3);
            controlPanel.Controls.Add(label2);
            controlPanel.Controls.Add(label1);
            controlPanel.Controls.Add(textBox1);
            controlPanel.Controls.Add(panel1);
            controlPanel.Location = new Point(866, 47);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(651, 1573);
            controlPanel.TabIndex = 12;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(89, 708);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(461, 39);
            textBox4.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.DarkGray;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(89, 649);
            label5.Name = "label5";
            label5.Size = new Size(196, 32);
            label5.TabIndex = 12;
            label5.Text = "Velocity (km/h):";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(89, 531);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(461, 39);
            textBox3.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.DarkGray;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(89, 475);
            label4.Name = "label4";
            label4.Size = new Size(113, 32);
            label4.TabIndex = 12;
            label4.Text = "Altitude:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(89, 357);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(461, 39);
            textBox2.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.DarkGray;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(89, 306);
            label3.Name = "label3";
            label3.Size = new Size(146, 32);
            label3.TabIndex = 12;
            label3.Text = "Longtitude:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.DarkGray;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(89, 159);
            label2.Name = "label2";
            label2.Size = new Size(123, 32);
            label2.TabIndex = 12;
            label2.Text = "Lattitude:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 19.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(50, 29);
            label1.Name = "label1";
            label1.Size = new Size(220, 60);
            label1.TabIndex = 12;
            label1.Text = "Controls";
            label1.Click += label1_Click_1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(89, 204);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(461, 39);
            textBox1.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkGray;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(signInButton);
            panel1.Controls.Add(autoRadioButton);
            panel1.Controls.Add(centreRadioButton);
            panel1.Controls.Add(satRadioButton);
            panel1.Location = new Point(43, 131);
            panel1.Name = "panel1";
            panel1.Size = new Size(552, 1033);
            panel1.TabIndex = 16;
            // 
            // signInButton
            // 
            signInButton.BackColor = Color.YellowGreen;
            signInButton.FlatAppearance.BorderSize = 0;
            signInButton.Font = new Font("Arial", 14F, FontStyle.Bold);
            signInButton.ForeColor = Color.White;
            signInButton.Location = new Point(143, 935);
            signInButton.Margin = new Padding(4);
            signInButton.Name = "signInButton";
            signInButton.Size = new Size(260, 64);
            signInButton.TabIndex = 13;
            signInButton.Text = "Send";
            signInButton.UseVisualStyleBackColor = false;
            // 
            // autoRadioButton
            // 
            autoRadioButton.AutoSize = true;
            autoRadioButton.Location = new Point(44, 679);
            autoRadioButton.Name = "autoRadioButton";
            autoRadioButton.Size = new Size(150, 36);
            autoRadioButton.TabIndex = 0;
            autoRadioButton.TabStop = true;
            autoRadioButton.Text = "Auto Pilot";
            autoRadioButton.UseVisualStyleBackColor = true;
            // 
            // centreRadioButton
            // 
            centreRadioButton.AutoSize = true;
            centreRadioButton.Location = new Point(301, 797);
            centreRadioButton.Name = "centreRadioButton";
            centreRadioButton.Size = new Size(116, 36);
            centreRadioButton.TabIndex = 2;
            centreRadioButton.TabStop = true;
            centreRadioButton.Text = "Centre";
            centreRadioButton.UseVisualStyleBackColor = true;
            // 
            // satRadioButton
            // 
            satRadioButton.AutoSize = true;
            satRadioButton.Location = new Point(44, 797);
            satRadioButton.Name = "satRadioButton";
            satRadioButton.Size = new Size(130, 36);
            satRadioButton.TabIndex = 1;
            satRadioButton.TabStop = true;
            satRadioButton.Text = "Satellite";
            satRadioButton.UseVisualStyleBackColor = true;
            // 
            // visualPanel
            // 
            visualPanel.BackColor = Color.Silver;
            visualPanel.BorderStyle = BorderStyle.Fixed3D;
            visualPanel.Controls.Add(consoleTextBox);
            visualPanel.Controls.Add(panel3);
            visualPanel.Controls.Add(panel2);
            visualPanel.Controls.Add(visualEarthPanel);
            visualPanel.Controls.Add(visualLabelSection);
            visualPanel.Location = new Point(1648, 47);
            visualPanel.Name = "visualPanel";
            visualPanel.Size = new Size(718, 1573);
            visualPanel.TabIndex = 11;
            // 
            // consoleTextBox
            // 
            consoleTextBox.Location = new Point(72, 974);
            consoleTextBox.Multiline = true;
            consoleTextBox.Name = "consoleTextBox";
            consoleTextBox.ReadOnly = true;
            consoleTextBox.Size = new Size(568, 479);
            consoleTextBox.TabIndex = 21;
            consoleTextBox.TextChanged += consoleTextBox_TextChanged;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkGray;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(latencyTextBox);
            panel3.Controls.Add(pktTextBox);
            panel3.Controls.Add(bandTextBox);
            panel3.Controls.Add(latencyLabel);
            panel3.Controls.Add(dropPacketLabel);
            panel3.Controls.Add(latencyRateLabel);
            panel3.Controls.Add(packetRateLabel);
            panel3.Controls.Add(bandWSpeedLabel);
            panel3.Controls.Add(bandLabel);
            panel3.Location = new Point(329, 535);
            panel3.Name = "panel3";
            panel3.Size = new Size(309, 381);
            panel3.TabIndex = 20;
            // 
            // latencyTextBox
            // 
            latencyTextBox.Location = new Point(20, 300);
            latencyTextBox.Name = "latencyTextBox";
            latencyTextBox.ReadOnly = true;
            latencyTextBox.Size = new Size(101, 39);
            latencyTextBox.TabIndex = 13;
            // 
            // pktTextBox
            // 
            pktTextBox.Location = new Point(20, 192);
            pktTextBox.Name = "pktTextBox";
            pktTextBox.ReadOnly = true;
            pktTextBox.Size = new Size(101, 39);
            pktTextBox.TabIndex = 12;
            // 
            // bandTextBox
            // 
            bandTextBox.Location = new Point(20, 79);
            bandTextBox.Name = "bandTextBox";
            bandTextBox.ReadOnly = true;
            bandTextBox.Size = new Size(101, 39);
            bandTextBox.TabIndex = 11;
            // 
            // latencyLabel
            // 
            latencyLabel.AutoSize = true;
            latencyLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            latencyLabel.Location = new Point(14, 254);
            latencyLabel.Name = "latencyLabel";
            latencyLabel.Size = new Size(101, 32);
            latencyLabel.TabIndex = 10;
            latencyLabel.Text = "Latency";
            // 
            // dropPacketLabel
            // 
            dropPacketLabel.AutoSize = true;
            dropPacketLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dropPacketLabel.Location = new Point(14, 135);
            dropPacketLabel.Name = "dropPacketLabel";
            dropPacketLabel.Size = new Size(63, 32);
            dropPacketLabel.TabIndex = 9;
            dropPacketLabel.Text = "DPR";
            // 
            // latencyRateLabel
            // 
            latencyRateLabel.AutoSize = true;
            latencyRateLabel.Font = new Font("Segoe UI", 9F);
            latencyRateLabel.Location = new Point(125, 303);
            latencyRateLabel.Name = "latencyRateLabel";
            latencyRateLabel.Size = new Size(45, 32);
            latencyRateLabel.TabIndex = 8;
            latencyRateLabel.Text = "ms";
            // 
            // packetRateLabel
            // 
            packetRateLabel.AutoSize = true;
            packetRateLabel.Font = new Font("Segoe UI", 9F);
            packetRateLabel.Location = new Point(125, 199);
            packetRateLabel.Name = "packetRateLabel";
            packetRateLabel.Size = new Size(72, 32);
            packetRateLabel.TabIndex = 7;
            packetRateLabel.Text = "pkt(s)";
            // 
            // bandWSpeedLabel
            // 
            bandWSpeedLabel.AutoSize = true;
            bandWSpeedLabel.Font = new Font("Segoe UI", 9F);
            bandWSpeedLabel.Location = new Point(125, 80);
            bandWSpeedLabel.Name = "bandWSpeedLabel";
            bandWSpeedLabel.Size = new Size(59, 32);
            bandWSpeedLabel.TabIndex = 4;
            bandWSpeedLabel.Text = "kb/s";
            // 
            // bandLabel
            // 
            bandLabel.AutoSize = true;
            bandLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bandLabel.Location = new Point(14, 15);
            bandLabel.Name = "bandLabel";
            bandLabel.Size = new Size(136, 32);
            bandLabel.TabIndex = 2;
            bandLabel.Text = "Bandwidth";
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkGray;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(verticalProgressBar);
            panel2.Location = new Point(73, 535);
            panel2.Name = "panel2";
            panel2.Size = new Size(221, 381);
            panel2.TabIndex = 19;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(31, 15);
            label6.Name = "label6";
            label6.Size = new Size(149, 32);
            label6.TabIndex = 1;
            label6.Text = "Signal Level";
            label6.Click += label6_Click;
            // 
            // verticalProgressBar
            // 
            verticalProgressBar.Location = new Point(80, 80);
            verticalProgressBar.Name = "verticalProgressBar";
            verticalProgressBar.Size = new Size(50, 272);
            verticalProgressBar.TabIndex = 0;
            verticalProgressBar.Value = 50;
            // 
            // visualEarthPanel
            // 
            visualEarthPanel.BackColor = Color.DarkGray;
            visualEarthPanel.BorderStyle = BorderStyle.Fixed3D;
            visualEarthPanel.Location = new Point(72, 141);
            visualEarthPanel.Name = "visualEarthPanel";
            visualEarthPanel.Size = new Size(568, 333);
            visualEarthPanel.TabIndex = 18;
            // 
            // visualLabelSection
            // 
            visualLabelSection.AutoSize = true;
            visualLabelSection.Font = new Font("Arial", 19.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            visualLabelSection.Location = new Point(62, 29);
            visualLabelSection.Name = "visualLabelSection";
            visualLabelSection.Size = new Size(192, 60);
            visualLabelSection.TabIndex = 17;
            visualLabelSection.Text = "Visuals";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(2474, 1679);
            Controls.Add(visualPanel);
            Controls.Add(controlPanel);
            Controls.Add(statusPanel);
            Name = "Dashboard";
            Text = "Dashboard - Ground Station";
            statusPanel.ResumeLayout(false);
            statusPanel.PerformLayout();
            controlPanel.ResumeLayout(false);
            controlPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            visualPanel.ResumeLayout(false);
            visualPanel.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel statusPanel;
        private Label statusLabel;
        private Label latLabel;
        private Label velLabel;
        private Label altLabel;
        private Label longLabel;
        private Label tempLabel;
        private Panel controlPanel;
        private Label label1;
        private TextBox textBox1;
        private RadioButton centreRadioButton;
        private RadioButton satRadioButton;
        private RadioButton autoRadioButton;
        private TextBox textBox4;
        private Label label5;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox2;
        private Label label3;
        private Label label2;
        private Button signInButton;
        private Panel panel1;
        private Panel visualPanel;
        private Panel visualEarthPanel;
        private Label visualLabelSection;
        private Panel panel2;
        private VerticalProgressBar verticalProgressBar;
        private Label label6;
        private Panel panel3;
        private Label bandLabel;
        private Label bandWSpeedLabel;
        private Label latencyLabel;
        private Label dropPacketLabel;
        private Label latencyRateLabel;
        private Label packetRateLabel;
        private TextBox consoleTextBox;
        private TextBox tempSatTextBox;
        private TextBox speedSatTextBox;
        private TextBox altSatTextBox;
        private TextBox longSatTextBox;
        private TextBox latSatTextBox;
        private TextBox latencyTextBox;
        private TextBox pktTextBox;
        private TextBox bandTextBox;
    }
}
