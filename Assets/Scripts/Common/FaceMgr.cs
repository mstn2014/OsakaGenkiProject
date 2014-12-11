using UnityEngine;
using System.Collections;

public class FaceMgr : MonoBehaviour {

    [Header("顔のマテリアル")]
    public Material m_face; // 顔のマテリアル

    [Header("顔のテクスチャ")]
    public Texture[] m_texture;

    public enum eFaceType { NORMAL, PASS, SAD, SMILE };
    public eFaceType m_faceType;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeFace(eFaceType type)
    {
        m_face.mainTexture = m_texture[(int)type];
    }
}
