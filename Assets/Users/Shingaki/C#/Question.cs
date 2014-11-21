using UnityEngine;
using System.Collections;

public class Question : MonoBehaviour {
	// 生成するボタンと正解格納用の構造体.
	struct QuesBox{
		public GameObject 	button;
		public int			ans;		// 	0:初期   1:黄色   2:緑.
	}

	private int m_nowRound;				// 現在のラウンド.
	private int m_nowQuestNum;			// 現在のボタン数.
	private int m_nowAns;				// 今何ボタン目か.
	private float m_createWeight;		// 生成されるボタンの重み.
	private QuesBox[] m_box;			// 格納用配列.
	private bool m_create;				// 生成確認.
	private bool m_clear;				// ステージクリア確認.
	private bool m_complete;			// ゲームクリア確認.
	private GameObject m_panel;			// パネル(一番上の親).
	private GameObject m_textWindow;	// テキストウィンドウ(座標参照).
	private GameObject m_QuestPanel;	// 生成されるボタンの親.
	private GameObject m_Quest;			// 生成されるボタン.
	private EffectMgr m_effect;			// エフェクト.

	// Game1共通設定.
	private Game1_Setting GAME1;
	
	// get プロパティ.
	public bool IsCreate{			// 生成フラグ.
		get{return m_create;}
	}
	public bool IsClear{			// ラウンドクリアフラグ.
		get{return m_clear;}
	}
	public bool IsComplete{			// ゲーム終了フラグ(全問正解).
		get{return m_complete;}
	}
	public int IsNowAns{			// 何ボタン目答えている?.
		get{return m_nowAns;}
	}
	public int IsNowRound{			// ラウンド数取得.
		get{return m_nowRound;}
	}
	
	// Use this for initialization
	void Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		// TextWindowとQuestPanelの発見と生成.
		m_panel = GameObject.Find("Panel");
		m_textWindow = GameObject.Find("TextWindow");
		Vector3 position = m_textWindow.transform.localPosition;
		m_QuestPanel = Resources.Load("Shingaki/testResource/prefab/QuestPanel") as GameObject;
		m_Quest = Resources.Load("Shingaki/testResource/prefab/Quest") as GameObject;
		m_QuestPanel = CreatePrefab.InstantiateGameObject (m_QuestPanel, position, Quaternion.identity,
		                                                   Vector3.one, m_panel);
		m_effect = GameObject.Find ("GameMain").GetComponent<EffectMgr> ();

		// 表示されるボタンの数だけ配列生成.
		m_box = new QuesBox[GAME1.MaxQuestNum];
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
		for (int i=0; i<m_nowQuestNum; i++) {
			/* ///// 生成したボタンを次の問題に保持するときのみ有効 /////
			// もし生成済みの場合表示のみ.
			if(m_box[i].button != null){
				DispButton(i);
			}else */
			///////////////////////////////////////////////////////
			// ボタン生成と出現位置計算.
			interval = i*(-GAME1.QuestInterval)+(GAME1.QuestInterval/2)*(m_nowQuestNum-1);
			m_box[i].button = Instantiate(m_Quest) as GameObject;
			m_box[i].button.transform.parent = m_QuestPanel.transform;
			m_box[i].button.transform.localPosition = new Vector3(0,interval,0);
			GameObject button = m_box[i].button.transform.FindChild("Button").gameObject;
			GameObject label = m_box[i].button.transform.FindChild("Label").gameObject;
			UISprite sprite = button.GetComponent<UISprite>() as UISprite;
			UILabel text = label.GetComponent<UILabel>() as UILabel;

			if (Random.value < m_createWeight) {
				// 黄生成.
				// ToDo Spriteの名前は.
				// スクリプトとラベルの設定.
				sprite.spriteName = "Bright";
				text.text = ("password");
				m_box[i].ans = 1;
				m_createWeight -= GAME1.WeightValue;
			}else{
				// 緑生成.
				sprite.spriteName = "Smooth";
				text.text = ("pause");
				m_box[i].ans = 2;
				m_createWeight += GAME1.WeightValue;
			}
			
			// 生成後時間をおく.
			yield return new WaitForSeconds(1f);
		}
		yield return new WaitForSeconds(1f);
		for(int i=0; i<m_nowQuestNum; i++){
			HideButton(i);
		}
		m_create = true;
		m_clear = false;
	}
	
	//======================================================
	// @brief:答え合わせ.
	//------------------------------------------------------
	// @author: T.Shingaki
	// @param: int:回答	1:黄色	2:緑.
	// @return: bool:	true:正解	false:不正解.
	//======================================================
	public bool CheckAns(int ans){
		// 正解なら.
		if (ans == m_box [m_nowAns].ans) {
			Debug.Log ("正解");
			DispButton (m_nowAns);
			m_nowAns++;

			///// エフェクト /////
			m_effect.SpreadCircle(m_nowAns);

			int work = m_effect.IsComboNum;	// コンボ数表示.
			m_effect.IsComboNum = ++work;
            m_effect.DispCombo();
            
			
			// コンプリートなら.
			if (m_nowAns == GAME1.MaxQuestNum) {
				m_complete = true;
			}
			// ステージクリアなら次のゲームへ.
			if (m_nowAns == m_nowQuestNum) {
				HideButton();
				/* ///// 生成したボタンを次の問題に保持するときのみ有効 /////
				m_QuestPanel.SetActive (true);
				m_clear = true;w
				m_nowAns=0;
				m_nowQuestNum++;
				m_create = false;
				*//////////////////////////////////////////////
				
				// ///// 毎回ランダムの場合.  /////
				int workQuest = m_nowQuestNum;
				int workRound = m_nowRound;
				InitQuest ();
				m_clear = true;
				m_nowQuestNum	= workQuest + 1;
				m_nowRound		= workRound + 1;
				//////////////////////////////////////////////
			}
			return true;
		} else {
			Debug.Log ("はずれ");

			///// エフェクト /////
			m_effect.IsComboNum = 0;
			return false;
		}
	}
	
	// ボタンの表示.
	public void DispButton(int i=-1){
		// 引数がない場合は全体表示.
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
		// 引数がない場合は全体非表示.
		if (i == -1) {
			m_QuestPanel.SetActive (false);
			return;
		}
		// GameObjectがない場合は処理しない.
		if (m_box [i].button == null)	return;
		m_box[i].button.SetActive(false);
	}
	
	// 問題の初期化.
	public void InitQuest(){
		for (int i=0; i<GAME1.MaxQuestNum; i++) {
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
		m_nowRound = 1;
		m_nowAns = 0;
		m_nowQuestNum = GAME1.MinQuestNum;
		m_createWeight = 0.5f;
	}
}
