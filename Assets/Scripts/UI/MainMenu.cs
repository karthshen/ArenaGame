using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    public List<Button> buttons;

    private int buttonIndex;

    private void Start()
    {
        buttonIndex = 0;
        selectedButton = buttons[buttonIndex];
    }

    public override void HandleInput(MenuInputHandler inputHandler, InputDevice inputDevice)
    {
        if (inputDevice.DPadRight.WasPressed)
        {
            buttonIndex++;
        }
        else if (inputDevice.DPadLeft.WasPressed)
        {
            buttonIndex--;
        }

        ButtonIndexCheck();
        selectedButton = buttons[buttonIndex];

        base.HandleInput(inputHandler, inputDevice);
    }

    private void ButtonIndexCheck()
    {
        if (buttonIndex >= buttons.Count)
            buttonIndex = 0;
        if (buttonIndex < 0)
            buttonIndex = buttons.Count - 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingMenu()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
