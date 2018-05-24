namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Controller
{
	class Wins
	{
		public const short FirstVertical = 448;
		public const short SecondVertical = 56;
		public const short ThirdVertical = 7;

		public const short FirstHorizontal = 292;
		public const short SecondHorizontal = 146;
		public const short ThirdHorizontal = 73;

		public const short Diagonal = 273;
		public const short AntiDiagonal = 84;

		public static short[][] CheckNumbers = 
		{
			new[] {FirstVertical, FirstHorizontal, Diagonal},
			new[] {FirstVertical, SecondHorizontal},
			new[] {FirstVertical, ThirdHorizontal, AntiDiagonal},
			new[] {FirstHorizontal, SecondVertical},
			new[] {SecondHorizontal, SecondHorizontal, Diagonal, AntiDiagonal},
			new[] {SecondVertical, ThirdHorizontal},
			new[] {FirstHorizontal, ThirdVertical, AntiDiagonal},
			new[] {SecondHorizontal, ThirdVertical},
			new[] {ThirdVertical, ThirdHorizontal, Diagonal},
		};

		
	}
}
