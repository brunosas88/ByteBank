using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByteBank1
{
	public class Utils
	{
		public static void WriteJSON(List<Client> clients)
		{
			string filePath = @"C:\Users\Public\Documents\clients.json";

			string jsonStringClients = JsonSerializer.Serialize(clients, new JsonSerializerOptions() { WriteIndented = true });

			using (StreamWriter outputFile = new StreamWriter(filePath))
				outputFile.WriteLine(jsonStringClients);

		}

		public static void ReadJSON(ref List<Client> clients)
		{
			string filePath = @"C:\Users\Public\Documents\clients.json";

			if (File.Exists(filePath))
			{
				using (StreamReader inputFile = new StreamReader(filePath))
				{
					string json = inputFile.ReadToEnd();
					clients = JsonSerializer.Deserialize<List<Client>>(json);
				}
			}
		}
	}
}
