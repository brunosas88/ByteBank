using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

		public bool SetBalance(decimal addition)
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
}

