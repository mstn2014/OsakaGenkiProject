using UnityEngine;
using System.Collections;

public class Game1Setting : ScriptableObject {

	[Header("時間関連")]
	public float Round1_TimeLimit;
	public float Round2_TimeLimit;
	public float Round3_TimeLimit;
	public float Round4_TimeLimit;
	public float Round5_TimeLimit;
	public float Round6_TimeLimit;
	public float Round7_TimeLimit;
	public float Round8_TimeLimit;

	[Header("問題関連")]
	public int		MinQuestNum;
	public int		MaxQuestNum;
	public float	QuestInterval;
	public float	WeightValue;

	[Header("Objetc関連")]
	public int		GalleryNum;
	public float	Gallery_MinX;	// ギャラリーの最小のｘ座標
	public float	Gallery_MaxX;	// ギャラリーの最大のｘ座標
	public float	Gallery_MinZ;	// ギャラリーの最小のz座標
	public float	Gallery_MaxZ;	// ギャラリーの最大のz座標
	public float	Obj_Y;			// ギャラリーの高さ
	public float	Gallery_NG_Range;// (プレイヤーから見て)ギャラリーが生成されてはいけない範囲

	[Header("Effect関連")]
	public float FadeTime_combo;	// フェードアウトする時間
	public float MoveY_combo;		// 移動距離
}
