using UnityEngine;
using System.Collections;
//======================================================
// @brief:ゲームの流れを管理するクラス（ゲーム３）
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Game3_Flowing : MonoBehaviour {

	public Game3Setting Seting;		// セッティング
	InputMgr m_Input;				// 入力
	public GameObject go;			// メイド呼び出し用
	Guide m_Guide;					// ゲームガイド
	StartCountDown m_Count;			// カウント
	public GameObject m_MainFlug;	// ゲームメイン有効化
	GameObject m_Main2DFlug;		// ゲームメイン有効化(2D)

	// ステート
	enum Game3State{
		GUIDE,COUNTDOWN,GAME,END
	};
	Game3State m_state;

	// Use this for initialization
	void Start () {
		// 共通設定の呼び出し
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_Input = gs.InputMgr;

		// ガイド呼び出し
		m_Guide = go.GetComponent<Guide>();
		//m_Guide = GameObject.Find ("Guide").GetComponent<Guide>();
		m_Guide.Begin(Seting.messagePath);

		// カウント呼び出し
		m_Count = GameObject.Find ("Count").GetComponent<StartCountDown>();
		// ステート設定
		m_state = Game3State.GUIDE;

		// ゲームメイン設定
		//m_MainFlug = m_MainFlug.transform.FindChild("Char_Obj").gameObject;
		m_Main2DFlug = GameObject.Find ("Panel").transform.FindChild("GameUI").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_state) {
			case Game3State.GUIDE:
			Guide_State();
			break;

			case Game3State.COUNTDOWN:
			CountDown_State();
			break;
		}
	}

	//======================================================
	// @brief:ガイドステート
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	void Guide_State(){
		if(!m_Guide.IsUse) {
			m_state = Game3State.COUNTDOWN;		// ステート更新
			m_Count.Begin();					// カウントダウンをスタート
		}
	}

	//======================================================
	// @brief:カウントダウンステート
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	void CountDown_State(){
		if(m_Count.IsFinished){
			m_MainFlug.SetActive (true);	// ゲームを有効化
			m_Main2DFlug.SetActive (true);	// ゲームを有効化(2D)
			m_state = Game3State.GAME;		// ステート更新
		}
	}
}
