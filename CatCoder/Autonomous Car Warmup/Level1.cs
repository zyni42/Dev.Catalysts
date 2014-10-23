using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Autonomous_Car_Warmup
{
	class Level1 : BaseStuff.ICccLevel<string>
	{
		public Level1 (string inputFile)
		{
			var inputData = BaseStuff.CccTest.ReadInputFile (inputFile);

			// Parse and prepare input data
		}

		public Level1 ()
		{

		}

		public class Field
		{
			public int Top;
			public int Bottom;
			public int Left;
			public int Right;
			public int	Width;
			public int	Height;
			public int	StepCount;

			public Field ()
			{
				Bottom = Height - 1 + Top;
				Right = Width - 1 + Left;
			}

			public Ball CalculateNextBallStep (Ball ball, Paddle leftPaddle, Paddle rightPaddle)
			{
				var nextBallStep = (Ball)ball.Clone ();

				// Calculate paddle bouncing
				var newX = nextBallStep.PosX + nextBallStep.VelocityX;
				if (nextBallStep.VelocityX > 0)
				{
					// moving RIGHT ++
					if (newX + nextBallStep.Radius/2 >= rightPaddle.Left)
					{
						var diffX = (newX + nextBallStep.Radius/2) - rightPaddle.Left;
						nextBallStep.VelocityX = -nextBallStep.VelocityX;
						newX = rightPaddle.Left - diffX;
					}
				}
				else
				{
					// moving LEFT --
					if (newX - nextBallStep.Radius/2 <= leftPaddle.Right)
					{
						var diffX = leftPaddle.Right - (newX - nextBallStep.Radius/2);
						nextBallStep.VelocityX = -nextBallStep.VelocityX;
						newX = leftPaddle.Right + diffX;
					}
				}
				nextBallStep.PosX = newX;

				// Calculate top/bottom bouncing
				var newY = nextBallStep.PosY + nextBallStep.VelocityY;
				if (nextBallStep.VelocityY > 0) {
					// moving DOWN ++
					if (newY + nextBallStep.Radius/2 >= Bottom)
					{
						var diffY = (newY + nextBallStep.Radius/2) - Bottom;
						nextBallStep.VelocityY = -nextBallStep.VelocityY;
						newY = Bottom - diffY;
					}
				}
				else
				{
					// moving UP --
					if (newY - nextBallStep.Radius/2 <= Top)
					{
						var diffY = Top - (newY - nextBallStep.Radius/2);
						nextBallStep.VelocityY = -nextBallStep.VelocityY;
						newY = Top + diffY;
					}
				}
				nextBallStep.PosY = newY;

				return nextBallStep;
			}

			public bool IsOutsideX (Ball ball)
			{
				return (ball.PosX < Left || ball.PosX > Right);
			}
		}
		public class Paddle
		{
			public int	Position; // 0..450
			public int	Distance; // 10 / 775
			public int	Move;
			public int	Width;    //  15
			public int	Height;   // 150
			public int  MoveMaxUp;		// -36
			public int  MoveMaxDown;	// +36
			public int Top { get { return Position; } }
			public int Bottom { get { return Position + Height; } }
			public int Center { get { return Position + (Height/2); } }
			public int Left { get { return Distance; } }
			public int Right { get { return Distance + Width; } }

			public int CalculateNextMove (Ball[] ballSteps)
			{
				var paddleMove = 0;

				if (Distance == 10)	// We are left paddle
				{
					// Did the ball pass our y-line?
					if (ballSteps[ballSteps.Length-1].Left <= Right)
					{
						// When did it pass us (on which step)?

						// Where did it pass us (y-position)

						// Can we move fast enough to bounce it?
						// yes -> return moves to hit point
						// no -> return moves back to middle
					}
				}
				else				// We are right paddle
				{
					// Did the ball pass our y-line?
					if (ballSteps[ballSteps.Length-1].Right >= Left)
					{
						// When did it pass us (on which step)?

						// Where did it pass us (y-position)

						// Can we move fast enough to bounce it?
						// yes -> return moves to hit point
						// no -> return moves back to middle
					}
				}

				return paddleMove;
			}
		}
		public class Ball : ICloneable
		{
			public float	PosX;
			public float	PosY;
			public float	VelocityX;
			public float	VelocityY;
			public float	Radius;

			public float Top { get { return PosY - Radius/2; } }
			public float Bottom { get { return PosY + Radius/2; } }
			public float Left { get { return PosX - Radius/2; } }
			public float Right { get { return PosX + Radius/2; } }

			public object Clone()
			{
 				return this.MemberwiseClone ();
			}
		}

		public void ErrOut (string msg)
		{
			msg += System.Environment.NewLine;
			byte[] bytes = new byte[msg.Length * sizeof(char)];
			System.Buffer.BlockCopy(msg.ToCharArray(), 0, bytes, 0, bytes.Length);
			Console.OpenStandardError ().Write (bytes, 0, bytes.Length);
		}

		public string CalculateResult()
		{
			var playerPaddle= new Paddle ()  { Height = 150, Width = 15, Distance =  10, MoveMaxUp = -36, MoveMaxDown = 36 };
			var cpuPaddle	= new Paddle ()  { Height = 150, Width = 15, Distance = 775, MoveMaxUp = -36, MoveMaxDown = 36 };
			var ball		= new Ball ()    { Radius = 15 };

			var field		= new Field () { Height = 600, Width = 800, Top = 0, Left = 0, StepCount = 6 };

			var update = string.Empty;

			while (!File.Exists ("result.txt"))
			{
				var conLine = Console.ReadLine ();
				ErrOut ("IN  : " + conLine);
				var playerData = conLine.Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				if (playerData[0] != "player") throw new Exception ("NOT player: " + playerData[0]);
				playerPaddle.Position = int.Parse (playerData[1]);
				playerPaddle.Move     = int.Parse (playerData[2]);

				conLine = Console.ReadLine ();
				ErrOut ("IN  : " + conLine);
				var cpuData = conLine.Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				if (cpuData[0] != "cpu") throw new Exception ("NOT cpu: " + cpuData[0]);
				cpuPaddle.Position = int.Parse (cpuData[1]);
				cpuPaddle.Move     = int.Parse (cpuData[2]);

				conLine = Console.ReadLine ();
				ErrOut ("IN  : " + conLine);
				var ballData = conLine.Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				if (ballData[0] != "ball") throw new Exception ("NOT ball: " + ballData[0]);
				ball.PosX		= float.Parse (ballData[1].Replace (".", ","));
				ball.PosY		= float.Parse (ballData[2].Replace (".", ","));
				ball.VelocityX	= float.Parse (ballData[3].Replace (".", ","));
				ball.VelocityY	= float.Parse (ballData[4].Replace (".", ","));

				conLine = Console.ReadLine ();
				ErrOut ("IN  : " + conLine);
				var updateCommand = conLine.Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				if (updateCommand[0] != "update") throw new Exception ("NOT update: " + updateCommand[0]);

				// Where is ball after n steps?
				var leftPaddle = (playerPaddle.Distance < cpuPaddle.Distance) ? playerPaddle : cpuPaddle;
				var rightPaddle = (playerPaddle.Distance > cpuPaddle.Distance) ? playerPaddle : cpuPaddle;
				var ballSteps = new List<Ball> (field.StepCount);
				var currBallStep = ball;
				for (var step = 0; step < field.StepCount; step++)
				{
					if (field.IsOutsideX (ball)) continue;		// Nothing to calculate here; ball has to be replaced into field
					currBallStep = field.CalculateNextBallStep (currBallStep, leftPaddle, rightPaddle);
					ballSteps.Add (currBallStep);
				}

				// Calculate paddle position
				var nextMove = playerPaddle.CalculateNextMove (ballSteps.ToArray ());
				update = "move " + nextMove;

				ErrOut ("OUT : " + update);
				Console.WriteLine (update);
			}

			var result = string.Empty;

			// Calculate result
			result += "something" + " ";

			return result.Substring (0,result.Length-1);
		}
	}
}
