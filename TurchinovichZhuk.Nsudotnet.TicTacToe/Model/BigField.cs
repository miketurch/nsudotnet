namespace TurchinovichZhuk.Nsudotnet.TicTacToe.Model
{
	class BigField
	{
		public SmallField[] SmallFields { get; set; }

		public BigField(SmallField[] smallFields)
		{
			SmallFields = smallFields;
		}
	}
}
