// ReSharper disable CA1303
namespace Spaceman
{
    using System;
    using System.Text;

    public class Game
    {
        private readonly string[] codewordOptions = { "Pippo", "Pluto", "Foo", "Bar" };
        private readonly int maxGuesses;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.CodeWord = this.codewordOptions[new Random().Next() % this.codewordOptions.Length];
            this.maxGuesses = 5;
            this.WrongGuesses = 0;

            var sb = new StringBuilder();
            foreach (var unused in this.CodeWord)
            {
                sb.Append("_");
            }

            this.CurrentWord = sb.ToString();
            this.Ufo = new Ufo();
        }

        public string CodeWord { get; }

        private string CurrentWord { get; set; }

        private int WrongGuesses { get; set; }

        private Ufo Ufo { get; }

        /// <summary>Welcome the user.</summary>
        public void Greet() => Console.WriteLine($"Welcome, player! You have {this.maxGuesses} guesses left.");

        /**
         * <summary>Check if the player won.</summary>
         * <returns>True if the player guessed the code word.</returns>
         */
        public bool DidWin() => this.CodeWord.Equals(this.CurrentWord, System.StringComparison.Ordinal);

        /**
         * <summary>Check if the player lost.</summary>
         * <returns>True if the player has done too much tries.</returns>
         */
        public bool DidLose() => this.WrongGuesses >= this.maxGuesses;

        /// <summary>Print all the necessary game information to the screen.</summary>
        public void Display()
        {
            Console.WriteLine(this.Ufo.Stringify());
            Console.WriteLine($"Current word is: {this.CurrentWord}");
            Console.WriteLine($"Number guesses remaining: {this.maxGuesses - this.WrongGuesses}");
        }

        /// <summary>Ask a letter to the user.</summary>
        public void Ask()
        {
            Console.WriteLine("Insert a letter:");
            var l = Console.ReadLine();
            if (string.IsNullOrEmpty(l) || l.Length != 1)
            {
                Console.Error.WriteLine("You should insert only a letter");
                return;
            }

            if (this.CodeWord.Contains(l, StringComparison.Ordinal))
            {
                var sb = new StringBuilder(this.CurrentWord);
                for (var i = 0; i < this.CodeWord.Length; i++)
                {
                    var c = this.CodeWord[i];
                    if ($"{c}".Equals(l, StringComparison.Ordinal))
                    {
                        sb.Remove(i, 1).Insert(i, c);
                    }

                    this.CurrentWord = sb.ToString();
                }
            }
            else
            {
                Console.WriteLine($"The word does not contain the letter {l}");
                this.WrongGuesses++;
                this.Ufo.AddPart();
            }
        }
    }
}