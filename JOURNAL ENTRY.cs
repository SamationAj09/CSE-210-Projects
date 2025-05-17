// Program.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        string choice = "";

        // Daily reminder message (creativity + exceeding requirement)
        Console.WriteLine("\n Daily Reminder: Don’t forget to write in your journal today! 🌙\n");

        while (choice != "6")
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file (.txt)");
            Console.WriteLine("4. Load the journal from a file (.txt)");
            Console.WriteLine("5. Export/Import as JSON (Advanced)");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGenerator.GetRandomPrompt();
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    journal.AddEntry(new Entry(prompt, response));
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter filename to save to: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;
                case "4":
                    Console.Write("Enter filename to load from: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;
                case "5":
                    Console.WriteLine("1. Export to JSON");
                    Console.WriteLine("2. Import from JSON");
                    Console.Write("Choose an option: ");
                    string jsonChoice = Console.ReadLine();
                    if (jsonChoice == "1")
                    {
                        Console.Write("Enter filename to export JSON: ");
                        string jsonExport = Console.ReadLine();
                        journal.ExportToJson(jsonExport);
                    }
                    else if (jsonChoice == "2")
                    {
                        Console.Write("Enter filename to import JSON: ");
                        string jsonImport = Console.ReadLine();
                        journal.ImportFromJson(jsonImport);
                    }
                    else
                    {
                        Console.WriteLine("Invalid JSON option.");
                    }
                    break;
                case "6":
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    // Exceeding Requirements:
    // - Shows reminder message
    // - Stores and displays time
    // - Saves and loads in a consistent, extendable format (| delimiter)
    // - Adds full JSON export/import capability
}

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry() { }

    public Entry(string prompt, string response)
    {
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Prompt = prompt;
        Response = response;
    }

    public void Display()
    {
        Console.WriteLine($"[{Date}]\nPrompt: {Prompt}\nResponse: {Response}\n");
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                Entry entry = new Entry
                {
                    Date = parts[0],
                    Prompt = parts[1],
                    Response = parts[2]
                };
                _entries.Add(entry);
            }
        }
        Console.WriteLine("Journal loaded successfully.");
    }

    public void ExportToJson(string filename)
    {
        string json = JsonSerializer.Serialize(_entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
        Console.WriteLine("Exported to JSON successfully.");
    }

    public void ImportFromJson(string filename)
    {
        string json = File.ReadAllText(filename);
        _entries = JsonSerializer.Deserialize<List<Entry>>(json);
        Console.WriteLine("Imported from JSON successfully.");
    }
}

class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(_prompts.Count);
        return _prompts[index];
    }
}
