using UnityEngine;
using System.Collections;

public class bigIvent1Mgr : MonoBehaviour {
    // マネージャー関連
    InputMgr m_btnState;        // 入力インスタンス
    FadeMgr m_scene;            // シーン遷移マネージ

    // コンポーネント関連
    Guide m_guide;

    // 
    bool m_isStart;             // お姉さんの会話が始まったらtrue

	// Use this for initialization
	void Start () {
        // コンポーネントを取得
        m_guide = GameObject.Find("Guide").GetComponent<Guide>();

        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;
        m_scene = gs.FadeMgr;

        m_isStart = false;

        StartCoroutine(BeginScene());
	}

    IEnumerator BeginScene()
    {
        yield return new WaitForSeconds(3.0f);
        m_guide.Begin("Message/bigIvent1");
        m_isStart = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_isStart && !m_guide.IsUse)
        {
            m_scene.LoadLevel("userRegister");
        }
	}
}
