using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game1_Setting : ScriptableObject {

	[Header("時間関連")]
	public List<float> Round_TimeLimit;

	[Header("問題関連")]
	public int		MinQuestNum;
	public int		MaxQuestNum;
	public float	QuestInterval;
	public float	WeightValue;

	[Header("Objetc関連")]
	public int		Gallery_Width;		// ギャラリーが生成される列数
	public int		Gallery_Height;		// ギャラリーが生成される行数
	public float	Gallery_Interval;	// Galleryの間隔
	public float	Gallery_Roll;		// ギャラリーの揺れ幅
	public float	Obj_Y;				// オブジェの高さ.
	public float	Gallery_NG_Range;	// (プレイヤーから見て)ギャラリーが生成されてはいけない範囲.
	public float	Ground_PositionX;	// 地面の座標
	public float	Player_WolkTime;	// プレイヤーの移動スピード

	[Header("Effect関連")]
	public float comboText_ScaleXY;	// ラベルのScale.
	public float combo_FadeTime;	// フェードアウトする時間.
	public float combo_MoveY;		// 移動距離.
	public float circle_ScaleTime;	// サークルの拡大時間.
	public float result_ScaleXY;	// リザルトScale.
	public float result_FadeTime;	// フェードアウトする時間
}
