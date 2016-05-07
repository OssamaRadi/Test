using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour {


	[SerializeField]

	private float bSpeed;

	private Rigidbody2D myRigidbody;

	private Vector2 direction;

	// Use this for initialization

	void Start () 

	{
		
		myRigidbody = GetComponent <Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () 

	{

		myRigidbody.velocity = direction * bSpeed;
	
	}
		 
	void OnBecameInvisible()

	{

		Destroy (gameObject);

	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void Initialize (Vector2 direction)

	{

		this.direction = direction;

	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Update () 

	{

	

	}
}
