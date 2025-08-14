public static class Level2
{
    public const int SizeX = 5;
    public const int SizeY = 5;
    public const int boxes = SizeX * SizeY;

    public static Random rng = new Random();

    public static int counte = 1;
    public static int countk = 1;
    public static int countm = 3;
    public static int countt = 3;
    public static int countEmpty = boxes - (counte + countk + countm + countt);
    public static List<string> inv = new List<string>();

    public static void RunGame()
    {
        Console.Clear();
        Console.WriteLine(@"
        ╔═══════════════════════════════════════════════════╗
        ║         LEVEL 2 – SHADOW REALM: MAZE ESCAPE       ║
        ╚═══════════════════════════════════════════════════╝
");
        Console.WriteLine("\nFind the key, reach the exit, survive the maze.");
        Console.WriteLine("Press any key to approach the gate...");
        Console.ReadKey(true);

        Console.Clear();
        Console.WriteLine(@"
         ███████████████████████████████████████
         ███████████████████████████████████████
         ███████████████████████████████████████
         ███████████████████████████████████████
         ███████████████████████████████████████
         ████████▀▀▀░░░░░░░░░░░░░░░▀▀▀██████████
         ███████▀░░░░░░░░░░░░░░░░░░░░░▀█████████
         ██████░░░░░░░░░░░░░░░░░░░░░░░░░████████
         ██████░░░░░░░░░░░░░░░░░░░░░░░░░████████
         ██████░░░░░░░░░░░░░░░░░░░░░░░░░████████
         ██████░░░░░░░░░░░░░░░░░░░░░░░░░████████
         ██████░░░░░░░░░░░░░░░░░░░░░░░░░████████
         ██████░░░░░░░░░░░░░░░░░░░░░░░░░████████
         ██████░░░░░░░░░░░░░░░░░░░░░░░░░████████
         ███████░░░░░░░░░░░░░░░░░░░░░░░█████████
         ████████▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄██████████
         ███████████████████████████████████████
         ███████████████████████████████████████
         ███████████████████████████████████████
         ███████████████████████████████████████
");
        Console.WriteLine("The gate looms before you...");
        Console.ReadKey(true);

        Console.Clear();
        Console.WriteLine("MOVEMENT:");
        Console.WriteLine(" N = North | S = South | E = East | W = West");
        Console.WriteLine(" Chain moves: e.g. 'NNEE' = 2 up, 2 right.");
        Console.WriteLine(" Stay within the 5x5 grid.");
        Console.ReadKey(true);

        Console.Clear();
        Console.WriteLine("ROOM TYPES:\n");
        Console.WriteLine(" ┌───┐");
        Console.WriteLine(" │ K │   = Key (collect before exit)");
        Console.WriteLine(" ├───┤");
        Console.WriteLine(" │ E │   = Exit (needs key)");
        Console.WriteLine(" ├───┤");
        Console.WriteLine(" │ M │   = Monster (fight quickly!)");
        Console.WriteLine(" ├───┤");
        Console.WriteLine(" │ T │   = Trap (-20 HP)");
        Console.WriteLine(" └───┘");
        Console.WriteLine("Empty = Nothing happens");
        Console.ReadKey(true);

        Console.Clear();
        Console.WriteLine("WIN: Get the key, then reach the exit.");
        Console.WriteLine("LOSE: HP drops to 0.");
        Console.WriteLine("\nPress any key to begin...");
        Console.ReadKey(true);

        Console.Clear();


        // build bag
        List<string> bag = new List<string>();
        bag.Add("E");
        bag.Add("K");
        for (int i = 0; i < countm; i++) bag.Add("M");
        for (int i = 0; i < countt; i++) bag.Add("T");
        for (int i = 0; i < countEmpty; i++) bag.Add(".");

        // shuffle bag
        for (int i = bag.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (bag[i], bag[j]) = (bag[j], bag[i]);
        }

        // load into 2D grid
        string[,] grid = new string[SizeY, SizeX];
        for (int index = 0; index < bag.Count; index++)
        {
            int row = index / SizeX;
            int col = index % SizeX;
            grid[row, col] = bag[index];
        }

        // Making sure the exit and key are not on the starting position
        if (grid[0, 0] == "E" || grid[0, 0] == "K")
        {
            List<int[]> empties = new List<int[]>();
            for (int r = 0; r < SizeY; r++)
            {
                for (int c = 0; c < SizeX; c++)
                {
                    if (grid[r, c] == ".")
                    {
                        empties.Add(new int[] { r, c });
                    }
                }
            }

            // Picking a random element from the empties list and replacing it wiht the E and K
            int[] target = empties[rng.Next(empties.Count)];
            int targetRow = target[0];
            int targetColumn = target[1];

            string temp = grid[targetRow, targetColumn];
            grid[targetRow, targetColumn] = grid[0, 0];
            grid[0, 0] = temp;
        }


        Player player = new Player(0, 0, 100, 25);
        while (true)
        {
            // HUD
            Console.WriteLine($"\nHP: {player.Health} | Key: {(inv.Contains("Key") ? "Yes" : "No")}");
            Console.Write("What is your move? ");
            string input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input. Please enter directions like N, S, E, W.");
                continue;
            }

            try
            {
                input = input.ToUpper();

                foreach (char move in input)
                {
                    if ("NSEW".Contains(move))
                    {
                        player.Move(move, SizeX, SizeY);
                    }
                    else
                    {
                        Console.WriteLine($"'{move}' is not a valid direction. Use N, S, E, or W.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                continue;
            }


            // Coding the Rooms that they go into
            // NOTE: grid[row, col] => [player.Y, player.X]
            string position = grid[player.Y, player.X];

            if (position == "T")
            {
                Console.WriteLine("You have allen into my trap");
                Console.WriteLine("You just lost 20hp");
                player.Health -= 20;
                Console.WriteLine($"Your health is now {player.Health}");
                grid[player.Y, player.X] = "."; // prevent retrigger
                if (player.Health <= 0) { Console.WriteLine("You died."); return; }
            }
            else if (position == "M")
            {
                int MonsterHealth = 100;
                Console.Clear();
                Console.WriteLine(@"
              ,     \    /      ,
             / \    )\__/(     / \
            /   \  (_\  /_)   /   \
      ____ /_____\__\@  @/___/_____\ ____
     |             |\../|              |
     |              \VV/               |
     |    MONSTER EMERGES FROM SHADOW  |
     |_________________________________|
             |    /\ /      \ /\    |
             |  /   V        V   \  |
             |/     `--------'     \|
             '----------------------'
    ");
                Console.WriteLine("Type 'attack' within 3 seconds to strike first!");
                Console.WriteLine("If you're too slow or type wrong, you take 30 damage.");
                Console.WriteLine($"Monster HP: {MonsterHealth} | Your HP: {player.Health}");
                Console.WriteLine("--------------------------------------------------");

                while (MonsterHealth > 0 && player.Health > 0)
                {
                    var startTime = DateTime.Now;
                    string attack1 = Console.ReadLine()!.ToLower();
                    var elapsed = (DateTime.Now - startTime).TotalSeconds;

                    if (elapsed > 3)
                    {
                        player.Health -= 30;
                        Console.WriteLine(@"
                💥 The monster strikes you! (-30 HP)
            ");
                    }
                    else if (attack1 == "attack")
                    {
                        MonsterHealth -= 25;
                        Console.WriteLine(@"
                ⚔ You slash the monster! (-25 HP)
            ");
                    }
                    else
                    {
                        player.Health -= 30;
                        Console.WriteLine(@"
                ❌ You missed! The monster claws you! (-30 HP)
            ");
                    }

                    Console.WriteLine($"Monster HP: {MonsterHealth} | Your HP: {player.Health}");
                    Console.WriteLine("--------------------------------------------------");
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine("💀 You died.");
                    return;
                }

                if (MonsterHealth <= 0)
                {
                    Console.WriteLine(@"
            🩸 The monster collapses!
            ❤️ You recover +50 HP
        ");
                    player.Health += 50;
                    Console.WriteLine($"Your HP: {player.Health}");
                    grid[player.Y, player.X] = ".";
                }
            }
            else if (position == "K")
            {
                inv.Add("Key");
                Console.Clear();
                Console.WriteLine(@"
        ______________
       /              \
      /    ________    \
     |    /        \    |
     |   |          |   |
      \   \   --   /   /
       \   \______/   /
        \            /
         \__________/
             ||
             ||
             ||
             ||
         ====||====
         ====||====
             ||
             ||
             ||
            ==== 
    ");
                Console.WriteLine("You found the KEY! The exit can now be unlocked...");
                grid[player.Y, player.X] = ".";
            }
            else if (position == "E")
            {
                if (inv.Contains("Key"))
                {
                    Console.WriteLine("Well Done you have exited the maze");
                    return; // end level
                }
                else
                {
                    Console.WriteLine("You do not have the key");
                    Console.WriteLine("First find the key and then come back here to exit");
                }
            }

            if (player.Health <= 0) { Console.WriteLine("You died."); return; }
        }
        }
    }


