using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ByteBank1
{
 
    public class Program
    {       
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            List<Client> clients = new List<Client>();
			List<BankTransactionRecord> bankTransactions = new List<BankTransactionRecord>();
            string option;
            string warningMessage = "";
			Utils.ReadJSON(ref clients, ref bankTransactions);

            do
            {
                Display.ShowBankInterface("Menu Inicial");
                Display.ShowMenu();

				Display.ShowWarning(warningMessage);

                Console.Write("Escolha operação a ser realizada indicando seu número: ");

				option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateUser(clients);
						Utils.WriteJSON(clients, bankTransactions);
						warningMessage = "";
						break;
                    case "2":
                        DeleteUser(clients);
						Utils.WriteJSON(clients, bankTransactions);
						warningMessage = "";
						break;
                    case "3":
                        GetAllUsers(clients);
						warningMessage = "";
						break;
                    case "4":
                        GetClientDetails(clients);
						warningMessage = "";
						break;
                    case "5":
                        GetTotalBalance(clients);
						warningMessage = "";
						break;
                    case "6":
                        ValidateCredentials(clients, bankTransactions);
						warningMessage = "";
						break;
					case "7":
						GetAllBankTransactions(bankTransactions);
						warningMessage = "";
						break;
					case "8":
						GetBankTransactionsDetails(clients, bankTransactions);
						warningMessage = "";
						break;
					case "0":
						Display.ShowBankInterface("Muito Obrigado por utilizar nosso aplicativo!");
						Utils.WriteJSON(clients, bankTransactions);
						break;
                    default: 
                        warningMessage = "Aviso: Opção inválida, favor inserir número de 0 a 8.";                        
                        break;
                }
            } while (option != "0");
        }

		public static void CreateUser(List<Client> clients)
		{
			string name, cpf, password, warning;
			bool isRegistered;
			Display.ShowBankInterface("Cadastro de Novo Cliente");
			Display.ShowWarningForWrongOption();

			Console.Write("Insira nome do novo cliente: ");
			name = Console.ReadLine();
			Console.Write("Insira cpf do novo cliente: ");
			cpf = Console.ReadLine();
			Console.Write("Insira senha do novo cliente: ");
			password = Utils.EncryptPassword();

			isRegistered = clients.Exists(client => client.Cpf == cpf);

			if (!string.IsNullOrEmpty(name) && !isRegistered && !string.IsNullOrEmpty(password))
			{
				clients.Add(new Client(name, cpf, password));
				Display.PrintClientInfo(clients[clients.Count - 1], clients.Count - 1);
				warning = "Cliente Cadastrado com Sucesso!";
			}
			else if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
				warning = "Entrada Inválida! Operação Não Realizada!";
			else
				warning = "Cliente já Cadastrado! Operação Não Realizada!";

			Display.ShowWarning(warning);

			Display.BackToMenu();
		}

		static void DeleteUser(List<Client> clients)
		{
			Display.ShowBankInterface("Deletar Cliente");
			Display.ShowWarningForWrongOption();

			int indexToBeDeleted = GetIndexUser(clients);

			if (indexToBeDeleted >= 0 && indexToBeDeleted < clients.Count)
			{
				Display.PrintClientInfo(clients[indexToBeDeleted], indexToBeDeleted);
				Display.ShowWarning("Cliente Deletado com Sucesso!");
				clients.RemoveAt(indexToBeDeleted);
			}
			else
				Display.ShowWarning("Cliente não encontrado!");

			Display.BackToMenu();
		}

		static int GetIndexUser(List<Client> clients)
		{
			Console.Write("Informe CPF do cliente requisitado: ");
			string userInformation = Console.ReadLine();

			int index = clients.FindIndex(client => client.Cpf == userInformation);

			return index;
		}

		static void GetAllUsers(List<Client> clients)
		{
			Display.ShowBankInterface("Mostrar Todos os Clientes");
			Console.WriteLine($"Total de Clientes: {clients.Count}\n");

			for (int clientIndex = 0; clientIndex < clients.Count; clientIndex++)
				Display.PrintClientInfo(clients[clientIndex], clientIndex);

			Display.BackToMenu();
		}

		static void GetClientDetails(List<Client> clients)
		{
			Display.ShowBankInterface("Mostrar Detalhe de Cliente");
			Display.ShowWarningForWrongOption();

			int clientIndex = GetIndexUser(clients);

			if (clientIndex >= 0 && clientIndex < clients.Count)
				Display.PrintClientInfo(clients[clientIndex], clientIndex);
			else
				Display.ShowWarning("Cliente não encontrado");

			Display.BackToMenu();
		}

		static void GetTotalBalance(List<Client> clients)
		{
			Display.ShowBankInterface("Operação Mostrar Valor Acumulado no Banco");

			decimal totalBalance = clients.Select(value => value.Balance).Sum();

			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"Valor Acumulado no Banco: R$ {totalBalance:F2}");
			Console.BackgroundColor = ConsoleColor.DarkBlue;

			Display.BackToMenu();
		}

		static void ValidateCredentials(List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			Display.ShowBankInterface("Validação de Credenciais de Cliente");
			Display.ShowWarningForWrongOption();

			int clientIndex = GetIndexUser(clients);

			if (clientIndex >= 0 && clientIndex < clients.Count)
			{
				Console.Write("\nFavor inserir a senha: ");
				string password = Utils.EncryptPassword();

				if (clients[clientIndex].Password == password)
					PerformBankTransactions(clientIndex, clients, bankTransactions);
				else
					Display.ShowWarning("Senha inválida");
			}
			else
				Display.ShowWarning("Cliente não encontrado");

			Display.BackToMenu();
		}

		private static void PerformBankTransactions(int indexLoggedClient, List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			string option;
			string warningMessage = "";

			do
			{
				Display.ShowBankInterface("Transações Bancárias");
				Display.ShowBankTransactionsMenu();

				Display.ShowWarning(warningMessage);		

				Console.Write("Escolha operação a ser realizada indicando seu número: ");

				option = Console.ReadLine();

				switch (option)
				{
					case "1":
						Deposit(indexLoggedClient, clients, bankTransactions);
						warningMessage = "";
						break;
					case "2":
						Withdraw(indexLoggedClient, clients, bankTransactions);
						warningMessage = "";
						break;
					case "3":
						Transfer(indexLoggedClient, clients, bankTransactions);
						warningMessage = "";
						break;
					case "4":
						GetClientBankTransactions(clients[indexLoggedClient], bankTransactions);
						warningMessage = "";
						break;
					case "0":
						break;
					default:
						warningMessage = "Aviso: Opção inválida, favor inserir número de 0 a 4.";						
						break;
				}
			} while (option != "0");
		}

		static void Transfer(int indexLoggedClient, List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			string findClient = "n", requestMessage = "Valor a ser transferido: R$ ";
			int indexClientToTransfer;

			do
			{
				Display.ShowBankInterface("Transferência");
				Display.ShowWarningForWrongOption();
				indexClientToTransfer = GetIndexUser(clients);

				if (indexClientToTransfer == -1 || indexClientToTransfer == indexLoggedClient)
				{
					Display.ShowWarning("Cliente Não Encontrado!");
					Console.Write("Tentar encontrar cliente novamente? S - sim / Qualquer outra tecla - não: ");
					findClient = Console.ReadLine();
				}
				else
					ProcessBankOperation((int) BankOperation.Transferencia, bankTransactions, clients, requestMessage, indexLoggedClient, indexClientToTransfer);

			} while (findClient == "s" || findClient == "S");

			Display.BackToMenu();
		}

		private static void Withdraw(int indexLoggedClient, List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			Display.ShowBankInterface("Saque");
			Display.ShowWarningForWrongOption();
			string requestMessage = "Valor a ser retirado: R$ ";

			ProcessBankOperation((int) BankOperation.Saque, bankTransactions, clients, requestMessage, indexLoggedClient);

			Display.BackToMenu();
		}

		private static void Deposit(int indexLoggedClient, List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			Display.ShowBankInterface("Depósito");
			Display.ShowWarningForWrongOption();
			string requestMessage = "Valor a ser depositado: R$ ";

			ProcessBankOperation((int) BankOperation.Deposito, bankTransactions, clients, requestMessage, indexLoggedClient);

			Display.BackToMenu();
		}

		static void ProcessBankOperation(int operationValue, List<BankTransactionRecord> bankTransactions, List<Client> clients, string requestMessage, int indexLoggedClient, int indexClientToTransfer = -1)
		{
			string getClientDetails, amount, errorMessage;
			bool validOperation = false;

			Console.Write(requestMessage);
			amount = Console.ReadLine();

			decimal.TryParse(amount, out decimal value);
			errorMessage = "Valor informado indevido!";

			if (value > 0 && operationValue == (int)BankOperation.Deposito)
				validOperation = clients[indexLoggedClient].SetBalance(value);

			else if (value > 0 && (operationValue == (int)BankOperation.Saque || operationValue == (int)BankOperation.Transferencia))
			{
				validOperation = clients[indexLoggedClient].SetBalance(-value);
				errorMessage = "Saldo Insuficiente!";

				if (operationValue == (int)BankOperation.Transferencia && validOperation)
					clients[indexClientToTransfer].SetBalance(value);
			}

			if (validOperation)
			{
				Display.ShowWarning("Operação Realizada com Sucesso!\n");

				Client? clientToTransfer = indexClientToTransfer != -1 ? clients[indexClientToTransfer] : null;

				RegisterBankTransaction(bankTransactions, operationValue, value, clients[indexLoggedClient], clientToTransfer);

				Utils.WriteJSON(clients, bankTransactions);

				Console.Write("Verificar detalhes da conta? S - sim / Qualquer outra tecla - não: ");

				getClientDetails = Console.ReadLine();

				if (getClientDetails == "S" || getClientDetails == "s")
					Display.PrintClientInfo(clients[indexLoggedClient], indexLoggedClient);
			}
			else
				Display.ShowWarning($"Operação Não Realizada! {errorMessage}");
		}

		private static void RegisterBankTransaction(List<BankTransactionRecord> bankTransactions, int operationValue, decimal value, Client originClient, Client? destinationClient)
		{			
			BankTransactionRecord newRecord = new BankTransactionRecord(((BankOperation)operationValue).ToString(), value, originClient, destinationClient);
			bankTransactions.Add(newRecord);
			Utils.WriteBankTransactionRecordFile(newRecord);
		}

		static void GetAllBankTransactions(List<BankTransactionRecord> bankTransactions)
		{
			Display.ShowBankInterface("Transações Bancárias");
			Console.WriteLine($"Total de Transações Bancárias Realizadas: {bankTransactions.Count}\n");

			for (int i = 0; i < bankTransactions.Count; i++)
				Display.PrintBankTransactionsInfo(bankTransactions[i]);

			Display.BackToMenu();
		}

		static void GetClientBankTransactions(Client client, List<BankTransactionRecord> bankTransactions)
		{
			Display.ShowBankInterface("Transações Bancárias");

			List<BankTransactionRecord> clientBankTransactions = bankTransactions.FindAll(bankTransaction => bankTransaction.OriginClient.Cpf == client.Cpf);
			clientBankTransactions.AddRange(bankTransactions.FindAll(bankTransaction => {
				if (bankTransaction.DestinationClient != null)
					return bankTransaction.DestinationClient.Cpf == client.Cpf;
				else
					return false;
				}));

			clientBankTransactions = clientBankTransactions.OrderBy(bankTransaction => bankTransaction.Date).ToList();

			if (clientBankTransactions.Count == 0)
				Display.ShowWarning("Nenhum Registro Encontrado");
			else
			{
				Console.WriteLine($"Total de Transações Bancárias: {clientBankTransactions.Count}\n");

				for (int i = 0; i < clientBankTransactions.Count; i++)
					Display.PrintBankTransactionsInfo(clientBankTransactions[i]);
			}

			Display.BackToMenu();
		}

		static void GetBankTransactionsByDate(List<BankTransactionRecord> bankTransactions, string dateToFind)
		{
			Display.ShowBankInterface("Transações Bancárias");

			List<BankTransactionRecord> bankTransactionsByDate = bankTransactions.FindAll(bankTransaction => bankTransaction.Date.ToShortDateString() == dateToFind);

			if (bankTransactionsByDate.Count == 0)
				Display.ShowWarning("Nenhum Registro Encontrado");
			else
			{
				Console.WriteLine($"Total de Transações Bancárias: {bankTransactionsByDate.Count}\n");

				for (int i = 0; i < bankTransactionsByDate.Count; i++)
					Display.PrintBankTransactionsInfo(bankTransactionsByDate[i]);
			}

			Display.BackToMenu();
		}

		private static void GetBankTransactionsDetails(List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			string option;
			string warningMessage = "";

			do
			{
				Display.ShowBankInterface("Buscar Transações Bancárias");
				Display.ShowBankTransactionsDetailsMenu();

				Display.ShowWarning(warningMessage);

				Console.Write("Escolha operação a ser realizada indicando seu número: ");

				option = Console.ReadLine();

				switch (option)
				{
					case "1":
						GetBankTransactionsByDateMenu(bankTransactions);
						warningMessage = "";
						break;
					case "2":
						GetBankTransactionsByClient(clients , bankTransactions);
						warningMessage = "";
						break;
					case "0":
						break;
					default:
						warningMessage = "Aviso: Opção inválida, favor inserir número de 0 a 2.";
						break;
				}
			} while (option != "0");
		}

		private static void GetBankTransactionsByDateMenu(List<BankTransactionRecord> bankTransactions)
		{
			string tryAgain = " ";
			string day, month, year, fullDate;
			do
			{
				Display.ShowBankInterface("Buscar Transação Bancária Por Data");
				Display.ShowWarningForWrongOption();

				Console.Write("Digite o dia (1 - 31): ");
				day = Console.ReadLine();
				Console.Write("Digite o mês (1 - 12): ");
				month = Console.ReadLine();
				Console.Write("Digite o ano (20XX): ");
				year = Console.ReadLine();		
				
				if (CheckDate(day, month, year))
				{
					day = String.Format("{0, 0:D2}", int.Parse(day));
					month = String.Format("{0, 0:D2}", int.Parse(month));
					GetBankTransactionsByDate(bankTransactions, $"{day}/{month}/{year}");
				}
								
				else
				{
					Display.ShowWarning("Data Inválida!");
					Console.Write("Inserir nova data? S - sim / Qualquer outra tecla - não: ");
					tryAgain = Console.ReadLine();
				}				
			} while (tryAgain == "s" || tryAgain == "S");
		}

		private static bool CheckDate(string day, string month, string year )
		{
			string fullDate = day + month + year;
			if (int.TryParse(fullDate, out int result) && 
				int.Parse(day) <= 31 && int.Parse(day) >= 1 &&
				int.Parse(month) <= 12 && int.Parse(month) >= 1 &&
				int.Parse(year) <= DateTime.Now.Year && int.Parse(year) >= 2000)
			{
				return true;
			}
			else
				return false;
		}

		private static void GetBankTransactionsByClient(List<Client> clients, List<BankTransactionRecord> bankTransactions)
		{
			string findClient = " ";
			do
			{
				Display.ShowBankInterface("Buscar Transação Bancária Por Cliente");
				Display.ShowWarningForWrongOption();
				int clientIndex = GetIndexUser(clients);

				if (clientIndex == -1)
				{
					Display.ShowWarning("Cliente Não Encontrado!");
					Console.Write("Tentar encontrar cliente novamente? S - sim / Qualquer outra tecla - não: ");
					findClient = Console.ReadLine();
				}
				else
					GetClientBankTransactions(clients[clientIndex], bankTransactions);

			} while (findClient == "s" || findClient == "S");				
		}
	}
}