using System;

namespace muphic.tag
{
	/// <summary>
	/// Wave_Riff の概要の説明です。
	/// </summary>
	public class Wave_Riff
	{
		public String aIdentity;				//これに"RIFF"が入ってないとおかしい
		public int bFileSize;					//これ以降のファイルサイズ(実際のサイズ-8)
		public String cRiffKind;				//RIFFの種類をあらわす識別子。"WAVE"じゃないとおかしい
		public Chunk[] chunk;

		public Wave_Riff()
		{
		}

	}
}
