using UnityEngine;
using System.Collections;

public class Game1EffectMgr : MonoBehaviour {

	private GameObject	m_circleParent;		// サークルの親.
	private GameObject	m_circle;			// サークル.
	private GameObject	m_oldcircle;		// 前回のサークル.
	private GameObject	m_comboText;		// combo表示.
	private GameObject	m_oldCombo;			// 古いコンボ.
	private int			m_comboNum;			// コンボ数.
	private GameObject	m_result;			// リザルト.
	private bool		m_effectPause;		// エフェクトポーズ
	private GameObject	m_panel;			// NGUIの親.
    public Game1MobMgr m_mobMgr;            // モブの管理（エフェクトも）
    public Game1CameraMove m_camera;
	
	// Game1共通設定.
	private Game1_Setting GAME1;

	// get プロパティ.
	public int IsComboNum{			// 生成フラグ.
		get{return m_comboNum;}
		set{m_comboNum = value;}
	}
	public bool IsPause{
		get{return m_effectPause;}
	}

	// Use this for initialization
	void Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		// リソースの読み込み.
		m_comboText = Resources.Load("Prefab/Game1/ComboText") as GameObject;
        m_circle = Resources.Load("Prefab/Game1/circle") as GameObject;
        m_result = Resources.Load("Prefab/Game1/ResultText") as GameObject;
		m_panel = GameObject.Find ("Panel");

		m_comboNum = 0;
		m_effectPause = false;

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
        float scaleNum = scalenum * GAME1.circle_Scale;
		// サークル作成	拡縮はこの前に作られたやつ参照.
		GameObject circle = CreatePrefab.InstantiateGameObject (m_circle, Vector3.zero, Quaternion.identity,
		                                                       scale, m_circleParent);
		circle.renderer.material.color = new Color(1f,0.1f*scaleNum, 0f, 1f);

		iTween.ScaleTo (circle, iTween.Hash ("x", scaleNum, "y", scaleNum, "time", GAME1.circle_ScaleTime));

        m_mobMgr.LookPlayer(scaleNum*1.65f);
        m_camera.Move(scalenum);

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
        m_comboNum = 0;
	}

	//======================================================
	// @brief:結果文字の表示.
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:none
	// @return:none
	//======================================================
	public IEnumerator DispResult(int type){
		m_effectPause = true;
		GameObject result;
		result = CreatePrefab.InstantiateGameObject (m_result, Vector3.zero, Quaternion.identity,
		                                            new Vector3 (GAME1.result_ScaleXY, GAME1.result_ScaleXY, -100), m_panel);
		UISprite workLabel = result.GetComponent<UISprite>();

		switch (type) {
		case 0:
			workLabel.spriteName = ("game1_perfect");
		    break;
		case 1:
			workLabel.spriteName = ("game1_miss");
			break;
		case 2:
			workLabel.spriteName = ("game1_timeup");
		    break;
		}

        workLabel.MakePixelPerfect();
        workLabel.transform.localScale *= 2.0f;

		// テキストの透過.
		TweenAlpha resultAlpha = result.GetComponent<TweenAlpha> ();
		resultAlpha.from = 1f;
		resultAlpha.to = 0f;
		resultAlpha.duration = GAME1.result_FadeTime;
		resultAlpha.Play (true);

		yield return new WaitForSeconds(GAME1.result_FadeTime); 

		m_effectPause = false;
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
		                                           new Vector3(GAME1.comboText_ScaleXY, GAME1.comboText_ScaleXY,0), m_panel);
		// comboの取得.
		UILabel comboNum;
		comboNum = combo.GetComponent ("UILabel") as UILabel;
		comboNum.text = m_comboNum.ToString()+("Combo!!");
		// ラベルの移動.
		iTween.MoveTo (combo, iTween.Hash ("y", GAME1.combo_MoveY, "time", GAME1.combo_FadeTime,"islocal",true));
		// ラベルの透過.
		TweenAlpha comboAlpha = combo.GetComponent<TweenAlpha> ();
		comboAlpha.from = 1f;
		comboAlpha.to = 0f;
		comboAlpha.duration = GAME1.combo_FadeTime;
		comboAlpha.Play (true);
		// 時間経過後削除.
		Destroy (combo, GAME1.combo_FadeTime);
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
        m_camera.Move(0);
	}





}
