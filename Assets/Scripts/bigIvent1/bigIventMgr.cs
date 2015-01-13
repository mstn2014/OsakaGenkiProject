using UnityEngine;
using System.Collections;

public class bigIventMgr : MonoBehaviour {
    // マネージャー関連
    InputMgr m_btnState;        // 入力インスタンス
    FadeMgr m_scene;            // シーン遷移マネージ
	SoundMgr m_sound;           // サウンド
    SaveMgr m_save;             // セーブデータ

    // インスペクターで設定する
    [Header("ガイドのファイルを指定")]
    public string m_guideFile;         // ガイドのファイルを指定
    public string m_guideSound;        // ガイドの音声ファイル

    [Header("次のシーンを指定")]
    public string m_nextScene;         // 次のシーンを指定

    public bool isGuide = false;

    // コンポーネント関連
    [Header("コンポーネントを取得")]
    public Guide m_guide;
    bigIvent2SE m_se;
    // 
    bool m_isStart;             // お姉さんの会話が始まったらtrue

	// Use this for initialization
	void Start () {
        // コンポーネントを取得
        //m_guide = GameObject.Find("Guide").GetComponent<Guide>();

        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;
        m_scene = gs.FadeMgr;
		m_sound = gs.SoundMgr;
		m_sound.PlayBigIvent();

        m_isStart = false;

        m_se = GetComponent<bigIvent2SE>();

        StartCoroutine(BeginScene());
	}

    IEnumerator BeginScene()
    {
        
        if (isGuide)
        {
            yield return new WaitForSeconds(5.0f);

            m_guide.Begin("Message/" + m_guideFile, "none");

            while (m_guide.IsUse)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2.0f);
        }
        m_isStart = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (!isGuide)
        {
            if (m_isStart && m_btnState.AnyButtonTrigger)
            {
                m_scene.LoadLevel(m_nextScene);
                m_isStart = false;
            }
        }
        else if (isGuide)
        {
            if (m_isStart)
            {
                m_se.isPlay = false;
                m_scene.LoadLevel(m_nextScene);
                m_isStart = false;
            }
        }
	}
}
