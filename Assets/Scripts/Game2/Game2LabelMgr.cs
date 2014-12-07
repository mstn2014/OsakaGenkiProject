//======================================================
// @brief:Miss,Safe,Good,Perfectのラベルを表示する.
//------------------------------------------------------
// @author:前田稚隼.
// @param:　m_dispTime 表示する時間.
// @return:　なし.
//======================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game2LabelMgr : MonoBehaviour
{
	//	ラベル関連
	private GameObject m_missLabel;			//	"Miss!!"と書かれたラベル.
	private GameObject m_safeLabel;			//	"Safe!!"と書かれたラベル.
	private GameObject m_goodLabel;			//	"Good!!"と書かれたラベル.
	private GameObject m_perfectLabel;		//	"perfect!!"と書かれたラベル.
    private GameObject[] m_dispLabel;         // 成功を表示するオブジェクト.
    private int m_index;
    public Transform m_parent;              // 親オブジェクト

	SoundMgr m_sound;          		// サウンド


    // 設定ファイル
    Game2Setting Setting;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_sound = gs.SoundMgr;

        // 設定ファイルの読み込み
        Setting = Resources.Load<Game2Setting>("Setting/Game2Setting");
        // ラベルオブジェクトの読み込み
		m_missLabel = Resources.Load<GameObject>("Effect/Prefab/Eff_bad");
        m_safeLabel = Resources.Load<GameObject>("Effect/Prefab/Eff_Safe");
        m_goodLabel = Resources.Load<GameObject>("Effect/Prefab/Eff_good");
        m_perfectLabel = Resources.Load<GameObject>("Effect/Prefab/Eff_perfect");
        m_index = 0;
        m_dispLabel = new GameObject[3];
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	//======================================================
	// @brief:一定の位置までボタンが流れてきたら"MISS"を表示.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　なし.
	// @return:　なし.
	//======================================================
	void OnTriggerEnter2D ()
	{
		Call("miss");
	}
	
	//======================================================
	// @brief:(MISS,SAFE)などのラベルを表示する.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　string labelName 表示するラベルの名前.
	// @return:　なし.
	//======================================================
	public void Call(string labelName)
	{
        if (m_dispLabel[m_index] != null)
        {
            Destroy(m_dispLabel[m_index]);
        }

		switch(labelName)
		{
			case "safe":
				m_dispLabel[m_index] = Instantiate(m_safeLabel,Vector3.zero,Quaternion.identity) as GameObject;
				m_sound.PlaySeSafe();
				break;
			
			case "miss":
                m_dispLabel[m_index] = Instantiate(m_missLabel, Vector3.zero, Quaternion.identity) as GameObject; 
				m_sound.PlaySeMiss();
				break;

			case "good":
                m_dispLabel[m_index] = Instantiate(m_goodLabel, Vector3.zero, Quaternion.identity) as GameObject;
				m_sound.PlaySeGood();
				break;

			case "perfect":
                m_dispLabel[m_index] = Instantiate(m_perfectLabel, Vector3.zero, Quaternion.identity) as GameObject; 
				m_sound.PlaySePerfect();
				break;
		}
        m_dispLabel[m_index].transform.parent = m_parent;
        m_dispLabel[m_index].transform.localPosition = new Vector3(0,0,10 - 0.0001f*m_index);
        m_index++;
        if (m_index >= 3) m_index = 0;
	}
}