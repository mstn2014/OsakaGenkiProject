using UnityEngine;
using System.Collections;
//======================================================
// @brief:ヒットクラス(手前)
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Hit_Obj_Sub : MonoBehaviour {

	// セッティングファイル
	public Game3Setting Seting;

	//入力
	InputMgr input_bt;
	GameObject go;
	
	// コンポーネント用
	Game3_Mg 		gamemain;
	Game3_Mg_Sub 	gamemain_sub;
	Hit_Obj			hit;
	Char_SpeedMgr	charspeed_mgr;
	Score			score;
	
	// ヒットしている色
	int HitNum = 0;
	
	// Use this for initialization
	void Start () {
		
		// コンポーネントをゲット
		gamemain = GameObject.Find ("Parade").GetComponent<Game3_Mg> ();
		gamemain_sub = GameObject.Find ("Parade").GetComponent<Game3_Mg_Sub> ();
		hit = GameObject.Find ("Light_L").GetComponent<Hit_Obj> ();
		charspeed_mgr = GameObject.Find ("Speed_Manager").GetComponent<Char_SpeedMgr> ();
		score = GameObject.Find ("ScoreName").GetComponent<Score> ();

		// 共通設定の呼び出し
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		input_bt = gs.InputMgr;
	}
	
	// Update is called once per frame
	void Update () {
		// 当たっていないときにボタンを押すと飛ぶ
		if (input_bt.AnyButtonTrigger) {
			if (gamemain_sub.ObjFlagC () == 0 && gamemain.ObjFlagC () == 0 && 
			    GameObject.Find ("Sayonara_Line").transform.position.x > this.transform.position.x) {
				gamemain_sub.SayonaraObj();
				//charspeed_mgr.SpeedDown();
			}
		}
	}

	//======================================================
	// @brief:当たった場合の処理(間違っていたら速攻で飛ばす)
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:Collider other   自動で入る
	// @return:なし
	//======================================================
	private void OnTriggerEnter(Collider other){
		HitNum = 0;
		// フラグを立てる
		gamemain_sub.ObjHitOn ();
		
		if (other.gameObject.renderer.material.name == "Red (Instance)" || 
		    other.gameObject.renderer.material.name == "red") {
			HitNum = 1;
		}
		
		if (other.gameObject.renderer.material.name == "Green (Instance)" ||
		    other.gameObject.renderer.material.name == "green") {
			HitNum = 2;
		}
		
		if (other.gameObject.renderer.material.name == "Blue (Instance)" ||
		    other.gameObject.renderer.material.name == "blue") {
			HitNum = 3;
		}
		
		if (other.gameObject.renderer.material.name == "Yerrow (Instance)" ||
		    other.gameObject.renderer.material.name == "yerrow") {
			HitNum = 4;
		}
	}
	
	//======================================================
	// @brief:当たっている場合の処理(間違っていたら速攻で飛ばす)
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:Collider other   自動で入る
	// @return:なし
	//======================================================
	private void OnTriggerStay(Collider other)
	{
		//Debug.Log(other.gameObject.renderer.material.name);
		if (input_bt.RedButtonTrigger) { 
			if (HitNum == 1) {					
				charspeed_mgr.CountUp ();					// 難易度設定カウントアップ
				score.Count_Up (1 * Seting.PointPercent);	// スコアカウントアップ
				gamemain_sub.ObjInList (other.gameObject);	// リストに格納
				gamemain_sub.CharMoveOrder ();				// 新しいターゲットの選定								
			} else { 
				if (gamemain.ObjFlagC () == 2 && hit.GetNum () != 1) {
					gamemain_sub.SayonaraObj ();
					charspeed_mgr.SpeedDown ();	
				}							
			}
		}
		
		if (input_bt.GreenButtonTrigger) { 
			if (HitNum == 2) {							
				charspeed_mgr.CountUp ();					// 難易度設定カウントアップ
				score.Count_Up (1 * Seting.PointPercent);	// スコアカウントアップ
				gamemain_sub.ObjInList (other.gameObject);	// リストに格納
				gamemain_sub.CharMoveOrder ();				// 新しいターゲットの選定
			} else {								
				if (gamemain.ObjFlagC () == 2 && hit.GetNum () != 2) {
					gamemain_sub.SayonaraObj ();
					charspeed_mgr.SpeedDown ();	
				}						
			}
		}
		
		if (input_bt.BlueButtonTrigger) { 
			if (HitNum == 3) {								
				charspeed_mgr.CountUp ();					// 難易度設定カウントアップ
				score.Count_Up (1 * Seting.PointPercent);	// スコアカウントアップ
				gamemain_sub.ObjInList (other.gameObject);	// リストに格納
				gamemain_sub.CharMoveOrder ();				// 新しいターゲットの選定								
			} else {							
				if (gamemain.ObjFlagC () == 2 && hit.GetNum () != 3) {
					gamemain_sub.SayonaraObj ();
					charspeed_mgr.SpeedDown ();	
				}							
			}
		}
		
		if (input_bt.YellowButtonTrigger) { 
			if (HitNum == 4) {								
				charspeed_mgr.CountUp ();					// 難易度設定カウントアップ
				score.Count_Up (1 * Seting.PointPercent);	// スコアカウントアップ
				gamemain_sub.ObjInList (other.gameObject);	// リストに格納
				gamemain_sub.CharMoveOrder ();				// 新しいターゲットの選定								
			} else {								
				if (gamemain.ObjFlagC () == 2 && hit.GetNum () != 4) {
					gamemain_sub.SayonaraObj ();
					charspeed_mgr.SpeedDown ();	
				}
			}						
		}
	}
	
	//======================================================
	// @brief:何色に当たっているか
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:int 当たっている色
	//======================================================
	public int GetNum(){
		return HitNum;
	}
}
