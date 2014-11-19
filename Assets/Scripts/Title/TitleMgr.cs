using UnityEngine;
using System.Collections;

public class TitleMgr : MonoBehaviour {
    // 設定ファイル
    TitleSetting m_setting;

    // マネージャー関連
    InputMgr m_btnState;        // 入力インスタンス
    FadeMgr m_fade;             // フェードマネージャ 
    SoundMgr m_sound;           // サウンド

    // コンポーネント関連
    TitleLogo m_logo;          // ロゴ
    TitlePlayerController m_player;

    // オブジェクト関連
    GameObject m_camera;
    public GameObject m_effect;

    bool m_isStart;

	// Use this for initialization
	void Start () {
	    // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;
        m_fade = gs.FadeMgr;
        m_sound = gs.SoundMgr;
        m_sound.PlayTitleBGM();

        // 設定ファイルの読み込み
        m_setting = Resources.Load<TitleSetting>("Setting/TitleSetting");

        // ゲームオブジェクトのアタッチ
        m_logo = GameObject.Find("Logo").GetComponent<TitleLogo>();
        m_player = GameObject.Find("motion_defo").GetComponent<TitlePlayerController>();

        m_camera = GameObject.Find("Main Camera");

        m_isStart = false;
	}
	
	// Update is called once per frame
	void Update () {
        // 赤ボタンでロゴを消す(ゲームスタート)
        if (m_btnState.RedButtonTrigger && !m_isStart)
        {
            StartCoroutine(LogoEffect());
            m_logo.FadeOut();
            m_isStart = true;
        }
	}

    IEnumerator LogoEffect()
    {
        yield return new WaitForSeconds(m_setting.disappearSpeed);
        iTweenEvent.GetEvent(m_camera, "MoveOut").Play();
        m_player.MoveToFront();
        m_effect.SetActive(true);
        yield return new WaitForSeconds(m_setting.nextSceneTime);
        m_fade.LoadLevel("bigIvent1");
    }
}
