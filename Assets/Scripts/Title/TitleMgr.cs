using UnityEngine;
using System.Collections;

public class TitleMgr : MonoBehaviour {
    // 設定ファイル
    TitleSetting m_setting;

    // マネージャー関連
    InputMgr m_btnState;        // 入力インスタンス

    // コンポーネント関連
    TitleLogo m_logo;          // ロゴ

    // オブジェクト関連
    GameObject m_camera;

	// Use this for initialization
	void Start () {
	    // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;

        // 設定ファイルの読み込み
        m_setting = Resources.Load<TitleSetting>("Setting/TitleSetting");

        // ゲームオブジェクトのアタッチ
        m_logo = GameObject.Find("Logo").GetComponent<TitleLogo>();

        m_camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        // 赤ボタンでロゴを消す(ゲームスタート)
        if (m_btnState.RedButtonTrigger)
        {
            StartCoroutine(LogoEffect());
            m_logo.FadeOut();
        }
	}

    IEnumerator LogoEffect()
    {
        yield return new WaitForSeconds(m_setting.disappearSpeed);
        iTweenEvent.GetEvent(m_camera, "MoveOut").Play();
    }
}
