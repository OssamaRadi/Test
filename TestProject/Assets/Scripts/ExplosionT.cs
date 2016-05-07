using UnityEngine;
using System.Collections;

public class ExplosionT : MonoBehaviour {


	public GameObject explosion;

	void Update () 

	{

	}

	void OnTriggerEnter2D (Collider2D other)

	{
		if (other.gameObject.tag == "BulletTag") 

		{
		
			Explode ();

		}

	}

	void Explode ()

	{

		Instantiate (explosion, transform.position, Quaternion.identity);

		gameObject.SetActive (false);


	}
}
