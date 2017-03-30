using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseStuffForms
{
	public partial class LoggingWindow : Form
	{
		public LoggingWindow()
		{
			InitializeComponent();
			CreateGrid ();
		}

		private void CreateGrid ()
		{
			gridOutput.ColumnCount = 5;
			gridOutput.ColumnHeadersVisible = true;

			// Set the column header names.
		   gridOutput.Columns[0].Name = "Number";
		   gridOutput.Columns[1].Name = "Time";
		   gridOutput.Columns[2].Name = "Component";
		   gridOutput.Columns[3].Name = "Title";
		   gridOutput.Columns[4].Name = "Message";
		}
	}
}
