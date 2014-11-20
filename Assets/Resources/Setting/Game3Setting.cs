using UnityEngine;
using System.Collections;

public class Game3Setting : ScriptableObject {
	// 移動関係
	public float 	Speed = 0.1f;		// 横移動スピード
	public Vector3	L_L_Position;		// スポットライトポジション
	public Vector3	L_R_Position;		// スポットライトポジション
	public Vector3	RetPosition;		// 帰るときのポジション
	
	public string messagePath = "Message/game3";		// メッセージ用パス
}
