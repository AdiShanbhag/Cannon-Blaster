using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public GameObject uiGameObject;
    public GameObject readyPopUpPanel;
    public GameObject pausePopUpPanel;

    //Transitioning backgroundImage
    public RectTransform backgroundImage;
    public float targetBottomValue = 0f;
    public float transitionDuration = 1f;
    private float currentBottomValue;
    private float initialBottomValue = 300f;
    private bool startTransition = false;
    private float tranisitionProgress = 0f;

    //Transitioning Player
    public Transform player;
    public float leftLimit = -3f;
    public float rightLimit = 3f;
    public float horizontalSpeed = 2f;
    public float verticalStartY = -5f;
    public float verticalTargetY = -6.5f;
    public float playerControlSpeed = 5f;
    private bool shouldPlayerMove = false;
    private bool isTransitioning = false;
    private bool isGameReady = false;
    private bool isPlayerControlled = false;
    private Vector3 direction = Vector3.right;


    public void Start()
    {
        /*this.currentBottomValue = this.initialBottomValue;
        SetBottomValue(this.initialBottomValue); */

        this.player.position = new Vector3(player.position.x, this.verticalStartY, player.position.z);
        this.shouldPlayerMove = true;

        this.readyPopUpPanel.SetActive(false);
        this.pausePopUpPanel.SetActive(false);
    }

    void Update()
    {
        /* if(this.startTransition)
        {
            SmoothTransition();
        }*/

        if(this.shouldPlayerMove && !this.isPlayerControlled)
        {
            MovePlayerLeftRight();
        }

        if(this.isTransitioning)
        {
            SmoothBackgroundTransition();
            SmoothPlayerVerticalTransition();
        }

        if(this.isGameReady && !this.isPlayerControlled)
        {
            DetectPlayerInput();
        }

        if(this.isPlayerControlled)
        {
            PlayerControlledMovement();
        }
    }

    public void OnClickPlay()
    {
        /* this.startTransition = true;
        this.tranisitionProgress = 0f; */
        this.isTransitioning = true;

        this.uiGameObject.SetActive(false);
        this.readyPopUpPanel.SetActive(true);
    }

    private void MovePlayerLeftRight()
    {
        this.player.position += this.direction * this.horizontalSpeed * Time.deltaTime;

        if(this.player.position.x > this.rightLimit)
        {
            this.direction = Vector3.left;
        }
        else if(this.player.position.x < this.leftLimit)
        {
            this.direction = Vector3.right;
        }
    }
    private void SmoothBackgroundTransition()
    {
        this.tranisitionProgress += Time.deltaTime / this.transitionDuration;
        this.currentBottomValue = Mathf.Lerp(this.initialBottomValue, this.targetBottomValue, this.tranisitionProgress);
        SetBottomValue(this.currentBottomValue);

        if(this.tranisitionProgress >= 1f)
        {
            this.startTransition = false;
            this.isGameReady = true;
        }
        /*
        float currentBottom = Mathf.Lerp(300f, 0f, Time.deltaTime * this.transitionDuration);
        this.backgroundImage.offsetMin = new Vector2(this.backgroundImage.offsetMin.x, currentBottom);

        if(Mathf.Abs(this.backgroundImage.offsetMin.y) < 0.01f)
        {
            this.isTransitioning = false;
            this.isGameReady = true;
        } */

    }

    private void SmoothPlayerVerticalTransition()
    {
        float currentY = Mathf.MoveTowards(this.player.position.y, this.verticalTargetY, this.transitionDuration * Time.deltaTime);
        this.player.position = new Vector3(this.player.position.x, currentY, this.player.position.z);
    }

    private void DetectPlayerInput()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            this.shouldPlayerMove = false;

            this.isPlayerControlled = true;

            this.readyPopUpPanel.SetActive(false);
        }
    }

    private void PlayerControlledMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 playerMovement = new Vector3(moveHorizontal, 0f, 0f) * this.playerControlSpeed * Time.deltaTime;

        this.player.position += playerMovement;
    }

    /* public void OnReadyScreenInput()
    {
        if(this.isGameReady)
        {
            this.shouldPlayerMove = true;
        }
    } */

    private void SetBottomValue(float bottomValue)
    {
        this.backgroundImage.offsetMin = new Vector2(this.backgroundImage.offsetMin.x, bottomValue);
        
    }

    public void OnClickPause()
    {
        this.pausePopUpPanel.SetActive(true);
    }

    public void OnClickResume()
    {
        this.pausePopUpPanel.SetActive(false);
    }
}
