using UnityEngine;
using System.Collections;

public class ResultSetting : ScriptableObject{
    [Header("最大ポイント")]
    public float game1MaxPoint = 100.0f;
    public float game2MaxPoint = 387.0f;
    public float game3MaxPoint = 300.0f;

    [System.Serializable]
    public class CRank
    {
        public string key;
        public float value;
    }

    public CRank[] game1Rank;
    public CRank[] game2Rank;
    public CRank[] game3Rank;
}
