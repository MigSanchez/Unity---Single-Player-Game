using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

	
   
    public float speed;
    public Text countText;
    public Text winText;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

    private Rigidbody rb;
    private int count;
    private int totalCount;
    private Scene currentScene;
    private string sceneName;


public Vector3 jump;
         public float jumpForce = 2.0f;
     
         public bool isGrounded;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        totalCount = 0;
        SetCountText ();
        winText.text = "";
        // Create a temporary reference to the current scene.
        currentScene = SceneManager.GetActiveScene ();
 
         // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);

       	if (Input.GetKeyDown ("space") && isGrounded) 
       	{
            jump = new Vector3(0.0f, 2.0f, 0.0f);
             rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                 isGrounded = false;

            //Vector3 jump = new Vector3 (0.0f, 200.0f, 0.0f);
 
    		//GetComponent<Rigidbody>().AddForce (jump);
     	}
        if (Input.GetKeyDown (KeyCode.F)) 
        {
            Vector3 jump = new Vector3 (0.0f, 200.0f, 0.0f);
 
            GetComponent<Rigidbody>().AddForce (jump);
        }
     	if (Input.GetKeyDown(KeyCode.S))
		{
		    Fire();
		}
    }

    void Fire()
	{
	    // Create the Bullet from the Bullet Prefab
	    var bullet = (GameObject)Instantiate (
	        bulletPrefab,
	        bulletSpawn.position,
	        bulletSpawn.rotation);

	    // Add velocity to the bullet
	    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

	    // Destroy the bullet after 2 secondss
	    Destroy(bullet, 2.0f);
	}
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Floor"))
        {
            isGrounded = true;
        }
        if (other.gameObject.CompareTag ( "Ground PickUp"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            totalCount = totalCount + 1;
            SetCountText ();
        }
        if (other.gameObject.CompareTag ( "Green PickUp"))
        {
            other.gameObject.SetActive (false);
            totalCount = totalCount + 3;
            SetCountText ();
        }
        if (other.gameObject.CompareTag ( "Purple PickUp"))
        {
            other.gameObject.SetActive (false);
            totalCount = totalCount + 5;
            SetCountText ();
        }
		if (other.transform.CompareTag ( "Death"))
        {
            transform.position = new Vector3(0.0f, 0.5f, 0.0f);
   		}

    }

    void SetCountText ()
    {
        countText.text = "Score: " + totalCount.ToString ();
        if (sceneName == "Official" && count >= 3)
        {
            winText.text = "Tutorial Finished";
            Application.LoadLevel("Level1");
        }
        if (sceneName == "Level1" && count == 21)
        {
            winText.text = "Level 1 complete!";
            Application.LoadLevel("Level2");
        }
        if (sceneName == "Level2" && count == 24)
        {
            winText.text = "Level 2 complete!";
            Application.LoadLevel("Level3");
        }
        if (sceneName == "Level3" && count == 40)
        {
            winText.text = "YOU WIN!!!!!";
        }
    }
}
