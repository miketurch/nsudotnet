namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Model
{
	class Model
	{
		private BigField _bigField;

		public BigField BigField
		{
			get { return _bigField; }
			set { _bigField = value; }
		}

		public void Init()
		{
			SmallField[] smallFields = new SmallField[9];
			for (int i = 0; i < 9; i++)
			{
				Cell[] cells = new Cell[9];
				for (int j = 0; j < 9; j++)
				{
					cells[j] =  new Cell();
				}
				SmallField smallField = new SmallField(cells);
				smallFields[i] = smallField;
			}
			_bigField = new BigField(smallFields);
		}

		public CellState[] GetField()
		{
			CellState[] cellStates = new CellState[81];
			for (int i = 0; i < 81; i++)
			{
				cellStates[i] = _bigField.SmallFields[i / 9].Cells[i % 9].CellState;
			}
			return cellStates;
		}

		public bool Full()
		{
			for (int i = 0; i < 9; i++)
			{
				if (!_bigField.SmallFields[i].IsFull())
				{
					return false;
				}
			}

			return true;
		}

	}
}
