using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	// Game1共通設定.
	private Game1_Setting GAME1;

	// Use this for initialization
	void Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 viwePos = Camera.main.WorldToViewportPoint (transform.position);
		if (viwePos.x > 2.0f) {
			/*Vector3 workPos;
			
			workPos.x = transform.localPosition.x+GAME1.Ground_PositionX*2;
			workPos.y = 0f;
			workPos.z = transform.localPosition.z;
			transform.localPosition = workPos;	*/
            Destroy(this.gameObject);
		}
	}
}
