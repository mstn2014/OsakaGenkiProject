using UnityEngine;
using System.Collections;

public class Game3Setting : ScriptableObject {
	[Header("移動関係")]
	public float 	Speed = 0.1f;			// 横移動スピード
	public float 	DanceLimit = 3;			// 初期ダンス時間
	public float 	SpotMoveTime = 1.0f;	// スポットライトまでの移動時間
	public float	DeleteMoveTime = 4.0f;	// 削除位置までの移動時間

	[Header("難易度関係")]
	public int 		CountNum = 5;			// 何回成功でスピードを上げるか
	public float	MaxMoveSpeed = 0.6f;	// 移動スピードの上げる上限
	public float	MaxDanceSpeed = 0.9f;	// ダンススピードの上げる上限
	public int 	  	PointPercent = 1;		// 得点のパーセンテージ

	[Header("ファイルパス関係")]
	public string 	messagePath = "Message/game3";	// メッセージ用パス
}
