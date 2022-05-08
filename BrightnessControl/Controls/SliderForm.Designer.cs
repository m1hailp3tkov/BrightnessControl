namespace BrightnessControl
{
    partial class SliderForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SliderForm));
            this.trackBar = new TrackBarWithoutFocus();
            this.label = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.brightnessSymbolLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.trackBar.LargeChange = 20;
            this.trackBar.Location = new System.Drawing.Point(40, 12);
            this.trackBar.Maximum = 100;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(240, 45);
            this.trackBar.SmallChange = 5;
            this.trackBar.TabIndex = 50;
            this.trackBar.TabStop = false;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trackBar_KeyDown);
            this.trackBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trackBar_KeyUp);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label.Location = new System.Drawing.Point(279, 12);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(37, 21);
            this.label.TabIndex = 1;
            this.label.Text = "100";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Brightness";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // brightnessSymbolLabel
            // 
            this.brightnessSymbolLabel.AutoSize = true;
            this.brightnessSymbolLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.brightnessSymbolLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.brightnessSymbolLabel.Location = new System.Drawing.Point(5, 4);
            this.brightnessSymbolLabel.Name = "brightnessSymbolLabel";
            this.brightnessSymbolLabel.Size = new System.Drawing.Size(38, 32);
            this.brightnessSymbolLabel.TabIndex = 51;
            this.brightnessSymbolLabel.Text = "🔆";
            // 
            // SliderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(319, 87);
            this.Controls.Add(this.brightnessSymbolLabel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.trackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SliderForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SliderForm";
            this.Activated += new System.EventHandler(this.SliderForm_Activated);
            this.Deactivate += new System.EventHandler(this.SliderForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SliderForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBarWithoutFocus trackBar;
        private Label label;
        private NotifyIcon notifyIcon;
        private Label brightnessSymbolLabel;
    }
}