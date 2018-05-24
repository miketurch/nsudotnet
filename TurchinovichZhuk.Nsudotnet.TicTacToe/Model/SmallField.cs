﻿using System;
using TurchinovichZhuk.Nsudotnet.TicTacToe.Controller;


namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Model
{
	class SmallField
	{
		private Cell[] _cells;
		public bool Full { get; set; }

		private Winner _winner;

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

		public bool IsWin()
		{
			if (_cells[0].CellState == CellState.O && _cells[1].CellState == CellState.O && _cells[2].CellState == CellState.O 
			    ||_cells[3].CellState == CellState.O && _cells[4].CellState == CellState.O && _cells[5].CellState == CellState.O
				|| _cells[6].CellState == CellState.O && _cells[7].CellState == CellState.O && _cells[8].CellState == CellState.O
				|| _cells[0].CellState == CellState.O && _cells[3].CellState == CellState.O && _cells[6].CellState == CellState.O
				|| _cells[1].CellState == CellState.O && _cells[4].CellState == CellState.O && _cells[7].CellState == CellState.O
				|| _cells[2].CellState == CellState.O && _cells[5].CellState == CellState.O && _cells[8].CellState == CellState.O
				|| _cells[0].CellState == CellState.O && _cells[4].CellState == CellState.O && _cells[8].CellState == CellState.O
				|| _cells[2].CellState == CellState.O && _cells[4].CellState == CellState.O && _cells[6].CellState == CellState.O)
			{
				_winner = Winner.OWinner;
				return true;
			}

			if(_cells[0].CellState == CellState.X && _cells[1].CellState == CellState.X && _cells[2].CellState == CellState.X 
				|| _cells[3].CellState == CellState.X && _cells[4].CellState == CellState.X && _cells[5].CellState == CellState.X
				|| _cells[6].CellState == CellState.X && _cells[7].CellState == CellState.X && _cells[8].CellState == CellState.X
				|| _cells[0].CellState == CellState.X && _cells[3].CellState == CellState.X && _cells[6].CellState == CellState.X
				|| _cells[1].CellState == CellState.X && _cells[4].CellState == CellState.X && _cells[7].CellState == CellState.X
				|| _cells[2].CellState == CellState.X && _cells[5].CellState == CellState.X && _cells[8].CellState == CellState.X
				|| _cells[0].CellState == CellState.X && _cells[4].CellState == CellState.X && _cells[8].CellState == CellState.X
				|| _cells[2].CellState == CellState.X && _cells[4].CellState == CellState.X && _cells[6].CellState == CellState.X)
			{
				_winner = Winner.XWinner;
				return true;
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
