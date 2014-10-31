using UnityEngine;
using System.Collections;

public class RankingSetting : ScriptableObject
{
    [Header("サーバーURL")]
    public string postURL = "http://mstn2014-osaka.herokuapp.com/users/regist";
    public string getURL = "http://mstn2014-osaka.herokuapp.com/users/score";
    [Header("必ず表示するランク")]
    public int dispRank = 5;
    
}
