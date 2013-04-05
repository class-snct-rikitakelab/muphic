using System;

namespace muphic.Link.Dialog.Select
{
	/// <summary>
	/// LinkFileData ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class LinkFileData
	{
		private String title;
		private int level;

		public int easy;
		public int normal;
		public int hard;

		public LinkFileData()
		{
			this.title = "";
			this.level = 0;
			this.hard = 0;
			this.normal = 0;
			this.easy = 0;
		}

		public String Title
		{
			get {return this.title;}
			set {this.title = value;}
		}

		public int Level
		{
			get {return this.level;}
			set
			{
				this.level = value;

				if (level <= 10) normal = 1;
				else if (level <= 20) normal = 2;
				else if (level <= 30) normal = 3;
				else if (level <= 40) normal = 4;
				else normal = 5;

				if (level+5 <= 10) hard = 1;
				else if (level+5 <= 20) hard = 2;
				else if (level+5 <= 30) hard = 3;
				else if (level+5 <= 40) hard = 4;
				else hard = 5;

				if (level-5 <= 10) easy = 1;
				else if (level-5 <= 20) easy = 2;
				else if (level-5 <= 30) easy = 3;
				else if (level-5 <= 40) easy = 4;
				else easy = 5;

				level = normal;
			}
		}

	}
}
