using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{
    protected Button selectedButton;

    public virtual void HandleInput(MenuInputHandler inputHandler, InputDevice inputDevice)
    {
        if (inputDevice.Action2 && selectedButton)
        {
            selectedButton.onClick.Invoke();
        }

        inputHandler.selector.transform.position = selectedButton.transform.position;
    }
}
