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
        else if (inputDevice.Action1.WasPressed)
        {
            PreviousMenu();
        }

        inputHandler.Selector.transform.position = selectedButton.transform.position;
    }

    public abstract GameObject getSelector();

    protected abstract void ButtonSelected();
    protected abstract void ButtonDeselected();

    protected abstract void PreviousMenu();

    protected void PlayHoverSound()
    {
        bool hasPlayed = false;
        SoundManager.instance.PlayEffect(SoundManager.instance.uiHover, ref hasPlayed);
    }

    protected void PlaySelectSound()
    {
        bool hasPlayed = false;
        SoundManager.instance.PlayEffect(SoundManager.instance.uiSelect, ref hasPlayed);
    }

    protected void PlayBackSound()
    {
        bool hasPlayed = false;
        SoundManager.instance.PlayEffect(SoundManager.instance.uiBack, ref hasPlayed);
    }
}

