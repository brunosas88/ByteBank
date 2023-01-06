using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ByteBank1.Model
{
    public class BankTransactionRecord
    {
        private string _id;
        private string _operationType;
        private DateTime _date;
        private decimal _value;
        private Client _originClient;
        private Client? _destinationClient;

        [JsonConstructor]
        public BankTransactionRecord(string id, string operationType, DateTime date, decimal value, Client originClient, Client? destinationClient)
        {
            _id = id;
            _operationType = operationType;
            _date = date;
            _value = value;
            _originClient = originClient;
            _destinationClient = destinationClient;
        }

        public BankTransactionRecord(string operationType, decimal value, Client originClient, Client? destinationClient)
        {
            _id = Guid.NewGuid().ToString();
            _operationType = operationType;
            _date = DateTime.Now;
            _value = value;
            _originClient = originClient;
            _destinationClient = destinationClient;
        }

        public string Id { get => _id; }
        public string OperationType { get => _operationType; }
        public DateTime Date { get => _date; }
        public decimal Value { get => _value; }
        public Client OriginClient { get => _originClient; }

        public Client? DestinationClient { get => _destinationClient; }

    }
}
