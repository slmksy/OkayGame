using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkayGame
{
    public class Stone
    {
		#region Constants

		#endregion

		#region Fields

		#endregion

		#region Constructors
		public Stone() 
		{
		}
		#endregion

		#region Properties
		public Color StoneColor 
		{
			get;
			set;
		}
		public int Number
		{
			get;
			set;
		}
		#endregion

		#region Public Methods

		public override String ToString() 
		{
			return string.Concat(StoneColor.Name, " ", Number);
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
