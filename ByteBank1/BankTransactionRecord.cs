using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ByteBank1
{
	public class BankTransactionRecord
	{
		private string id;
		private string operationType;
		private DateTime date;
		private decimal value;
		private Client originClient;
		private Client? destinationClient;

		[JsonConstructor]
		public BankTransactionRecord(string id, string operationType, DateTime date, decimal value, Client originClient, Client? destinationClient)
		{
			this.id = id;
			this.operationType = operationType;
			this.date = date;
			this.value = value;
			this.originClient = originClient;
			this.destinationClient = destinationClient;
		}

		public BankTransactionRecord(string operationType, decimal value, Client originClient, Client? destinationClient)
		{			
			id = Guid.NewGuid().ToString();
			this.operationType = operationType;
			date = DateTime.Now;
			this.value = value;
			this.originClient = originClient;
			this.destinationClient = destinationClient;
		}

		public string Id { get => id; }
		public string OperationType { get => operationType; }
		public DateTime Date { get => date; }
		public decimal Value { get => value; }
		public Client OriginClient { get => originClient; }

		public Client? DestinationClient { get => destinationClient; }

	}
}
