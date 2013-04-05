using System;
using System.Collections;
using System.Drawing;

namespace muphic.Link.Dialog.Answer
{
	/// <summary>
	/// ListenSelect ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class AnswerCheck : Screen
	{
		public ArrayList AnimalList, QuestionList;
		public LinkScreen parent;
		public bool isPlay;
		public bool isEnd;
		public int PlayOffset;
		public bool flag;//test
		public bool[] answerFlag;

		public AnswerCheck(LinkScreen dia)
		{
			parent = dia;

			isPlay = false;
			isEnd = false;
		}

//		public void Play()
//		{
//			this.State = parent.QuestionNum-1;
//			QuestionList = ArrayList.Adapter(parent.quest.Question[this.State]);
//			AnimalList = ArrayList.Adapter(parent.score.AnimalList);
//			answerFlag = new bool[QuestionList.Count];
//			for (int i = 0; i < QuestionList.Count; i++)
//			{
//				answerFlag[i] = false;
//			}
//			flag = Check();
////			isPlay = true;
////			parent.score.isPlay = true;
//		}

		public bool Check()
		{

			this.State = parent.QuestionNum-1;
			QuestionList = parent.quest.AnimalList;//ArrayList.Adapter(parent.quest.Question[this.State]);
			AnimalList = ArrayList.Adapter(parent.score.AnimalList);
			answerFlag = new bool[QuestionList.Count];
			for (int i = 0; i < QuestionList.Count; i++)
			{
				answerFlag[i] = false;
			}
			//flag = Check();

			for (int i = 0; i < QuestionList.Count; i++)
			{
				Animal Q = (Animal)QuestionList[i];
				for (int j = 0; j < AnimalList.Count; j++)
				{
					Animal A = (Animal)AnimalList[j];

					if (Q.place == A.place && Q.code == A.code)
					{
						answerFlag[i] = true;
						break;
					}
				}
			}
			bool ret = true;
			for (int i = 0; i < QuestionList.Count; i++)
			{
				if (!answerFlag[i]) ret = false;
			}

			if (ret && AnimalList.Count == QuestionList.Count)
			{
				return flag = true;
			}
			else
			{
				return flag = false;
			}
		}
	}
}