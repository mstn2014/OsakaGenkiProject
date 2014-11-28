using UnityEngine;
using System.Collections;

public class Game3MoveObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(-0.02f, 0, 0));
	}
}
