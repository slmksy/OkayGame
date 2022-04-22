using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkayGame
{
    public class StoneModel
    {
		#region Constants

		#endregion

		#region Fields
		private Dictionary<int, Stone> dictStone;
		private static StoneModel instance = null;
		#endregion

		#region Constructors
		private StoneModel()
		{
			dictStone = new Dictionary<int, Stone>();
		}
		#endregion

		#region Properties
		public static StoneModel Instance 
		{
            get 
			{
				if (instance == null)
				{
					instance = new StoneModel();
				}

				return instance;
			}			
		}
		#endregion

		#region Public Methods
		public Stone GetStone(int index) 
		{
			return dictStone[index];
		}

		public Stone GetStone(int number,  Color color)
		{
			return dictStone.First(t => t.Value.Number == number &&
				t.Value.StoneColor == color).Value;
		}
		private void CreateStones() 
		{
			int id = 0;
			for (int i = 1; i <= 13; ++i) 
			{
				dictStone.Add(id++, new Stone() { StoneColor = Color.Yellow, Number = i });
			}

			for (int i = 1; i <= 13; ++i) 
			{
				dictStone.Add(id++, new Stone() { StoneColor = Color.Blue, Number = i });
			}

			for (int i = 1; i <= 13; ++i)
			{
				dictStone.Add(id++, new Stone() { StoneColor = Color.Black, Number = i });
			}

			for (int i = 1; i <= 13; ++i)
			{
				dictStone.Add(id++, new Stone() { StoneColor = Color.Red, Number = i });
			}

			//sahte okay
			dictStone.Add(id++, new Stone() { StoneColor = Color.White, Number = 0 });

		}
		#endregion

		#region Private Methods

		#endregion

		#region Protected Methods

		#endregion

		#region Events

		#endregion
	}
}
