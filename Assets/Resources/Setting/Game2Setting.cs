using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game2Setting : ScriptableObject
{
    [Header("ファイルパス関連")]
    public string messageTextPath;

    [Header("ボタンタイプ")]
	public string[] JapanButtounType = { "blue","red"};
	public string[] AmericaButtounType = { "blue","red","green"};
    public string[] TurkeyButtounType = { "blue", "red", "green" };
    public string[] BrazilButtounType = { "yellow","blue", "red", "green" };
    public string[] SpainButtounType = { "yellow", "blue", "red", "green" };

    [Header("ボタンパターン")]
    public int[] JapanPattern = {
                                    1,0,1,0,1,0,0,0,
                                    1,1,0,0,1,0,0,0,
                                    0,0,0,0,0,0,0,0
                                };
    public int[] AmericaPattern = {
                                    1,0,0,1,0,0,1,1,
                                    1,0,0,1,0,0,1,0,
                                    0,0,0,0,0,0,0,0
                                };
    public int[] TurkeyPattern = {
                                    1,0,1,1,0,0,1,0,
                                    1,1,0,0,1,0,1,0,
                                    0,0,0,0,0,0,0,0
                                };
    public int[] BrazilPattern = {
                                    1,1,0,1,0,1,1,0,
                                    1,0,0,1,1,1,0,1,
                                    0,0,0,0,0,0,0,0
                                };
    public int[] SpainPattern = {
                                    1,0,1,1,1,0,1,1,
                                    1,0,1,1,0,1,0,1,
                                    0,1,0,0,0,0,0,0
                                };

    [Header("ボタン生成間隔")]
    public float createButtonTime = 1.0f;
    public float difficulty = 0.8f;

    [Header("判定関連")]
    public float safeRange = 0.3f;	    //	セーフの範囲
    public float goodRange = 0.2f;	    //	グッドの範囲
    public float perfectRange = 0.1f;	//	パーフェクトの範囲
    public float playDistance = 10.0f;  // 判定の遊びの部分

    [Header("判定表示")]
    public float dispTime = 0.3f;	    //	表示する時間

    [Header("得点関連")]
    public float perfectPoint = 3.0f;
    public float goodPoint = 2.0f;
    public float safePoint = 1.0f;
    public float maxPoint = 387.0f;
    [System.Serializable]
    public class CRank
    {
        public string key;
        public float value;
    }
    public CRank[] rank;

	[Header("盛り上がりイベントに必要なスコア")]
	public int Event1Score = 5;	    //	盛り上がりイベント１に必要なスコア
	public int Event2Score = 20;	//	盛り上がりイベント２に必要なスコア
	public int Event3Score = 40;	//	盛り上がりイベント３に必要なスコア
	public int Event4Score = 60;	//	盛り上がりイベント４に必要なスコア
	public int Event5Score = 80;	//	盛り上がりイベント５に必要なスコア

	[Header("盛り上がりイベントの参加者人数")]
	public int Event1Guest = 3;	    //	盛り上がりイベント１に増える参加人数
	public int Event2Guest = 5;	//	盛り上がりイベント２に増える参加人数
	public int Event3Guest = 10;	//	盛り上がりイベント３に増える参加人数
	public int Event4Guest = 10;	//	盛り上がりイベント４に増える参加人数
	public int Event5Guest = 20;	//	盛り上がりイベント５に増える参加人数
}
