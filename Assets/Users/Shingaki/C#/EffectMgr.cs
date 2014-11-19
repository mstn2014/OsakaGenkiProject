using UnityEngine;
using System.Collections;

public class EffectMgr : MonoBehaviour {

	private GameObject	m_topCircle;		// サークル
	private GameObject	m_target;
	private GameObject	m_comboText;		// combo表示.
	private int			m_comboNum;			// コンボ数
	private GameObject	m_panel;			// NGUIの親
	
	// Game1共通設定
	private Game1Setting GAME1;

	// get プロパティ.
	public int IsComboNum{			// 生成フラグ
		get{return m_comboNum;}
		set{m_comboNum = value;}
	}

	// Use this for initialization
	void Start () {
		// Game1共通設定
		GAME1 = Resources.Load<Game1Setting>("Setting/Game1_Setting");

		// リソースの読み込み
		m_topCircle = Resources.Load("Shingaki/testResource/prefab/Circle") as GameObject;
		m_comboText = Resources.Load("Shingaki/testResource/prefab/ComboText") as GameObject;
		m_panel = GameObject.Find ("Panel");

		m_comboNum = 0;
		Quaternion angle = new Quaternion();
		angle.eulerAngles  = new Vector3 (90, 0, 0);
		m_topCircle = Instantiate (m_topCircle, new Vector3 (0, 0.1f, 0), angle) as GameObject; 
		InitCircle ();
	}
	
	// Update is called once per frame
	void Update () {}

	// 正解を受け取るとサークルが広がる.
	public void SpreadCircle(){
		iTween.ScaleTo (m_target, iTween.Hash ("x",1.5, "y",1.5, "time",1.0f));
		// 子オブジェクトへ.
		// ToDo 最後の輪の場合探さない追加
		if (m_target.transform.childCount>0) {
			Debug.Log("true");
			m_target = m_target.transform.FindChild ("circle").gameObject;
		}

	}

	// サークルの初期化.
	public void InitCircle(){
		// サークルを隠す
		GameObject init;
		m_topCircle.transform.localScale = Vector3.zero;
		init = m_topCircle;
		//m_target = m_topCircle.gameObject;
		while (init.transform.childCount>0) {
			init = init.transform.FindChild ("circle").gameObject;
			init.transform.localScale = Vector3.zero;
		}
		m_target = m_topCircle.gameObject;

	}

	// 「パーフェクト」「タイムアップ」「失敗」の文字表示.
	private void DispResult(){

	}

	// コンボ数表示
	public void DispCombo(){
		GameObject combo;
		combo = CreatePrefab.InstantiateGameObject (m_comboText, Vector3.zero, Quaternion.identity,
		                                           new Vector3(75, 75,0), m_panel);
		// comboの取得
		UILabel comboNum;
		comboNum = combo.GetComponent ("UILabel") as UILabel;
		comboNum.text = m_comboNum.ToString()+("Combo!!");
		// ラベルの移動.
		iTween.MoveTo (combo, iTween.Hash ("y", GAME1.MoveY_combo, "time", GAME1.FadeTime_combo,"islocal",true));
		// ラベルの透過.
		TweenAlpha comboAlpha = combo.GetComponent<TweenAlpha> ();
		comboAlpha.from = 1f;
		comboAlpha.to = 0f;
		comboAlpha.duration = GAME1.FadeTime_combo;
		// 時間経過後削除
		Destroy (combo, GAME1.FadeTime_combo);
	}
}
