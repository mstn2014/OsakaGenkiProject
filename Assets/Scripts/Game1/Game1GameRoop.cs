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
    SaveMgr m_saveData;         // セーブデータ
    private ScoreMgr m_scoreMgr;         //　スコア
    [Header("ゲーム開始時にオンにするオブジェクト一覧")]
    public GameObject m_timeFrame;      // 制限時間表示オブジェクト
    public DisplayHowTo m_howTo;          // ハウトゥ

	InputMgr m_btnState;                // 入力インスタンス.
	FadeMgr m_fadeMgr;                  // フェード.
	SoundMgr m_sound;          			// サウンド

	// Game1共通設定.
	private Game1_Setting GAME1;

	// Use this for initialization
	IEnumerator Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		m_start = false;
		m_state = GameState.guide;
		m_timer = GetComponent<Game1CountDown>();
		m_quest = GetComponent<Game1Question>();
		m_effect = GetComponent<Game1EffectMgr> ();
		m_product = GetComponent<Game1ProductionMgr> ();
        m_scoreMgr = GetComponent<ScoreMgr>();


		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;
		m_fadeMgr = gs.FadeMgr;
		m_sound = gs.SoundMgr;
        m_saveData = gs.SaveMgr;
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
                        yield return new WaitForSeconds(9.0f);

                        m_guide.Begin("Message/small_event_1_0","Sound/GUIDE/small_event_1_0/small_event_1_0_talk_");
                        while (m_guide.IsUse)
                        {
                            yield return null;
                        }

                        m_howTo.Play();

                        yield return new WaitForSeconds(0.5f);

                        while(!m_btnState.RedButtonTrigger)
                        {
                            yield return null;
                        }

                        m_howTo.End();

                        yield return new WaitForSeconds(1.0f);

                        m_countDown.Begin();
                        m_state = GameState.ready;
                    break;
				case GameState.ready:
					Debug.Log("ゲーム準備");
                    while (!m_countDown.IsFinished)
                    {
                        yield return null;
                    }
                    
                    m_player.gameObject.SetActive(true);
					///// ラウンドへの初期化 /////
					m_timer.ResetTimer();		// タイマー初期化.
					m_effect.InitEffect();		// エフェクト関連初期化.
					StartCoroutine (m_quest.CreateQuestion ());	// 問題生成.
					while (!m_quest.IsCreate) {
						yield return null;
					}

                    m_timeFrame.SetActive(true);
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
                            if (m_quest.IsComplete)
                            {
                                StartCoroutine(m_effect.DispResult(1));
                                m_state = GameState.stop;
                                break;
                            }
                            else
                            {
                                StartCoroutine(m_product.ResultRound(1));
                                m_state = GameState.product;
                            }
						}
					}
					if(m_btnState.GreenButtonTrigger){
                        // プレイヤーのモーション
                        m_player.DoPose();
						if(!m_quest.CheckAns(2)){
							m_sound.PlaySeMiss();
                            if (m_quest.IsComplete)
                            {
                                StartCoroutine(m_effect.DispResult(1));
                                m_state = GameState.stop;
                                break;
                            }
                            else
                            {
                                StartCoroutine(m_product.ResultRound(1));
                                m_state = GameState.product;
                            }
						}
					}
					// クリアチェック.
					if(m_quest.IsClear){
                        if (m_quest.IsComplete)
                        {
                            StartCoroutine(m_effect.DispResult(0));
                            m_state = GameState.stop;
                            break;
                        }
                        else
                        {
                            m_state = GameState.product;
                            StartCoroutine(m_product.ResultRound(0));
                        }
					}

					if(m_timer.IsStop){			// 時間切れ
                        if (m_quest.IsComplete)
                        {
                            StartCoroutine(m_effect.DispResult(2));
                            m_state = GameState.stop;
                            break;
                        }
                        else
                        {
                            StartCoroutine(m_product.ResultRound(2));
                            m_state = GameState.product;
                        }
					}					
					break;
                case GameState.product:
                    m_timer.StopTimer();              
                    yield return new WaitForSeconds(2.0f);
                    m_timeFrame.SetActive(false); 
                    m_timer.ResetTimer();		// タイマー初期化
					
					m_quest.ReadyNextRound();
                    
					while(!m_product.IsEnd){
						yield return null;
					}

                    m_state = GameState.ready;
                    m_player.DoStand();
					
					break;
				case GameState.end:	// ToDo ここの処理は特に重要ではありません
					Debug.Log("終了");
					// 各クラスの初期化.
					m_quest.InitQuest();
					m_timer.ResetTimer();
					m_start = false;
					m_state = GameState.stop;
					
					break;
                case GameState.stop:
                    m_timeFrame.SetActive(false);
                    yield return new WaitForSeconds(3.0f);

                    m_effect.InitCircle();

                    m_guide.Begin("Message/small_event_1_1", "Sound/GUIDE/small_event_1_1/small_event_1_1_talk_");
                    while (m_guide.IsUse)
                    {
                        yield return null;
                    }

                    m_saveData.game1Score = m_scoreMgr.Score;
                    m_saveData.gameState = SaveMgr.eState.GAME1;
                    m_fadeMgr.LoadLevel("result");

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
