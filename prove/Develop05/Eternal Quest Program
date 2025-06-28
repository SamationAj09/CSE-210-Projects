// Eternal Quest Program - Exceeding Version
// Features:
// 1. Leveling System (every 1000 points = level up)
// 2. Celebration Message for completing checklist goals
// 3. Daily Streak tracking and bonuses
// 4. Negative goals to lose points
// 5. Badge system for achievements

using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent(ref int score);
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string SaveString();
    public string GetName() => _name;
    public string GetDescription() => _description;
    public int GetPoints() => _points;
}

class SimpleGoal : Goal
{
    private bool _isComplete = false;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent(ref int score)
    {
        if (!_isComplete)
        {
            _isComplete = true;
            score += _points;
        }
    }

    public override bool IsComplete() => _isComplete;
    public override string GetStatus() => _isComplete ? "[X]" : "[ ]";
    public override string SaveString() => $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent(ref int score)
    {
        score += _points;
    }

    public override bool IsComplete() => false;
    public override string GetStatus() => "[∞]";
    public override string SaveString() => $"EternalGoal|{_name}|{_description}|{_points}";
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, int currentCount = 0)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = currentCount;
    }

    public override void RecordEvent(ref int score)
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            score += _points;
            if (_currentCount == _targetCount)
            {
                score += _bonusPoints;
                Console.WriteLine("🎉 Bonus achieved!");
            }
        }
    }

    public override bool IsComplete() => _currentCount >= _targetCount;
    public override string GetStatus() => $"[{_currentCount}/{_targetCount}]";
    public override string SaveString() => $"ChecklistGoal|{_name}|{_description}|{_points}|{_targetCount}|{_currentCount}|{_bonusPoints}";
}

class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int penaltyPoints)
        : base(name, description, -Math.Abs(penaltyPoints)) { }

    public override void RecordEvent(ref int score)
    {
        score += _points;
        Console.WriteLine($"⚠️ Lost {-_points} points for: {_name}");
    }

    public override bool IsComplete() => false;
    public override string GetStatus() => "[!]";
    public override string SaveString() => $"NegativeGoal|{_name}|{_description}|{_points}";
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static List<string> badges = new List<string>();
    static int score = 0;
    static int streak = 0;
    static DateTime lastRecordedDate = DateTime.MinValue;

    static void Main()
    {
        string input;
        do
        {
            Console.WriteLine("\n--- Eternal Quest Menu ---");
            Console.WriteLine($"Score: {score} | Level: {score / 1000} | Streak: {streak}");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. View Badges");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": ShowBadges(); break;
                case "7": Console.WriteLine("Goodbye!"); break;
                default: Console.WriteLine("Invalid input."); break;
            }
        } while (input != "7");
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Negative Goal");
        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (choice == "1")
            goals.Add(new SimpleGoal(name, desc, points));
        else if (choice == "2")
            goals.Add(new EternalGoal(name, desc, points));
        else if (choice == "3")
        {
            Console.Write("Target count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
        else if (choice == "4")
            goals.Add(new NegativeGoal(name, desc, points));
    }

    static void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            var g = goals[i];
            Console.WriteLine($"{i + 1}. {g.GetStatus()} {g.GetName()} - {g.GetDescription()}");
        }
    }

    static void RecordEvent()
    {
        ListGoals();
        Console.Write("Which goal did you accomplish? Enter number: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < goals.Count)
        {
            goals[index].RecordEvent(ref score);

            // Handle streak logic
            DateTime today = DateTime.Today;
            if ((today - lastRecordedDate).Days == 1)
            {
                streak++;
                Console.WriteLine($"🔥 Daily streak: {streak} days!");
                if (streak % 5 == 0)
                {
                    int bonus = 100;
                    score += bonus;
                    Console.WriteLine($"🎁 Streak bonus! +{bonus} points");
                }
            }
            else if ((today - lastRecordedDate).Days > 1)
            {
                streak = 1;
                Console.WriteLine("😢 Streak reset. Starting over.");
            }
            lastRecordedDate = today;

            // Check for badges
            int simpleCount = 0, checklistCount = 0;
            foreach (var g in goals)
            {
                if (g is SimpleGoal && g.IsComplete()) simpleCount++;
                if (g is ChecklistGoal && g.IsComplete()) checklistCount++;
            }
            if (simpleCount >= 3 && !badges.Contains("Simple Starter"))
            {
                badges.Add("Simple Starter");
                Console.WriteLine("🏅 You earned the 'Simple Starter' badge!");
            }
            if (checklistCount >= 3 && !badges.Contains("Checklist Champion"))
            {
                badges.Add("Checklist Champion");
                Console.WriteLine("🏅 You earned the 'Checklist Champion' badge!");
            }
            if (streak >= 5 && !badges.Contains("Streak Survivor"))
            {
                badges.Add("Streak Survivor");
                Console.WriteLine("🏅 You earned the 'Streak Survivor' badge!");
            }
        }
    }

    static void SaveGoals()
    {
        Console.Write("Filename to save: ");
        string filename = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(score);
            writer.WriteLine(streak);
            writer.WriteLine(lastRecordedDate);
            writer.WriteLine(string.Join(",", badges));
            foreach (Goal g in goals)
            {
                writer.WriteLine(g.SaveString());
            }
        }
        Console.WriteLine("Saved successfully.");
    }

    static void LoadGoals()
    {
        Console.Write("Filename to load: ");
        string filename = Console.ReadLine();
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            score = int.Parse(lines[0]);
            streak = int.Parse(lines[1]);
            lastRecordedDate = DateTime.Parse(lines[2]);
            badges = new List<string>(lines[3].Split(","));
            goals.Clear();
            for (int i = 4; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                switch (parts[0])
                {
                    case "SimpleGoal":
                        var simple = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                        if (bool.Parse(parts[4])) simple.RecordEvent(ref score);
                        goals.Add(simple);
                        break;
                    case "EternalGoal":
                        goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                        break;
                    case "ChecklistGoal":
                        goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[6]), int.Parse(parts[5])));
                        break;
                    case "NegativeGoal":
                        goals.Add(new NegativeGoal(parts[1], parts[2], int.Parse(parts[3])));
                        break;
                }
            }
            Console.WriteLine("Loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    static void ShowBadges()
    {
        Console.WriteLine("🏅 Badges Earned:");
        if (badges.Count == 0) Console.WriteLine("(No badges yet.)");
        foreach (var badge in badges)
            Console.WriteLine($"- {badge}");
    }
} 
