using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_27_Rathaus.Levels
{
	public class Account2
	{
		public string Name;
		public string AccountNumber;
		public double Balance;
		public double OverdraftLimit;
		public static bool IsValidAccountNumber (string AccNumber)
		{
			var sum = 0;
			var AccNumberEmpty = /*AccNumber.Substring(0, 3) + "00" +*/ AccNumber.Substring(5) + "CAT00";
			foreach (char c in AccNumberEmpty) {
				sum += (int) c;
			}
			var rest = sum % 97;
			var checkSum = 98 - rest;
			var AccNumberCheck = string.Format("CAT{0:00}{1}", checkSum, AccNumber.Substring(5));
			if (string.Compare(AccNumber, AccNumberCheck) != 0)
				return false;

			var accNumForCharCount = AccNumber.Substring(5);
			var checkDict = new Dictionary<char, int> ();
			foreach (char c in accNumForCharCount) {
				if (!checkDict.ContainsKey(c))
					checkDict.Add(c, 0);
				checkDict[c]++;
			}
			foreach (var key in checkDict.Keys) {
				if (char.IsLower(key)) {
					char cHi = (char) (((int) key) - 32);
					if (!checkDict.ContainsKey(cHi))
						return false;
					if (checkDict[key] != checkDict[cHi])
						return false;
				}
				else {
					char cLow = (char) (((int) key) + 32);
					if (!checkDict.ContainsKey(cLow))
						return false;
					if (checkDict[key] != checkDict[cLow])
						return false;
				}
			}
			return true;
		}
		public bool IsValidSubtraction(double amountSub)
		{
			return (Balance + OverdraftLimit) - amountSub >= 0;
		}
	}
	public class Transaction2
	{
		public string NumberFrom;
		public string NumberTo;
		public double Amount;
		public long TransactionSubmitTime;
	}

	public class Level2 // : BaseStuff.ICccLevel<string[]>
	{
		public string[] CalculateResult(string inputFileName)
		{
			Dictionary<long, Transaction2> transactions = null;
			var accounts = ParseInput(inputFileName, out transactions);

			var sortedXAct = transactions.OrderBy(t => t.Key);
			foreach (var x in sortedXAct) {
				var xact = x.Value;
				if (!accounts.ContainsKey(xact.NumberFrom) || !accounts.ContainsKey(xact.NumberTo))
					continue;
				if (!accounts[xact.NumberFrom].IsValidSubtraction(xact.Amount))
					continue;
				accounts[xact.NumberFrom].Balance -= x.Value.Amount;
				accounts[xact.NumberTo  ].Balance += x.Value.Amount;
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
			return lines.ToArray();
		}

		public Dictionary<string, Account2> ParseInput(string inputFile, out Dictionary<long, Transaction2> Transactions)
		{
			var lines = BaseStuff.CccTest.ReadInputFile(inputFile);

			var numAccounts = int.Parse(lines[0][0]);
			var accounts = new Dictionary<string, Account2>(numAccounts);

			for (int i = 1; i < numAccounts + 1; i++) {
				var name = lines[i][0];
				var accNum = lines[i][1];
				var ballance = double.Parse(lines[i][2]);
				var overLimit = double.Parse(lines[i][3]);
				if (Account2.IsValidAccountNumber (accNum))
					accounts.Add(accNum, new Account2() { Name = name, AccountNumber = accNum, Balance = ballance, OverdraftLimit = overLimit });
			}

			var lineOffset = numAccounts + 1;
			var numXAct = int.Parse(lines[lineOffset + 0][0]);
			Transactions = new Dictionary<long, Transaction2>(numXAct);

			for (int t = 1 + lineOffset; t < numXAct + lineOffset + 1; t++) {
				var numFrom = lines[t][0];
				var numTo = lines[t][1];
				var amount = double.Parse(lines[t][2]);
				var time = long.Parse(lines[t][3]);
				Transactions.Add(time, new Transaction2() { NumberFrom = numFrom, NumberTo = numTo, Amount = amount, TransactionSubmitTime = time });
			}

			return accounts;
		}

	}
}
