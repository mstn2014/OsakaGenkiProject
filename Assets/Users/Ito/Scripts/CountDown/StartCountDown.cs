using UnityEngine;
using System.Collections;


//======================================================
// @brief:ゲーム開始時のカウントダウンクラス
// Begin()を一回呼ぶとカウントダウンが開始する。連続で呼ぶとバグります。
// Begin()を呼ぶ毎にリセットがかかる。
// IsFinishedでカウントダウンが終わったかどうかわかる。
//------------------------------------------------------
// @author:K.Ito
// @param:none
// @return:none
//======================================================
public class StartCountDown : MonoBehaviour {

    bool m_isFinished = false;                 // カウントが終わったらtrue
    UISprite m_sprite;
	SoundMgr m_sound;          				   // サウンド

    [Header("カウントダウンの表示時間")]
    public float m_time = 0.7f;
    [Header("スプライトの名前")]
    public string three;
    public string two;
    public string one;
    public string start;

    //set getアクセサ
    public bool IsFinished
    {
        get { return m_isFinished; }
    }

	// Use this for initialization
	void Awake () {
        m_sprite = GetComponent<UISprite>();
        Reset();
	}

	void Start () {
		// 入力クラスの取得
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_sound = gs.SoundMgr;
	}
	
	// Update is called once per frame
	void Update () {
	}

    //======================================================
    // @brief:カウントダウンスタート
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void Begin()
    {
        Reset();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        m_sprite.enabled = true;
		m_sound.PlaySeCountDown();
        // m_time秒待つ
        yield return new WaitForSeconds(m_time);

        // ここで次のテクスチャに切り替える
        ChangeSprite(two);
		m_sound.PlaySeCountDown();

        // m_time秒待つ
        yield return new WaitForSeconds(m_time);

        // ここで次のテクスチャに切り替える
        ChangeSprite(one);
		m_sound.PlaySeCountDown();

        // m_time秒待つ
        yield return new WaitForSeconds(m_time);

        // ここで次のテクスチャに切り替える
        ChangeSprite(start);
		m_sound.PlaySeStart();

        // m_time秒待つ
        yield return new WaitForSeconds(m_time);

        // 終わる
        m_sprite.enabled = false;
        m_isFinished = true;
    }

    void ChangeSprite(string name)
    {
        m_sprite.spriteName = name;
        m_sprite.MakePixelPerfect();
    }

    //======================================================
    // @brief:カウントダウンリセット
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void Reset()
    {
        m_isFinished = false;
        m_sprite.spriteName = three;
        m_sprite.enabled = false;
        m_sprite.MakePixelPerfect();
    }
}
