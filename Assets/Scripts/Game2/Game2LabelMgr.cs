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
    private GameObject m_dispLabel;         // 成功を表示するオブジェクト.


    // 設定ファイル
    Game2Setting Setting;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        // 設定ファイルの読み込み
        Setting = Resources.Load<Game2Setting>("Setting/Game2Setting");
        // ラベルオブジェクトの読み込み
		m_missLabel = Resources.Load<GameObject>("Prefab/Game2/LabelMiss");
		m_safeLabel = Resources.Load<GameObject>("Prefab/Game2/LabelSafe");
		m_goodLabel = Resources.Load<GameObject>("Prefab/Game2/LabelGood");
		m_perfectLabel = Resources.Load<GameObject>("Prefab/Game2/LabelPerfect");
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
		switch(labelName)
		{
			case "safe":
				m_dispLabel = Instantiate(m_safeLabel,transform.position,transform.rotation) as GameObject;
				break;
			
			case "miss":
				m_dispLabel = Instantiate(m_missLabel,transform.position,transform.rotation) as GameObject; 
				break;

			case "good":
				m_dispLabel = Instantiate(m_goodLabel,transform.position,transform.rotation) as GameObject; 
				break;

			case "perfect":
				m_dispLabel = Instantiate(m_perfectLabel,transform.position,transform.rotation) as GameObject; 
				break;
		}
		m_dispLabel.transform.parent = this.transform;
	}
}