using UnityEngine;
using System.Collections;

public class CollisonTrigger : MonoBehaviour {

	private BoxCollider2D playerCollider;

	private bool playeron;

	[SerializeField]

	private BoxCollider2D platformCollider;

	[SerializeField]

	private BoxCollider2D platformTigger;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () 

	{
		playerCollider = GameObject.Find ("Player").GetComponent < BoxCollider2D > ();
	
		Physics2D.IgnoreCollision (platformCollider, platformTigger , true);
	
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	void OnTriggerEnter2D (Collider2D other)

	{
		if (other.gameObject.name == "Player") 
		
		{
			
			Physics2D.IgnoreCollision (platformCollider, playerCollider, true);

			playeron = true;

		}

	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	 void OnTriggerExit2D (Collider2D other)

	{

		if (other.gameObject.name == "Player") 
		
		{

			Physics2D.IgnoreCollision (platformCollider, playerCollider, false);

			playeron = false;

		}

	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void FallThrough()

	{
		Physics2D.IgnoreCollision (platformCollider, playerCollider, true);

		Invoke ("ColliderCheck", 1f);
	}

	void ColliderCheck ()

	{
		if (playeron ==false)
			
		Physics2D.IgnoreCollision (platformCollider, playerCollider, false);

	}

}
