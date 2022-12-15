using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ByteBank1
{
    public class Client
    {
        private string Name;
        private string Cpf;
        private string Password;
        private decimal Balance;
        private string AccountNumber;       

        public Client(string name, string cpf, string password)
        {
            Name = name;
            Cpf = cpf;
            Password = password;			
			int randomNumber = RandomNumberGenerator.GetInt32(99999999);
			AccountNumber = String.Format("{0, 0:D8}", randomNumber);
            randomNumber = RandomNumberGenerator.GetInt32(999);
			Balance = randomNumber;
		}

        public bool SetBalance (decimal addition)
        {
            decimal newBalance = Balance + addition;

            if (newBalance >= 0)
            {
                Balance = newBalance;
                return true;
            }
            else
                return false;
        }

        public decimal GetBalance()
		{ return Balance; }

		public string GetName()
        { return Name; }

        public string GetCpf()
        { return Cpf; }

        public string GetPassword()
        { return Password; }

        public string GetAccount()
        { return AccountNumber; }

    }
    public class Program
    {
        static void GetWarning(string message)
        {
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
        }

        static void PrintClientInfo(List<Client>clients, int index)
        {
			Console.WriteLine();
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"Cliente ID: {index}".PadRight(30, ' '));
			Console.WriteLine($"Nome: {clients[index].GetName()}".PadRight(30, ' '));
			Console.WriteLine($"Conta: {clients[index].GetAccount()}".PadRight(30, ' '));
			Console.WriteLine($"CPF: {clients[index].GetCpf()}".PadRight(30, ' '));
			Console.WriteLine($"Saldo: R$ {clients[index].GetBalance():F2}".PadRight(30, ' '));
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine();
		}

        static void GetWarningForWrongOption()
        {
            GetWarning("Se essa operação foi escolhida por engano, basta não inserir valor em um ou mais campos do formulário.");
            Console.WriteLine();
        }

        static void BackToMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Pressione Enter para voltar ao menu anterior.");
            Console.ReadLine();
        }

        static void BankInterface (string message)
        {
            Console.Clear();
            string title = "ByteBank";
            string lineAlignedCenter = String.Format("{0,-80}",
                                    String.Format("{0," + ((80 + title.Length) / 2).ToString() + "}", title));
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"===================================================================================================="); // 100 "="
            Console.Write("=========|");
            Console.Write(lineAlignedCenter);
            Console.Write("|=========\n");            
            Console.WriteLine($"====================================================================================================");
            
            Console.BackgroundColor = ConsoleColor.DarkGray;
            
            Console.WriteLine($"----------------------------------------------------------------------------------------------------"); 
            lineAlignedCenter = String.Format("{0,-80}",
                                    String.Format("{0," + ((80 + message.Length) / 2).ToString() + "}", message));
            Console.Write("---------|");
            Console.Write(lineAlignedCenter);
            Console.Write("|---------\n");
            Console.WriteLine($"----------------------------------------------------------------------------------------------------");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
        }

        static void ShowMenu()
        {
            Console.WriteLine("1 - Inserir novo cliente");
            Console.WriteLine("2 - Deletar um cliente");
			Console.WriteLine("3 - Listar todos os clientes registrados");
			Console.WriteLine("4 - Detalhes de um cliente");
			Console.WriteLine("5 - Valor total armazenado no banco");
			Console.WriteLine("6 - Realizar transações bancárias");
			Console.WriteLine("0 - Sair do programa");
            Console.WriteLine();
        }

		static void ShowBankTransactionsMenu()
		{
			Console.WriteLine("1 - Depositar");
			Console.WriteLine("2 - Sacar");
			Console.WriteLine("3 - Transferir");
			Console.WriteLine("0 - Voltar ao Menu Principal");
			Console.WriteLine();
		}

		public static void CreateUser (List<Client> clients)
        {            
            string name, cpf, password;
            BankInterface("Cadastro de Novo Cliente");
            GetWarningForWrongOption();

            Console.Write("Insira nome do novo cliente: ");
            name = Console.ReadLine();
			Console.Write("Insira cpf do novo cliente: ");
			cpf = Console.ReadLine();
			Console.Write("Insira senha do novo cliente: ");
			password = Console.ReadLine();

			if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(cpf) && !string.IsNullOrEmpty(password))
            {
				clients.Add(new Client(name, cpf, password));
				PrintClientInfo(clients, clients.Count - 1);
                GetWarning("Cliente Cadastrado com Sucesso!");
			}
            else
				GetWarning("Cliente não Cadastrado!");

            BackToMenu();
		}

        static void GetAllUsers(List<Client> clients)
        {            
            BankInterface("Mostrar Todos os Clientes");
            Console.WriteLine($"Total de Clientes: {clients.Count}\n");

            for (int i = 0; i < clients.Count; i++)
                PrintClientInfo(clients, i); 
            
            BackToMenu();
        }

        static int GetIndexUser(List<Client> clients)
        {  
            Console.Write("Informe CPF do cliente requisitado: ");
			string userInformation = Console.ReadLine();

			int index = clients.FindIndex(client => client.GetCpf() == userInformation);       
			
			return index;
        }

        static void ShowUserDetails(List<Client> clients)
        {        
            BankInterface("Mostrar Detalhe de Usuário");
            GetWarningForWrongOption();

            int clientIndex = GetIndexUser(clients);

			if (clientIndex >= 0 && clientIndex < clients.Count)
                PrintClientInfo(clients, clientIndex);
            else
                GetWarning("Cliente não encontrado");
            BackToMenu();
        }

        static int DeleteUser(List<Client> clients)
        {
            BankInterface("Deletar Usuário");
            GetWarningForWrongOption();

            int indexSubtraction = 0;

            int indexToBeDeleted = GetIndexUser(clients);

			if (indexToBeDeleted >= 0 && indexToBeDeleted < clients.Count)
			{
                PrintClientInfo(clients, indexToBeDeleted);
                GetWarning("Cliente Deletado com Sucesso!");
                clients.RemoveAt(indexToBeDeleted);
            }
            else
                GetWarning("Cliente não encontrado!");
            BackToMenu();

            return indexSubtraction;
        }

	    static void ShowTotalBalance(List<Client> clients)
		{
			BankInterface("Operação Mostrar Valor Acumulado no Banco");

            decimal totalBalance = clients.Select(value => value.GetBalance()).Sum();

			Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Valor Acumulado no Banco: R$ {totalBalance:F2}");
            Console.BackgroundColor = ConsoleColor.DarkBlue;

			BackToMenu();
		}

		private static void PerformBankTransactions(int indexLoggedClient, List<Client> clients)
		{
            int option;            
            string warningMessage = "";
            bool warning = false, validEntry;

            do
            {
                BankInterface("Transações Bancárias");
                ShowBankTransactionsMenu();
                
                if (warning)
                {
                    GetWarning(warningMessage);
                    warning = false;
                }

                Console.Write("Escolha operação a ser realizada indicando seu número: ");

                validEntry = int.TryParse(Console.ReadLine(), out option);

                switch (option)
                {
                    case 1:
                        Deposit(indexLoggedClient, clients);                           
                        break;
                    case 2:
                        Withdraw(indexLoggedClient, clients);
						break;
                    case 3:
												                   
                        break; 
					case 0:                        
                    default:
                        if (validEntry && option == 0)                                                  
                            break;                        
                        else                        
                            option = -1;                       
                        warningMessage = "Aviso: Opção inválida, favor inserir número de 0 a 3.";
                        warning = true;
                        break;
                }      
            } while (option != 0); 
		}

		private static void Withdraw(int indexLoggedClient, List<Client> clients)
		{
			BankInterface("Saque");
			GetWarningForWrongOption();
			string details, withdraw;

			Console.Write("Quantia a ser retirada: R$ ");
			withdraw = Console.ReadLine();

			if (decimal.TryParse(withdraw, out decimal value) && value > 0 && clients[indexLoggedClient].SetBalance(-value))
			{
				GetWarning("Quantia Retirada com Sucesso!\n");

				Console.Write("Verificar detalhes da conta? S - sim / Qualquer outra tecla - não: ");
				details = Console.ReadLine();
				if (details == "S" || details == "s")
					PrintClientInfo(clients, indexLoggedClient);

			}
			else
				GetWarning("Operação Não Realizada!");

			BackToMenu();
		}

		private static void Deposit(int indexLoggedClient, List<Client> clients)
		{
			BankInterface("Depósito");
            GetWarningForWrongOption();
            string details, deposit;

			Console.Write("Quantia a ser depositada: R$ ");
            deposit = Console.ReadLine();

			if (decimal.TryParse(deposit, out decimal value) && value > 0 && clients[indexLoggedClient].SetBalance(value))
            {                
                GetWarning("Quantia adicionada com Sucesso!\n");
                
				Console.Write("Verificar detalhes da conta? S - sim / Qualquer outra tecla - não: ");
                details = Console.ReadLine();
				if (details == "S" || details == "s")                
                    PrintClientInfo(clients, indexLoggedClient);
                
			}
            else
				GetWarning("Operação Não Realizada!");

			BackToMenu();
		}

		static bool ValidateCredentials(List<Client> clients)
		{
			BankInterface("Validação de Credenciais de Cliente");
			
			int clientIndex = GetIndexUser(clients);		           

			if (clientIndex >= 0 && clientIndex < clients.Count)
			{
				Console.Write("\nFavor inserir a senha: ");
                string password = Console.ReadLine();

                if (clients[clientIndex].GetPassword() == password)                
                    PerformBankTransactions(clientIndex, clients);
                else
					GetWarning("Senha inválida");
			}
			else
				GetWarning("Cliente não encontrado");
			BackToMenu();

			return true;
		}

		public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
			List<Client> clients = new List<Client>();
			int option;            
            string warningMessage = "";
            bool warning = false, validEntry;

            do
            {
                BankInterface("Menu Inicial");
                ShowMenu();
                
                if (warning)
                {
                    GetWarning(warningMessage);
                    warning = false;
                }

                Console.Write("Escolha operação a ser realizada indicando seu número: ");

                validEntry = int.TryParse(Console.ReadLine(), out option);

                switch (option)
                {
                    case 1:                                                                
                        CreateUser(clients);                            
                        break;
                    case 2:
                        DeleteUser(clients);
                        break;
                    case 3:
                        GetAllUsers(clients);
                        break;
                    case 4:
						ShowUserDetails(clients);
						break;
                    case 5:
						ShowTotalBalance(clients);
						break;
                    case 6:
						ValidateCredentials(clients);
                        break;
					case 0:                        
                    default:
                        if (validEntry && option == 0)
                        {
                            BankInterface("Muito Obrigado por utilizar nosso aplicativo!");
                            break;
                        }
                        else                        
                            option = -1;                       
                        warningMessage = "Aviso: Opção inválida, favor inserir número de 0 a 6.";
                        warning = true;
                        break;
                }      
            } while (option != 0);
        }

	}
}