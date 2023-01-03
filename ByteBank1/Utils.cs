using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByteBank1
{
	public class Utils
	{
		public static void WriteJSON(List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			string filePath = @"C:\Users\Public\Documents\clients.json";

			string jsonStringClients = JsonSerializer.Serialize(clients, new JsonSerializerOptions() { WriteIndented = true });

			using (StreamWriter outputFile = new StreamWriter(filePath))
				outputFile.WriteLine(jsonStringClients);

			filePath = @"C:\Users\Public\Documents\bankTransactions.json";

			string jsonStringBankTransactions = JsonSerializer.Serialize(bankTransactions, new JsonSerializerOptions() { WriteIndented = true });

			using (StreamWriter outputFile = new StreamWriter(filePath))
				outputFile.WriteLine(jsonStringBankTransactions);

		}

		public static void ReadJSON(ref List<Client> clients, ref List<BankTransactionRecord> bankTransactions)
		{
			string filePath = @"C:\Users\Public\Documents\clients.json";

			if (File.Exists(filePath))
			{
				using (StreamReader inputFile = new StreamReader(filePath))
				{
					string json = inputFile.ReadToEnd();
					clients = JsonSerializer.Deserialize<List<Client>>(json);
				}
			}

			filePath = @"C:\Users\Public\Documents\bankTransactions.json";

			if (File.Exists(filePath))
			{
				using (StreamReader inputFile = new StreamReader(filePath))
				{
					string json = inputFile.ReadToEnd();
					bankTransactions = JsonSerializer.Deserialize<List<BankTransactionRecord>>(json);
				}
			}
		}

		public static void WriteBankTransactionRecordFile (BankTransactionRecord record)
		{
			string directoryPath = @"C:\Users\Public\Documents\BankTransactionRecords";

			Directory.CreateDirectory(directoryPath);

			string filePath = @$"{directoryPath}\{record.Id}.txt";

			using StreamWriter file = File.CreateText(filePath);

			file.WriteLine(Display.alignMessage(new string('=', 82)));
			file.WriteLine(Display.alignMessage($"REDE DE AGENCIAS BYTE BANK"));
			file.WriteLine(Display.alignMessage($"COMPROVANTE DE {record.OperationType.ToUpper()}"));			
			file.WriteLine(Display.alignMessage($"{record.Date}"));
			file.WriteLine(Display.alignMessage(new string('-', 82)));
			file.WriteLine(Display.alignMessage($"ID: {record.Id}"));
			file.WriteLine(Display.alignMessage($"VALOR: R$ {record.Value:F2}"));
			file.WriteLine(Display.alignMessage(new string('-', 82)));

			if (record.OperationType == BankOperation.Deposit.ToString() || record.OperationType == BankOperation.Withdraw.ToString())
			{				
				file.WriteLine(Display.alignMessage($"CONTA: {record.OriginClient.AccountNumber}"));
				file.WriteLine(Display.alignMessage($"NOME: {record.OriginClient.Name}"));
				file.WriteLine(Display.alignMessage($"CPF: {record.OriginClient.Cpf}"));
			}
			else
			{
				file.WriteLine(Display.alignMessage("DESTINO"));
				file.WriteLine(Display.alignMessage($"CONTA: {record.DestinationClient.AccountNumber}"));
				file.WriteLine(Display.alignMessage($"NOME: {record.DestinationClient.Name}"));
				file.WriteLine(Display.alignMessage($"CPF: {record.DestinationClient.Cpf}"));
				file.WriteLine(Display.alignMessage(new string('-', 82)));
				file.WriteLine(Display.alignMessage("ORIGEM"));
				file.WriteLine(Display.alignMessage($"CONTA: {record.OriginClient.AccountNumber}"));
				file.WriteLine(Display.alignMessage($"NOME: {record.OriginClient.Name}"));
				file.WriteLine(Display.alignMessage($"CPF: {record.OriginClient.Cpf}"));
			}
			file.WriteLine(Display.alignMessage(new string('=', 82)));
			file.Close();
		}
	}
}
