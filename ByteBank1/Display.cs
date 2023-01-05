using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank1
{
	public class Display
	{

		public static string alignMessage(string message, int blankSpace = 82)
		{
			return String.Format($"{{0,-{blankSpace}}}", String.Format("{0," + ((blankSpace + message.Length) / 2).ToString() + "}", message));
		}

		public static void ShowBankInterface(string message)
		{
			Console.Clear();
			int blankSpace = 82;
			int totalCharsHeader = 102;
			message = alignMessage(message, blankSpace);
			string title = alignMessage("ByteBank", blankSpace);
			Console.BackgroundColor = ConsoleColor.DarkRed;

			Console.WriteLine(new string('=', totalCharsHeader));
			Console.Write("=========|");
			Console.Write(title);
			Console.Write("|=========\n");
			Console.WriteLine(new string('=', totalCharsHeader));

			Console.BackgroundColor = ConsoleColor.DarkGray;

			Console.WriteLine(new string('-', totalCharsHeader));
			Console.Write("---------|");
			Console.Write(message);
			Console.Write("|---------\n");
			Console.WriteLine(new string('-', totalCharsHeader));
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine();
		}
		public static void ShowMenu()
		{
			Console.WriteLine("1 - Inserir novo cliente");
			Console.WriteLine("2 - Deletar um cliente");
			Console.WriteLine("3 - Listar todos os clientes registrados");
			Console.WriteLine("4 - Detalhes de um cliente");
			Console.WriteLine("5 - Valor total armazenado no banco");
			Console.WriteLine("6 - Realizar transações bancárias");
			Console.WriteLine("7 - Listar todas as transações bancárias");
			Console.WriteLine("8 - Detalhes de transações bancárias");
			Console.WriteLine("0 - Sair do programa");
		}

		public static void ShowBankTransactionsMenu()
		{
			Console.WriteLine("1 - Depositar");
			Console.WriteLine("2 - Sacar");
			Console.WriteLine("3 - Transferir");
			Console.WriteLine("4 - Histórico de Transações Bancárias");
			Console.WriteLine("0 - Voltar ao Menu Principal");			
		}

		public static void ShowWarning(string message)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.Write(message + "\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine();
		}

		public static void ShowWarningForWrongOption()
		{
			Display.ShowWarning("Se essa operação foi escolhida por engano, basta não inserir valor em um ou mais campos do formulário.");
		}

		public static void BackToMenu()
		{
			Console.WriteLine();
			Console.WriteLine("Pressione Enter para voltar ao menu anterior.");
			Console.ReadLine();
		}

		public static void PrintClientInfo(Client client, int index)
		{
			Console.WriteLine();			
			Console.WriteLine($"Cliente ID: {index}");
			Console.WriteLine($"Nome: {client.Name}");
			Console.WriteLine($"Conta: {client.AccountNumber}");
			Console.WriteLine($"CPF: {client.Cpf}");
			Console.WriteLine($"Saldo: R$ {client.Balance:F2}");			
			Console.WriteLine();
		}

		internal static void PrintBankTransactionsInfo(BankTransactionRecord bankTransactionRecord)
		{
			Console.WriteLine();
			Console.WriteLine($"Id: {bankTransactionRecord.Id}");
			Console.WriteLine($"Operação: {bankTransactionRecord.OperationType}");
			Console.WriteLine($"Data: {bankTransactionRecord.Date}");
			Console.WriteLine($"Valor: R$ {bankTransactionRecord.Value:F2}");
			Console.WriteLine("---");

			if (bankTransactionRecord.DestinationClient == null)
			{				
				Console.WriteLine($"Cliente: {bankTransactionRecord.OriginClient.Name}");
				Console.WriteLine($"Conta: {bankTransactionRecord.OriginClient.AccountNumber}");
				Console.WriteLine($"CPF: {bankTransactionRecord.OriginClient.Cpf}");
			}
			
			else
			{				
				Console.WriteLine($"Beneficiador: {bankTransactionRecord.OriginClient.Name}");
				Console.WriteLine($"Conta: {bankTransactionRecord.OriginClient.AccountNumber}");
				Console.WriteLine($"CPF: {bankTransactionRecord.OriginClient.Cpf}");
				Console.WriteLine("---");
				Console.WriteLine($"Beneficiário: {bankTransactionRecord.DestinationClient.Name}");
				Console.WriteLine($"Conta: {bankTransactionRecord.DestinationClient.AccountNumber}");
				Console.WriteLine($"CPF: {bankTransactionRecord.DestinationClient.Cpf}");
			}
			Console.WriteLine("---");
			Console.WriteLine();
		}

		internal static void ShowBankTransactionsDetailsMenu()
		{
			Console.WriteLine("1 - Buscar por Data");
			Console.WriteLine("2 - Buscar por Cliente");
			Console.WriteLine("0 - Voltar ao Menu Principal");
		}
	}
}
