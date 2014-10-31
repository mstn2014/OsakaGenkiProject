using UnityEngine;
using System.Collections;

public class GameRoop : MonoBehaviour {

	enum GameState{stop, ready, play, end};
	private bool m_start;				// スタート確認.
	private GameState m_state;			// ゲームの状態.
	private CountDown	m_timer;
	private Question_ver2	m_quest;	// ToDo 今だけ_ver2ついてる.
	InputMgr m_btnState;                // 入力インスタンス.
	FadeMgr m_fadeMgr;                  // フェード.

	// Use this for initialization
	IEnumerator Start () {
		m_start = false;
		m_state = GameState.stop;
		GameObject countDown = GameObject.Find("Timer");
		m_timer = countDown.GetComponent<CountDown>();
		m_quest = GetComponent<Question_ver2>();
		// ToDo最大問題数の取得.

		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;
		m_fadeMgr = gs.FadeMgr;

		// ゲームループ.
		while (true) {
			// スタート.
			if (m_start) {
				switch(m_state){
				case GameState.ready:
					Debug.Log("ゲーム準備");
					StartCoroutine (m_quest.CreateQuestion ());
					while (!m_quest.IsCreate) {
						yield return null;
					}
					m_timer.ResetTimer();
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
						// ステージクリアなら次のゲームへ.
						m_timer.ResetTimer();
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
