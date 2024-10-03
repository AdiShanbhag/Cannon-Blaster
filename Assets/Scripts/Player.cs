using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float leftLimit = -3f;
    public float rightLimit = 3f;
    public float horizontalSpeed = 2f;
    public float playerControlSpeed = 5f;
    private bool isPlayerControlled = false;
    private Vector3 direction = Vector3.right;
    public GameObject readyPopUpPanel;
    
    private GameManager gameManager;
    void Start()
    {
        this.isPlayerControlled = false;

        this.gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.isPlayerControlled)
        {
            MovePlayerLeftRight();
        }

        if(this.gameManager != null && this.gameManager.isGameReady)
        {
            PlayerControlledMovement();
            DetectPlayerInput();
        }
    }

    private void MovePlayerLeftRight()
    {
        this.transform.position += this.direction * this.horizontalSpeed * Time.deltaTime;

        if(this.transform.position.x > this.rightLimit)
        {
            this.direction = Vector3.left;
        }
        else if(this.transform.position.x < this.leftLimit)
        {
            this.direction = Vector3.right;
        }
    }

    private void DetectPlayerInput()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            this.isPlayerControlled = true;
            this.readyPopUpPanel.SetActive(false);
        }
    }

    private void PlayerControlledMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 playerMovement = new Vector3(moveHorizontal, 0f, 0f) * this.playerControlSpeed * Time.deltaTime;

        this.transform.position += playerMovement;

        RestrictMovementWithinScreen();
    }

    private void RestrictMovementWithinScreen()
    {
        Vector3 playerViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
        playerViewportPos.x = Mathf.Clamp(playerViewportPos.x, 0f, 1f);

        Vector3 clampedWorldPos = Camera.main.ViewportToWorldPoint(playerViewportPos);
        this.transform.position = new Vector3(clampedWorldPos.x, this.transform.position.y, this.transform.position.z);
    }
}
