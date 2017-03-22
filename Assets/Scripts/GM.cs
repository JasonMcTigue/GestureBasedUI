using UnityEngine;
using System.Collections;
using UnityEngine.UI;//Accesing members of the UI namespace

public class GM : MonoBehaviour {

	public int lives = 3;//Number of lives
	public int bricks = 20;//Number of bricks in the game
	public float resetDelay = 1f;//Time in seconds before the game is reset
	public Text livesText;//displays lives
	public GameObject gameOver;//refrence to the game over object
	public GameObject youWon;
	public GameObject bricksPrefab;//Instantiates new bricks
	public GameObject paddle;//Instantiates new paddles when the player looses a life

	public GameObject deathParticles;

	public static GM instance = null;//Access the variale through a class so you can access variables from other scripts.

	private GameObject clonePaddle;

	// Use this for initialization
	void Awake () 
	{
		//Need this to add menus and other levels
		if (instance == null)
			instance = this;//checks to see if there is a game manager
		else if (instance != this)
			Destroy (gameObject);//if not then destroy the game manager

		Setup();

	}

	public void Setup()
	{
		clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;//creates the paddle on the screen. 
		Instantiate(bricksPrefab, transform.position, Quaternion.identity);//Instantiates the bricks
	}

	//Checks to see if the game has ended after you lose a life
	void CheckGameOver()
	{
		//If there are no more bricks then you have won the game
		if (bricks < 1)
		{
			youWon.SetActive(true);
			Time.timeScale = .25f;//takes the game into slow motion
			Invoke ("Reset", resetDelay);//allows you to call a funtion with a delay
		}

		//If player has run out of lives then the game is over
		if (lives < 1)
		{
			gameOver.SetActive(true);//game resets
			Time.timeScale = .25f;
			Invoke ("Reset", resetDelay);
		}

	}

	//
	void Reset()
	{
		Time.timeScale = 1f;//go back to normal time
		Application.LoadLevel(Application.loadedLevel);//the last level that was loaded will be reloaded.
	}

	public void LoseLife()
	{
		lives--;//Takes away a life from total number
		livesText.text = "Lives: " + lives;//On screen text is equal to varibale number
		Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);//Instantiate death particals where the paddle was.
		Destroy(clonePaddle);//Destroys the clone paddle
		Invoke ("SetupPaddle", resetDelay);//Invoke the set up paddle function.
		CheckGameOver();//checks to see if the game has ended.
	}

	void SetupPaddle()
	{
		clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;//makes a new paddle when the player has lost a life
	}

	public void DestroyBrick()
	{
		bricks--;//takes away one brick when they get destroyed.
		CheckGameOver();//checks to see if the game has ended.
	}
}

