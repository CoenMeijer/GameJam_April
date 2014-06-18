using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {
    
	public float delay;
	public float delayLeft;

	public GameObject bulletType;

	void Start () {
		//delayLeft = delay;
	}

	void Update () {
		delayLeft -= Time.deltaTime;

		if(delayLeft <= 0){
			GameObject bull = Instantiate(bulletType, gameObject.transform.position, transform.rotation) as GameObject;
			Destroy(bull, 3);
			delayLeft = delay;
		}
	}
}
