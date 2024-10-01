using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popUpMenu;

    private Vector3 originalSize;
    private bool isHighlighted = true;

    private void Awake()
    {
        this.originalSize = transform.localScale;
    }

    void Start()
    {
        transform.localScale = this.originalSize * 1.5f;

        this.popUpMenu.SetActive(true);
    }

    public void OnPlayButtonClick(Button button)
    {
        if(isHighlighted)
        {
            button.transform.localScale = this.originalSize * 1.5f;
            //isHighlighted = true;
        }
        this.popUpMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        this.transform.localScale = this.originalSize;
        this.isHighlighted = false;
        this.popUpMenu.SetActive(false);
    }
}
