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
            this.infectNeighbors = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // infectNeighbors
            // 
            this.infectNeighbors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infectNeighbors.Location = new System.Drawing.Point(1333, 116);
            this.infectNeighbors.Name = "infectNeighbors";
            this.infectNeighbors.Size = new System.Drawing.Size(206, 46);
            this.infectNeighbors.TabIndex = 3;
            this.infectNeighbors.Text = "Infect Neighbors";
            this.infectNeighbors.UseVisualStyleBackColor = true;
            this.infectNeighbors.Click += new System.EventHandler(this.infectNeighbors_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1563, 748);
            this.Controls.Add(this.infectNeighbors);
            this.Controls.Add(this.pickRandom);
            this.Controls.Add(this.buildTreeButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private Button buildTreeButton;
        private Button pickRandom;
        private Button infectNeighbors;
    }
}