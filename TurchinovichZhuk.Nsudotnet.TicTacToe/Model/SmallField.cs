using System;
using TurchinovichZhuk.Nsudotnet.TicTacToe.Controller;


namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Model
{
	class SmallField
	{
		private Cell[] _cells;
		public bool Full { get; set; }

		private Winner _winner = Winner.None;

		public Cell[] Cells
		{
			get { return _cells; }
			set { _cells = value; }
		}

		public Winner Winner
		{
			get { return _winner; }
			set { _winner = value; }
		}

		public SmallField(Cell[] cells)
		{
			_cells = cells;
		}

		public bool IsWin(int cellNumber, bool xStep)
		{
			short check = 0;
			CellState state = (xStep) ? CellState.X : CellState.O;
			foreach (var cell in _cells)
			{
				check = (short)(check << 1);
				if (cell.CellState == state)
				{
					check |= 1;
				}
			}

			for (int i = 0; i < Wins.CheckNumbers[cellNumber].Length; i++)
			{
				if ((check & Wins.CheckNumbers[cellNumber][i]) == Wins.CheckNumbers[cellNumber][i])
				{
					_winner = (xStep) ? Winner.XWinner : Winner.OWinner;
					return true;
				}
			}

			return false;
		}

		public bool IsFull()
		{
			foreach (var cell in _cells)
			{
				if (cell.CellState == CellState.None)
				{
					return false;
				}
			}
			return true;
		}
	}
}
