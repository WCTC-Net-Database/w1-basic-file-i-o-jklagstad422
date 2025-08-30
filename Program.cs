using System;
using System.IO;

class Program
{
    static string fileName = "input.csv";
    static string[] names = new string[40];
    static string[] classes = new string[40];
    static int[] levels = new int[40];
    static int[] hps = new int[999];
    static string[] equipment = new string[60];
    static int characterCount = 0;

    static void Main()
    {
        LoadCharacters();

        while (true)
        {
            Console.WriteLine("\n--- Character Manager ---");
            Console.WriteLine("1) Display Characters");
            Console.WriteLine("2) Add Character");
            Console.WriteLine("3) Level Up Character");
            Console.WriteLine("4) Exit");
            Console.Write("Choose (1-4): ");
            string choice = Console.ReadLine();

            if (choice == "1") DisplayCharacters();
            else if (choice == "2") AddCharacter();
            else if (choice == "3") LevelUpCharacter();
            else if (choice == "4")
            {
                SaveCharacters();
                Console.WriteLine("Goodbye!");
                break;
            }
            else Console.WriteLine("Invalid choice.");
        }
    }
    static void LoadCharacters()
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("input.csv not found. Creating default characters.");
            string[] defaultData = {
                "John,Fighter,1,10,sword|shield|potion",
                "Jane,Wizard,2,6,staff|robe|book",
                "Bob,Rogue,3,8,dagger|lockpick|cloak",
                "Alice,Cleric,4,12,mace|armor|potion"
            };
            File.WriteAllLines(fileName, defaultData);
        }

        string[] lines = File.ReadAllLines(fileName);
        characterCount = 0;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 5) continue;

            names[characterCount] = parts[0];
            classes[characterCount] = parts[1];
            levels[characterCount] = int.Parse(parts[2]);
            hps[characterCount] = int.Parse(parts[3]);
            equipment[characterCount] = parts[4];
            characterCount++;
        }

        Console.WriteLine($"Loaded {characterCount} character(s) from {fileName}.");
    }

    static void SaveCharacters()
    {
        string[] lines = new string[characterCount];
        for (int i = 0; i < characterCount; i++)
        {
            lines[i] = $"{names[i]},{classes[i]},{levels[i]},{hps[i]},{equipment[i]}";
        }
        File.WriteAllLines(fileName, lines);
        Console.WriteLine($"Saved {characterCount} character(s) to {fileName}.");
    }

    static void DisplayCharacters()
    {
        if (characterCount == 0)
        {
            Console.WriteLine("No characters available.");
            return;
        }

        for (int i = 0; i < characterCount; i++)
        {
            Console.WriteLine($"{names[i]} the {classes[i]} (Level {levels[i]}, HP {hps[i]}) | Equipment: {equipment[i]}");
        }
    }
    static void AddCharacter()
    {
        if (characterCount >= names.Length)
        {
            Console.WriteLine("Character list is full!");
            return;
        }

        Console.Write("Enter name: ");
        names[characterCount] = Console.ReadLine();

        Console.Write("Enter class: ");
        classes[characterCount] = Console.ReadLine();

        Console.Write("Enter level: ");
        levels[characterCount] = int.Parse(Console.ReadLine());

        Console.Write("Enter HP: ");
        hps[characterCount] = int.Parse(Console.ReadLine());

        Console.Write("Enter equipment (use | between items): ");
        equipment[characterCount] = Console.ReadLine();

        characterCount++;
        Console.WriteLine("Character added!");
    }

    static void LevelUpCharacter()
    {
        Console.Write("Enter name to level up: ");
        string target = Console.ReadLine();

        for (int i = 0; i < characterCount; i++)
        {
            if (names[i].Equals(target, StringComparison.OrdinalIgnoreCase))
            {
                levels[i]++;
                hps[i] += 5;
                Console.WriteLine($"{names[i]} leveled up to {levels[i]}! (HP {hps[i]})");
                return;
            }
        }
        Console.WriteLine("Character not found.");
    }
}
