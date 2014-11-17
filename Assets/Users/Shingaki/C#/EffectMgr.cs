using UnityEngine;
using System.Collections;

public class EffectMgr : MonoBehaviour {

	private GameObject	m_circleParent;		// サークルの親.
	private GameObject	m_circle;			// サークル.
	private GameObject	m_oldcircle;		// 前回のサークル.
	private GameObject	m_comboText;		// combo表示.
	private GameObject	m_oldCombo;			// 古いコンボ.
	private int			m_comboNum;			// コンボ数.
	private GameObject	m_panel;			// NGUIの親.
	
	// Game1共通設定.
	private Game1_Setting GAME1;

	// get プロパティ.
	public int IsComboNum{			// 生成フラグ.
		get{return m_comboNum;}
		set{m_comboNum = value;}
	}

	// Use this for initialization
	void Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		// リソースの読み込み.
		m_comboText = Resources.Load("Shingaki/testResource/prefab/ComboText") as GameObject;
		m_circle = Resources.Load("Shingaki/testResource/prefab/circle") as GameObject;
		m_panel = GameObject.Find ("Panel");

		m_comboNum = 0;
	}
	
	// Update is called once per frame
	void Update () {}

	//======================================================
	// @brief:サークル拡大.
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:none
	// @return:none
	//======================================================
	public void SpreadCircle(int scalenum){
		Vector3 scale;
		if (m_oldcircle != null) {
			scale = m_oldcircle.transform.localScale;
		} else {
			scale = Vector3.zero;
		}
		// サークル作成	拡縮はこの前に作られたやつ参照.
		GameObject circle = CreatePrefab.InstantiateGameObject (m_circle, Vector3.zero, Quaternion.identity,
		                                                       scale, m_circleParent);
		circle.renderer.material.color = new Color(1f,0.1f*scalenum, 0f, 1f);

		iTween.ScaleTo (circle, iTween.Hash ("x", scalenum, "y", scalenum, "time", GAME1.ScaleTime_circle));

		m_oldcircle = circle;
	}

	// サークルの初期化.
	public void InitCircle(){
		if (m_circleParent != null) {
			Destroy (m_circleParent);
			m_circleParent = null;
		}
		m_circleParent = new GameObject("CirclParent");
		m_circleParent.transform.localRotation = Quaternion.Euler (new Vector3 (90, 0, 0));
		m_circleParent.transform.localPosition = new Vector3 (0,0.1f,0);

		m_oldcircle = null;
	}

	// 「パーフェクト」「タイムアップ」「失敗」の文字表示.
	private void DispResult(){

	}

	//======================================================
	// @brief:コンボの表示.
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:none
	// @return:none
	//======================================================
	public void DispCombo(){
		GameObject combo;
		combo = CreatePrefab.InstantiateGameObject (m_comboText, Vector3.zero, Quaternion.identity,
		                                           new Vector3(GAME1.ScaleXY_comboText, GAME1.ScaleXY_comboText,0), m_panel);
		// comboの取得.
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
		comboAlpha.Play (true);
		// 時間経過後削除.
		Destroy (combo, GAME1.FadeTime_combo);
	}

	//======================================================
	// @brief:エフェクト関連初期化.
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:none
	// @return:none
	//======================================================
	public void InitEffect(){
		InitCircle();	// サークル初期化.

	}





}
