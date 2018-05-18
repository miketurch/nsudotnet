namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Model
{
	class Cell
	{
		private CellState _cellState = CellState.None;

		public CellState CellState
		{
			get { return _cellState; }
			set { _cellState = value; }
		}
	}
}
