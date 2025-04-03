// Chandler Wixom, 4/2/2025, lab 9 maze part 2

// Introduction
using System.ComponentModel;
using System.Drawing;
using System.Net.Http.Headers;
using System.Xml.Serialization;

Console.BackgroundColor = ConsoleColor.DarkGray;
Console.ForegroundColor = ConsoleColor.Gray;
Console.Clear();

Console.WriteLine(" Hello, This is a maze game. Your job is to reach the (#) while avoiding the (%)\n\n Use \tw, a, s, d   or   Up, Down, Left, Right Arrow keys to move.\n\n Press Any key to start \n\tEsc to exit");
Console.ReadKey(true);

// Map
string[] mapRows = File.ReadAllLines("../../../maze.txt");
Console.Clear();
WriteMap(mapRows);
Console.ForegroundColor = ConsoleColor.DarkGreen;

// Active Running Loop
Console.SetCursorPosition(0,0);
(bool esc, bool moved) pass;
bool dead = false;
var badguyLocation = BadGuys();
var coins = CoinLocation(10);
var gems = GemLocation(9);
int score = 0;
char banned = '|';
bool WallGone = false;
Random random = new Random();
do

{
      if (score >= 1000 && !WallGone)
    {
        banned = '=';
        RemoveWall();
        WallGone = true;
    }
    
    pass = Movement(KeyRead());
    if (pass.moved)
    {
        

        if (LocationCheck())
        break;

        BadGuysMove();

         if (LocationCheck())
        break;
        WriteScore();
    }
    

}
while (!pass.esc || dead);









// Movement methods ---------------------------------------------------------------


//Reads the key and returning a string of the key ---------------------------
string KeyRead()
{
    ConsoleKey key = Console.ReadKey(true).Key;
    string move = Convert.ToString(key);

     if (move == "Escape")
    {
        return "Escape";
    }
    else if (move == "W" || move == "UpArrow")
    {
    
        return "up";
        
    }
    else if (move == "S" || move == "DownArrow")
    {
        return "down";
    }
    else if (move == "A" || move == "LeftArrow")
    {
        return "left";
    }
    else if (move == "D" || move == "RightArrow")
    {
    
        return "right";
    }
    else
    {
        return "";
    }

}

// Wall Check

bool WallCheck(int top, int left)
{
    char temp = mapRows[Console.CursorTop - top][Console.CursorLeft + left];
    if ( temp == '*' || temp == banned)
    {
        return true;
    }
    else 
    {
        return false;
    }
}



// movement check sees if player is allowed to move -------------------------------
bool CanMove(string move)
{
    if (move == "up")
    {
        if (Console.CursorTop < 1)
        {
            return false;
        }
        else 
        {
            if (WallCheck(1,0))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else if (move == "down")
    {
        if (Console.CursorTop >= mapRows.Length - 1)
        {
            return false;
        }
        else 
        {
             if (WallCheck(-1,0))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else if (move == "right")
    {
        if (Console.CursorLeft >= mapRows[1].Length - 1)
        {
            return false;
        }
        else 
        {
             if (WallCheck(0,1))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else if (move == "left")
    {
        if (Console.CursorLeft <= 0)
        {
            return false;
        }
        else 
        {
             if (WallCheck(0,-1))
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
    else
    {
        return false;
    }
}

// runs a movement check and if true then player moves ------------------------------------
(bool esc,bool moved) Movement(string move)
{
    if (move == "Escape")
    {
        return (true,false);
    }
    else if (move == "up")
    {
        if (CanMove(move))
        {
        Console.CursorTop--;
         return (false, true);
        }
        return (false, false);
        
    }
    else if (move == "down")
    {
        if (CanMove(move))
        {
        Console.CursorTop++;
         return (false, true);
        }
        return (false, false);

    }
    else if (move == "left")
    {
        if (CanMove(move))
        {
        Console.CursorLeft--;
            return (false, true);
        }
        return (false, false);

    }
    else if (move == "right")
    {
        if (CanMove(move))
        {
        Console.CursorLeft++;
            return (false, true);
        }
        return (false, false);

    }
    else
    {
        return (false,false);
    }
}


// Loop Writes Map --------------------------------------------------------------
void WriteMap(string[] map)
{
    foreach (var row in map)
    {
        Console.WriteLine(row);
    }
}


// locate badguys
// look for % and remembers theri location
(int x, int y)[] BadGuys()
{
    (int x, int y)[] badGuys = new (int x, int y)[2];
int count =0;
for (int i = 0; i < mapRows.Length; i++)
{
    for (int j = 0; j < mapRows[i].Length; j++)
    {
        if (mapRows[i][j] == '%')
        {
            badGuys[count] = (j,i);
                count++;
        }
    }
}
return badGuys;
}

// bad guys move
void BadGuysMove()
{
    (int, int) temp = Console.GetCursorPosition();
    Console.ForegroundColor = ConsoleColor.DarkRed;
for (int i = 0; i < badguyLocation.Length; i++)
{
    
    string move = RandomMovement(); // temp
    Console.SetCursorPosition(badguyLocation[i].x,badguyLocation[i].y);
    

    if (CanMove(move))
    {
    Console.Write(" ");

    Movement(move);
            Console.CursorLeft--;
            badguyLocation[i] = Console.GetCursorPosition();
            Console.Write("%");
            
    }
    
}
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.SetCursorPosition(temp.Item1,temp.Item2);
}


// random movement for the bad guys
string RandomMovement()
{
    int randind = random.Next(0,4);

    if (randind == 0)
    {
        return "up";
    }
    else if (randind == 1)
    {
        return "down";
    }
    else if (randind == 2) 
    {
        return "right";
    }
    else 
    {
        return "left";
    }
}


// checks if the player is on anything specific
bool LocationCheck()
{
    (int x, int y) player = Console.GetCursorPosition();
    if (player == badguyLocation[0] || player == badguyLocation[1])
    {
        Console.Clear();
        Console.WriteLine("YOU ARE DEAD");
        return true;
    }
    else if (mapRows[player.y][player.x] == '#')
    {
        WriteWin();
        return true;
    }
    else
    {
    for (int i = 0; i < coins.Length; i++)
    {
        if (player == coins[i])
        {
            Console.Write(" ");
            Console.CursorLeft--;
            coins[i] = (mapRows.Length, mapRows[1].Length);
            score = score + 100;
        }
    }
    

      
    for (int i = 0; i < gems.Length; i++)
    {
        if (player == gems[i])
        {
            Console.Write(" ");
            Console.CursorLeft--;
            gems[i] = (mapRows.Length, mapRows[1].Length);
            score = score + 500;
        }
    }
    return false;
    }
}


// locate coins
// look for ^ and remembers theri location
(int x, int y)[] CoinLocation(int coincount)
{
    (int x, int y)[] coins = new (int x, int y)[coincount];
    int count = 0;
    for (int i = 0; i < mapRows.Length; i++)
    {
        for (int j = 0; j < mapRows[i].Length; j++)
        {
            if (mapRows[i][j] == '^')
            {
                coins[count] = (j, i);
                count++;
            }
        }
    }
    return coins;
}

// locate gems
// look for $ and remembers theri location
(int x, int y)[] GemLocation(int coincount)
{
    (int x, int y)[] gems = new (int x, int y)[coincount];
    int count = 0;
    for (int i = 0; i < mapRows.Length; i++)
    {
        for (int j = 0; j < mapRows[i].Length; j++)
        {
            if (mapRows[i][j] == '$')
            {
                gems[count] = (j, i);
                count++;
            }
        }
    }
    return gems;
}

void WriteScore()
{
    (int, int) temp = Console.GetCursorPosition();
    Console.SetCursorPosition(15, mapRows.Length);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Score: {score}");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.SetCursorPosition(temp.Item1, temp.Item2);
}




// look for | and deletes them
void RemoveWall()
{
    (int x, int y) player = Console.GetCursorPosition();
    for (int i = 0; i < mapRows.Length; i++)
    {
        for (int j = 0; j < mapRows[i].Length; j++)
        {
            if (mapRows[i][j] == '|')
            {
                Console.SetCursorPosition(j,i);
                Console.Write("    ");
                Console.SetCursorPosition(player.x,player.y);
            }
        }
    }
}


void WriteWin()
{
    Console.BackgroundColor = ConsoleColor.Green;
    Console.ForegroundColor = ConsoleColor.White;
    Console.Clear();
    Console.SetCursorPosition(0,3);
    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    Console.WriteLine("!!          YOU WIN          !!");
    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
}