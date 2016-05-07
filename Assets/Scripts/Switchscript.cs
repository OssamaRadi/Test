using UnityEngine;
using System.Collections;

public class Switchscript : MonoBehaviour {

	public Animator switchAnimator;

	public Doorscript script;

	private Animator doorAnim;

	public GameObject door;

	public Animator doorAnimator;

	public bool playeron;

	public bool sOn;


	private static Switchscript instance;

	public static Switchscript Instance
	{

		get

		{ 
			if (instance == null) 

			{

				instance = GameObject.FindObjectOfType <Switchscript> ();

			}

			return instance; 

		}

	}

	public bool Onoff { get; set; }

	void Start ()

	{  

		switchAnimator = GetComponent <Animator> ();

		doorAnim = script.GetComponent <Animator> ();

		sOn = false;

		playeron = false;

	}

	void Update ()

	{
		 
		if (Input.GetKeyDown (KeyCode.E)) 
		
		{

			if (playeron == true)

				{
				
					switchAnimator.SetTrigger ("Onoff");

				doorAnim.SetTrigger ("Opened");

				}

		}

	}


	void OnTriggerEnter2D (Collider2D other)


	{
		if (other.gameObject.name == "Player") {

			playeron = true;

		} 


		else 
		

		{

			{playeron = false;}
	
		}


	}
	
		void OnTriggerExit2D (Collider2D other)
		{

			if (other.gameObject.name == "Player") 

		{
			playeron = false;
		}

		}
}


