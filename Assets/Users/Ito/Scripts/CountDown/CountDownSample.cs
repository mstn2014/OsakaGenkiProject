using UnityEngine;
using System.Collections;

public class CountDownSample : MonoBehaviour {

    public GameObject countDown;
	// Use this for initialization
	void Start () {
        countDown.GetComponent<StartCountDown>().Begin();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
