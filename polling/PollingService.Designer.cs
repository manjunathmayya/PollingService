
namespace PollingService
{
    partial class PollingService
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
            this.btnStart = new System.Windows.Forms.Button();
            this.lblThreadCount = new System.Windows.Forms.Label();
            this.txtThreadCount = new System.Windows.Forms.TextBox();
            this.btnStopWatch = new System.Windows.Forms.Button();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(260, 56);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(84, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Watch";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblThreadCount
            // 
            this.lblThreadCount.AutoSize = true;
            this.lblThreadCount.Location = new System.Drawing.Point(30, 56);
            this.lblThreadCount.Name = "lblThreadCount";
            this.lblThreadCount.Size = new System.Drawing.Size(79, 15);
            this.lblThreadCount.TabIndex = 2;
            this.lblThreadCount.Text = "Thread Count";
            // 
            // txtThreadCount
            // 
            this.txtThreadCount.Location = new System.Drawing.Point(115, 53);
            this.txtThreadCount.Name = "txtThreadCount";
            this.txtThreadCount.Size = new System.Drawing.Size(100, 23);
            this.txtThreadCount.TabIndex = 3;
            this.txtThreadCount.Text = "4";
            // 
            // btnStopWatch
            // 
            this.btnStopWatch.Location = new System.Drawing.Point(361, 56);
            this.btnStopWatch.Name = "btnStopWatch";
            this.btnStopWatch.Size = new System.Drawing.Size(86, 23);
            this.btnStopWatch.TabIndex = 4;
            this.btnStopWatch.Text = "Stop Watch";
            this.btnStopWatch.UseVisualStyleBackColor = true;
            this.btnStopWatch.Click += new System.EventHandler(this.btnStopWatch_Click);
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(30, 13);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(43, 15);
            this.lblSource.TabIndex = 5;
            this.lblSource.Text = "Source";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(79, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(204, 23);
            this.txtSource.TabIndex = 6;
            this.txtSource.Text = "C:\\Root";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(361, 11);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(204, 23);
            this.txtDestination.TabIndex = 8;
            this.txtDestination.Text = "C:\\Destination";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(291, 14);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(67, 15);
            this.lblDestination.TabIndex = 7;
            this.lblDestination.Text = "Destination";
            // 
            // PollingService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 88);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.btnStopWatch);
            this.Controls.Add(this.txtThreadCount);
            this.Controls.Add(this.lblThreadCount);
            this.Controls.Add(this.btnStart);
            this.Name = "PollingService";
            this.Text = "PollingService";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblThreadCount;
        private System.Windows.Forms.TextBox txtThreadCount;
        private System.Windows.Forms.Button btnStopWatch;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label lblDestination;
    }
}

