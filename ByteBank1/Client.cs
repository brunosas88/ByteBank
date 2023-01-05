using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ByteBank1
{
	public class Client
	{
		private string name;
		private string cpf;
		private string password;
		private decimal balance;
		private string accountNumber;
		
		[JsonConstructor]
		public Client(string name, string cpf, string password, decimal balance, string accountNumber) : this(name, cpf, password)
		{
			this.balance = balance;
			this.accountNumber = accountNumber;
		}

		public Client(string name, string cpf, string password)
		{
			this.name = name;
			this.cpf = cpf;
			this.password = password;
			int randomNumber = RandomNumberGenerator.GetInt32(99999999);
			accountNumber = String.Format("{0, 0:D8}", randomNumber);			
			balance = 0;
		}

		public bool SetBalance(decimal addition)
		{
			decimal newBalance = balance + addition;

			if (newBalance >= 0)
			{
				balance = newBalance;
				return true;
			}
			else
				return false;
		}

		public string Name { get => name; }

		public string Cpf { get => cpf; }

		public string Password { get => password; }

		public decimal Balance { get => balance; }

		public string AccountNumber { get => accountNumber; }

		public string MaskedCpf { get => Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00"); }		

	}
}

