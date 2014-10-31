using UnityEngine;
using System.Collections;

public class SaveData : ScriptableObject
{
    [Header("ユーザー名")]
    public string userName = "";
    [Header("スコア")]
    public int userScore = 0;
    [Header("ユーザーランク")]
    public int userRank = 0;
}
