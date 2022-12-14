using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
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

        public void SetBalance (decimal addition)
        { Balance += addition; }

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
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
        }

        static void PrintClientInfo(List<Client>clients, int index)
        {
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"Cliente ID {index}".PadRight(30, ' '));
			Console.WriteLine($"Nome: {clients[index].GetName()}".PadRight(30, ' '));
			Console.WriteLine($"Conta: {clients[index].GetAccount()}".PadRight(30, ' '));
			Console.WriteLine($"CPF: {clients[index].GetCpf()}".PadRight(30, ' '));
			Console.WriteLine($"Saldo: {clients[index].GetBalance()}".PadRight(30, ' '));
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
            Console.WriteLine("Pressione Enter para voltar ao menu principal.");
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

        static int GetNumberOfUsers ()
        {
            int users;
            int warning = 0;
            while (true)
            {
                BankInterface("Configurações Iniciais");
                if (warning > 0)                
                    GetWarning("Aviso: Quantidade inválida, favor inserir quantidade maior que 0.");

                Console.Write("Informe a quantidade máxima de usuários que poderão ser cadastrados no sistema: ");

                if (int.TryParse(Console.ReadLine(), out users) && users > 0)
                    break;
                warning++;
                Console.Clear();
            }
            Console.Clear();
            return users;
        }

        static void ShowMenu()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
			Console.WriteLine("3 - Listar todas as contas registradas");
			Console.WriteLine("4 - Detalhes de um usuário");
			Console.WriteLine("5 - Valor total armazenado no banco");
			Console.WriteLine("6 - Realizar transações bancárias");
			Console.WriteLine("0 - Sair do programa");
            Console.WriteLine();
        }

        public static void CreateUser (List<Client> clients)
        {            
            string name, cpf, password;
            BankInterface("Operação Criação de Novo Usuário");
            GetWarningForWrongOption();

            Console.Write("Insira nome do novo usuário: ");
            name = Console.ReadLine();
			Console.Write("Insira cpf do novo usuário: ");
			cpf = Console.ReadLine();
			Console.Write("Insira senha do novo usuário: ");
			password = Console.ReadLine();

			if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(cpf) && !string.IsNullOrEmpty(password))            
                clients.Add(new Client(name, cpf, password));           
                     
        }

        static void GetAllUsers(List<Client> clients)
        {            
            BankInterface("Operação Mostrar Todos Usuário");
            Console.WriteLine($"Total de Clientes: {clients.Count()}\n");

            for (int i = 0; i < clients.Count(); i++)
                PrintClientInfo(clients, i); 
            
            BackToMenu();
        }

        static int GetIndexUser(List<Client> clients)
        {
            int index;
            string userInformation;
 
            Console.Write("Informe nome ou numero da conta do usuário requisitado: ");
            userInformation = Console.ReadLine();

            if (string.IsNullOrEmpty(userInformation))
                index = -1;
            else if (int.TryParse(userInformation, out int saida))
                index = clients.FindIndex(client => client.GetAccount() == userInformation);
            else
				index = clients.FindIndex(client => client.GetName() == userInformation);

			return index;
        }

        static void ShowUserDetails(List<Client> clients)
        {           
            int index;

            BankInterface("Operação Mostrar Detalhe de Usuário");
            GetWarningForWrongOption();

            index = GetIndexUser(clients);
			Console.WriteLine();

			if (index >= 0 && index < clients.Count())
                PrintClientInfo(clients, index);
            else
                GetWarning("Cliente não encontrado");
            BackToMenu();
        }

        static int DeleteUser(List<Client> clients)
        {
            BankInterface("Operação Deletar Usuário");
            GetWarningForWrongOption();

            int indexSubtraction = 0;

            int indexToBeDeleted = GetIndexUser(clients);
			Console.WriteLine();

			if (indexToBeDeleted >= 0 && indexToBeDeleted < clients.Count())
            {
                PrintClientInfo(clients, indexToBeDeleted);
                GetWarning("Cliente Deletado com Sucesso!");
                clients.RemoveAt(indexToBeDeleted);
            }
            else
                GetWarning("Cliente não encontrado");
            BackToMenu();

            return indexSubtraction;
        }

		private static void ShowTotalBalance(List<Client> clients)
		{
			BankInterface("Operação Mostrar Valor Acumulado no Banco");

            decimal totalBalance = clients.Select(value => value.GetBalance()).Sum();

			Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Valor Acumulado no Banco: R$ {totalBalance:F2}");
            Console.BackgroundColor = ConsoleColor.DarkBlue;

			BackToMenu();

		}

		public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White; 
            int option;
            List<Client> clients = new List<Client>();
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
					case 0:                        
                    default:
                        if (validEntry && option == 0)
                        {
                            BankInterface("Muito Obrigado por utilizar nosso aplicativo!");
                            break;
                        }
                        else                        
                            option = -1;                        

                        warningMessage = "Aviso: Opção inválida, favor inserir número de 1 a 6.";
                        warning = true;

                        break;
                }      
            } while (option != 0);
        }

	}
}