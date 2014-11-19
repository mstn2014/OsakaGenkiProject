using UnityEngine;
using System.Collections;

public class Delete_Char : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerStay(Collider other)
	{
		Destroy(other.gameObject);
	}
}
