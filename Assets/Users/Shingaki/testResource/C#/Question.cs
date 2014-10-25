using UnityEngine;
using System.Collections;

public class Question : MonoBehaviour {
	// 生成するボタンと正解格納用の構造体.
	struct QuesBox{
		public GameObject 	button;
		public int			ans;		// 	0:初期   1:黄色   2:緑
	}

	[Header("QuestionNum")]
	private int m_minQuestionNum = 3;	// 表示されるボタンの最小の数.
	private int m_maxQuestionNum = 10;	// 表示されるボタンの最大の数.
	private int m_nowQuestionNum;		// 現在のボタン番号.
	
	private float m_createWeight = 0.5f;// 生成されるボタンの重み.
	private const float m_value = 0.1f;	// m_createWeightの増減値.
	private QuesBox[] m_box;			// 格納用配列
	private bool m_create;				// 生成確認
	private GameObject m_panel;			// 生成されるボタンの親
	//public GameObject m_workBox;
	public GameObject m_yellow;			// 黄ボタンのprefab(Inspectorより設定)
	public GameObject m_green;			// 緑ボタンのprefab(Inspectorより設定)

	// get プロパティ.
	public int IsAns(int i){
		// 引数チェック
		if (i >= 0 && i < m_maxQuestionNum) {
			Debug.Log ("配列の添え字に適していません"+i);
			return 0;
		}
		return m_box[i].ans;
	}

	public int IsNowQuestNum{
		get{return m_nowQuestionNum;}
	}

	public bool createCheck{
		get{return m_create;}
	}

	// Use this for initialization
	void Start () {
		m_panel = GameObject.Find("Panel");
		m_nowQuestionNum = m_minQuestionNum;
		// 表示されるボタンの数だけ配列生成.
		m_box = new QuesBox[m_maxQuestionNum];
		m_create = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// 問題生成
	/*
	public void CreateQuestion(){
		// ボタンの数だけ配列生成.
		for (int i=0; i<m_nowQuestionNum; i++) {
			// もし生成済みの場合表示のみ.
			if(m_box[i].button != null){
				DispButton(i);
			}
			// ToDo ボタンの位置設定.
			else if (Random.value < m_createWeight) {
				// 黄色生成.
				m_box[i].button = CreatePrefab.InstantiateGameObject(m_yellow,Vector3.one,Quaternion.identity,
				                                                     new Vector3(100,100,1),m_panel);
				m_box[i].ans = 1;
				
				m_createWeight -= m_value;
			}else{
				// 緑生成.
				m_box[i].button = CreatePrefab.InstantiateGameObject(m_green,Vector3.one,Quaternion.identity,
				                                                     new Vector3(100,100,1),m_panel);
				m_box[i].ans = 2;
				m_createWeight += m_value;
			}

			// 生成後時間をおく
			for(int wait=0; wait<60; wait++){

			}
		}
	}
	*/
	public IEnumerator CreateQuestion(){
		// ボタンの数だけ配列生成.
		for (int i=0; i<m_nowQuestionNum; i++) {
			// もし生成済みの場合表示のみ.
			if(m_box[i].button != null){
				DispButton(i);
			}
			// ToDo ボタンの位置設定.
			else if (Random.value < m_createWeight) {
				// 黄色生成.
				m_box[i].button = CreatePrefab.InstantiateGameObject(m_yellow,Vector3.one,Quaternion.identity,
				                                                     new Vector3(100,100,1),m_panel);
				m_box[i].ans = 1;
				
				m_createWeight -= m_value;
			}else{
				// 緑生成.
				m_box[i].button = CreatePrefab.InstantiateGameObject(m_green,Vector3.one,Quaternion.identity,
				                                                     new Vector3(100,100,1),m_panel);
				m_box[i].ans = 2;
				m_createWeight += m_value;
			}
			
			// 生成後時間をおく
			yield return new WaitForSeconds(1f);
		}
		m_create = true;
	}

	// ボタンの表示.
	void DispButton(int i){
		m_box[i].button.SetActive(true);
	}
	// ボタンの非表示.
	void HideButton(int i){
		m_box[i].button.SetActive(false);
	}

	// 問題の初期化.
	void QuestInit(){
		for (int i=0; i<m_maxQuestionNum; i++) {
			if(m_box[i].button != null){
				Destroy(m_box[i].button);
				m_box[i].button = null;
				m_box[i].ans = 0;
			}
		}
	}
}
