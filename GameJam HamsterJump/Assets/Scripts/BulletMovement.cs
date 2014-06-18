using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour
{
    //-- Vars --//
    
	//Speed
	private float _speed = 5;

	void Start ()
    {
	}

	void Update () 
	{
		// Movement
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.Self);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// Player Collision
		if (col.gameObject.tag == "Player")
        {
            // Remove Bullet
			Destroy(gameObject);
            // Do Damage
            col.gameObject.GetComponent<PlayerMovement>().Hit(1);
		}
        // Platform Collision
        if (col.gameObject.tag == "Floor")
        {
            // Remove Bullet
            Destroy(gameObject);
        }
	}
}
