using UnityEngine;
using System.Collections;

// 適当に書いてるので綺麗に書き直してちょ
public class charactorController : MonoBehaviour {

    GameObject chara;
    GameObject[] child = new GameObject[4];
    Vector3[] pos = new Vector3[4];

    string[] material = { "red", "green", "blue", "yellow" };
	// Use this for initialization
	void Start () {
        // プレハブロード
        chara = Resources.Load<GameObject>("charactor");
        // タグ付けしたオブジェクトをまとめて取得
        child = GameObject.FindGameObjectsWithTag("charactor");
        for (int i = 0; i < 4;i++ )
        {
            pos[i] = child[i].transform.localPosition;
        }
        // 動くキャラクタを選択
        SelectMoveCharactor();
	}
	
	// Update is called once per frame
	void Update () {
        // ここは工夫してキャラクタを消したときだけ走るようにして（イベントドリブン）
        for(int i = 0;i<4;i++)
        {
            if( child[i] == null)
            {
                child[i] = CreatePrefab.InstantiateGameObject(chara,pos[i],Quaternion.identity,Vector3.one,GameObject.Find("charactorController"));
                child[i].renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
                SelectMoveCharactor();
            }
        }
	}

    void SelectMoveCharactor()
    {
        //　charactorスクリプトを追加してキャラクタとして動かす。
        // charactorスクリプトをつけておいてフラグを立ててもいいと思う。
        child[Random.Range(0, 4)].AddComponent<charactor>();
    }
}
