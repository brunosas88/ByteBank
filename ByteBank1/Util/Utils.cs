using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ByteBank1.View;
using ByteBank1.Model;

namespace ByteBank1.Util
{
    public class Utils
    {
        public static void WriteJSON(List<Client> clients, List<BankTransactionRecord> bankTransactions)
        {


            if (!Directory.Exists(Constants.AppDirectoryPath))
                Directory.CreateDirectory(Constants.AppDirectoryPath);

            string filePath = @$"{Constants.AppDirectoryPath}\clients.json";

            string jsonStringClients = JsonSerializer.Serialize(clients, new JsonSerializerOptions() { WriteIndented = true });

            using (StreamWriter outputFile = new StreamWriter(filePath))
                outputFile.WriteLine(jsonStringClients);

            filePath = @$"{Constants.AppDirectoryPath}\bankTransactions.json";

            string jsonStringBankTransactions = JsonSerializer.Serialize(bankTransactions, new JsonSerializerOptions() { WriteIndented = true });

            using (StreamWriter outputFile = new StreamWriter(filePath))
                outputFile.WriteLine(jsonStringBankTransactions);
        }

        public static void ReadJSON(ref List<Client> clients, ref List<BankTransactionRecord> bankTransactions)
        {
            string filePath = @$"{Constants.AppDirectoryPath}\clients.json";

            if (File.Exists(filePath))
            {
                using (StreamReader inputFile = new StreamReader(filePath))
                {
                    string json = inputFile.ReadToEnd();
                    clients = JsonSerializer.Deserialize<List<Client>>(json);
                }
            }

            filePath = @$"{Constants.AppDirectoryPath}\bankTransactions.json";

            if (File.Exists(filePath))
            {
                using (StreamReader inputFile = new StreamReader(filePath))
                {
                    string json = inputFile.ReadToEnd();
                    bankTransactions = JsonSerializer.Deserialize<List<BankTransactionRecord>>(json);
                }
            }
        }

        public static void WriteBankTransactionRecordFile(BankTransactionRecord record)
        {
            string directoryPath = @$"{Constants.AppDirectoryPath}\Records\{ChangeDateComposition(record.Date)}";

            if (!Directory.Exists(directoryPath))
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

            if (record.OperationType == BankOperation.Deposito.ToString() || record.OperationType == BankOperation.Saque.ToString())
            {
                file.WriteLine(Display.alignMessage($"CONTA: {record.OriginClient.AccountNumber}"));
                file.WriteLine(Display.alignMessage($"NOME: {record.OriginClient.Name}"));
                file.WriteLine(Display.alignMessage($"CPF: {record.OriginClient.MaskedCpf}"));
            }
            else
            {
                file.WriteLine(Display.alignMessage("DESTINO"));
                file.WriteLine(Display.alignMessage($"CONTA: {record.DestinationClient.AccountNumber}"));
                file.WriteLine(Display.alignMessage($"NOME: {record.DestinationClient.Name}"));
                file.WriteLine(Display.alignMessage($"CPF: {record.DestinationClient.MaskedCpf}"));
                file.WriteLine(Display.alignMessage(new string('-', 82)));
                file.WriteLine(Display.alignMessage("ORIGEM"));
                file.WriteLine(Display.alignMessage($"CONTA: {record.OriginClient.AccountNumber}"));
                file.WriteLine(Display.alignMessage($"NOME: {record.OriginClient.Name}"));
                file.WriteLine(Display.alignMessage($"CPF: {record.OriginClient.MaskedCpf}"));
            }
            file.WriteLine(Display.alignMessage(new string('=', 82)));
            file.Close();
        }

        public static string ChangeDateComposition(DateTime date)
        {
            return date.ToShortDateString().Replace('/', '-');
        }

        public static string EncryptPassword()
        {
            string password = "";
            ConsoleKey dataEntry;

            do
            {
                ConsoleKeyInfo dataEntryKeyInfoFormat = Console.ReadKey(intercept: true);
                dataEntry = dataEntryKeyInfoFormat.Key;

                if (dataEntry == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                else if (!char.IsControl(dataEntryKeyInfoFormat.KeyChar))
                {
                    Console.Write("*");
                    password += dataEntryKeyInfoFormat.KeyChar;
                }
            } while (dataEntry != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}
