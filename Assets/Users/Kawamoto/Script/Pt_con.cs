using UnityEngine;
using System.Collections;

public class Pt_con : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void particle_On(string st){
		GameObject.Find(st).particleSystem.Play();
	}

	void particle_Off(string st){
		GameObject.Find(st).particleSystem.Stop();
	}
}
