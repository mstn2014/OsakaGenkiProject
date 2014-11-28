using UnityEngine;
using System.Collections;
//======================================================
// @brief:タイムカウントクラス
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Time_Count : MonoBehaviour {

	UILabel Time_text;			// タイムテキスト
	string time;				// 数値を一時的に格納しておく場所
	float TimeCount = 0;		// カウント用
	
	// Use this for initialization
	void Start () {
		Time_text = GameObject.Find ("Time").GetComponent<UILabel>();
		time = Time_text.text;
	}
	
	// Update is called once per frame
	void Update () {
		TimeCount += Time.deltaTime;
		if (TimeCount >= 1) {
			TimeCount = 0;
			Count_Down(1);
		}
	}
	
	//======================================================
	// @brief:カウントダウン
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:int point  下げる数値
	// @return:なし
	//======================================================
	void Count_Down(int point){
		// トータルスコアをintで計算
		int to_time = System.Convert.ToInt32(time) - point;
		// 数字を文字列型に変換
		time = to_time.ToString();
		// 最後に数字以外は0を埋める
		Time_text.text = time.PadLeft(3, '0');
	}
}
