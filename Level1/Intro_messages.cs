public static class Intro
{
    public static void message()
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
}