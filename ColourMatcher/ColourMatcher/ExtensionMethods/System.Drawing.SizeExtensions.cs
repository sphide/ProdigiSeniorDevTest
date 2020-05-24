using System.Drawing;

namespace ColourMatcher.ExtensionMethods
{
	public static class System_Drawing_SizeExtensions
	{
		public static bool IsGreaterThan(this Size me, Size size)
		{
			int myArea = (me.Height * me.Width);
			int hisArea = (size.Height * size.Width);
			return myArea > hisArea;
		}
	}
}
