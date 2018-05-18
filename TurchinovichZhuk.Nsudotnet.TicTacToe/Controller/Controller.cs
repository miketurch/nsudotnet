using System;
using TurchinovichZhuk.Nsudotnet.TicTacToe.Model;

namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Controller
{
	class Controller
	{
		private readonly Model.Model _model;
		private readonly View.ConsoleView _view;

		private bool _xStep = true;

		private int _winner;
		private const int XWinner = 1;
		private const int OWinner = 2;

		private const int Ok = 0;
		private const int BusyCell = 1;
		private const int FullField = 2;
		private const int WinSmallField = 3;
		private const int WrongSmallField = 4;
		private const int WinGame = 5;
		private const int GameOver = 6;

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
			_winner = 0;
			while (_winner == 0)
			{
				int[] steps = _view.GetSteps();
				int result = MakeStep(steps[0], steps[1]);
				if (result == WrongSmallField)
				{
					_view.WrongFieldMessage();
					continue;
				}
				if (result == BusyCell)
				{
					_view.BusyCellMessage();
					continue;
				}
				if (result == FullField)
				{
					_view.WrongFieldMessage();
					continue;
				}
				
				if (result == WinSmallField || result == WinGame)
				{
					_view.SetLittleWinner(!_xStep, _littlePoint);// not _xStep cause its current player but we need previous there!!
				}
				_view.SetField(_model.GetField());
				_view.PrintField();
				if (result == GameOver)
				{
					_view.Lose();
					break;
				}
				if (result == WinGame)
				{
					_view.Win(_xStep);
					break;
				}
			}
			
		}
	
		public int MakeStep(int smallFieldNumber, int cellNumber)
		{
			if (!_lastStep.Equals(smallFieldNumber) && !_lastStep.Equals(-1) && !_model.BigField.SmallFields[_lastStep].Full)
			{
				// Если ходит в маленькое поле, в которое не должен ходить
				return WrongSmallField; 
			}

			if (_model.BigField.SmallFields[smallFieldNumber].Full)
			{
				// Если это маленькое поле полностью заполнено
				return FullField;
			}

			if (_model.BigField.SmallFields[smallFieldNumber].Cells[cellNumber].CellState != CellState.None)
			{
				// Эта клетка занята
				return BusyCell;
			}
			
			_lastStep = cellNumber;

			_model.BigField.SmallFields[smallFieldNumber].Cells[cellNumber].CellState = _xStep ? CellState.X : CellState.O;
			_model.BigField.SmallFields[smallFieldNumber].Full = _model.BigField.SmallFields[smallFieldNumber].IsFull();

			// Если в этом поле уже выиграли, то не надо проверять условия на победу
			if (_model.BigField.SmallFields[smallFieldNumber].Winner.Equals(0))
			{
				if (_model.BigField.SmallFields[smallFieldNumber].IsWin())
				{
					if (IsWin())
					{
						// Если выиграл всю игру. В поле _winner - победитель игры.
						_littlePoint = smallFieldNumber;
						_xStep = !_xStep;
						return WinGame;
					}

					// Если выиграл маленькое поле, по smallFieldNumber можешь узнать, какие клетки нужно закрасить. 
					// А по _xStep - чей ход был (для цвета).
					_littlePoint = smallFieldNumber;
					_xStep = !_xStep;
					return WinSmallField;
				}
			}

			if (_model.Full())
			{
				return GameOver;
			}

			// Ход прошел успешно. Нет ни побед, ни ошибок
			_xStep = !_xStep;
			return Ok;
		}

		private bool IsWin()
		{
			if (_model.BigField.SmallFields[0].Winner == OWinner && _model.BigField.SmallFields[1].Winner == OWinner && _model.BigField.SmallFields[2].Winner == OWinner
				|| _model.BigField.SmallFields[3].Winner == OWinner && _model.BigField.SmallFields[4].Winner == OWinner && _model.BigField.SmallFields[5].Winner == OWinner
				|| _model.BigField.SmallFields[6].Winner == OWinner && _model.BigField.SmallFields[7].Winner == OWinner && _model.BigField.SmallFields[8].Winner == OWinner
				|| _model.BigField.SmallFields[0].Winner == OWinner && _model.BigField.SmallFields[3].Winner == OWinner && _model.BigField.SmallFields[6].Winner == OWinner
				|| _model.BigField.SmallFields[1].Winner == OWinner && _model.BigField.SmallFields[4].Winner == OWinner && _model.BigField.SmallFields[7].Winner == OWinner
				|| _model.BigField.SmallFields[2].Winner == OWinner && _model.BigField.SmallFields[5].Winner == OWinner && _model.BigField.SmallFields[8].Winner == OWinner
				|| _model.BigField.SmallFields[0].Winner == OWinner && _model.BigField.SmallFields[4].Winner == OWinner && _model.BigField.SmallFields[8].Winner == OWinner
				|| _model.BigField.SmallFields[2].Winner == OWinner && _model.BigField.SmallFields[4].Winner == OWinner && _model.BigField.SmallFields[6].Winner == OWinner)
			{
				_winner = OWinner;
				return true;
			}

			if (_model.BigField.SmallFields[0].Winner == XWinner && _model.BigField.SmallFields[1].Winner == XWinner && _model.BigField.SmallFields[2].Winner == XWinner
				|| _model.BigField.SmallFields[3].Winner == XWinner && _model.BigField.SmallFields[4].Winner == XWinner && _model.BigField.SmallFields[5].Winner == XWinner
				|| _model.BigField.SmallFields[6].Winner == XWinner && _model.BigField.SmallFields[7].Winner == XWinner && _model.BigField.SmallFields[8].Winner == XWinner
				|| _model.BigField.SmallFields[0].Winner == XWinner && _model.BigField.SmallFields[3].Winner == XWinner && _model.BigField.SmallFields[6].Winner == XWinner
				|| _model.BigField.SmallFields[1].Winner == XWinner && _model.BigField.SmallFields[4].Winner == XWinner && _model.BigField.SmallFields[7].Winner == XWinner
				|| _model.BigField.SmallFields[2].Winner == XWinner && _model.BigField.SmallFields[5].Winner == XWinner && _model.BigField.SmallFields[8].Winner == XWinner
				|| _model.BigField.SmallFields[0].Winner == XWinner && _model.BigField.SmallFields[4].Winner == XWinner && _model.BigField.SmallFields[8].Winner == XWinner
				|| _model.BigField.SmallFields[2].Winner == XWinner && _model.BigField.SmallFields[4].Winner == XWinner && _model.BigField.SmallFields[6].Winner == XWinner)
			{
				_winner = XWinner;
				return true;
			}

			return false;
		}

	}
}
