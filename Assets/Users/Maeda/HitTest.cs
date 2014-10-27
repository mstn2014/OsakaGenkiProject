using UnityEngine;
using System.Collections;

public class HitTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (Input.GetKey (KeyCode.Space))
		{
			print ("Good!!");
			Debug.Log("Good!!");
		}
	}
}
