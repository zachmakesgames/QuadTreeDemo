namespace QuadTreeDemo
{
    partial class TimeAttack
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nodeBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.treeWidthBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.searchRadiusBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.runCountBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.timeAttackButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.treeTimeLabel = new System.Windows.Forms.Label();
            this.noTreeTimeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nodeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeWidthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchRadiusBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.runCountBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nodeBox
            // 
            this.nodeBox.Location = new System.Drawing.Point(12, 12);
            this.nodeBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nodeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nodeBox.Name = "nodeBox";
            this.nodeBox.Size = new System.Drawing.Size(240, 39);
            this.nodeBox.TabIndex = 0;
            this.nodeBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nodes to generate";
            // 
            // treeWidthBox
            // 
            this.treeWidthBox.Location = new System.Drawing.Point(12, 57);
            this.treeWidthBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.treeWidthBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.treeWidthBox.Name = "treeWidthBox";
            this.treeWidthBox.Size = new System.Drawing.Size(240, 39);
            this.treeWidthBox.TabIndex = 2;
            this.treeWidthBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tree width";
            // 
            // searchRadiusBox
            // 
            this.searchRadiusBox.Location = new System.Drawing.Point(12, 102);
            this.searchRadiusBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.searchRadiusBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.searchRadiusBox.Name = "searchRadiusBox";
            this.searchRadiusBox.Size = new System.Drawing.Size(240, 39);
            this.searchRadiusBox.TabIndex = 4;
            this.searchRadiusBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Search Radius";
            // 
            // runCountBox
            // 
            this.runCountBox.Location = new System.Drawing.Point(12, 147);
            this.runCountBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.runCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.runCountBox.Name = "runCountBox";
            this.runCountBox.Size = new System.Drawing.Size(240, 39);
            this.runCountBox.TabIndex = 6;
            this.runCountBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 32);
            this.label4.TabIndex = 7;
            this.label4.Text = "Run count";
            // 
            // timeAttackButton
            // 
            this.timeAttackButton.Location = new System.Drawing.Point(12, 192);
            this.timeAttackButton.Name = "timeAttackButton";
            this.timeAttackButton.Size = new System.Drawing.Size(240, 46);
            this.timeAttackButton.TabIndex = 8;
            this.timeAttackButton.Text = "Run";
            this.timeAttackButton.UseVisualStyleBackColor = true;
            this.timeAttackButton.Click += new System.EventHandler(this.timeAttackButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(321, 32);
            this.label5.TabIndex = 9;
            this.label5.Text = "Average time with tree (ms): ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(357, 32);
            this.label6.TabIndex = 10;
            this.label6.Text = "Average time without tree (ms): ";
            // 
            // treeTimeLabel
            // 
            this.treeTimeLabel.AutoSize = true;
            this.treeTimeLabel.Location = new System.Drawing.Point(339, 279);
            this.treeTimeLabel.Name = "treeTimeLabel";
            this.treeTimeLabel.Size = new System.Drawing.Size(27, 32);
            this.treeTimeLabel.TabIndex = 11;
            this.treeTimeLabel.Text = "0";
            // 
            // noTreeTimeLabel
            // 
            this.noTreeTimeLabel.AutoSize = true;
            this.noTreeTimeLabel.Location = new System.Drawing.Point(375, 321);
            this.noTreeTimeLabel.Name = "noTreeTimeLabel";
            this.noTreeTimeLabel.Size = new System.Drawing.Size(27, 32);
            this.noTreeTimeLabel.TabIndex = 12;
            this.noTreeTimeLabel.Text = "0";
            // 
            // TimeAttack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 441);
            this.Controls.Add(this.noTreeTimeLabel);
            this.Controls.Add(this.treeTimeLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.timeAttackButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.runCountBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchRadiusBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeWidthBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nodeBox);
            this.Name = "TimeAttack";
            this.Text = "TimeAttack";
            ((System.ComponentModel.ISupportInitialize)(this.nodeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeWidthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchRadiusBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.runCountBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown nodeBox;
        private Label label1;
        private NumericUpDown treeWidthBox;
        private Label label2;
        private NumericUpDown searchRadiusBox;
        private Label label3;
        private NumericUpDown runCountBox;
        private Label label4;
        private Button timeAttackButton;
        private Label label5;
        private Label label6;
        private Label treeTimeLabel;
        private Label noTreeTimeLabel;
    }
}