using System;
using System.Collections.Generic;
using System.Linq;
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
			Console.WriteLine("0 - Sair do programa");
		}

		public static void ShowBankTransactionsMenu()
		{
			Console.WriteLine("1 - Depositar");
			Console.WriteLine("2 - Sacar");
			Console.WriteLine("3 - Transferir");
			Console.WriteLine("0 - Voltar ao Menu Principal");
			Console.WriteLine();
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

		public static void PrintClientInfo(List<Client> clients, int index)
		{
			Console.WriteLine();
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"Cliente ID: {index}".PadRight(30, ' '));
			Console.WriteLine($"Nome: {clients[index].Name}".PadRight(30, ' '));
			Console.WriteLine($"Conta: {clients[index].AccountNumber}".PadRight(30, ' '));
			Console.WriteLine($"CPF: {clients[index].Cpf}".PadRight(30, ' '));
			Console.WriteLine($"Saldo: R$ {clients[index].Balance:F2}".PadRight(30, ' '));
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine();
		}



	}
}
