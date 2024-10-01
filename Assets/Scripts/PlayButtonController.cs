using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonController : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] popUpMenus;

    private Vector3 originalSize;
    private Button currentSelectedButton;

    private GameObject currentPopUpMenu;

    void Start()
    {
        // Store the original size of the buttons
        if(this.buttons.Length > 0)
        {
            this.originalSize = this.buttons[0].transform.localScale;
        }
        // Set the Play button as the default selected and enlarged button
        this.currentSelectedButton = this.buttons[2];
        EnlargeButton(this.currentSelectedButton);

        // Set the corresponding Play button pop-up menu as the default
        this.currentPopUpMenu = this.popUpMenus[2];
        this.currentPopUpMenu.SetActive(true);

        // Disable other pop-up menus except for the Play button's by default
        for(int i = 0; i < this.popUpMenus.Length; i++)
        {
            if(i != 2)
            {
                this.popUpMenus[i].SetActive(false);
            }
        }
    }

    // Method to handle any button click
    public void OnButtonClick(Button clickedButton)
    {
        // Restore the size of the currently selected button
        if(this.currentSelectedButton != null)
        {
            RestoreButtonSize(this.currentSelectedButton);
        }

        // Enlarge the clicked button and set it as the new selected button
        EnlargeButton(clickedButton);
        this.currentSelectedButton = clickedButton;

        // Get the index of the clicked button in the array
        int clickedButtonIndex = System.Array.IndexOf(buttons, clickedButton);

        // Close the currently active pop-up menu and open the new one
        if (this.currentPopUpMenu != null)
        {
            this.currentPopUpMenu.SetActive(false);
        }

        // Set the corresponding pop-up menu for the clicked button as active
        if (clickedButtonIndex >= 0 && clickedButtonIndex < popUpMenus.Length)
        {
            this.currentPopUpMenu = popUpMenus[clickedButtonIndex];
            this.currentPopUpMenu.SetActive(true);
        }
    }

    // Method to enlarge a button by 20%
    private void EnlargeButton(Button button)
    {
        button.transform.localScale = originalSize * 1.2f;
    }

    // Method to restore the button to its original size
    private void RestoreButtonSize(Button button)
    {
        button.transform.localScale = originalSize;
    }
}
