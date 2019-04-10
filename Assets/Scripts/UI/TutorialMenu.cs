using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.SceneManagement;

public class TutorialMenu : Menu
{
    public GameObject selector;

    public override GameObject getSelector()
    {
        return selector;
    }

    protected override void ButtonDeselected()
    {
        Debug.Log("Button Deselected at Setting Menu");
    }

    protected override void ButtonSelected()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Button Selected at Setting Menu");
    }

    protected override void PreviousMenu()
    {
        PlayBackSound();
        SceneManager.LoadScene("Start");
    }

    // Use this for initialization
    void Start()
    {
        ButtonSelected();
    }

    public override void HandleInput(InputDevice inputDevice)
    {
        ButtonDeselected();

        if (inputDevice.LeftTrigger.WasPressed || inputDevice.LeftBumper.WasPressed)
        {
            SettingMenu();
        }
        else if (inputDevice.RightTrigger.WasPressed || inputDevice.RightBumper.WasPressed)
        {
            CreditMenu();
        }

        base.HandleInput(inputDevice);
    }

    private void SettingMenu()
    {
        PlaySelectSound();
        gameObject.SetActive(false);
        InputHandler.menu = InputHandler.menus[0];
        InputHandler.ReloadCanvas();
    }

    private void CreditMenu()
    {
        PlaySelectSound();
        gameObject.SetActive(false);
        InputHandler.menu = InputHandler.menus[2];
        InputHandler.ReloadCanvas();
    }
}
