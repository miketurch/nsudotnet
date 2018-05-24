using System;
using TurchinovichZhuk.Nsudotnet.TicTacToe.Model;

namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Controller
{
	class Controller
	{
		private readonly Model.Model _model;
		private readonly View.ConsoleView _view;

		private bool _xStep = true;

		private Winner _winner = Winner.None;

		private int _lastStep = -1;
		private int _littlePoint = -1;
		public Controller(Model.Model model, View.ConsoleView view)
		{
			_model = model;
			_view = view;
		}

		public void StartGame()
		{
			_view.SetField(_model.GetField());
			_view.ShowRules();
			_view.PrintField();
			_winner = Winner.None;
			while (_winner == Winner.None)
			{
				int[] steps = _view.GetSteps();
				GameState result = MakeStep(steps[0], steps[1]);
				if (result == GameState.WrongSmallField)
				{
					_view.WrongFieldMessage();
					continue;
				}
				if (result == GameState.BusyCell)
				{
					_view.BusyCellMessage();
					continue;
				}
				if (result == GameState.FullField)
				{
					_view.WrongFieldMessage();
					continue;
				}
				
				if (result == GameState.WinSmallField || result == GameState.WinGame)
				{
					_view.SetLittleWinner(!_xStep, _littlePoint);// not _xStep cause its current player but we need previous there!!
				}
				_view.SetField(_model.GetField());
				_view.PrintField();
				if (result == GameState.GameOver)
				{
					_view.Lose();
					break;
				}
				if (result == GameState.WinGame)
				{
					_view.Win(_xStep);
					break;
				}
			}
			
		}
	
		public GameState MakeStep(int smallFieldNumber, int cellNumber)
		{
			if (!_lastStep.Equals(smallFieldNumber) && !_lastStep.Equals(-1) && !_model.BigField.SmallFields[_lastStep].Full)
			{
				// Если ходит в маленькое поле, в которое не должен ходить
				return GameState.WrongSmallField; 
			}

			if (_model.BigField.SmallFields[smallFieldNumber].Full)
			{
				// Если это маленькое поле полностью заполнено
				return GameState.FullField;
			}

			if (_model.BigField.SmallFields[smallFieldNumber].Cells[cellNumber].CellState != CellState.None)
			{
				// Эта клетка занята
				return GameState.BusyCell;
			}
			
			_lastStep = cellNumber;

			_model.BigField.SmallFields[smallFieldNumber].Cells[cellNumber].CellState = _xStep ? CellState.X : CellState.O;
			_model.BigField.SmallFields[smallFieldNumber].Full = _model.BigField.SmallFields[smallFieldNumber].IsFull();

			// Если в этом поле уже выиграли, то не надо проверять условия на победу
			if (_model.BigField.SmallFields[smallFieldNumber].Winner.Equals(Winner.None))
			{
				if (_model.BigField.SmallFields[smallFieldNumber].IsWin(cellNumber, _xStep))
				{
					if (IsWin(smallFieldNumber))
					{
						// Если выиграл всю игру. В поле _winner - победитель игры.
						_littlePoint = smallFieldNumber;
						_xStep = !_xStep;
						return GameState.WinGame;
					}

					// Если выиграл маленькое поле, по smallFieldNumber можешь узнать, какие клетки нужно закрасить. 
					// А по _xStep - чей ход был (для цвета).
					_littlePoint = smallFieldNumber;
					_xStep = !_xStep;
					return GameState.WinSmallField;
				}
			}

			if (_model.Full())
			{
				return GameState.GameOver;
			}

			// Ход прошел успешно. Нет ни побед, ни ошибок
			_xStep = !_xStep;
			return GameState.Ok;
		}

		private bool IsWin(int smallFieldNumber)
		{
			short check = 0;
			Winner winner = (_xStep) ? Winner.XWinner : Winner.OWinner;
			foreach (var smallField in _model.BigField.SmallFields)
			{
				check = (short)(check << 1);
				if (smallField.Winner == winner)
				{
					check |= 1;
				}
			}

			for (int i = 0; i < Wins.CheckNumbers[smallFieldNumber].Length; i++)
			{
				if ((check & Wins.CheckNumbers[smallFieldNumber][i]) == Wins.CheckNumbers[smallFieldNumber][i])
				{
					_winner = winner;
					return true;
				}
			}

			return false;
		}

	}
}
