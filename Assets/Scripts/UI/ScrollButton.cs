using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using InControl;

public class ScrollButton : Button
{
    public List<string> texts;

    public Text scrollText;

    public int scrollIndex = 0;

    public void InitializeButton()
    {
        if(scrollText)
            scrollText.text = texts[scrollIndex];
    }

    public void OnClickScroll(InputDevice inputDevice)
    {
        if (!scrollText)
            return;

        if (inputDevice.DPadRight.WasPressed)
        {
            scrollIndex++;

            bool hasPlayed = false;
            SoundManager.instance.PlayEffect(SoundManager.instance.uiSelect, ref hasPlayed);
        }
        else if (inputDevice.DPadLeft.WasPressed)
        {
            scrollIndex--;

            bool hasPlayed = false;
            SoundManager.instance.PlayEffect(SoundManager.instance.uiSelect, ref hasPlayed);
        }

        ButtonIndexCheck();

        scrollText.text = texts[scrollIndex];
    }

    private void ButtonIndexCheck()
    {
        if (scrollIndex >= texts.Count)
            scrollIndex = 0;
        if (scrollIndex < 0)
            scrollIndex = texts.Count - 1;
    }

    public void CallMouseOver()
    {
        OnMouseOver();
    }

    private void OnMouseOver()
    {
        
    }
}
