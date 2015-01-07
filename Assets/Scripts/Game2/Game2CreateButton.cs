//======================================================
// @brief:赤、青、黄色、緑のボタンの動的生成を行う.
//------------------------------------------------------
// @author:前田稚隼.
// @param:　m_createTime 生成する時間.
// @return:　なし.
//======================================================

using UnityEngine;
using System.Collections;

public class Game2CreateButton : MonoBehaviour {

	private float nowTime;
    private int m_playCount;                // ゲームのカウント.
    private float m_createTime;             // ボタン生成時間.
    private int m_patternIndex;             // パターン情報のインデックス.
    private int m_contryIndex;              // 国のインデックス.
    private int[][] m_pattern;              // 各国のパターン情報を格納.
    private string[][] m_buttonName;        // 使用するボタンを取り出す.
	private GameObject m_gameObject;		//	ゲームオブジェクト本体.
	private GameObject m_redGameObject;		//	赤いアイコン.
	private GameObject m_blueGameObject;	//	青いアイコン.
	private GameObject m_greenGameObject;	//	緑のアイコン.
	private GameObject m_yellowGameObject;	//	黄色のアイコン.
    private Vector3 m_createPosition;   	// ボタンを生成する位置.
    const int countryNum = 5;


    Game2Setting Setting;                   // 設定ファイル
    //set getアクセサ
    public bool WaitFlg{set;get;}           // 盛り上がりイベントを待つフラグ
    public bool IsFinished { set; get; }    // すべてのボタンを生成したらたてる
    public int CountryIndex { get { return m_contryIndex; } }

	// Use this for initialization
	void Start () {
        Setting = Resources.Load<Game2Setting>("Setting/Game2Setting");
        m_createTime = Setting.createButtonTime;
		//	ボタン情報代入.
		m_blueGameObject = Resources.Load<GameObject>("Prefab/Game2/blue");  
		m_redGameObject = Resources.Load<GameObject>("Prefab/Game2/red");  
		m_greenGameObject = Resources.Load<GameObject>("Prefab/Game2/green");  
		m_yellowGameObject = Resources.Load<GameObject>("Prefab/Game2/yellow"); 
		nowTime = 0.0f;
        m_patternIndex = 0;
        // 各国のパターン情報を呼び出しておく
        m_pattern = new int[countryNum][];
        m_pattern[0] = Setting.JapanPattern;
        m_pattern[1] = Setting.AmericaPattern;
        m_pattern[2] = Setting.TurkeyPattern;
        m_pattern[3] = Setting.BrazilPattern;
        m_pattern[4] = Setting.SpainPattern;

        // ボタン情報を設定ファイルから呼び出す
        m_buttonName = new string[countryNum][];
        m_buttonName[0] = Setting.JapanButtounType;
        m_buttonName[1] = Setting.AmericaButtounType;
        m_buttonName[2] = Setting.TurkeyButtounType;
        m_buttonName[3] = Setting.BrazilButtounType;
        m_buttonName[4] = Setting.SpainButtounType;

        // ボタン生成位置の設定
        m_createPosition = GameObject.Find("ButtonCreatePosition").GetComponent<Transform>().position;

        m_contryIndex = 0;
        WaitFlg = false;
        IsFinished = false;
        m_playCount = 0;
	}
	
	// Update is called once per fra
	void Update () {

        // 盛り上がりイベント待ちの時は更新しない
        if (WaitFlg) return;

		nowTime += Time.deltaTime;

		if ( nowTime >= m_createTime)
		{
            if (m_pattern[m_contryIndex][m_patternIndex++] == 1)
            {
                // ボタンを国別で決められた色からランダムで取り出す
                string buttonName = string.Empty;
                buttonName = m_buttonName[m_contryIndex][Random.Range(0, m_buttonName[m_contryIndex].Length)];

                switch (buttonName)
                {
                    case "blue":
                        m_gameObject = Instantiate(m_blueGameObject, m_createPosition, transform.rotation) as GameObject;
                        break;

                    case "red":
                        m_gameObject = Instantiate(m_redGameObject, m_createPosition, transform.rotation) as GameObject;
                        break;

                    case "green":
                        m_gameObject = Instantiate(m_greenGameObject, m_createPosition, transform.rotation) as GameObject;
                        break;

                    case "yellow":
                        m_gameObject = Instantiate(m_yellowGameObject, m_createPosition, transform.rotation) as GameObject;
                        break;
                }
                m_gameObject.transform.parent = this.transform;
            }

            // パターンが最後までいったとき
            if (m_patternIndex == m_pattern[m_contryIndex].Length )
            {
                // 試行回数が2回なら次
                if (m_playCount >= 0)
                {
                    // とりあえず次の国のパターンを表示
                    m_playCount = 0;
                    m_contryIndex++;
                    m_createTime = Setting.createButtonTime;
                    WaitFlg = true;
                    if (m_contryIndex == 5)
                    {
                        IsFinished = true;
                    }
                }
                else
                {
                    m_playCount++;
                    m_createTime *= Setting.difficulty;
                }
                m_patternIndex = 0;
            }
			nowTime = 0.0f;
		}
	}
}
