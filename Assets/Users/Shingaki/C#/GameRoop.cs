using UnityEngine;
using System.Collections;

public class GameRoop : MonoBehaviour {

	enum GameState{stop, ready, play, end};
	private bool m_start;				// スタート確認.
	private GameState m_state;			// ゲームの状態.
	private CountDown	m_timer;		
	private Question	m_quest;		// 問題生成.
	private EffectMgr	m_effect;		// エフェクト.

	InputMgr m_btnState;                // 入力インスタンス.
	FadeMgr m_fadeMgr;                  // フェード.

	// Game1共通設定.
	private Game1_Setting GAME1;

	// データセーブ用
	private SaveData SAVE;

	// Use this for initialization
	IEnumerator Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");
		// データセーブ用
		SAVE = Resources.Load<SaveData>("SaveData/SaveData");

		m_start = false;
		m_state = GameState.stop;
		m_timer = GetComponent<CountDown>();
		m_quest = GetComponent<Question>();
		m_effect = GetComponent<EffectMgr> ();


		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;
		m_fadeMgr = gs.FadeMgr;

		// プレイヤー,ギャラリー,背景の設置.
		// ToDo クラス呼び出し(そのクラスに移動なども任せる).

		// ゲームループ.
		while (true) {
			// スタート.
			if (m_start) {
				switch(m_state){
				case GameState.ready:
					Debug.Log("ゲーム準備");
					///// ラウンドへの初期化 /////
					m_timer.ResetTimer();		// タイマー初期化.
					m_effect.InitEffect();		// エフェクト関連初期化.
					StartCoroutine (m_quest.CreateQuestion ());	// 問題生成.
					while (!m_quest.IsCreate) {
						yield return null;
					}

					m_timer.StartTimer();
					m_state = GameState.play;
					break;
				case GameState.play:
					Debug.Log("ゲームプレイ");
					// 入力(正誤判定).
					if(m_btnState.YellowButtonTrigger){
						if(!m_quest.CheckAns(1)){
							m_state = GameState.end;
						}
					}
					if(m_btnState.GreenButtonTrigger){
						if(!m_quest.CheckAns(2)){
							m_state = GameState.end;
						}
					}
					// クリアチェック.
					if(m_quest.IsComplete || m_timer.IsPaused){
						// コンプリートならゲーム終了.
						m_state = GameState.end;
					}else if(m_quest.IsClear){
						// ラウンドクリアなら次のゲームへ.
						m_state = GameState.ready;
					}
					
					break;
				case GameState.end:
					Debug.Log("終了");
					// 各クラスの初期化.
					m_quest.InitQuest();
					m_timer.ResetTimer();
					m_start = false;
					m_state = GameState.stop;
					
					break;
				}
			}
			Debug.Log("ループ");
			yield return null;		
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartGame(){
		if (m_state == GameState.stop) {
			m_start = true;
			m_state = GameState.ready;
		}
	}
}
