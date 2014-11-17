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
	public int		GalleryNum;
	public int		Gallery_SomeNum;// 分けられた一部に生成されるギャラリーの数.
	public float	Gallery_MinX;	// ギャラリーの最小のｘ座標.
	public float	Gallery_MaxX;	// ギャラリーの最大のｘ座標.
	public float	Gallery_Width;	// NG_Rangeからの増え幅:X
	public float	Gallery_MinZ;	// ギャラリーの最小のz座標.
	public float	Gallery_MaxZ;	// ギャラリーの最大のz座標.
	public float	Obj_Y;			// オブジェの高さ.
	public float	Gallery_NG_Range;// (プレイヤーから見て)ギャラリーが生成されてはいけない範囲.
	public int		Gallery_DivNum;

	[Header("Effect関連")]
	public float comboText_ScaleXY;	// ラベルのScale.
	public float combo_FadeTime;	// フェードアウトする時間.
	public float combo_MoveY;		// 移動距離.
	public float circle_ScaleTime;	// サークルの拡大時間.
}
