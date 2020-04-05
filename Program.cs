using System;

namespace Spaceman
{
  /// <summary>The Spaceman program.</summary>
  public static class Program
  {
    /// <summary>Start the program.</summary>
    public static void Main()
    {
      var game = new Game();
      game.Greet();

      while (!(game.DidWin() || game.DidLose()))
      {
        game.Display();
        game.Ask();
      }

      Console.WriteLine($"You {(game.DidWin() ? "win" : "lose")}! Word was: {game.CodeWord}");
    }
  }
}
