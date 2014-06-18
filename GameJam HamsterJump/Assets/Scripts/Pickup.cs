using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	private PickupControl control;

	void Start()
	{
		control = GameObject.Find("GameControl").GetComponent<PickupControl>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			control.Spawn();
			other.gameObject.GetComponent<PlayerMovement>()._pickUpCount++;
			Destroy(gameObject);
		}
	}
}
