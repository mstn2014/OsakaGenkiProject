using UnityEngine;
using System.Collections;

public class Game1_Setting : ScriptableObject {

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
	public int MinQuestNum;
	public int MaxQuestNum;
	public float QuestInterval;
	public float WeightValue;

}
