using System;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace ByteBank1
{
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
            Console.WriteLine("3 - Detalhes de um usuário");
            Console.WriteLine("4 - Total de usuários armazenados no banco");
            Console.WriteLine("5 - Sair do programa");
            Console.WriteLine();
        }

        public static int CreateUser (string[] userName, string[] userAccountNumber, int indexToPopulate)
        {

            int indexAddition = 0;
            string name;
            BankInterface("Operação Criação de Novo Usuário");
            GetWarningForWrongOption();

            Console.Write("Insira nome do novo usuário: ");
            name = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(name))
            {
                userName[indexToPopulate] = name;
                int accountNumber = RandomNumberGenerator.GetInt32(99999999);
                userAccountNumber[indexToPopulate] = String.Format("{0, 0:D8}", accountNumber);
                indexAddition++;
            }

            return indexAddition;            
        }

        static void GetAllUsers(string[] userName, string[] userAccountNumber, int currentIndex)
        {            
            BankInterface("Operação Mostrar Todos Usuário");
            Console.WriteLine($"Total de Clientes: {currentIndex}\n");

            for (int i = 0; i < currentIndex; i++)
            {
                Console.BackgroundColor= ConsoleColor.DarkGreen;
                Console.WriteLine($"Cliente ID {i}".PadRight(30, ' '));
                Console.WriteLine($"Nome: {userName[i]}".PadRight(30, ' '));
                Console.WriteLine($"Conta: {userAccountNumber[i]}".PadRight(30, ' '));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine();
            }
            BackToMenu();
        }

        static int GetIndexUser(string[] userName, string[] userAccountNumber)
        {
            int index;
            string userInformation;
 
            Console.Write("Informe nome ou numero da conta do usuário requisitado: ");
            userInformation = Console.ReadLine();

            if (string.IsNullOrEmpty(userInformation))
                index = -1;
            else if (int.TryParse(userInformation, out int saida))
                index = Array.IndexOf(userAccountNumber, userInformation);
            else
                index = Array.IndexOf(userName, userInformation);            

            return index;
        }

        static void ShowUserDetails(string[] userName, string[] userAccountNumber, int currentIndex)
        {           
            int index;

            BankInterface("Operação Mostrar Detalhe de Usuário");
            GetWarningForWrongOption();

            index = GetIndexUser(userName, userAccountNumber);

            if (index >= 0 && index < currentIndex)
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Cliente ID {index}".PadRight(30, ' '));
                Console.WriteLine($"Nome: {userName[index]}".PadRight(30, ' '));
                Console.WriteLine($"Conta: {userAccountNumber[index]}".PadRight(30, ' '));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine();
            }
            else
                GetWarning("Cliente não encontrado");
            BackToMenu();
        }

        static int DeleteUser(string[] userName, string[] userAccountNumber, int currentEmptyIndex)
        {
            BankInterface("Operação Deletar Usuário");
            GetWarningForWrongOption();

            int indexSubtraction = 0;

            int indexToBeDeleted = GetIndexUser(userName, userAccountNumber);

            if (indexToBeDeleted >= 0 && indexToBeDeleted < currentEmptyIndex)
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Cliente ID {indexToBeDeleted}");
                Console.WriteLine($"Nome: {userName[indexToBeDeleted]}");
                Console.WriteLine($"Conta: {userAccountNumber[indexToBeDeleted]}");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine();
                GetWarning("Cliente Deletado com Sucesso!");
                for (int i = indexToBeDeleted; i < currentEmptyIndex; i++)
                {
                    if (i == userName.Length - 1)
                    {
                        userName[i] = string.Empty;
                        userAccountNumber[i] = string.Empty;
                    }
                    else
                    {
                        userName[i] = userName[i + 1];
                        userAccountNumber[i] = userAccountNumber[i + 1];
                    }                       
                }
                indexSubtraction = 1;
            }
            else
                GetWarning("Cliente não encontrado");
            BackToMenu();

            return indexSubtraction;
        }

        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            int numberOfUsers = GetNumberOfUsers();
            int currentIndex = 0;
            int option;
            string[] dbName = new string[numberOfUsers];
            string[] dbAccountNumber = new string[numberOfUsers];
            string warningMessage = "";
            bool warning = false;

            do
            {
                BankInterface("Menu Inicial");
                ShowMenu();
                //Console.WriteLine($"\n index atual:{currentIndex} \n");
                if (warning)
                {
                    GetWarning(warningMessage);
                    warning = false;
                }
                    
                Console.Write("Escolha operação a ser realizada indicando seu número: ");
                
                int.TryParse(Console.ReadLine(), out option);

                switch (option)
                {
                    case 1:
                        if (currentIndex < numberOfUsers)                                         
                            currentIndex += CreateUser(dbName, dbAccountNumber, currentIndex);
                        else
                        {
                            warningMessage = "Aviso: Opção invalida, número máximo de usuários já está cadastrado!";
                            warning = true;
                        }                            
                        break;
                    case 2:
                        currentIndex -= DeleteUser(dbName, dbAccountNumber, currentIndex);
                        break;
                    case 3:
                        ShowUserDetails(dbName, dbAccountNumber, currentIndex);                   
                        break;
                    case 4:
                        GetAllUsers(dbName, dbAccountNumber, currentIndex);                      
                        break;
                    case 5:
                        BankInterface("Muito Obrigado por utilizar nosso aplicativo!");                     
                        break;
                    default:
                        warningMessage = "Aviso: Opção inválida, favor inserir número de 1 a 5.";
                        warning = true;
                        break;
                }      
            } while (option != 5);

        }
    }

}
