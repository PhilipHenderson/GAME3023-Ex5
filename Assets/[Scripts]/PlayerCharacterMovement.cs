using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0,10)] 
    private float MoveSpeed = 10;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    Rigidbody2D rigidbody;

    // Save/Load
    [SerializeField]
    bool isLoading = false;

    public float xPosition;
    public float yPosition;

    private string xKey = "Players X Position";
    private string yKey = "Players Y Position";

    Vector2 LastSavedLocation;

    public void Awake()
    {
        if (isLoading)
        {
            player.GetComponent<GameObject>();
            player.transform.position = LastSavedLocation;
        }
    }


    public void Loading()
    {
        if (xKey != null && yKey != null)
        {
            isLoading = true;
            xPosition = PlayerPrefs.GetFloat(xKey);
            yPosition = PlayerPrefs.GetFloat(yKey);
            Debug.Log("xKey: " + xPosition);
            Debug.Log("yKey: " + yPosition);
            LastSavedLocation = new Vector2(xPosition, yPosition);
            Debug.Log("playerPosition: " + LastSavedLocation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            Save(); 
        }

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 oldPosition = transform.position;

        rigidbody.MovePosition(oldPosition + new Vector3(xInput, yInput, 0) * MoveSpeed * Time.deltaTime);

        xPosition = player.transform.position.x;
        yPosition = player.transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player Collided with " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Triggered with: " + collision.gameObject.name);
    }

    void Save()
    {
        PlayerPrefs.SetFloat(xKey, xPosition);
        PlayerPrefs.SetFloat(yKey, yPosition);
        Debug.Log("Players X Position: " + xPosition);
        Debug.Log("Players Y Position: " + yPosition);
    }

}
