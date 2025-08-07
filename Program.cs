using System;
using System.Collections.Generic;
using System.Threading;

namespace project_rgp;

class Program
{
    static Random rng = new Random();

    static void Main(string[] args)
    {
        Intro();
        RunGame();
        EndGame();
    }

    static void Intro()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("           ██████████╗ ███████╗███████╗");
        Console.WriteLine("           ╚══██╔══╝ ██╔════╝╚══██╔══╝");
        Console.WriteLine("              ██║    █████╗     ██║   ");
        Console.WriteLine("              ██║    ██╔══╝     ██║   ");
        Console.WriteLine("              ██║    ███████╗   ██║   ");
        Console.WriteLine("              ╚═╝    ╚══════╝   ╚═╝   ");
        Console.ResetColor();
        Console.WriteLine("             GATEKEEPER’S TEST\n\n");
        Console.ReadKey(true);
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════╗");
        Console.WriteLine("║           DARK ROOM INITIATED...          ║");
        Console.WriteLine("╚════════════════════════════════════════════╝");
        Console.ReadKey(true);
        Console.WriteLine();
        Console.WriteLine(">> You wake up in a dark, silent room...");
        Console.ReadKey(true);
        Console.WriteLine("\n>> A shadowy figure slowly emerges from the darkness...");
        Console.ReadKey(true);
        Console.WriteLine("\n>> \"I am the Master Quiz-Knower.\" he declares...");
        Console.ReadKey(true);
        Console.WriteLine("\n>> \"Answer correctly to live... answer wrong, and you bleed.\"");
        Console.ReadKey(true);

        Console.WriteLine("\n===============================================");
        Console.WriteLine("      You have been granted 2 potions.");
        Console.WriteLine("      1) Health (+50hp)");
        Console.WriteLine("      2) Elixir (+30hp)");
        Console.WriteLine("      Use them wisely. There will be no mercy.");
        Console.WriteLine("===============================================");
        Console.ReadKey(true);
        Console.Clear();
    }

    static void RunGame()
    {
        string[] questions =
        {
            "Which empire was ruled by Mansa Musa in the 14th century?",
            "What is the only known material that can conduct electricity without resistance at room temperature (under high pressure)?",
            "In formal logic, what does the symbol ‘⊨’ represent?",
            "What is the powerhouse of the cell?",
            "Who painted the ceiling of the Sistine Chapel?",
            "Which programming language is known for its use in statistical computing?",
            "What does the acronym 'HTTP' stand for?"
        };

        string[] answers =
        {
            "A. Ottoman Empire\nB. Mali Empire\nC. Byzantine Empire\nD. Songhai",
            "A. Graphene\nB. Copper\nC. Lanthanum decahydride\nD. Superfluid helium",
            "A. Logical contradiction\nB. Material implication\nC. Semantic entailment\nD. Biconditional",
            "A. Nucleus\nB. Mitochondria\nC. Ribosome\nD. Endoplasmic reticulum",
            "A. Leonardo da Vinci\nB. Raphael\nC. Michelangelo\nD. Donatello",
            "A. C#\nB. Python\nC. R\nD. Java",
            "A. HyperText Transfer Protocol\nB. HighText Transfer Protocol\nC. HyperTerminal Tracking Program\nD. HyperText Technical Protocol"
        };

        int[] correctAnswers = { 1, 2, 2, 1, 2, 2, 0 };
        List<string> potions = new() { "Health", "Elixir" };
        int playerHealth = 100;

        for (int i = 0; i < questions.Length; i++)
        {
            playerHealth = AskQuestion(i, questions, answers, correctAnswers, playerHealth, potions);

            if (playerHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou died. The Master Quiz-Knower takes your soul.");
                Console.ResetColor();
                return;
            }
        }

        Console.WriteLine($"\nYou survived with {playerHealth} HP. The door creaks open...");
    }

    static int AskQuestion(int index, string[] questions, string[] answers, int[] correctAnswers, int health, List<string> potions)
    {
        Console.WriteLine($"\nQuestion {index + 1}:");
        Console.WriteLine(questions[index]);
        Console.WriteLine(answers[index]);

        string input = "";
        while (true)
        {
            Console.Write("Enter A, B, C, D or P (to use potion): ");
            input = Console.ReadLine()!.Trim().ToUpper();

            if (input == "P")
            {
                UsePotion(ref health, potions);
                Console.WriteLine($"Health: {health}");
                continue;
            }

            if (input is "A" or "B" or "C" or "D")
                break;

            Console.WriteLine("Invalid input.");
        }

        int selectedIndex = input switch
        {
            "A" => 0,
            "B" => 1,
            "C" => 2,
            "D" => 3,
            _ => -1
        };

        if (selectedIndex == correctAnswers[index])
        {
            Console.WriteLine("Correct! +10 HP");
            health += 10;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong. -20 HP");
            Console.ResetColor();
            health -= 20;
        }

        Console.WriteLine($"Health: {health}");
        EnemyAttack(ref health);
        return health;
    }

    static void UsePotion(ref int health, List<string> potions)
    {
        if (potions.Count == 0)
        {
            Console.WriteLine("No potions left.");
            return;
        }

        Console.WriteLine("Available potions: " + string.Join(", ", potions));
        Console.Write("Type potion name: ");
        string chosen = Console.ReadLine()!.Trim().ToLower();

        if (chosen == "health" && potions.Contains("Health"))
        {
            health += 50;
            potions.Remove("Health");
            Console.WriteLine("+50 HP");
        }
        else if (chosen == "elixir" && potions.Contains("Elixir"))
        {
            health += 30;
            potions.Remove("Elixir");
            Console.WriteLine("+30 HP");
        }
        else
        {
            Console.WriteLine("Invalid or already used.");
        }
    }

    static void EnemyAttack(ref int playerHealth)
    {
        if (rng.Next(100) < 15)
        {
            Console.WriteLine(">> The shadowy figure whispers... then vanishes...");
            Console.WriteLine(">> A mysterious creature stabs you in the chest .......");
            Thread.Sleep(500);

            Console.WriteLine("     ⚔"); Thread.Sleep(150);
            Console.WriteLine("\n     ⚔"); Thread.Sleep(150);
            Console.WriteLine("\n     ⚔"); Thread.Sleep(150);
            Console.WriteLine("\n     ⚔"); Thread.Sleep(150);
            Console.WriteLine("\n     ⚔"); Thread.Sleep(200);
            Console.WriteLine("\n     ⚔"); Thread.Sleep(200);
            Console.WriteLine("\n     ⚔"); Thread.Sleep(250);

            Console.WriteLine("\n     |");
            Console.WriteLine("    /|\\");
            Console.WriteLine("   /_|_\\");
            Thread.Sleep(200);
            Console.WriteLine("     |");

            Thread.Sleep(200);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   *SPLASH*");
            Thread.Sleep(300);
            Console.WriteLine("   _______");
            Console.WriteLine("  /       \\");
            Console.WriteLine(" /  BLOOD  \\");
            Console.WriteLine("/___________\\");
            Thread.Sleep(300);
            Console.WriteLine(">> The blade pierces flesh. Blood floods the floor. -50 HP");
            Console.ResetColor();

            playerHealth -= 50;
            Console.WriteLine($"Your health is now at {playerHealth}");
        }
    }

    static void EndGame()
    {
        // ASCII Art
        Console.WriteLine("\n     QUIZ COMPLETE\n");
        Console.WriteLine("          __________           ");
        Console.WriteLine("         |  __  __  |          ");
        Console.WriteLine("         | |  ||  | |          ");
        Console.WriteLine("         | |  ||  | |          ");
        Console.WriteLine("         | |__||__| |          ");
        Console.WriteLine("         |  __  __()|          ");
        Console.WriteLine("         | |  ||  | |          ");
        Console.WriteLine("         | |  ||  | |     ??? ");
        Console.WriteLine("         | |__||__| |     /|\\ <- Shadowy Figure");
        Console.WriteLine("         |__________|     / \\ ");
        Console.WriteLine("                             ");
        Console.WriteLine("    The door creaks open. The figure watches silently...");
        Console.WriteLine("          ___________         ");
        Console.WriteLine("         |           |        ");
        Console.WriteLine("         |  _______  |        ");
        Console.WriteLine("         | |       | |        ");
        Console.WriteLine("         | |       | |        ");
        Console.WriteLine("         | |       | |        ");
        Console.WriteLine("         | |       | |        ");
        Console.WriteLine("         | |_______| |        ");
        Console.WriteLine("         |  _______  |        ");
        Console.WriteLine("         | /       \\ |     ???");
        Console.WriteLine("         |/         \\|     /|\\  <- Shadowy Figure");
        Console.WriteLine("                      \\     / \\ ");
        Console.WriteLine("\nThe door creaks open, just enough for you to slip through...");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(true);
    }
}
