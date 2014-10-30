using UnityEngine;
using System.Collections;

public class UserRegSetting : ScriptableObject
{
    [Header("プレハブPath")]
    public string prefabPath = "CharPrefab";
    [Header("使用文字セット")]
    public string userChar = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをんぁぃぅぇぉゃゅょゎっ";
    [Header("初期文字インデックス")]
    public int initChar = 1;
    [Header("Tweenパラメーター")]
    public float duration = 1.0f;
    public Vector3[] position = new Vector3[7];
    public Vector3[] scale = new Vector3[7];
}
