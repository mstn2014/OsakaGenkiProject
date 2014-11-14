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
}
