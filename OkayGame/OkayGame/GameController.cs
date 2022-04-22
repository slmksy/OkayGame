using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkayGame
{
    public class GameController
    {
		#region Constants
		
		#endregion

		#region Fields
		private int[] stonesIdArr;
		private Random random;
		private Dictionary<int, Stone[]> dictPlayerStones;
		#endregion

		#region Constructors
		public GameController() 
		{
			stonesIdArr = new int[106];
			dictPlayerStones = new Dictionary<int, Stone[]>();
			random = new Random();

			dictPlayerStones.Add(0, null);
			dictPlayerStones.Add(1, null);
			dictPlayerStones.Add(2, null);
			dictPlayerStones.Add(3, null);
		}
		#endregion

		#region Properties
		public Stone OkayStone  
		{
			get;
			private set;
		}
		#endregion

		#region Public Methods
		public void CreateNewGame() 
		{
			dictPlayerStones[0] = null;
			dictPlayerStones[1] = null;
			dictPlayerStones[2] = null;
			dictPlayerStones[3] = null;

			CreateMixData();
			CreateOkayStone();
			ShareStones();

		}

		public int CalculateFreeStones(int playerID) 
		{
			var stones = dictPlayerStones[playerID];

			return 0;
		}

		public void WriteStones(int playerID) 
		{
			var stones = dictPlayerStones[playerID];

			foreach(var stone in stones) 
			{
				Console.WriteLine(stone.ToString() + " | ");
			}
		}


		#endregion

		#region Private Methods
		private void ShareStones() 
		{
			var chosenPlayerID = random.Next(0, 4);
			var lastIndex = GiveStone(chosenPlayerID, 15,0);

			for(int i = 0; i < 4; ++i) 
			{
                if (i == chosenPlayerID) 
				{
					continue;
				}

				lastIndex = GiveStone(i, 14, lastIndex);
			}
		}

		private int GiveStone(int playerID, int stoneCount, int startIndex) 
		{
			Stone[] playerStones = new Stone[stoneCount];
			var stoneIndex = 0;
			for (int i = startIndex; i < startIndex + stoneCount; ++i)
			{
				var stone = StoneModel.Instance.GetStone(stonesIdArr[i]);
				playerStones[stoneIndex++] = stone;
			}

			dictPlayerStones[playerID] = playerStones;

			return startIndex + stoneCount;
		}
		private void CreateMixData()
		{
			CreateNumbers(0);
			CreateNumbers(53);
		}

		private void CreateOkayStone()
		{
			var randVal = random.Next(0, 53);
			var stone = StoneModel.Instance.GetStone(randVal);
			var nextNumber = (stone.Number + 1) > 13 ? 1 : (stone.Number + 1);

			OkayStone = StoneModel.Instance.GetStone(nextNumber, stone.StoneColor);
		}
		private void CreateNumbers(int startIndex) 
		{
			List<int> tempList = new List<int>();
			for (int i = 0; i < 53; ++i)
			{
				tempList.Add(i);
			}

			for (int i = startIndex; i < startIndex + 53; ++i)
			{
				int newNumber = random.Next(0, tempList.Count);
				stonesIdArr[i] = tempList[newNumber];
				tempList.RemoveAt(newNumber);
			}
		}
		#endregion

		#region Protected Methods

		#endregion

		#region Events

		#endregion
	}
}
