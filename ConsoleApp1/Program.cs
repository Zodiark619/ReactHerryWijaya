var random=new Random();
var dice=new Dice(random);
var play=new Game(dice);
var result=play.Play();
Game.PrintResult(result);
enum GameResult
{
    Win,Loss
}
class Game
{
    private readonly Dice _dice; 

   

    private const int Tries = 3;
    public Game(Dice dice )
    {
        _dice = dice; 
    }
    public GameResult Play()
    {
        var result = _dice.Roll();
        var tries=Tries;
        while (tries > 0)
        {
            var guess = Print.ReadInteger("enter number:");
            if(guess == result)
            { 
                return GameResult.Win;
            }
            tries--;
            Console.WriteLine("wrong");
        }
        return GameResult.Loss;
    }
    public static void PrintResult(GameResult result1)
    {
        string message = "";
        message=result1 == GameResult.Win ? "You Wing" : "you Lose";
        Console.WriteLine(result1);
    }

    
}
static class Print
{
    public static int ReadInteger(string v)
    {
        int result;
        do
        {
            Console.WriteLine(v);
        } while (!int.TryParse(Console.ReadLine(), out result));
        return result;
    }
}
class Dice
{
    private readonly Random _random;
    private readonly int _sidesCount=6;

    public Dice(Random random)
    {
        _random = random;
    }
    public int Roll()
    {
        return _random.Next(1, _sidesCount+1);
    }
    public string Describe()
    {
        return $"dice with {_sidesCount} sides";
    }
}