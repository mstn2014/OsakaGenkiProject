using UnityEngine;
using System.Collections;

public class GameRoop : MonoBehaviour {

	enum GameState{stop, ready, play, product, end};
	private bool m_start;				// スタート確認.
	private GameState m_state;			// ゲームの状態.
	private CountDown	m_timer;		
	private Question	m_quest;		// 問題生成.
	private EffectMgr	m_effect;		// エフェクト.
	private ProductionMgr	m_product;	// 演出
    private Game1PlayerController m_player; // プレイヤー

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
		m_product = GetComponent<ProductionMgr> ();
        m_player = GameObject.Find("game1_motion_defo(Clone)").GetComponent<Game1PlayerController>();


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
                        // プレイヤーのモーション
                        m_player.DoPass();
						if(!m_quest.CheckAns(1)){
							StartCoroutine(m_product.ResultRound(1));
							m_state = GameState.product;
						}
					}
					if(m_btnState.GreenButtonTrigger){
                        // プレイヤーのモーション
                        m_player.DoPose();
						if(!m_quest.CheckAns(2)){
							StartCoroutine(m_product.ResultRound(1));
							m_state = GameState.product;
						}
					}
					// クリアチェック.
					if(m_quest.IsClear){
						StartCoroutine(m_product.ResultRound(0));
						m_state = GameState.product;
					}

					if(m_timer.IsPaused){			// 時間切れ
						StartCoroutine(m_product.ResultRound(2));
						m_state = GameState.product;
					}					
					break;
				case GameState.product:
					m_timer.ResetTimer();		// タイマー初期化.
					m_quest.ReadyNextRound();
					while(!m_product.IsEnd){
						yield return null;
					}
					if(!m_quest.IsComplete){
						m_state = GameState.ready;
					}else{
						m_state = GameState.end;	// ゲーム終了小イベントへ
					}
					break;
				case GameState.end:	// ToDo ここの処理は特に重要ではありません
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
	void Update () {}

	public void StartGame(){
		if (m_state == GameState.stop) {
			m_start = true;
			m_state = GameState.ready;
		}
	}
}
