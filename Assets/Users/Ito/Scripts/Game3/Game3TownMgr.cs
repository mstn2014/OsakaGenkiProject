using UnityEngine;
using System.Collections;

public class Game3TownMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= -100.0f)
        {
            /*GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/Game3/Town")) as GameObject;
            go.transform.parent = transform.parent;
            go.transform.position = new Vector3(transform.position.x + 37.2f*3, transform.position.y, transform.position.z);
            Destroy(this.gameObject);*/
            transform.position = new Vector3(transform.position.x + 36.86f * 3, transform.position.y, transform.position.z);
        }	
	}
}
