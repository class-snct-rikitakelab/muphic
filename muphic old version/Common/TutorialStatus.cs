using System;
using System.Collections;

namespace muphic.Common
{
	/// <summary>
	/// TutorialStatus の概要の説明です。
	/// </summary>
	public class TutorialStatus
	{
		private static bool isTutorial;										// チュートリアル実行中かどうかのフラグ
		private static ArrayList TriggerPartsList = new ArrayList();		// チュートリアル実行時に次のステートに進むトリガーになるパーツのリスト
		private static ArrayList PermissionPartsList = new ArrayList();		// チュートリアル実行時にトリガーパーツ以外でクリックを許可するパーツのリスト
		private static bool NextStateFlag;									// 次のステートに進んでいいかどうかのフラグ
		private static bool NextStateStandBy;								// 何らかの動作を待って待機している最に使用する
		private static bool ClickControl;									// クリック操作を禁止する際に使用する
		private static bool isTutorialDialog;								// チュートリアル本体がダイアログを表示している場合に立てるフラグ
		private static bool isDrawString;									// チュートリアル管理側以外での文字列描画を行うかどうか
		private static string isSPMode;										// 自由操作を行う場合に使用
		private static int FrameCount;										// 何らかの遅延を発生させる際に使用する（廃止
		
		public TutorialStatus()
		{
			// （・ε・）
		}
		
		/// <summary>
		/// このクラスのprivate staticフィールドを全て初期化するメソッド
		/// </summary>
		public static void initTutorialStatus()
		{
			TutorialStatus.setDisableTutorial();			// チュートリアルフラグoff
			TutorialStatus.setDisableNextState();			// ステート進行フラグoff
			TutorialStatus.setDisableNextStateStandBy();	// 待機フラグoff
			TutorialStatus.setDisableTutorialDialog();		// ダイアログフラグoff
			TutorialStatus.setDisableClickControl();		// クリック許可
			TutorialStatus.initTriggerPartsList();			// トリガーリストクリア
			TutorialStatus.initPermissionPartsList();		// 許可リストクリア
			TutorialStatus.setDisableIsSPMode();			// 自由操作off
		}
		
		#region isTutorial に関するメソッド
		
		/// <summary>
		/// isTutorialを返す
		/// </summary>
		/// <returns>trueならTutorial実行中</returns>
		public static bool getIsTutorial()
		{
			return TutorialStatus.isTutorial;
		}
		
		/// <summary>
		/// isTutorialをtrueにするメソッド
		/// Tutorial開始時に実行すべし
		/// </summary>
		public static void setEnableTutorial()
		{
			TutorialStatus.isTutorial = true;
		}
		
		/// <summary>
		/// isTutorialをfalseにするメソッド
		/// プログラム開始時とTutorial終了時に実行すべし
		/// </summary>
		public static void setDisableTutorial()
		{
			TutorialStatus.isTutorial = false;
		}
		
		#endregion
		
		#region TriggerPartsList に関するメソッド
		
		/// <summary>
		/// 次のステートへのトリガーパーツリストをクリアする
		/// </summary>
		public static void initTriggerPartsList()
		{
			TutorialStatus.TriggerPartsList.Clear();
		}
		
		/// <summary>
		/// トリガーパーツリストを作成するメソッド
		/// </summary>
		/// <param name="data">トリガーパーツリスト</param>
		public static void setTriggerPartsList(string[] data)
		{
			// リストをクリア
			TutorialStatus.initTriggerPartsList();
			
			for(int i=0; i<data.Length; i++)
			{
				TutorialStatus.TriggerPartsList.Add(data[i]);
			}
		}
		
		/// <summary>
		/// トリガーパーツリストに追加するメソッド
		/// </summary>
		/// <param name="data"></param>
		public static void addTriggerPartsList(string data)
		{
			TutorialStatus.TriggerPartsList.Add(data);
		}
		
		/// <summary>
		/// 与えられた文字列がトリガーパーツリスト内にあるかをチェックするメソッド
		/// </summary>
		/// <param name="data">チェックする文字列</param>
		/// <returns>trueならトリガーパーツリスト内に含まれている</returns>
		public static bool checkTriggerPartsList(string data)
		{
			for(int i=0; i<TutorialStatus.TriggerPartsList.Count; i++)
			{
				if( data.Equals(TutorialStatus.TriggerPartsList[i].ToString()) ) return true;
			}
			return false;
		}
		
		/// <summary>
		/// 与えられた文字列をトリガーパーツリストから削除するメソッド
		/// </summary>
		/// <param name="data">削除する文字列</param>
		/// <returns>trueなら削除成功</returns>
		public static bool deleteTriggerParts(string data)
		{
			if(TutorialStatus.checkTriggerPartsList(data))
			{
				TutorialStatus.TriggerPartsList.Remove(data);
				return true;
			}
			else
			{
				return false;
			}
		}
		
		#endregion
		
		#region PermissionPartsList に関するメソッド
		
		/// <summary>
		/// チュートリアル実行中のクリック許可リストを初期化するメソッド
		/// </summary>
		public static void initPermissionPartsList()
		{
			TutorialStatus.PermissionPartsList.Clear();
		}
				
		/// <summary>
		/// クリック許可リストを作成するメソッド
		/// </summary>
		/// <param name="data">許可リスト</param>
		public static void setPermissionPartsList(string[] data)
		{
			// リストをクリア
			TutorialStatus.initPermissionPartsList();
			
			for(int i=0; i<data.Length; i++)
			{
				TutorialStatus.PermissionPartsList.Add(data[i]);
			}
		}
		
		/// <summary>
		/// クリック許可リストに追加するメソッド
		/// </summary>
		/// <param name="data">追加する文字列</param>
		public static void addPermissionPartsList(string data)
		{
			TutorialStatus.PermissionPartsList.Add(data);
		}
		
		/// <summary>
		/// 与えられた文字列がクリック許可リスト内にあるかをチェックするメソッド
		/// </summary>
		/// <param name="data">チェックする文字列</param>
		/// <returns>trueなら許可リスト内に含まれている</returns>
		public static bool checkPermissionPartsList(string data)
		{
			for(int i=0; i<TutorialStatus.PermissionPartsList.Count; i++)
			{
				string parts = (string)TutorialStatus.PermissionPartsList[i];
				if( parts == data ) return true;
			}
			return false;
		}
		
		/// <summary>
		/// 与えられた文字列を許可リストから削除するメソッド
		/// </summary>
		/// <param name="data">削除する文字列</param>
		/// <returns>trueなら削除成功</returns>
		public static bool deletePermissionParts(string data)
		{
			if(TutorialStatus.checkPermissionPartsList(data))
			{
				TutorialStatus.PermissionPartsList.Remove(data);
				return true;
			}
			else
			{
				return false;
			}
		}
		
		#endregion
		
		#region NextStateFlag に関するメソッド
		
		/// <summary>
		/// 次のステートに進んでいいかどうか問い合わせるメソッド
		/// </summary>
		/// <returns>trueなら次のステートに進んでよし</returns>
		public static bool getNextStateFlag()
		{
			return TutorialStatus.NextStateFlag;
		}
		
		/// <summary>
		/// 次のステートへ進んでいいかどうかのフラグ trueにセット
		/// </summary>
		public static void setEnableNextState()
		{
			TutorialStatus.NextStateFlag = true;
		}
		
		/// <summary>
		/// 次のステートへ進んでいいかどうかのフラグ falseにセット
		/// </summary>
		public static void setDisableNextState()
		{
			TutorialStatus.NextStateFlag = false;
		}
		
		#endregion
		
		#region NextStateStandBy に関するメソッド
		
		/// <summary>
		/// 待機フラグを立てるメソッド
		/// </summary>
		public static void setEnableNextStateStandBy()
		{
			TutorialStatus.NextStateStandBy = true;
		}
		
		/// <summary>
		/// 待機を解除するメソッド
		/// </summary>
		public static void setDisableNextStateStandBy()
		{
			TutorialStatus.NextStateStandBy = false;
		}
		
		/// <summary>
		/// 待機中かどうかを得るメソッド
		/// </summary>
		/// <returns></returns>
		public static bool getNextStateStandBy()
		{
			return TutorialStatus.NextStateStandBy;
		}
		
		/// <summary>
		/// 待機を終了させるメソッド
		/// </summary>
		public static void EndStandBy()
		{
			// チュートリアル実行中で、待機フラグが立っていてステート進行フラグが立っていなかった場合
			if( TutorialStatus.isTutorial && (TutorialStatus.NextStateStandBy && !TutorialStatus.NextStateFlag) )
			{
				TutorialStatus.setEnableNextState();	// ステート進行フラグを立てる
			}
		}
		
		#endregion
		
		#region ClickContorol に関するメソッド
		
		/// <summary>
		/// クリック禁止状態かどうか問い合わせるためのメソッド
		/// </summary>
		/// <returns>trueならクリック禁止</returns>
		public static bool getClickControl()
		{
			return TutorialStatus.ClickControl;
		}
		
		/// <summary>
		/// クリック禁止の状態にするメソッド
		/// </summary>
		public static void setEnableClickControl()
		{
			TutorialStatus.ClickControl = true;
		}
		
		/// <summary>
		/// クリック許可の状態にするメソッド
		/// </summary>
		public static void setDisableClickControl()
		{
			TutorialStatus.ClickControl = false;
		}
		
		#endregion
		
		#region isTutorialDialog に関するメソッド
		
		/// <summary>
		/// isTutorialDialogを返す
		/// つまるところチュートリアル管理側でダイアログを表示しているかどうか
		/// </summary>
		/// <returns>trueならダイアログ表示中 文字列さん達は自重してください</returns>
		public static bool getisTutorialDialog()
		{
			return TutorialStatus.isTutorialDialog;
		}
		
		/// <summary>
		/// isTutorialDialogをtrueにするメソッド
		/// チュートリアル管理側のダイアログ表示時に呼び出す
		/// </summary>
		public static void setEnableTutorialDialog()
		{
			TutorialStatus.isTutorialDialog = true;
		}
		
		/// <summary>
		/// isTutorialDialogをfalseにするメソッド
		/// チュートリアル管理側のダイアログを閉じるときに呼び出す
		/// </summary>
		public static void setDisableTutorialDialog()
		{
			TutorialStatus.isTutorialDialog = false;
		}
		
		#endregion
		
		#region isSPModeに関するメソッド
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static string getisSPMode()
		{
			return TutorialStatus.isSPMode;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static bool getIsSPModeBool()
		{
			if(TutorialStatus.isSPMode == "") return false;
			else return true;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static string getIsSPMode()
		{
			return TutorialStatus.isSPMode;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void setEnableIsSPMode(string str)
		{
			TutorialStatus.isSPMode = str;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void setDisableIsSPMode()
		{
			TutorialStatus.isSPMode = "";
		}
		
		#endregion
		
		#region DrawString に関するメソッド
		
		/// <summary>
		/// isDrawStringを返す
		/// </summary>
		/// <returns>trueなら文字列を描画してよし</returns>
		public static bool getisDrawString()
		{
			return TutorialStatus.isDrawString;
		}
		
		/// <summary>
		/// isDrawStringをtrueにする
		/// チュートリアル管理側以外の文字列の描画を許可する(こちらが通常時)
		/// </summary>
		public static void setEnableDrawString()
		{
			TutorialStatus.isDrawString = true;
		}
		
		/// <summary>
		/// isDrawStringをfalseにする
		/// チュートリアル管理側以外の文字列の描画をさせない
		/// 主にスライド等の表示のため
		/// </summary>
		public static void setDisableDrawString()
		{
			TutorialStatus.isDrawString = false;
		}
		
		
		/// <summary>
		/// チュートリアル実行中で文字列を描画してほしくない場合にfalseを返します
		/// チュートリアル管理側でダイアログを表示している場合や、スライドを前面に描画している場合などが該当
		/// </summary>
		/// <returns>文字列を描画すべきかどうか</returns>
		public static bool StringDrawFlag()
		{
			// チュートリアル実行中で、ダイアログが表示されているか文字列描画が許可されていなければ Falseを返す
			return !(TutorialStatus.getIsTutorial() & (TutorialStatus.getisTutorialDialog() | !TutorialStatus.getisDrawString()) );
		}
		
		#endregion
		
		#region FrameCount に関するメソッド
		
		public static void setFrameCount(int frame)
		{
			TutorialStatus.FrameCount = frame;
		}
		
		public static bool checkFrameCount(int frame, int num)
		{
			int thisframe = TutorialStatus.FrameCount + num;
			if(thisframe >= 60) thisframe -= 60;
			if(thisframe < frame) return false;
			else return true;
		}
		
		#endregion
		
		/// <summary>
		/// 開発/デバッグ用
		/// </summary>
		public static void test()
		{
			TutorialStatus.setEnableTutorial();
			TutorialStatus.setPermissionPartsList(new string[] { "muphic.Top.OneButton", "muphic.One.Score", "muphic.One.RightButtons.SheepButton" });
		}
	}
}
