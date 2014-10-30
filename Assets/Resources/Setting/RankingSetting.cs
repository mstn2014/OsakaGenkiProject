using UnityEngine;
using System.Collections;

public class RankingSetting : ScriptableObject
{
    [Header("サーバーURL")]
    public string postURL = "http://mstn2014-osaka.herokuapp.com/users/regist";
    public string getURL = "http://mstn2014-osaka.herokuapp.com/users/score";
    [Header("１位の高さ")]
    public float firstHeight = 300.0f;
    [Header("縦のピッチ")]
    public float heightPitch = 10.0f;
    [Header("順位のX座標")]
    public float rankXpos = -630.0f;
    [Header("名前のX座標")]
    public float nameXpos = -430.0f;
    [Header("スコアのX座標")]
    public float scoreXpos = 0.0f;
}
