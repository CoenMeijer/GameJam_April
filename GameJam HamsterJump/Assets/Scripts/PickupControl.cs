using UnityEngine;
using System.Collections;

public class PickupControl : MonoBehaviour {

	//--Properties--//

	public Transform[] PickupPositions;
	public GameObject pickup;

	public float pickupCount;
	
	void Start()
	{
		Instantiate(pickup, PickupPositions[Random.Range(0,7)].position, Quaternion.identity);
	}

	public void Spawn() 
	{
		Instantiate(pickup, PickupPositions[Random.Range(0,7)].position, Quaternion.identity);
	}
}
