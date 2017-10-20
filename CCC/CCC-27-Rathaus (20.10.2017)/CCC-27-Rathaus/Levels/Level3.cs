using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_27_Rathaus.Levels
{
	//public class Account3
	//{
	//	public string Name;
	//	public string AccountNumber;
	//	public double Balance;
	//	public double OverdraftLimit;
	//	public static bool IsValidAccountNumber(string AccNumber)
	//	{
	//		var sum = 0;
	//		var AccNumberEmpty = /*AccNumber.Substring(0, 3) + "00" +*/ AccNumber.Substring(5) + "CAT00";
	//		foreach (char c in AccNumberEmpty) {
	//			sum += (int) c;
	//		}
	//		var rest = sum % 97;
	//		var checkSum = 98 - rest;
	//		var AccNumberCheck = string.Format("CAT{0:00}{1}", checkSum, AccNumber.Substring(5));
	//		if (string.Compare(AccNumber, AccNumberCheck) != 0)
	//			return false;

	//		var accNumForCharCount = AccNumber.Substring(5);
	//		var checkDict = new Dictionary<char, int>();
	//		foreach (char c in accNumForCharCount) {
	//			if (!checkDict.ContainsKey(c))
	//				checkDict.Add(c, 0);
	//			checkDict[c]++;
	//		}
	//		foreach (var key in checkDict.Keys) {
	//			if (char.IsLower(key)) {
	//				char cHi = (char) (((int) key) - 32);
	//				if (!checkDict.ContainsKey(cHi))
	//					return false;
	//				if (checkDict[key] != checkDict[cHi])
	//					return false;
	//			}
	//			else {
	//				char cLow = (char) (((int) key) + 32);
	//				if (!checkDict.ContainsKey(cLow))
	//					return false;
	//				if (checkDict[key] != checkDict[cLow])
	//					return false;
	//			}
	//		}
	//		return true;
	//	}
	//	public bool IsValidSubtraction(double amountSub)
	//	{
	//		return (Balance + OverdraftLimit) - amountSub >= 0;
	//	}
	//}
	public class Transaction3
	{
		public string ID;
		public List<Element3In> eIn;
		public List<Element3Out> eOut;
		public long Time;

		internal bool IsValidAmount()
		{
			foreach (var element in eIn) {
				int inAmountInt = (int) element.Amount;
				if ((double) inAmountInt != element.Amount) {
//					Console.WriteLine("not integer {0}", element.Amount);
					return false;
				}
				if (inAmountInt <= 0) {
//					Console.WriteLine("<= 0 : {0}", element.Amount);
					return false;
				}
			}
			foreach (var element in eOut) {
				int inAmountInt = (int) element.Amount;
				if ((double) inAmountInt != element.Amount) {
					//					Console.WriteLine("not integer {0}", element.Amount);
					return false;
				}
				if (inAmountInt <= 0) {
					//					Console.WriteLine("<= 0 : {0}", element.Amount);
					return false;
				}
			}
			return true;
		}

		internal bool IsEqualInOutAmount()
		{
			double inAmount = 0;
			foreach (var element in eIn)
				inAmount += element.Amount;
			double outAmount = 0;
			foreach (var element in eOut)
				outAmount += element.Amount;
			return inAmount == outAmount;
		}

		internal bool OutOwnersAreUnique()
		{
			var uniqueOwnerCount = eOut.Select(e => e.Owner).Distinct().Count();
			return (uniqueOwnerCount == eOut.Count);
			//var foundOutOwners = new Dictionary<string, int>();
			//foreach (var element in eOut) {
			//	if (foundOutOwners.ContainsKey(element.Owner))
			//		return false;
			//	foundOutOwners.Add(element.Owner, 0);
			//}
			//return true;
		}

		internal bool FindAllInputElements(List<Transaction3> validTransactions)
		{
			var weAreOk = true;
			var usedByMe = new List<Element3Out>();
			foreach (var element in eIn) {
				if (element.Owner == "origin")
					continue;
				var inXact = validTransactions.Find(t => t.ID == element.InID);
				if (inXact == null) {
					weAreOk = false;
					break;
				}
				var foundList = inXact.eOut.Where(e => e.Owner == element.Owner && e.Amount == element.Amount);
				if (foundList == null) {
					weAreOk = false;
					break;
				}
				if (foundList.Count () != 1) {
					weAreOk = false;
					break;
				}
				var foundOutEle = foundList.First();
				if (foundOutEle.WeAreUsedInElement != null) {
					weAreOk = false;
					break;
				}
				foundOutEle.WeAreUsedInElement = element;
				usedByMe.Add(foundOutEle);
			}

			if (!weAreOk) {
				foreach (var u in usedByMe)
					u.WeAreUsedInElement = null;
			}
			return weAreOk;
		}
	}
	public class Element3In
	{
		public string InID;
		public string Owner;
		public double Amount;
	}
	public class Element3Out
	{
		public string Owner;
		public double Amount;
		public Element3In WeAreUsedInElement;
	}


	public class Level3 // : BaseStuff.ICccLevel<string[]>
	{
		public string[] CalculateResult(string inputFileName)
		{
			Dictionary<long, Transaction3> transactions = null;
			ParseInput(inputFileName, out transactions);

			var sortedXAct = transactions.OrderBy(t => t.Key);
			List<Transaction3> validTransactions = new List<Transaction3>();

			foreach (var x in sortedXAct) {
				var xact = x.Value;
				if (!xact.IsValidAmount()) {
					Console.WriteLine("IsValidAmount {0}", xact.ID);
					continue;
				}
				if (!xact.IsEqualInOutAmount())
					continue;
				if (!xact.OutOwnersAreUnique())
					continue;
				if (!xact.FindAllInputElements(validTransactions))
					continue;
				validTransactions.Add(xact);
			}

			var lines = new List<string>();
			lines.Add(validTransactions.Count.ToString());
			foreach (var xact in validTransactions) {

				var sb = new StringBuilder();
				sb.Append(xact.ID);
				sb.Append(" ");
				sb.Append(xact.eIn.Count);
				foreach (var element in xact.eIn) {
					sb.Append(" ");
					sb.Append(element.InID);
					sb.Append(" ");
					sb.Append(element.Owner);
					sb.Append(" ");
					sb.Append(element.Amount);
				}
				sb.Append(" ");
				sb.Append(xact.eOut.Count);
				foreach (var element in xact.eOut) {
					sb.Append(" ");
					sb.Append(element.Owner);
					sb.Append(" ");
					sb.Append(element.Amount);
				}
				sb.Append(" ");
				sb.Append(xact.Time);

				lines.Add(sb.ToString());
			}
			//foreach (var xact in transactions.Values) {

			//	if (!validTransactions.Contains(xact))
			//		continue;

			//	var sb = new StringBuilder();
			//	sb.Append(xact.ID);
			//	sb.Append(" ");
			//	sb.Append(xact.eIn.Count);
			//	foreach (var element in xact.eIn) {
			//		sb.Append(" ");
			//		sb.Append(element.InID);
			//		sb.Append(" ");
			//		sb.Append(element.Owner);
			//		sb.Append(" ");
			//		sb.Append(element.Amount);
			//	}
			//	sb.Append(" ");
			//	sb.Append(xact.eOut.Count);
			//	foreach (var element in xact.eOut) {
			//		sb.Append(" ");
			//		sb.Append(element.Owner);
			//		sb.Append(" ");
			//		sb.Append(element.Amount);
			//	}
			//	sb.Append(" ");
			//	sb.Append(xact.Time);

			//	lines.Add(sb.ToString ());
			//}


			var resultFileName = inputFileName + ".out";
			var outFile = new System.IO.StreamWriter(resultFileName, false);
			foreach (var line in lines)
				outFile.WriteLine(line);
			outFile.Close();
			return lines.ToArray();
		}

		void ParseInput(string inputFile, out Dictionary<long, Transaction3> Transactions)
		{
			var lines = BaseStuff.CccTest.ReadInputFile(inputFile);

			var numXAct = int.Parse(lines[0][0]);
			Transactions = new Dictionary<long, Transaction3>(numXAct);

			for (int t = 1 ; t < numXAct + 1; t++) {
				int Column = 0;
				var xact = new Transaction3() { ID = lines[t][Column++] };
				var numIn = int.Parse (lines[t][Column++]);

				xact.eIn = new List<Element3In> ();
				for (int inIdx = 0; inIdx < numIn; inIdx++) {
					var inEl = new Element3In();
					inEl.InID = lines[t][Column++];
					inEl.Owner = lines[t][Column++];
					inEl.Amount = double.Parse (lines[t][Column++]);
					xact.eIn.Add(inEl);
				}

				var numOut = int.Parse(lines[t][Column++]);
				xact.eOut = new List<Element3Out>();
				for (int outIdx = 0; outIdx < numOut; outIdx++) {
					var outEl = new Element3Out();
					outEl.Owner = lines[t][Column++];
					outEl.Amount = double.Parse(lines[t][Column++]);
					xact.eOut.Add(outEl);
				}
				xact.Time = long.Parse(lines[t][Column]);
				Column++;

				Transactions.Add(xact.Time, xact);
			}
			return ;
		}

	}
}
