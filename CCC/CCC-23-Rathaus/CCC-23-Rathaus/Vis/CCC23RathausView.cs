using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCC_23_Rathaus.Vis
{
	public partial class CCC23RathausView : Form
	{
		[Flags]
		public enum Operation
		{
			Nix = 0,
			SpeedPlus,
			SpeedMinus
		}
		private class RGB { public int R; public int G; public int B; public RGB (int r, int g, int b) { R = r; G = g; B = b; } }
		private Label [] _segments;
		private string   _title;
		private int		 _sleepMSec = 50;
		public CCC23RathausView ()
		{
			InitializeComponent ();
		}

		public void Init (Car1 [] carArray, Road road, string title)
		{
			var rndSource = new Random (Environment.TickCount);
			foreach (var car in carArray) {
//				int raw = ( car.CarNum * 97 ) % 0x1000000;
//				int r = ( raw <<  0 ) & 0xFF ;
//				int g = ( raw <<  8 ) & 0xFF ;
//				int b = ( raw << 16 ) & 0xFF ;
				int r = 0; // rndSource.Next (0, 255);
				int g = rndSource.Next (0, 255);
				int b = rndSource.Next (0, 255);
				car.VisTag = new RGB (r, g, b);
			}

			var numDig = 1 + (int)Math.Log10 (road.CountSegments - 1);
			int colWidth = numDig * 8;
			int colHeight = (int)(colWidth * 1.5);
			Font f = new Font ("Arial", 5.0f, FontStyle.Regular);
			while (this.Controls.Count > 0)
				this.Controls.RemoveAt (0);

			_segments = new Label [road.CountSegments];

#if false
			var maxWidth = SystemInformation.VirtualScreen.Width - (this.Width - this.ClientRectangle.Width);
#else
			var maxWidth = SystemInformation.PrimaryMonitorSize.Width - (this.Width - this.ClientRectangle.Width);
#endif
			var maxCols  = maxWidth / colWidth;
			var numCols  = Math.Min (road.CountSegments, maxCols);
			var numRows  = (road.CountSegments + numCols - 1) / numCols;

			for (int i = 0; i < road.CountSegments; i++) {
				var l = new Label () {
					Left   = ( i % numCols ) * colWidth,
					Top    = ( i / numCols ) * colHeight,
					Width  = colWidth,
					Height = colHeight,
					BackColor = ((i % 2) == 0) ? Color.Yellow : Color.Turquoise,
					Font = f
				};
				this.Controls.Add (l);
				_segments[i] = l;
			}

			this.Width  = numCols * colWidth + (this.Width - this.ClientRectangle.Width);
			this.Height = numRows * colHeight + (this.Height - this.ClientRectangle.Height);

			this.Text = _title = string.Format ("{0} --->  {1} x {2}", title, numCols, numRows);


#if false
			if (this.Left + this.Width > SystemInformation.VirtualScreen.Left + SystemInformation.VirtualScreen.Width)
				this.Left = SystemInformation.VirtualScreen.Width - this.Width + SystemInformation.VirtualScreen.Left;
#else
			if (this.Left + this.Width > 0 + SystemInformation.PrimaryMonitorSize.Width)
				this.Left = SystemInformation.PrimaryMonitorSize.Width - this.Width + 0;
#endif

			this.Show ();
		}

		public void ShowData (Road road, int currStep, Operation op)
		{
			
			if (op.HasFlag (Operation.SpeedMinus)) {
				_sleepMSec += 25;
				_sleepMSec = Math.Min (_sleepMSec, 2000);
			}
			else if (op.HasFlag (Operation.SpeedPlus)) {
				_sleepMSec -= 25;
				_sleepMSec = Math.Max (_sleepMSec, 0);
			}
			this.Text = string.Format ("{0}  -  step {1}  -  wait {2}msec", _title, currStep, _sleepMSec);
			this.SuspendLayout ();
			for (int i = 0; i < road.CountSegments; i++) {
				var car = road[i];
				if (car == null) {
					// empty segment
					_segments[i].BackColor = Color.WhiteSmoke;
					_segments[i].ForeColor = Color.Black;
					_segments[i].Text = string.Format ("{0}", i);
					_segments[i].TextAlign = ContentAlignment.BottomCenter;
				}
				else {
					if (car.Waiting) {
						// car is waiting
						_segments[i].BackColor = Color.Red;
						_segments[i].ForeColor = Color.White;
					}
					else if (car.IsNewOnRoad) {
						// car is new on road
						_segments[i].BackColor = Color.Orange;
						_segments[i].ForeColor = Color.Black;
					}
					else if (car.IsLeaving) {
						// car is leaving
						_segments[i].BackColor = Color.Black;
						_segments[i].ForeColor = Color.White;
					}
					else {
						// car is driving
						var rgb = car.VisTag as RGB;
						_segments[i].BackColor = Color.FromArgb (      rgb.R,       rgb.G,       rgb.B);
						_segments[i].ForeColor = Color.FromArgb (255 - rgb.R, 255 - rgb.G, 255 - rgb.B);
					}
					_segments[i].Text = car.CarName;
					_segments[i].TextAlign = ContentAlignment.TopCenter;
				}
			}
			this.ResumeLayout ();
			this.Refresh ();
			this.Show ();
			if (_sleepMSec > 0)
				System.Threading.Thread.Sleep (_sleepMSec);
		}
	}
}
