using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float ballInitialVelocity = 600f;//inital burst of force to start the ball in game.


	private Rigidbody rb;//
	private bool ballInPlay;

	void Awake () {

		rb = GetComponent<Rigidbody>();

	}

	//Check for input
	void Update () 
	{
		if (Input.GetButtonDown("Fire1") && ballInPlay == false)//fire1 is the left key or left mouse
			//if ball has not been launched
		{
			transform.parent = null;//fires ball around the level
			ballInPlay = true;
			rb.isKinematic = false;
			rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));//sends the ball in the direction of x and y
		}
	}
}