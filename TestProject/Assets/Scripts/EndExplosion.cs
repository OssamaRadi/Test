using UnityEngine;
using System.Collections;

public class EndExplosion : MonoBehaviour {

	void Start ()

	{
	
		Invoke ("Deleteme", 0.5f);

	}


	void Deleteme ()

	{

		gameObject.SetActive (false);

	}

}
