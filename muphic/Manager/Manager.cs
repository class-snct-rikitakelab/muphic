using System;

namespace Muphic.Manager
{
	/// <summary>
	/// 抽象管理クラス。各管理クラスはこのクラスを継承して作成する。
	/// </summary>
	public abstract class Manager
	{
		/// <summary>
		/// 初期化済みであることを示す値を取得または設定する。
		/// </summary>
		protected bool _IsInitialized { get; set; }

		/// <summary>
		/// 管理クラスの新しいインスタンスを初期化する。
		/// </summary>
		protected Manager()
		{
			this._IsInitialized = false;
		}


		/// <summary>
		/// 安全なリソース破棄を行う。
		/// </summary>
		/// <param name="resource">破棄するリソース。</param>
		protected void _SafeDispose(IDisposable resource)
		{
			if (resource != null)
			{
				resource.Dispose();
			}
		}

	}
}
