namespace QuadTreeDemo
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buildTreeButton = new System.Windows.Forms.Button();
            this.pickRandom = new System.Windows.Forms.Button();
            this.igniteNeighbors = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1315, 724);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // buildTreeButton
            // 
            this.buildTreeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buildTreeButton.Location = new System.Drawing.Point(1333, 12);
            this.buildTreeButton.Name = "buildTreeButton";
            this.buildTreeButton.Size = new System.Drawing.Size(206, 46);
            this.buildTreeButton.TabIndex = 1;
            this.buildTreeButton.Text = "Build Tree";
            this.buildTreeButton.UseVisualStyleBackColor = true;
            this.buildTreeButton.Click += new System.EventHandler(this.buildTreeButton_Click);
            // 
            // pickRandom
            // 
            this.pickRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pickRandom.Location = new System.Drawing.Point(1333, 64);
            this.pickRandom.Name = "pickRandom";
            this.pickRandom.Size = new System.Drawing.Size(206, 46);
            this.pickRandom.TabIndex = 2;
            this.pickRandom.Text = "Pick Random";
            this.pickRandom.UseVisualStyleBackColor = true;
            this.pickRandom.Click += new System.EventHandler(this.pickRandom_Click);
            // 
            // igniteNeighbors
            // 
            this.igniteNeighbors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.igniteNeighbors.Location = new System.Drawing.Point(1333, 116);
            this.igniteNeighbors.Name = "igniteNeighbors";
            this.igniteNeighbors.Size = new System.Drawing.Size(206, 46);
            this.igniteNeighbors.TabIndex = 3;
            this.igniteNeighbors.Text = "Ignite Neighbors";
            this.igniteNeighbors.UseVisualStyleBackColor = true;
            this.igniteNeighbors.Click += new System.EventHandler(this.igniteNeighbors_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(1333, 225);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(208, 90);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1333, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Spread Radius";
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(1333, 704);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(0, 32);
            this.timeLabel.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(1333, 321);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(202, 36);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Use Quad Tree";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(1333, 363);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(175, 36);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Draw Quads";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // radiusLabel
            // 
            this.radiusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(1333, 273);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(78, 32);
            this.radiusLabel.TabIndex = 9;
            this.radiusLabel.Text = "label2";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1333, 643);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 46);
            this.button1.TabIndex = 10;
            this.button1.Text = "Open Time Attack";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1563, 748);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radiusLabel);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.igniteNeighbors);
            this.Controls.Add(this.pickRandom);
            this.Controls.Add(this.buildTreeButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Forest Fire";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Button buildTreeButton;
        private Button pickRandom;
        private Button igniteNeighbors;
        private TrackBar trackBar1;
        private Label label1;
        private Label timeLabel;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label radiusLabel;
        private Button button1;
    }
}