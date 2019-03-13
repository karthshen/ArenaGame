using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{
    protected Button selectedButton;

    private MenuInputHandler inputHandler;

    public MenuInputHandler InputHandler
    {
        get
        {
            return inputHandler;
        }

        set
        {
            inputHandler = value;
        }
    }

    public virtual void HandleInput(InputDevice inputDevice)
    {
        if (inputDevice.Action2 && selectedButton)
        {
            selectedButton.onClick.Invoke();
        }

        inputHandler.Selector.transform.position = selectedButton.transform.position;
    }

    public abstract GameObject getSelector();

    protected abstract void ButtonSelected();
    protected abstract void ButtonDeselected();
}
