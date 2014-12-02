using UnityEngine;
using System.Collections;

public class Game1GameRoop : MonoBehaviour {

	enum GameState{guide,stop, ready, play, product, end};
	private bool m_start;				// スタート確認.
	private GameState m_state;			// ゲームの状態.
	private Game1CountDown	m_timer;		
	private Game1Question	m_quest;		// 問題生成.
	private Game1EffectMgr	m_effect;		// エフェクト.
	private Game1ProductionMgr	m_product;	// 演出
    public Game1PlayerController m_player; // プレイヤー
    public Guide m_guide;               // ガイドマネージャ
    public StartCountDown m_countDown;  // カウントダウン
    [Header("ゲーム開始時にオンにするオブジェクト一覧")]
    public GameObject m_timeFrame;      // 制限時間表示オブジェクト

	InputMgr m_btnState;                // 入力インスタンス.
	FadeMgr m_fadeMgr;                  // フェード.
	SoundMgr m_sound;          			// サウンド

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
		m_state = GameState.guide;
		m_timer = GetComponent<Game1CountDown>();
		m_quest = GetComponent<Game1Question>();
		m_effect = GetComponent<Game1EffectMgr> ();
		m_product = GetComponent<Game1ProductionMgr> ();


		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;
		m_fadeMgr = gs.FadeMgr;
		m_sound = gs.SoundMgr;
		m_sound.PlayGame_1();
 
        m_start = true;

		// プレイヤー,ギャラリー,背景の設置.
		// ToDo クラス呼び出し(そのクラスに移動なども任せる).

		// ゲームループ.
		while (true) {
			// スタート.
			if (m_start) {
				switch(m_state){
                case GameState.guide:
                        m_guide.Begin("Message/small_event_1_0");
                        while (m_guide.IsUse)
                        {
                            yield return null;
                        }
                        m_countDown.Begin();
                        m_state = GameState.ready;
                    break;
				case GameState.ready:
					Debug.Log("ゲーム準備");
                    while (!m_countDown.IsFinished)
                    {
                        yield return null;
                    }
                    m_timeFrame.SetActive(true);
                    m_player.gameObject.SetActive(true);
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
							m_sound.PlaySeMiss();
							StartCoroutine(m_product.ResultRound(1));
							m_state = GameState.product;
						}
					}
					if(m_btnState.GreenButtonTrigger){
                        // プレイヤーのモーション
                        m_player.DoPose();
						if(!m_quest.CheckAns(2)){
							m_sound.PlaySeMiss();
							StartCoroutine(m_product.ResultRound(1));
							m_state = GameState.product;
						}
					}
					// クリアチェック.
					if(m_quest.IsClear){
						m_state = GameState.product;
						StartCoroutine(m_product.ResultRound(0));
					}

					if(m_timer.IsPaused){			// 時間切れ
						StartCoroutine(m_product.ResultRound(2));
						m_state = GameState.product;
					}					
					break;
				case GameState.product:
					m_timer.ResetTimer();		// タイマー初期化.
					m_quest.ReadyNextRound();
                    m_timeFrame.SetActive(false);
					while(!m_product.IsEnd){
						yield return null;
					}
					if(!m_quest.IsComplete){
						m_state = GameState.ready;
                        m_player.DoStand();
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
