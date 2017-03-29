
/*
using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float paddleSpeed = 1f;


	private Vector3 playerPos = new Vector3 (0, -9.5f, 0);//sets the position of the paddle

	void Update () 
	{
		float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);//x axis position, transform adds input to the paddel
		//The horizontal axis is default that listens to left and right arrow keys and the a and d keys. Mulpily it by paddle speed so input adds
		//to the x posistion
		playerPos = new Vector3 (Mathf.Clamp (xPos, -8f, 8f), -9.5f, 0f);//mathf.clamp limits a number between certain values. Limits how far 
		//the paddle can move along the x axis.
		transform.position = playerPos;//sets the position based on the input to the keyboard.

	}
}
*/

using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
	public float paddleSpeed = 1f;

	private Vector3 playerPos = new Vector3(0, -9.5f, 0);

   

	void Update()
	{
		float xPos = transform.position.x;
		if (KinectManager.instance.IsAvailable)
		{
			xPos = KinectManager.instance.PaddlePosition;
		}
		else
		{
			xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
		}

		playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -9.5f, 0f);

		transform.position = playerPos;
	}
}

