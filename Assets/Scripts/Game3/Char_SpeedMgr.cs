using UnityEngine;
using System.Collections;

//======================================================
// @brief:キャラをスピードを管理するクラス(難易度設定)
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Char_SpeedMgr : MonoBehaviour {

	// セッティングファイルに移行する変数多数あり
	float Speed_velo = 0;
	float Dans_Speed = 0;
	int	  Count = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//======================================================
	// @brief:速度をアップ
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	public void CountUp(){
		Count++;
		if (Count == 5) {
			if(Speed_velo < 0.6f)	Speed_velo += 0.1f;
			if(Dans_Speed < 1.0f)	Dans_Speed += 0.1f;
			Count = 0;
		}
		Debug.Log("速度" + Speed_velo);
		Debug.Log("ダンス速度" + Dans_Speed);
	}
	
	//======================================================
	// @brief:速度をダウン
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	public void SpeedDown(){
		if (Speed_velo > 0.0f)		Speed_velo -= 0.1f;
		if (Dans_Speed > 0.0f)		Dans_Speed -= 0.1f;
	}
	
	//======================================================
	// @brief:速度ゲット(移動)
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:float    移動スピード
	//======================================================
	public float GetSpeed(){
		return Speed_velo;
	}
	
	//======================================================
	// @brief:速度ゲット(ダンス)
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:float    ダンススピード
	//======================================================
	public float GetDansSpeed(){
		return Dans_Speed;
	}
}
