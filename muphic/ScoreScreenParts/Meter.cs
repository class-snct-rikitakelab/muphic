using System.Collections.Generic;
using System.Text;

namespace Muphic.ScoreScreenParts
{
	/// <summary>
	/// ある拍がタイを持つ要素であるかを示す識別子を指定する。
	/// </summary>
	public enum Tie
	{
		/// <summary>
		/// タイの始端。
		/// </summary>
		Start,

		/// <summary>
		/// タイの終端。
		/// </summary>
		End,

		/// <summary>
		/// タイでない。
		/// </summary>
		None,
	}

	/// <summary>
	/// 拍の長さを示す識別子を指定する。
	/// </summary>
	public enum MeterLength : int
	{
		/// <summary>
		/// 全音符 / 休符。
		/// </summary>
		L4_0 = 8,

		/// <summary>
		/// 付点二分音符 / 休符。
		/// </summary>
		L3_0 = 6,

		/// <summary>
		/// 二分音符 / 休符。
		/// </summary>
		L2_0 = 4,

		/// <summary>
		/// 付点四分音符 / 休符。
		/// </summary>
		L1_5 = 3,

		/// <summary>
		/// 四分音符 / 休符。
		/// </summary>
		L1_0 = 2,

		/// <summary>
		/// 八分音符 / 休符。
		/// </summary>
		L0_5 = 1,

		/// <summary>
		/// 長さ無し。
		/// </summary>
		None = 0,
	}


	/// <summary>
	/// 楽譜内の 1 拍の情報を保持する。
	/// </summary>
	public class Meter
	{
		/// <summary>
		/// この拍に含まれる音のリストを取得または設定する。
		/// </summary>
		private List<int> Codes { get; set; }

		/// <summary>
		/// この拍に含まれる音の音階を取得する。この拍に音が含まれていない場合、-1 が返される。
		/// </summary>
		/// <param name="index">含まれる音の index 番号。</param>
		/// <returns>指定した index 番号に対応する音の音階。</returns>
		public int this[int index]
		{
			get
			{
				if (this.IsRest) return -1;
				else return this.Codes[index];
			}
		}

		/// <summary>
		/// この拍に含まれる音の数を取得する。
		/// </summary>
		public int Count
		{
			get { return this.Codes.Count; }
		}

		/// <summary>
		/// foreach 文で利用可能なコレクションとして、この拍に含まれる音を取得する (イテレータ構文)。
		/// </summary>
		public IEnumerable<int> Enumerable
		{
			get { foreach (int code in this.Codes) yield return code; }
		}


		/// <summary>
		/// この拍がタイであるかどうかを示す値を取得または設定する。
		/// </summary>
		public Tie Tie { get; set; }

		/// <summary>
		/// この拍に含まれる音の長さを取得または設定する。
		/// </summary>
		public MeterLength Length { get; set; }

		/// <summary>
		/// この拍が休符であることを示す値を取得する。
		/// </summary>
		public bool IsRest
		{
			get { return (this.Codes.Count == 0); }
		}

		/// <summary>
		/// 1 拍の情報を保持する Meter クラスの新しいインスタンスを初期化する。
		/// </summary>
		public Meter()
		{
			this.Codes = new List<int>();
			this.Length = MeterLength.None;
			this.Tie = Tie.None;
		}


		/// <summary>
		/// この拍に指定された音階の音を追加する。
		/// </summary>
		/// <param name="code">追加する音の音階。</param>
		/// <returns>正常に追加された場合は true、追加に失敗した場合は false。</returns>
		public bool AddCode(int code)
		{
			// この拍に同じ音程の音が追加済みであった場合か、和音の制限を超える場合、追加できない。
			// 作曲した時点でこのような状況は回避されている筈なので、普通はここで false が返るようなことは無い
			if (this.Codes.Contains(code) || this.Codes.Count >= Manager.ConfigurationManager.Locked.HarmonyNum) return false;

			// 音階が初めて追加されると、音の長さの初期値が設定される (八分音符)
			if (this.Length == MeterLength.None) this.Length = MeterLength.L0_5;

			// 音階を追加し、ソート
			this.Codes.Add(code);
			this.Codes.Sort();

			return true;
		}


		/// <summary>
		/// 拍の現在の状態を示す文字列を返す。
		/// </summary>
		/// <returns>拍の現在の状態を示す文字列。</returns>
		public override string ToString()
		{
			if (this.Length == MeterLength.None)
			{
				return "Meter [Empty]";
			}

			var codeMsg = new StringBuilder();		// 拍表示メッセージ
			int count = 0;							// 拍の音階表示用変数

			codeMsg.Append("Meter [");

			if (this.IsRest)
			{
				// 休符ならメッセージ文字列に Rest 表示
				codeMsg.Append("Rest");
			}
			else
			{
				// 休符でなければ拍の全ての音階をメッセージ文字列に追加
				codeMsg.Append("Code");

				for (; count < this.Codes.Count; count++)
				{
					codeMsg.Append("--" + this.Codes[count].ToString());
				}
			}

			// 許される和音の数になるよう空白で埋める (一覧表示時の可読性向上)
			for (; count < Manager.ConfigurationManager.Locked.HarmonyNum; count++)
			{
				codeMsg.Append("---");
			}

			// 拍の長さをメッセージ文字列に追加
			codeMsg.Append("  Length--" + ((int)this.Length / 2.0).ToString("0.0"));

			if (this.Tie == Tie.Start) codeMsg.Append(" TieStart");
			else if (this.Tie == Tie.End) codeMsg.Append(" TieEnd  ");

			codeMsg.Append("]");

			return codeMsg.ToString();
		}

	}
}
