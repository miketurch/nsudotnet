using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TurchinovichZhuk.Nsudotnet.TicTacToe
{
	class TicTacToe
	{
		static void Main(string[] args)
		{
			Model.Model model = new Model.Model();
			model.Init();
			View.ConsoleView view = new View.ConsoleView();
			
			Controller.Controller controller = new Controller.Controller(model, view);
			controller.StartGame();
		}
	}
}
