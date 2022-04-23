using System;
using System.Collections.Generic;
using System.Linq;

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
		private StoneComparater stoneComparater = new StoneComparater();
		private List<Stone> remainingList = new List<Stone>();
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
		public bool HaveOkayStone(List<Stone> stones, List<Stone> willRemoved)
		{
			var haveItems = stones.FindAll(t => !t.IsFakeOkay &&
			t.Number == OkayStone.Number && t.StoneColor == OkayStone.StoneColor);

			var removedItems = willRemoved.FindAll(t => !t.IsFakeOkay &&
			t.Number == OkayStone.Number && t.StoneColor == OkayStone.StoneColor);

			return (haveItems.Count != willRemoved.Count);
		}

		public void CreateNewGame() 
		{
			dictPlayerStones[0] = null;
			dictPlayerStones[1] = null;
			dictPlayerStones[2] = null;
			dictPlayerStones[3] = null;

			CreateMixData();
			CreateOkayStone();
			ShareStones();

			Console.WriteLine("OKAY STONE : " + OkayStone.ToString()+ "\n");
		}

		public IEnumerable<int> GetWinnerPlayer() 
		{
			Dictionary<int, int> dictRemaingCount = new Dictionary<int, int>();
			dictRemaingCount.Add(0, CalculateFreeStones(0));
			dictRemaingCount.Add(1, CalculateFreeStones(1));
			dictRemaingCount.Add(2, CalculateFreeStones(2));
			dictRemaingCount.Add(3, CalculateFreeStones(3));

			var minCount = dictRemaingCount.Min(t=>t.Value);
			return dictRemaingCount.ToList().FindAll(t => t.Value == minCount).Select(t=>t.Key);
		}

		public int CalculateFreeStones(int playerID) 
		{
			var stones = dictPlayerStones[playerID];
			var stonesList = stones.ToList();
			stonesList = stonesList.OrderBy(t => t.Number).ToList();
			stonesList.Sort(stoneComparater);
			remainingList = new List<Stone>(stonesList);

			Console.WriteLine("\n\n----SORTED stones----- playerID :" + playerID);
			foreach (var t in stonesList) 
			{
				Console.WriteLine(t.ToString());
			}

			RemoveSameColorQNumbers(stonesList, playerID, 4);
			RemoveSameNumbers();
			remainingList.Sort(stoneComparater);
			RemoveSameColorQNumbers(remainingList, playerID, 3);
			RemoveOkayStone();
			WriteRemaining(playerID);

			return remainingList.Count;
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
		private void RemoveSameColorQNumbers(List<Stone> stonesList, int playerID, int qCount)
		{
			List<Stone> willRemoved = new List<Stone>();
			for (int i = 0; i < stonesList.Count - 1; ++i)
			{
				List<Stone> tempList = new List<Stone>();

				var current = stonesList[i];
				var next = stonesList[i + 1];
				int serialCount = 1;
				bool haveOkay = HaveOkayStone(stonesList, willRemoved);

				if (current.StoneColor == next.StoneColor)
				{
					if (current.Number + 1 == next.Number)
					{
						tempList.Add(current);
						tempList.Add(next);
						serialCount += 1;

						for (int k = i + 1; k < stonesList.Count - 1; ++k)
						{
							current = stonesList[k];
							next = stonesList[k + 1];
							if (
								next.Number == stonesList[k - 1].Number + 2)
							{
								tempList.Add(next);
								serialCount += 1;
								i += 1;
								continue;
							}

							if (current.StoneColor == next.StoneColor &&
								current.Number + 1 == next.Number)
							{
								tempList.Add(next);
								serialCount += 1;
								i += 1;
							}
							else
							{
								break;
							}
						}
						if (tempList.Count >= qCount)
						{
							willRemoved.AddRange(tempList);
						}
					}
				}
			}

			foreach (var t in willRemoved)
			{
				remainingList.Remove(t);
			}

			remainingList = remainingList.OrderBy(t => t.Number).ToList();
		}

		private void RemoveOkayStone()
		{
			var items = remainingList.FindAll(t => t.Number == OkayStone.Number &&
			t.StoneColor == OkayStone.StoneColor && !t.IsFakeOkay);

			foreach (var t in items)
			{
				remainingList.Remove(t);
			}
		}

		private void WriteRemaining(int playerID)
		{
			remainingList.Sort(stoneComparater);
			Console.WriteLine("\n\n --- Remaining Stones --- PlayerID : " + playerID);

			foreach (var t in remainingList)
			{
				Console.WriteLine(t.ToString());
			}
		}

		private void RemoveSameNumbers()
		{
			List<Stone> willRemoved = new List<Stone>();
			for (int i = 0; i < remainingList.Count - 1; ++i)
			{
				List<Stone> tempList = new List<Stone>();

				var current = remainingList[i];
				var next = remainingList[i + 1];
				int serialCount = 1;

				if (current.Number == next.Number)
				{
					if (current.StoneColor != next.StoneColor)
					{
						tempList.Add(current);
						tempList.Add(next);
						serialCount = 1;

						for (int k = i + 1; k < remainingList.Count - 1; ++k)
						{
							current = remainingList[k];
							next = remainingList[k + 1];

							if (current.StoneColor == next.StoneColor &&
								current.Number + 1 == next.Number)
							{
								tempList.Add(next);
								serialCount += 1;
								i += 1;
							}
							else
							{
								break;
							}
						}
						if (tempList.Count >= 3)
						{
							willRemoved.AddRange(tempList);
						}
					}
				}
			}
			foreach (var t in willRemoved)
			{
				remainingList.Remove(t);
			}
		}

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
			stonesIdArr = ShuffleArray(stonesIdArr);
		}

		private int[] ShuffleArray(int[] array)
		{
			Random r = new Random();
			for (int i = array.Length; i > 0; i--)
			{
				int j = r.Next(i);
				int k = array[j];
				array[j] = array[i - 1];
				array[i - 1] = k;
			}
			return array;
		}

		private void CreateOkayStone()
		{
			var randVal = random.Next(0, 53);
			var stone = StoneModel.Instance.GetStone(randVal);
			var nextNumber = (stone.Number + 1) > 13 ? 1 : (stone.Number + 1);

			OkayStone = StoneModel.Instance.GetStone(nextNumber, stone.StoneColor);
			StoneModel.Instance.SetFakeOkayStone(OkayStone);
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
