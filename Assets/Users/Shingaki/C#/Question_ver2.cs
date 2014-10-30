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
	[Header("ボタン間隔")]
	public	float m_interval = 10.0f;	// ボタンの間隔.
	
	private float m_createWeight;		// 生成されるボタンの重み.
	private const float m_value = 0.1f;	// m_createWeightの増減値.
	private QuesBox[] m_box;			// 格納用配列.
	private bool m_create;				// 生成確認.
	private bool m_clear;				// ステージクリア確認.
	private bool m_complete;			// ゲームクリア確認.
	private GameObject m_panel;			// パネル(一番上の親).
	private GameObject m_textWindow;	// テキストウィンドウ(座標参照).
	public GameObject m_QuestPanel;		// 生成されるボタンの親(Inspectorより設定).
	public GameObject m_Quest;			// 生成されるボタン(Inspectorより設定).
	
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
		// TextWindowとQuestPanelの発見と生成.
		m_textWindow = GameObject.Find("TextWindow");
		m_QuestPanel = CreatePrefab.InstantiateGameObject (m_QuestPanel, Vector3.zero, Quaternion.identity,
		                                   Vector3.one, m_textWindow);
		// 表示されるボタンの数だけ配列生成.
		m_box = new QuesBox[m_maxQuestionNum];
		InitQuest();			// 初期化.
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
		float interval;
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
			interval = i*(-m_interval/2)+(m_interval/2)*(m_nowQuestionNum - m_nowQuestionNum/2);
			m_box[i].button = CreatePrefab.InstantiateGameObject(m_Quest, new Vector3(0,interval,0), Quaternion.identity,
			                                                     Vector3.one, m_QuestPanel);
			if (Random.value < m_createWeight) {
				// 黄生成.
				// ToDoアタッチ.
				// スクリプトとラベルの設定
				m_box[i].ans = 1;
				m_createWeight -= m_value;
			}else{
				// 緑生成.

				m_box[i].ans = 2;
				m_createWeight += m_value;
			}
			
			// 生成後時間をおく.
			yield return new WaitForSeconds(1f);
		}
		yield return new WaitForSeconds(1f);
		for (int i=0; i<m_maxQuestionNum; i++) {
			HideButton(i);
		}
		m_create = true;
		m_clear = false;
	}
	
	//======================================================
	// @brief:答え合わせ.
	//------------------------------------------------------
	// @author: T.Shingaki
	// @param: int:回答	1:黄色	2:緑
	// @return: bool:	true:正解	false:不正解
	//======================================================
	public bool CheckAns(int ans){
		// 正解なら.
		if (ans == m_box [m_nowAns].ans) {
			Debug.Log ("正解");
			DispButton (m_nowAns);
			m_nowAns++;
	
			// コンプリートなら.
			if (m_nowAns == m_maxQuestionNum) {
				m_complete = true;
			}
			// ステージクリアなら次のゲームへ.
			if (m_nowAns == m_nowQuestionNum) {
				for (int i=0; i<m_nowQuestionNum; i++) {
					HideButton (i);
				}
				/* ///// 生成したボタンを次の問題に保持するときのみ有効 /////
				m_clear = true;
				m_nowAns=0;
				m_nowQuestionNum++;
				m_create = false;
				*//////////////////////////////////////////////
	
				// ///// 毎回ランダムの場合.  /////
				int workQuest = m_nowQuestionNum;
				InitQuest ();
				m_clear = true;
				m_nowQuestionNum = workQuest + 1;
				//////////////////////////////////////////////
	
			}
				return true;
			} else {
				Debug.Log ("はずれ");
				return false;
			}
		}
	
	// ボタンの表示.
	public void DispButton(int i=-1){
		// 引数がない場合は全体表示
		if (i == -1) {
			m_QuestPanel.SetActive (true);
			return;
		}
		// GameObjectがない場合は処理しない.
		if (m_box [i].button == null)	return;
		m_box[i].button.SetActive(true);
	}
	// ボタンの非表示.
	public void HideButton(int i=-1){
		// 引数がない場合は全体非表示
		if (i == -1) {
			m_QuestPanel.SetActive (false);
		}
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
		m_QuestPanel.SetActive (true);
		m_create = false;
		m_clear = false;
		m_complete = false;
		m_nowAns = 0;
		m_nowQuestionNum = m_minQuestionNum;
		m_createWeight = 0.5f;
	}
}
