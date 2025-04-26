using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();
        int grade = int.Parse(input);

        string letter = "";
        string sign = "";

        // Determine the letter grade
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the + or - sign
        int lastDigit = grade % 10;

        if (letter == "A")
        {
            if (grade < 93) // Only A- for 90-92
            {
                sign = "-";
            }
            // No A+ even if last digit is >=7
        }
        else if (letter == "B" || letter == "C" || letter == "D")
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            // No sign if lastDigit between 3 and 6
        }
        // No + or - for F grades

        // Final grade output
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Pass or not
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course!");
        }
        else
        {
            Console.WriteLine("Better luck next time. Keep trying!");
        }
    }
}
