using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	enum GameState{stop, ready, play, end};
	private bool m_start;				// スタート確認
	private GameState m_state;		// ゲームの状態
	private CountDown	m_timer;
	private Question	m_quest;
	InputMgr m_btnState;                    // 入力インスタンス
	FadeMgr m_fadeMgr;                      // フェード
	// Use this for initialization
	void Start () {
		m_start = false;
		m_state = GameState.ready;
		GameObject countDown = GameObject.Find("Timer");
		m_timer = countDown.GetComponent<CountDown>();
		m_quest = GetComponent<Question>();

		// 共通設定の呼び出し
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;
		m_fadeMgr = gs.FadeMgr;

	}
	
	// Update is called once per frame
	void Update () {
		// スタート.
		if (m_start) {
			StartCoroutine("main");
		}
	}

	// ゲームメイン
	private IEnumerator main(){
		switch(m_state){
		case GameState.ready:
			StartCoroutine (m_quest.CreateQuestion ());
			while (!m_quest.createCheck) {
				yield return null;
			}
			m_timer.StartTimer();
			m_state = GameState.play;
		break;
		case GameState.play:
			// 時間切れなら終了.
			// if文でキー入力確認
			// 入力(正誤判定).

			// 成功なら問題生成へ(+1問).
			// 失敗(ボタン間違え、時間切れ)なら最初へ.
			if(m_timer.IsPaused){
				m_state = GameState.end;
			}

		break;
		case GameState.end:
			Debug.Log("終了");
		break;
		}
	}

	public void StartGame(){
		m_start = true;
		m_state = GameState.ready;
	}
}
