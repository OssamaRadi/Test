using UnityEngine;

using System.Collections;


public class Player : MonoBehaviour 

{

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Callouts

    

	private static Player instance;

	public static Player Instance

	{

		get

		{ 
			if (instance == null) 

			{

				instance = GameObject.FindObjectOfType <Player> ();

			}

			return instance; 

		}

	}


	private Animator myAnimator;

	[SerializeField]

	private Transform muzzleHole;


	[SerializeField]

	private GameObject Bullet1;


	[SerializeField]

	private LayerMask whatIsGround; 

	[SerializeField]

	private Transform [] groundPoints;


	private RaycastHit2D currentPlat; 

	public  Rigidbody2D MyRigidbody { get; set;}

	public bool Jump { get; set; }

	public bool OnGround { get; set; }

	public bool Attack { get; set; }

	public float onair;


	//Floats

	[SerializeField]

	private float speed;

	[SerializeField]

	private float groundRadius;


	[SerializeField]
	private float jumpForce;

	private float canjump;


	//Bools

	private bool facingR;


	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	void Start () 
	{
		MyRigidbody = GetComponent <Rigidbody2D> ();

		myAnimator = GetComponent <Animator> ();

		facingR = true;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Update ()

	{

		HandleInput(); 



		if (MyRigidbody.velocity.y < 0) 

		{
			onair = 1;
		}

		else 
		{
			onair = 0;
		}



	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void FixedUpdate () 

	{

		float horizontal = Input.GetAxis ("Horizontal");

		OnGround = IsGrounded ();

		HandleMovements(horizontal);

		HandleLayers ();

		Flip (horizontal);


	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	private void HandleMovements (float horizontal) 

	{

		if (MyRigidbody.velocity.y < 0) 

		{

			myAnimator.SetBool ("land", true);

		}

		if (!Attack ) 
		{

			MyRigidbody.velocity = new Vector2 (horizontal * speed, MyRigidbody.velocity.y);

		}

		if (Jump && !Attack) 

		{
			if (canjump == 2f)
			
			{
				
				MyRigidbody.AddForce (new Vector2 (0, jumpForce)); 

				canjump -= 1;
			}


		}

		myAnimator.SetFloat ("speed", Mathf.Abs (horizontal));
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void HandleInput()
	{


		if (Input.GetMouseButtonDown(0))

		{

			myAnimator.SetTrigger ("attack");

		}


		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W))
		{

			myAnimator.SetTrigger ("Jump");

			if  (canjump ==1)

			{

				MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x , jumpForce/105);

				canjump = 0f;

			};

		}

		if (Input.GetMouseButtonDown(1))

		{

			myAnimator.SetTrigger ("shoot");

		}

		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) 

		{
			foreach (Transform point in groundPoints)
				
				currentPlat = Physics2D.CircleCast (point.position , groundRadius, Vector2.down, 0.1f, whatIsGround);

			if (currentPlat.collider) 

			{

				if (currentPlat.transform.tag == "Main") 

				{

					currentPlat.transform.GetComponent<CollisonTrigger> ().FallThrough ();

				}

			}
		}

	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	private bool IsGrounded ()

	{
		if (MyRigidbody.velocity.y <= 0)

		{ 

			foreach (Transform point in groundPoints)

			{ 

				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position,groundRadius,whatIsGround);

				for  (int i = 0; i < colliders.Length; i++ )

					if (colliders[i].gameObject != gameObject)

					{
						canjump = 2;

						return true;

					}
			}

		} 


		return false;

	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void HandleLayers()

	{

		if (!OnGround)

		{
			myAnimator.SetLayerWeight(1,1);

		}
		else

		{

			myAnimator.SetLayerWeight (1, 0);

		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void  Flip (float horizontal)
	{

		if (horizontal > 0 && !facingR && !Attack || horizontal < 0 && facingR && !Attack) 

		{
			facingR = !facingR;

			Vector3 theScale = transform.localScale;

			theScale.x *= -1;

			transform.localScale = theScale;
		}

	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void ShootB (int value)

	{

		if (!OnGround && value == 1 || OnGround && value == 0) 

		{

			if (facingR) 

			{

				GameObject tmp =(GameObject) Instantiate (Bullet1, muzzleHole.position, Quaternion.identity);

				tmp.GetComponent <Bullet> ().Initialize (Vector2.right);
			}

			else 

			{

				GameObject tmp =(GameObject) Instantiate (Bullet1, muzzleHole.position, Quaternion.Euler (new Vector3(0,0,180)));

				tmp.GetComponent <Bullet>().Initialize(Vector2.left) ;
			}


		}


	}



}
