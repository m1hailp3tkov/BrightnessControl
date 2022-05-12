using BrightnessControl.Helpers;

namespace BrightnessControl.Controls
{
    partial class BrightnessBlock
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar = new BrightnessControl.TrackBarWithoutFocus();
            this.label = new System.Windows.Forms.Label();
            this.brightnessIconLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.LargeChange = 20;
            this.trackBar.Location = new System.Drawing.Point(40, 13);
            this.trackBar.Maximum = 100;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(240, 45);
            this.trackBar.TabIndex = 0;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BrightnessBlock_KeyUp);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // label
            // 
            this.label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.label.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label.Location = new System.Drawing.Point(280, 12);
            this.label.Name = "label";
            this.label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label.Size = new System.Drawing.Size(38, 18);
            this.label.TabIndex = 1;
            this.label.Text = "100";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // brightnessIconLabel
            // 
            this.brightnessIconLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.brightnessIconLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.brightnessIconLabel.Location = new System.Drawing.Point(2, 0);
            this.brightnessIconLabel.Margin = new System.Windows.Forms.Padding(0);
            this.brightnessIconLabel.Name = "brightnessIconLabel";
            this.brightnessIconLabel.Size = new System.Drawing.Size(40, 40);
            this.brightnessIconLabel.TabIndex = 2;
            this.brightnessIconLabel.Text = "🔅";
            // 
            // BrightnessBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.Controls.Add(this.brightnessIconLabel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.trackBar);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BrightnessBlock";
            this.Size = new System.Drawing.Size(320, 45);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BrightnessBlock_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBarWithoutFocus trackBar;
        private Label label;
        private Label brightnessIconLabel;
    }
}
