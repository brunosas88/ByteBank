using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ByteBank1.Model
{
    public class Client
    {
        private string _name;
        private string _cpf;
        private string _password;
        private decimal _balance;
        private string _accountNumber;

        [JsonConstructor]
        public Client(string name, string cpf, string password, decimal balance, string accountNumber) : this(name, cpf, password)
        {
            _balance = balance;
            _accountNumber = accountNumber;
        }

        public Client(string name, string cpf, string password)
        {
            _name = name;
            _cpf = cpf;
            _password = password;
            int randomNumber = RandomNumberGenerator.GetInt32(99999999);
            _accountNumber = string.Format("{0, 0:D8}", randomNumber);
            _balance = 0;
        }

        public bool SetBalance(decimal addition)
        {
            decimal newBalance = _balance + addition;

            if (newBalance >= 0)
            {
                _balance = newBalance;
                return true;
            }
            else
                return false;
        }

        public string Name { get => _name; }

        public string Cpf { get => _cpf; }

        public string Password { get => _password; }

        public decimal Balance { get => _balance; }

        public string AccountNumber { get => _accountNumber; }

        public string MaskedCpf { get => Convert.ToUInt64(_cpf).ToString(@"000\.000\.000\-00"); }

    }
}

