
namespace cos1_gol
{
    partial class Options
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
            this.Save = new System.Windows.Forms.Button();
            this.TickerIntervalLabel = new System.Windows.Forms.Label();
            this.XCellsLabel = new System.Windows.Forms.Label();
            this.YCellsLabel = new System.Windows.Forms.Label();
            this.TickerInterval = new System.Windows.Forms.NumericUpDown();
            this.XCells = new System.Windows.Forms.NumericUpDown();
            this.YCells = new System.Windows.Forms.NumericUpDown();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TickerInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCells)).BeginInit();
            this.SuspendLayout();
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Save.Location = new System.Drawing.Point(118, 108);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 0;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // TickerIntervalLabel
            // 
            this.TickerIntervalLabel.AutoSize = true;
            this.TickerIntervalLabel.Location = new System.Drawing.Point(9, 9);
            this.TickerIntervalLabel.Name = "TickerIntervalLabel";
            this.TickerIntervalLabel.Size = new System.Drawing.Size(97, 13);
            this.TickerIntervalLabel.TabIndex = 1;
            this.TickerIntervalLabel.Text = "Ticker Interval (ms)";
            // 
            // XCellsLabel
            // 
            this.XCellsLabel.AutoSize = true;
            this.XCellsLabel.Location = new System.Drawing.Point(12, 36);
            this.XCellsLabel.Name = "XCellsLabel";
            this.XCellsLabel.Size = new System.Drawing.Size(39, 13);
            this.XCellsLabel.TabIndex = 2;
            this.XCellsLabel.Text = "X Cells";
            // 
            // YCellsLabel
            // 
            this.YCellsLabel.AutoSize = true;
            this.YCellsLabel.Location = new System.Drawing.Point(12, 62);
            this.YCellsLabel.Name = "YCellsLabel";
            this.YCellsLabel.Size = new System.Drawing.Size(39, 13);
            this.YCellsLabel.TabIndex = 3;
            this.YCellsLabel.Text = "Y Cells";
            // 
            // TickerInterval
            // 
            this.TickerInterval.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TickerInterval.Location = new System.Drawing.Point(136, 7);
            this.TickerInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.TickerInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TickerInterval.Name = "TickerInterval";
            this.TickerInterval.Size = new System.Drawing.Size(57, 20);
            this.TickerInterval.TabIndex = 4;
            this.TickerInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TickerInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // XCells
            // 
            this.XCells.Location = new System.Drawing.Point(136, 34);
            this.XCells.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.XCells.Name = "XCells";
            this.XCells.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.XCells.Size = new System.Drawing.Size(57, 20);
            this.XCells.TabIndex = 5;
            this.XCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.XCells.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // YCells
            // 
            this.YCells.Location = new System.Drawing.Point(136, 60);
            this.YCells.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.YCells.Name = "YCells";
            this.YCells.Size = new System.Drawing.Size(57, 20);
            this.YCells.TabIndex = 6;
            this.YCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YCells.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(12, 108);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AcceptButton = this.Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(205, 143);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.YCells);
            this.Controls.Add(this.XCells);
            this.Controls.Add(this.TickerInterval);
            this.Controls.Add(this.YCellsLabel);
            this.Controls.Add(this.XCellsLabel);
            this.Controls.Add(this.TickerIntervalLabel);
            this.Controls.Add(this.Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.TickerInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCells)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCells)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label TickerIntervalLabel;
        private System.Windows.Forms.Label XCellsLabel;
        private System.Windows.Forms.Label YCellsLabel;
        private System.Windows.Forms.NumericUpDown TickerInterval;
        private System.Windows.Forms.NumericUpDown XCells;
        private System.Windows.Forms.NumericUpDown YCells;
        private System.Windows.Forms.Button Cancel;
    }
}