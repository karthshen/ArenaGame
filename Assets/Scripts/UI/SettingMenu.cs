using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.SceneManagement;

public class SettingMenu : Menu
{
    public GameObject selector;

    private float loadTimer = 0.3f;
    private bool loaded = false;

    public override GameObject getSelector()
    {
        return selector;
    }

    protected override void ButtonDeselected()
    {
        //Debug.Log("Button Deselected at Setting Menu");
    }

    protected override void ButtonSelected()
    {
        //throw new System.NotImplementedException();
        //Debug.Log("Button Selected at Setting Menu");
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

        if (!loaded)
            return;

        if(inputDevice.LeftTrigger.WasPressed || inputDevice.LeftBumper.WasPressed)
        {
            CreditMenu();
        }
        else if(inputDevice.RightTrigger.WasPressed || inputDevice.RightBumper.WasPressed){
            TutorialMenu();   
        }

        base.HandleInput(inputDevice);
    }

    private void Update()
    {
        if (loadTimer > 0)
        {
            loadTimer -= Time.deltaTime;
            if (loadTimer <= 0)
            {
                loaded = true;
            }
        }
    }

    private void TutorialMenu()
    {
        PlaySelectSound();
        gameObject.SetActive(false);
        InputHandler.menu = InputHandler.menus[1];
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
