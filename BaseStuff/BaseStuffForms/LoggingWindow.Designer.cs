namespace BaseStuffForms
{
	partial class LoggingWindow
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
			this.pnlButtons = new System.Windows.Forms.Panel();
			this.pnlOutput = new System.Windows.Forms.Panel();
			this.gridOutput = new System.Windows.Forms.DataGridView();
			this.pnlOutput.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridOutput)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlButtons
			// 
			this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlButtons.Location = new System.Drawing.Point(0, 490);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Size = new System.Drawing.Size(1013, 100);
			this.pnlButtons.TabIndex = 0;
			// 
			// pnlOutput
			// 
			this.pnlOutput.Controls.Add(this.gridOutput);
			this.pnlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlOutput.Location = new System.Drawing.Point(0, 0);
			this.pnlOutput.Name = "pnlOutput";
			this.pnlOutput.Size = new System.Drawing.Size(1013, 490);
			this.pnlOutput.TabIndex = 1;
			// 
			// gridOutput
			// 
			this.gridOutput.AllowUserToAddRows = false;
			this.gridOutput.AllowUserToDeleteRows = false;
			this.gridOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridOutput.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.gridOutput.Location = new System.Drawing.Point(0, 0);
			this.gridOutput.Name = "gridOutput";
			this.gridOutput.ReadOnly = true;
			this.gridOutput.Size = new System.Drawing.Size(1013, 490);
			this.gridOutput.TabIndex = 0;
			// 
			// LoggingWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1013, 590);
			this.Controls.Add(this.pnlOutput);
			this.Controls.Add(this.pnlButtons);
			this.Name = "LoggingWindow";
			this.Text = "LoggingWindow";
			this.pnlOutput.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridOutput)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlButtons;
		private System.Windows.Forms.Panel pnlOutput;
		private System.Windows.Forms.DataGridView gridOutput;
	}
}