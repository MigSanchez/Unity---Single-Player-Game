using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	
   
    public float speed;
    public Text countText;
    public Text winText;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

    private Rigidbody rb;
    private int count;
    private int totalCount;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        totalCount = 0;
        SetCountText ();
        winText.text = "";
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);

       	if (Input.GetKeyDown ("space") && GetComponent<Rigidbody>().transform.position.y <= 0.5f) 
       	{
     		Vector3 jump = new Vector3 (0.0f, 180.0f, 0.0f);
 
    		GetComponent<Rigidbody>().AddForce (jump);
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
            totalCount = count + 3;
            SetCountText ();
        }
        if (other.gameObject.CompareTag ( "Purple PickUp"))
        {
            other.gameObject.SetActive (false);
            totalCount = count + 5;
            SetCountText ();
        }
    }

    void SetCountText ()
    {
        countText.text = "Score: " + totalCount.ToString ();
        if (count >= 21)
        {
            winText.text = "You Win!";
        }
    }
}
