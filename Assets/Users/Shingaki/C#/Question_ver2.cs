using UnityEngine;
using System.Collections;

public class Question_ver2 : MonoBehaviour {
	// 生成するボタンと正解格納用の構造体.
	struct QuesBox{
		public GameObject 	button;
		public int			ans;		// 	0:初期   1:黄色   2:緑.
	}
	
	private const int m_minQuestionNum = 3;	// 表示されるボタンの最小の数.
	private const int m_maxQuestionNum = 10;// 表示されるボタンの最大の数.
	private int m_nowQuestionNum;		// 現在のボタン数.
	private int m_nowAns;				// 今何ボタン目か.
	
	private float m_createWeight; // 生成されるボタンの重み.
	private const float m_value = 0.1f;	// m_createWeightの増減値.
	private QuesBox[] m_box;			// 格納用配列.
	private bool m_create;				// 生成確認.
	private bool m_clear;				// ステージクリア確認.
	private bool m_complete;			// ゲームクリア確認.
	private GameObject m_panel;			// パネル(一番上の親).
	public GameObject m_QuestPanel;		// 生成されるボタンの親(Inspectorより設定).
	public GameObject m_Quest;			// 生成されるボタン(Inspectorより設定).
	//public GameObject m_green;			// 緑ボタンのprefab(Inspectorより設定).
	
	// get プロパティ.
	public bool IsCreate{
		get{return m_create;}
	}
	public bool IsClear{
		get{return m_clear;}
	}
	public bool IsComplete{
		get{return m_complete;}
	}
	
	// Use this for initialization
	void Start () {
		m_panel = GameObject.Find("Panel");
		CreatePrefab.InstantiateGameObject (m_QuestPanel, Vector3.zero, Quaternion.identity,
		                                   Vector3.one, m_panel);
		// 表示されるボタンの数だけ配列生成.
		m_box = new QuesBox[m_maxQuestionNum];
		InitQuest();			// 初期化
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//======================================================
	// @brief:問題の生成.
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:none
	// @return:none
	//======================================================
	public IEnumerator CreateQuestion(){
		// ボタンの数だけ配列生成.
		for (int i=0; i<m_nowQuestionNum; i++) {
			/* ///// 生成したボタンを次の問題に保持するときのみ有効 /////
			// もし生成済みの場合表示のみ.
			if(m_box[i].button != null){
				DispButton(i);
			}else */
			///////////////////////////////////////////////////////
			// ToDo ボタンの位置設定.
			// ボタン生成と出現位置計算.

			// Instantiate 使う？
			// m_box[i].button = 
			if (Random.value < m_createWeight) {
				// ToDo問題の生成
			//	m_box[i].button = CreatePrefab.InstantiateGameObject(m_yellow,Vector3.one,Quaternion.identity,	前田エラーが邪魔だったので
				//	                                                     new Vector3(100,100,1),m_panel);			前田エラーが邪魔だったので
				m_box[i].ans = 1;
				
				m_createWeight -= m_value;
			}else{
				// 緑生成.
				//	m_box[i].button = CreatePrefab.InstantiateGameObject(m_green,Vector3.one,Quaternion.identity,	前田エラーが邪魔だったので
				//                                                     new Vector3(100,100,1),m_panel);	前田エラーが邪魔だったので
				m_box[i].ans = 2;
				m_createWeight += m_value;
			}
			
			// 生成後時間をおく
			yield return new WaitForSeconds(1f);
		}
		yield return new WaitForSeconds(1f);
		for (int i=0; i<m_maxQuestionNum; i++) {
			HideButton(i);
		}
		m_create = true;
		m_clear = false;
	}
	
	// 答え合わせ
	public bool CheckAns(int ans){
		// 正解なら
		if (ans == m_box [m_nowAns].ans) {
			Debug.Log("正解");
			DispButton(m_nowAns);
			m_nowAns++;
			
			// コンプリートなら
			if(m_nowAns == m_maxQuestionNum){
				m_complete = true;
			}
			// ステージクリアなら次のゲームへ
			if(m_nowAns == m_nowQuestionNum){
				for(int i=0; i<m_nowQuestionNum; i++){
					HideButton(i);
				}
				/* ///// 生成したボタンを次の問題に保持するときのみ有効 /////
				m_clear = true;
				m_nowAns=0;
				m_nowQuestionNum++;
				m_create = false;
				*//////////////////////////////////////////////
				
				// ///// 毎回ランダムの場合.  /////
				int workQuest = m_nowQuestionNum;
				InitQuest();
				m_clear = true;
				m_nowQuestionNum = workQuest+1;
				//////////////////////////////////////////////
				
			}
			return true;
		}else{
			Debug.Log("はずれ");
			return false;
		}
		/*
		 *m_create = false;
		m_clear = false;
		m_complete = false;
		m_nowAns = 0;
		m_nowQuestionNum = m_minQuestionNum;
		*/
	}
	
	// ボタンの表示.
	public void DispButton(int i){
		// GameObjectがない場合は処理しない.
		if (m_box [i].button == null)	return;
		m_box[i].button.SetActive(true);
	}
	// ボタンの非表示.
	public void HideButton(int i){
		// GameObjectがない場合は処理しない.
		if (m_box [i].button == null)	return;
		m_box[i].button.SetActive(false);
	}
	
	// 問題の初期化.
	public void InitQuest(){
		for (int i=0; i<m_maxQuestionNum; i++) {
			if(m_box[i].button != null){
				Destroy(m_box[i].button);
				m_box[i].button = null;
				m_box[i].ans = 0;
			}
		}
		m_create = false;
		m_clear = false;
		m_complete = false;
		m_nowAns = 0;
		m_nowQuestionNum = m_minQuestionNum;
		m_createWeight = 0.5f;
	}
}
