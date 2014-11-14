//======================================================
// @brief:赤、青、黄色、緑のボタンの動的生成を行う.
//------------------------------------------------------
// @author:前田稚隼.
// @param:　m_createTime 生成する時間.
// @return:　なし.
//======================================================

using UnityEngine;
using System.Collections;

public class CreateButton : MonoBehaviour {

	private float nowTime;
    private int m_playCount;                // ゲームのカウント
    private float m_createTime;             // ボタン生成時間
    private int m_patternIndex;             // パターン情報のインデックス
    private int m_contryIndex;              // 国のインデックス
    private int[][] m_pattern;              // 各国のパターン情報を格納
    private string[][] m_buttonName;        // 使用するボタンを取り出す
    public GameObject m_parent;             // 親オブジェクト
	private GameObject m_gameObject;		//	ゲームオブジェクト本体.
	private GameObject m_redGameObject;		//	赤いアイコン.
	private GameObject m_blueGameObject;	//	青いアイコン.
	private GameObject m_greenGameObject;	//	緑のアイコン.
	private GameObject m_yellowGameObject;	//	黄色のアイコン.

    Game2Setting Setting;                   // 設定ファイル

	// Use this for initialization
	void Start () {
        Setting = Resources.Load<Game2Setting>("Setting/Game2Setting");
        m_createTime = Setting.createButtonTime;
		//	ボタン情報代入.
		m_blueGameObject = Resources.Load<GameObject>("blue");  
		m_redGameObject = Resources.Load<GameObject>("red");  
		m_greenGameObject = Resources.Load<GameObject>("green");  
		m_yellowGameObject = Resources.Load<GameObject>("yellow"); 
		nowTime = 0.0f;
        m_patternIndex = 0;
        // 各国のパターン情報を呼び出しておく
        m_pattern = new int[5][];
        m_pattern[0] = Setting.JapanPattern;
        m_pattern[1] = Setting.AmericaPattern;
        m_pattern[2] = Setting.TurkeyPattern;
        m_pattern[3] = Setting.BrazilPattern;
        m_pattern[4] = Setting.SpainPattern;

        // ボタン情報を設定ファイルから呼び出す
        m_buttonName = new string[5][];
        m_buttonName[0] = Setting.JapanButtounType;
        m_buttonName[1] = Setting.AmericaButtounType;
        m_buttonName[2] = Setting.TurkeyButtounType;
        m_buttonName[3] = Setting.BrazilButtounType;
        m_buttonName[4] = Setting.SpainButtounType;

        m_contryIndex = 0;
        m_playCount = 0;
	}
	
	// Update is called once per fra
	void Update () {

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
                        m_gameObject = Instantiate(m_blueGameObject, transform.position, transform.rotation) as GameObject;
                        break;

                    case "red":
                        m_gameObject = Instantiate(m_redGameObject, transform.position, transform.rotation) as GameObject;
                        break;

                    case "green":
                        m_gameObject = Instantiate(m_greenGameObject, transform.position, transform.rotation) as GameObject;
                        break;

                    case "yellow":
                        m_gameObject = Instantiate(m_yellowGameObject, transform.position, transform.rotation) as GameObject;
                        break;
                }
                m_gameObject.transform.parent = m_parent.transform;
            }

            // パターンが最後までいったとき
            if (m_patternIndex == m_pattern[m_contryIndex].Length )
            {
                // 試行回数が3回なら次
                if (m_playCount >= 2)
                {
                    // ToDO:盛り上がりイベント処理を書く

                    // とりあえず次の国のパターンを表示
                    m_playCount = 0;
                    m_contryIndex++;
                    m_createTime = Setting.createButtonTime;
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
