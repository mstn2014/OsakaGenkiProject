using UnityEngine;
using System.Collections;

//======================================================
// @brief:スコアクラス
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Score : MonoBehaviour {

	UILabel Score_text;			// スコアラベル
	string score;				// スコアを一時的に格納する場所
	
	// Use this for initialization
	void Start () {
		Score_text = GameObject.Find ("Score").GetComponent<UILabel>();
		score = Score_text.text;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//======================================================
	// カウントを上げる
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:int point 上げたいポイント数
	// @return:なし
	//======================================================
	public void Count_Up(int point){
		// トータルスコアをintで計算
		int to_score = System.Convert.ToInt32(score) + point;
		// 数字を文字列型に変換
		score = to_score.ToString();
		// 最後に数字以外は0を埋める
		Score_text.text = score.PadLeft(6, '0');
	}
}
