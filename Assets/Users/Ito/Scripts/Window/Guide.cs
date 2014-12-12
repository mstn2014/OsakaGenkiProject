using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guide : MonoBehaviour
{
    InputMgr m_btnState;                    // 入力インスタンス
	SoundMgr m_sound;          				// サウンド
    WindowMgr m_windowMgr;                  // ウィンドウマネージャー
    GuideMgr m_guideMgr;                    // ガイド役のマネージャー
    List<string> m_messageText;             // メッセージデータ
    int m_messageIndex;                     // メッセージのインデックス
    string m_soundPath;

    // set getアクセサ
    public bool IsUse { set; get; }         // ガイドを呼び出していればtrue,いなければfalse

    // Use this for initialization
    void Awake()
    {
        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;

        // ウィンドウクラスの呼び出し
        m_windowMgr = GameObject.Find("WindowMgr").GetComponent<WindowMgr>();

        // ガイドを呼び出す
        m_guideMgr = GameObject.Find("GuideMgr").GetComponent<GuideMgr>();

        // テキストファイルの読み込み 
        m_messageText = new List<string>();

        m_messageIndex = 0;
        IsUse = false;
    }

	void Start () {
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_sound = gs.SoundMgr;
	}

    // Update is called once per frame
    void Update()
    {
        // 呼び出されていなければ処理を行わない。
        if (!IsUse) return;

        // メッセージ送り
        if (m_btnState.AnyButtonTrigger && m_windowMgr.IsFinished && (m_messageText.Count > (m_messageIndex + 1)))
        {
            m_windowMgr.Text = m_messageText[++m_messageIndex];
            string speakName = m_soundPath + m_messageIndex.ToString();
            AudioClip audioClip = Resources.Load<AudioClip>(speakName);

            // audioClipの再生
            this.GetComponent<AudioSource>().clip = audioClip;
            this.GetComponent<AudioSource>().loop = false;
            this.GetComponent<AudioSource>().PlayOneShot(audioClip);
			m_sound.PlaySeReturn();
        }
        //　メッセージが最後まで行った時の処理 
        else if (m_btnState.AnyButtonTrigger && m_windowMgr.IsFinished && (m_messageText.Count == (m_messageIndex + 1)))
        {
            m_windowMgr.CloseWindow();
            m_guideMgr.EndGuide();
            m_messageIndex = 0;
            StartCoroutine(CheckUseFlg());
            this.GetComponent<AudioSource>().Stop();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            End();
        }
    }

    //======================================================
    // @brief:ガイドを呼び出す
    //------------------------------------------------------
    // @author:K.Ito
    // @param:ガイドがしゃべる内容が書いてあるtxtファイルのパス
    // @param:ガイドがしゃべる内容の音声ファイルのパス
    // @return:none
    //=====================================================
    public void Begin(string textPath,string soundPath)
    {
        if (IsUse) return;

        // テキストファイルをパースする
        ParseMessageText textParser = new ParseMessageText();
        m_messageText = textParser.LoadText(textPath);
        m_windowMgr.Text = m_messageText[m_messageIndex];

        m_soundPath = soundPath;
        string speakName = m_soundPath + m_messageIndex.ToString();
        AudioClip audioClip = Resources.Load<AudioClip>(speakName);

        // audioClipの再生
        this.GetComponent<AudioSource>().clip = audioClip;
        this.GetComponent<AudioSource>().loop = false;
        this.GetComponent<AudioSource>().PlayOneShot(audioClip);

        // ウィンドウとガイドを呼び出す
        m_windowMgr.OpenWindow();
        m_guideMgr.CallGuide();

        IsUse = true;
    }

    //======================================================
    // @brief:強制的にガイドを終了させる（通常は自動的に閉じる)
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //=====================================================
    public void End()
    {
        m_windowMgr.CloseWindow();
        m_guideMgr.EndGuide();
        StartCoroutine(CheckUseFlg());
        m_messageIndex = 0;
    }

    //======================================================
    // @brief:クローズするまでIsUseフラグを立てるのを待つ
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //=====================================================
    IEnumerator CheckUseFlg()
    {
        while(!m_windowMgr.IsFinished)
        {
            yield return null;
        }
        IsUse = false;
    }
}
