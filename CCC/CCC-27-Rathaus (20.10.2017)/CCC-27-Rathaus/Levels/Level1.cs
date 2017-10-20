using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_27_Rathaus.Levels
{
	public class Account
	{
		public string Name;
		public double Balance;
	}
	public class Transaction
	{
		public string NameFrom;
		public string NameTo;
		public double Amount;
		public long TransactionSubmitTime;
	}

	public class Level1 // : BaseStuff.ICccLevel<string[]>
	{
		public string[] CalculateResult(string inputFileName)
		{
			Dictionary<long, Transaction> Transactions = null;
			var accounts = ParseInput(inputFileName, out Transactions);

			var sortedXAct = Transactions.OrderBy(t => t.Key);
			foreach (var x in sortedXAct) {
				accounts[x.Value.NameFrom].Balance -= x.Value.Amount;
				accounts[x.Value.NameTo  ].Balance += x.Value.Amount;
			}

			var lines = new List<string>();
			lines.Add(accounts.Count.ToString());
			foreach (var acc in accounts) {
				lines.Add($"{acc.Value.Name} {acc.Value.Balance}");
			}

			var resultFileName = inputFileName + ".out";
			var outFile = new System.IO.StreamWriter(resultFileName, false);
			foreach (var line in lines)
				outFile.WriteLine(line);
			outFile.Close();
			return lines.ToArray ();
		}

		public Dictionary<string, Account> ParseInput(string inputFile, out Dictionary<long,Transaction> Transactions)
		{
			var lines = BaseStuff.CccTest.ReadInputFile(inputFile);

			var numAccounts = int.Parse(lines[0][0]);
			var accounts = new Dictionary<string, Account>(numAccounts);

			for (int i = 1; i < numAccounts+1; i++) {
				var name = lines[i][0];
				var ballance = double.Parse(lines[i][1]);
				accounts.Add(name, new Account() { Name = name, Balance = ballance });
			}

			var lineOffset = numAccounts + 1;
			var numXAct = int.Parse(lines[lineOffset + 0][0]);
			Transactions = new Dictionary<long, Transaction>(numXAct);

			for (int t = 1+ lineOffset; t < numXAct+ lineOffset+1; t++) {
				var nameFrom = lines[t][0];
				var nameTo   = lines[t][1];
				var amount = double.Parse(lines[t][2]);
				var time = long.Parse(lines[t][3]);
				Transactions.Add(time, new Transaction() { NameFrom = nameFrom, NameTo = nameTo, Amount = amount, TransactionSubmitTime = time });
			}

			return accounts;
		}

	}
}
