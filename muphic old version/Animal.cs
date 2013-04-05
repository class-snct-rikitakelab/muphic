using System;

namespace muphic
{
	/// <summary>
	/// Animal ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class Animal : Base
	{
		public String AnimalName;
		public int code, place;
		public int group;

		public Animal(int place, int code)
		{
			AnimalInit(place, code, "");
		}

		public Animal(int place, int code, muphic.One.OneButtonsClickMode mode)
		{
			AnimalInit(place, code, mode.ToString());
		}

		public Animal(int place, int code, int num, muphic.Link.LinkButtonsClickMode mode)
		{
			AnimalInit(place, code, num, mode.ToString());
		}

		public Animal(int place, int code, muphic.Story.StoryButtonsClickMode mode)
		{
			AnimalInit(place, code, mode.ToString());
		}

		public Animal(int place, int code, String mode)
		{
			AnimalInit(place, code, mode);
		}

		private void AnimalInit(int place, int code,  String AnimalName)
		{
			this.code = code;
			this.place = place;
			this.AnimalName = AnimalName;
		}

		private void AnimalInit(int place, int code, int group, String AnimalName)
		{
			this.code = code;
			this.place = place;
			this.group = group;
			this.AnimalName = AnimalName;
		}

		public override string ToString()
		{
			return this.AnimalName;									//óÕãZ
		}

	}
}
