using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkayGame
{
    public  class StoneComparater : IComparer<Stone>
    {
		#region Constants

		#endregion

		#region Fields

		#endregion

		#region Constructors

		#endregion

		#region Properties

		#endregion

		#region Public Methods
		public int Compare(Stone x, Stone y)
		{
			if (x.StoneColor == y.StoneColor)
			{
				return (x.Number < y.Number) ? -1 : 1;
			}
			else
			{
				if (x.StoneColor == Color.Yellow)
				{
					return -1;
				}
				if (y.StoneColor == Color.Yellow)
				{
					return 1;
				}
				if (x.StoneColor == Color.Blue)
				{
					return -1;
				}
				if (y.StoneColor == Color.Blue)
				{
					return 1;
				}
				if (x.StoneColor == Color.Black)
				{
					return -1;
				}
				if (y.StoneColor == Color.Black)
				{
					return 1;
				}

				if (x.StoneColor == Color.Red)
				{
					return -1;
				}
				if (y.StoneColor == Color.Red)
				{
					return 1;
				}
			}

			return 1;
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
