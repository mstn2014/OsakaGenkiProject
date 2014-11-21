using UnityEngine;
using System.Collections;

public class SaveData : ScriptableObject
{
    [Header("ユーザー名")]
    public string userName = "";
    [Header("スコア")]
    public float totalScore = 0;
    public float game1Score = 0;
    public float game2Score = 0;
    public float game3Score = 0;
    [Header("ユーザーランク")]
    public int userRank = 0;
    
    public enum eState{GAME1,GAME2,GAME3};
    [Header("ゲームステート")]
    public eState gameState;
}
