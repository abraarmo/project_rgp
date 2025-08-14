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
        Console.WriteLine("------------------------------------");
        Console.WriteLine("LEVEL 2 – SHADOW REALM: MAZE ESCAPE");
        Console.WriteLine("------------------------------------");
        Console.ReadKey(true);
        Console.WriteLine("Find the key, unlock the exit, and escape the maze.");
        Console.ReadKey(true);
        Console.Clear();
        Console.WriteLine("Move: N, S, E, W (one step at a time).");
        Console.WriteLine("You can move using a combination, e.g., WWWEE");
        Console.WriteLine("This moves you three spaces west and then two spaces east.");
        Console.ReadKey(true);
        Console.Clear();
        Console.WriteLine("The grid is 5x5 and the map will be hidden, so you may want to draw it out.");
        Console.ReadKey(true);
        Console.Clear();
        Console.WriteLine("Rooms: \n Exit (needs key) \n Key (collect once) \n Monster (fight) \n Trap (damage) \n Empty (nothing).");
        Console.WriteLine("Win: Exit with key.  Lose: Health reaches 0.");
        Console.WriteLine("Press any key to continue...");
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
                Console.WriteLine("Write 'attack' to damage the monster. Per hit is 25hp.");
                Console.WriteLine("Take more than 3 seconds or type anything else and it will damage you by 30hp.");

                while (MonsterHealth > 0 && player.Health > 0)
                {
                    var startTime = DateTime.Now;
                    string attack1 = Console.ReadLine()!.ToLower();
                    var elapsed = (DateTime.Now - startTime).TotalSeconds;

                    if (elapsed > 3)
                    {
                        player.Health -= 30;
                        Console.WriteLine($"Too slow! Monster hits you! Your HP: {player.Health}");
                    }
                    else if (attack1 == "attack")
                    {
                        MonsterHealth -= 25;
                        Console.WriteLine($"You hit the monster! Monster HP: {MonsterHealth}");
                    }
                    else
                    {
                        player.Health -= 30;
                        Console.WriteLine($"Miss! Monster hits you! Your HP: {player.Health}");
                    }
                }

                if (player.Health <= 0) { Console.WriteLine("You died."); return; }

                if (MonsterHealth <= 0)
                {
                    Console.WriteLine("You defeated the monster!");
                    player.Health += 50;
                    Console.WriteLine($"Your HP: {player.Health}");
                    grid[player.Y, player.X] = "."; // clear room after win
                }
            }
            else if (position == "K")
            {
                inv.Add("Key");
                Console.WriteLine("You can now find the exit and use it to exit the maze");
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


