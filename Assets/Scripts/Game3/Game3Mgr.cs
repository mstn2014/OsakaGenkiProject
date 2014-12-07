using UnityEngine;
using System.Collections;
//======================================================
// @brief:ゲームの流れを管理するクラス（ゲーム３）
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Game3Mgr : MonoBehaviour
{

    public Game3Setting Setting;    // セッティング
    InputMgr m_Input;				// 入力
    FadeMgr m_fade;                 // 遷移
	SoundMgr m_sound;          		// サウンド
    public Guide m_Guide;					// ゲームガイド
    StartCountDown m_Count;			// カウント
    public GameObject m_MainFlg;	// ゲームメイン有効化
    GameObject m_Main2DFlg;		// ゲームメイン有効化(2D)
    bool m_isNextState;             // 次のシーンに遷移することを許す
    public ScoreMgr m_scoreMgr;     // スコアマネージャ
    SaveMgr m_saveData;     // セーブデータ
    public GameObject m_timeUp;     // タイムアップスプライト
    public Game3MoveObj m_moveObj;  // 移動オブジェクト
    public DisplayHowTo m_howTo;      // ハウトゥ
    public ResultSetting m_resultSet;
    public Game3Balancer m_balancer;

    // ステート
    enum Game3State
    {
        GUIDE, COUNTDOWN, GAME,END
    };
    Game3State m_state;

    // Use this for initialization
    void Start()
    {
        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_Input = gs.InputMgr;
        m_fade = gs.FadeMgr;
		m_sound = gs.SoundMgr;
        m_saveData = gs.SaveMgr;
		m_sound.PlayGame_3();

        // ガイド呼び出し
        //m_Guide = GameObject.Find ("Guide").GetComponent<Guide>(); 
        StartCoroutine(WaitGuide());

        // カウント呼び出し
        m_Count = GameObject.Find("Count").GetComponent<StartCountDown>();
        // ステート設定
        m_state = Game3State.GUIDE;

        // ゲームメイン設定
        m_Main2DFlg = GameObject.Find("Panel").transform.FindChild("GameUI").gameObject;


        m_isNextState = false;
    }

    IEnumerator WaitGuide()
    {
        yield return new WaitForSeconds(3.0f);

        m_Guide.Begin(Setting.FirstMessagePath,"Sound/GUIDE/small_event_3_0/small_event_3_0_talk_");

        while (m_Guide.IsUse)
        {
            yield return null;
        }

        m_howTo.Play();

        yield return new WaitForSeconds(0.5f);

        while (!m_Input.RedButtonTrigger)
        {
            yield return null;
        }

        m_howTo.End();

        yield return new WaitForSeconds(1.0f);

        m_isNextState = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case Game3State.GUIDE:
                Guide_State();
                break;

            case Game3State.COUNTDOWN:
                CountDown_State();
                break;
        }
    }

    //======================================================
    // @brief:ガイドステート
    //------------------------------------------------------
    // @author:A,Kawamoto
    // @param:なし
    // @return:なし
    //======================================================
    void Guide_State()
    {
        if (m_isNextState)
        {
            m_state = Game3State.COUNTDOWN;		// ステート更新
            m_Count.Begin();					// カウントダウンをスタート
        }
    }

    //======================================================
    // @brief:カウントダウンステート
    //------------------------------------------------------
    // @author:A,Kawamoto
    // @param:なし
    // @return:なし
    //======================================================
    void CountDown_State()
    {
        if (m_Count.IsFinished)
        {
            m_Main2DFlg.SetActive(true);	// ゲームを有効化(2D)
            m_MainFlg.SetActive(true);	// ゲームを有効化
            m_moveObj.IsMove = true;
            m_state = Game3State.GAME;		// ステート更新
        }
    }


    //======================================================
    // @brief:時間切れで呼び出される
    //------------------------------------------------------
    // @author:K.Ito
    // @param:なし
    // @return:なし
    //======================================================
    IEnumerator TimeOver()
    {
        m_saveData.game3Score = m_scoreMgr.Score;
        m_saveData.game3Max = m_balancer.AppearNum;
        m_saveData.gameState = SaveMgr.eState.GAME3;

        m_MainFlg.SetActive(false);	// ゲームを有効化
        m_Main2DFlg.SetActive(false);	// ゲームを有効化(2D)
        m_timeUp.SetActive(true);       // タイムアップを表示

        yield return new WaitForSeconds(3.0f);

        m_timeUp.SetActive(false);
        m_moveObj.IsMove = false;

        m_Guide.Begin(Setting.LastMessagePath, "Sound/GUIDE/small_event_3_1/small_event_3_1_talk_");

        while (m_Guide.IsUse)
        {
            yield return null;
        }

        m_fade.LoadLevel("result");
    }
}
