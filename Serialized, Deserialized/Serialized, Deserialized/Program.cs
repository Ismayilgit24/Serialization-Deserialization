using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
	static string jsonFilePath = "D:\\CodeAcademy\\Serialized, Deserialized\\Serialized, Deserialized\\Names.json";

	static void Main()
	{
		
		Add("Alice");
		Add("Bob");
		Add("Nick");
		Add("Charlie");
		Console.WriteLine(Search("Alice")); 
		Console.WriteLine(Search("Charlie")); 
		Delete(0);
	}

	static void Add(string name)
	{
		var names = ExtractNames();
		names.Add(name);
		SerializeNames(names);
	}

	static bool Search(string name)
	{
		var names = ExtractNames();
		return names.Exists(n => n.Equals(name, StringComparison.OrdinalIgnoreCase));
	}

	static void Delete(int index)
	{
		var names = ExtractNames();
		if (index >= 0 && index < names.Count)
		{
			names.RemoveAt(index);
			SerializeNames(names);
		}
	}

	static List<string> ExtractNames()
	{
		if (!File.Exists(jsonFilePath))
		{
			return new List<string>();
		}

		using (var reader = new StreamReader(jsonFilePath))
		{
			var jsonData = reader.ReadToEnd();
			if (string.IsNullOrWhiteSpace(jsonData))
			{
				return new List<string>();
			}

			return JsonConvert.DeserializeObject<List<string>>(jsonData) ?? new List<string>();
		}
	}

	static void SerializeNames(List<string> names)
	{
		using (var writer = new StreamWriter(jsonFilePath))
		{
			var jsonData = JsonConvert.SerializeObject(names, Formatting.Indented);
			writer.Write(jsonData);
		}
	}
}
