using System;
using System.Collections;
using System.Drawing;
using muphic;

namespace muphic.MakeStory
{
	public class Slide
	{
		public ArrayList ObjList;
		public Obj haikei;
		public ArrayList AnimalList;
		public int tempo = 3;
		public string Sentence;

		public Slide()
		{
			ObjList = new ArrayList();
			haikei = new Obj(0);
			AnimalList = new ArrayList();
			Sentence = null;
		}
	}
}