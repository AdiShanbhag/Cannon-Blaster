using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public GameObject uiGameObject;
    public GameObject readyPopUpPanel;
    public GameObject pausePopUpPanel;

    //Transitioning backgroundImage
    public RectTransform backgroundImage;
    public float transitionDuration = 1f;
    private float initialBottomValue = 300f;

    //Transitioning Player
    public Transform player;
    public float verticalStartY = -5f;
    public float verticalTargetY = -6.5f;
    public bool isGameReady {get; private set;}

    private bool isGamePaused = false;


    public void Start()
    {
        this.player.position = new Vector3(player.position.x, this.verticalStartY, player.position.z);
        this.backgroundImage.offsetMin = new Vector2(this.backgroundImage.offsetMin.x, this.initialBottomValue);

        this.readyPopUpPanel.SetActive(false);
        this.pausePopUpPanel.SetActive(false);

        Time.timeScale = 1;
    }

    public void OnClickPlay()
    {
        this.uiGameObject.SetActive(false);
        this.readyPopUpPanel.SetActive(true);

        StartCoroutine(TransitionBackgroundAndPlayer());
    }

    private IEnumerator TransitionBackgroundAndPlayer()
    {
        float elapsedTime = 0f;

        while(elapsedTime < this.transitionDuration)
        {
            float newBottom = Mathf.Lerp(this.initialBottomValue, 0f, elapsedTime / this.transitionDuration);
            this.backgroundImage.offsetMin = new Vector2(this.backgroundImage.offsetMin.x, newBottom);

            float currentY = Mathf.Lerp(this.verticalStartY, this.verticalTargetY, elapsedTime / this.transitionDuration);
            this.player.position = new Vector3(this.player.position.x, currentY, this.player.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        this.backgroundImage.offsetMin = new Vector2(this.backgroundImage.offsetMin.x, 0f);
        this.player.position = new Vector3(this.player.position.x, this.verticalTargetY, this.player.position.z);

        this.isGameReady = true;
    }

    public void OnClickPause()
    {
        if(!this.isGamePaused)
        {
            Time.timeScale = 0;
            this.pausePopUpPanel.SetActive(true);
            this.isGamePaused = true;
        }

    }

    public void OnClickResume()
    {
        if(this.isGamePaused)
        {
            Time.timeScale = 1;
            this.pausePopUpPanel.SetActive(false);
            this.isGamePaused = false;
        }
    }
}
