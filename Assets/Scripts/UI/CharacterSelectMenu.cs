﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using InControl;
using UnityEngine.SceneManagement;

public class CharacterSelectMenu : Menu
{
    public GameObject selector;

    public List<Button> characters;

    public List<Sprite> characterImgs;

    public Image characterDisplayImage;

    public CharacterSelectMenu otherPlayerMenu;

    private int buttonIndex;

    private bool selectionConfirmed = false;

    [SerializeField]
    private int playerNum;

    public bool SelectionConfirmed
    {
        get
        {
            return selectionConfirmed;
        }
    }

    private void Start()
    {
        if(playerNum == 0)
        {
            buttonIndex = 0;
            selectedButton = characters[buttonIndex];
        }else if (playerNum == 1)
        {
            buttonIndex = characters.Count-1;
            selectedButton = characters[buttonIndex];
        }

        ButtonSelected();
    }

    public override void HandleInput(InputDevice inputDevice)
    {
        ButtonDeselected();

        if (inputDevice.DPadRight.WasPressed)
        {
            buttonIndex++;
        }
        else if (inputDevice.DPadLeft.WasPressed)
        {
            buttonIndex--;
        }

        if (inputDevice.Action2)
        {
            ConfirmSelection();
        }

        ButtonIndexCheck();

        if (playerNum == 0)
        {
            selectedButton = characters[buttonIndex];
        }
        else if (playerNum == 1)
        {
            selectedButton = characters[buttonIndex];
        }

        ButtonSelected();

        base.HandleInput(inputDevice);
    }

    public override GameObject getSelector()
    {
        return selector;
    }

    protected override void ButtonDeselected()
    {
        selectedButton.transform.localScale -= new Vector3(0.2f, 0.8f, 0);
    }

    protected override void ButtonSelected()
    {
        selectedButton.transform.localScale += new Vector3(0.2f, 0.8f, 0);
        ReloadImage();
    }

    private void ButtonIndexCheck()
    {
        if (buttonIndex >= characters.Count)
            buttonIndex = 0;
        if (buttonIndex < 0)
            buttonIndex = characters.Count - 1;
    }

    private void ReloadImage()
    {
        characterDisplayImage.sprite = characterImgs[buttonIndex];
    }

    private void ConfirmSelection()
    {
        selectionConfirmed = true;
        if (otherPlayerMenu.SelectionConfirmed)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
