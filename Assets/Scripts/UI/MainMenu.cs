using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    public List<Button> buttons;

    public GameObject selector;

    private int buttonIndex;

    private void Start()
    {
        buttonIndex = 0;
        selectedButton = buttons[buttonIndex];
        SoundManager.instance.PlayMusic(SoundManager.instance.menu01);
        ButtonSelected();
    }

    public override void HandleInput(InputDevice inputDevice)
    {
        ButtonDeselected();

        if (inputDevice.DPadRight.WasPressed)
        {
            buttonIndex++;
            PlayHoverSound();
        }
        else if (inputDevice.DPadLeft.WasPressed)
        {
            buttonIndex--;
            PlayHoverSound();
        }

        ButtonIndexCheck();
        selectedButton = buttons[buttonIndex];

        ButtonSelected();

        base.HandleInput(inputDevice);
    }

    private void ButtonIndexCheck()
    {
        if (buttonIndex >= buttons.Count)
            buttonIndex = 0;
        if (buttonIndex < 0)
            buttonIndex = buttons.Count - 1;
    }

    public void StageSelectMenu()
    {
        PlaySelectSound();
        gameObject.SetActive(false);
        InputHandler.menu = InputHandler.menus[1];
        InputHandler.ReloadCanvas();
    }

    public void SettingMenu()
    {
        PlaySelectSound();
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        PlaySelectSound();
        Application.Quit();
    }

    public override GameObject getSelector()
    {
        return selector;
    }

    protected override void ButtonSelected()
    {
        selectedButton.transform.localScale += new Vector3(0.2f, 0.2f, 0);
    }

    protected override void ButtonDeselected()
    {
        selectedButton.transform.localScale -= new Vector3(0.2f, 0.2f, 0);
    }

    protected override void PreviousMenu()
    {
        //Exit?
        ExitGame();
    }
}
