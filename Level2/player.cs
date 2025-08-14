public class Player
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Health { get; set; }
    public int AttackPower { get; set; }

    public Player(int startX, int startY, int health, int attackPower)
    {
        X = startX;
        Y = startY;
        Health = health;
        AttackPower = attackPower;
    }

    public void Move(char direction, int maxX, int maxY)
    {
        switch (direction)
        {
            case 'N':
                if (Y > 0) Y -= 1;
                else Console.WriteLine("You can't move further north.");
                break;
            case 'S':
                if (Y < maxY - 1) Y += 1;
                else Console.WriteLine("You can't move further south.");
                break;
            case 'W':
                if (X > 0) X -= 1;
                else Console.WriteLine("You can't move further west.");
                break;
            case 'E':
                if (X < maxX - 1) X += 1;
                else Console.WriteLine("You can't move further east.");
                break;
            default:
                // ignore invalid directions
                break;
        }
    }
}
