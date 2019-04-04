using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using InControl;
using UnityEngine.SceneManagement;

public class StageSelectMenu : Menu
{
    //Selector
    public GameObject selector;

    //Buttons
    public ScrollButton timeLimitButton;

    public ScrollButton playerHealthButton;

    public ScrollButton lockRageButton;

    public ScrollButton runesButton;

    public ScrollButton itemDropButton;

    public ScrollButton stageHazardsButton;

    public ScrollButton characterSelectButton;

    //List of String
    public List<string> timeLimitList;

    public List<string> playerHealthList;

    public List<string> lockRageList;

    public List<string> runesList;

    public List<string> itemDropList;

    public List<string> stageHazardList;

    //List of Text
    public Text timeLimitText;

    public Text playerHealthText;

    public Text lockRageText;

    public Text runesText;

    public Text itemDropText;

    public Text stageHazardText;

    private int buttonIndex;

    private List<ScrollButton> buttons = new List<ScrollButton>();


    // Use this for initialization
    void Start()
    {
        timeLimitButton.texts = timeLimitList;
        timeLimitButton.scrollText = timeLimitText;

        playerHealthButton.texts = playerHealthList;
        playerHealthButton.scrollText = playerHealthText;

        lockRageButton.texts = lockRageList;
        lockRageButton.scrollText = lockRageText;

        runesButton.texts = runesList;
        runesButton.scrollText = runesText;

        itemDropButton.texts = itemDropList;
        itemDropButton.scrollText = itemDropText;

        stageHazardsButton.texts = stageHazardList;
        stageHazardsButton.scrollText = stageHazardText;

        buttons.Add(timeLimitButton);
        buttons.Add(playerHealthButton);
        buttons.Add(lockRageButton);
        buttons.Add(runesButton);
        buttons.Add(itemDropButton);
        buttons.Add(stageHazardsButton);
        buttons.Add(characterSelectButton);

        foreach (ScrollButton button in buttons)
        {
            button.InitializeButton();
        }

        buttonIndex = 0;
        selectedButton = buttons[buttonIndex];

        ButtonSelected();
    }

    // Update is called once per frame
    public override void HandleInput(InputDevice inputDevice)
    {
        ButtonDeselected();
        if (inputDevice.DPadDown.WasPressed)
        {
            buttonIndex++;
        }
        else if (inputDevice.DPadUp.WasPressed)
        {
            buttonIndex--;
        }

        else
        {
            ((ScrollButton)selectedButton).OnClickScroll(inputDevice);
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

    public override GameObject getSelector()
    {
        return selector;
    }

    public void CharacterSelectMenu()
    {
        SaveMapSettings();
        SceneManager.LoadScene("CharacterSelect");
    }

    private void SaveMapSettings()
    {
        //Set up the values in static class
        GameStageSetting.GameDuration = int.Parse(timeLimitButton.texts[timeLimitButton.scrollIndex].Substring(0, 1)) * 60;
        if (playerHealthButton.texts[playerHealthButton.scrollIndex].Length == 4)
        {
            GameStageSetting.PlayerStartingHealth = 100f;
        }
        else
        {
            GameStageSetting.PlayerStartingHealth = int.Parse(playerHealthButton.texts[playerHealthButton.scrollIndex].Substring(0, 2));
        }

        if (lockRageButton.texts[lockRageButton.scrollIndex] == "Lock")
        {
            GameStageSetting.LockEnergy = true;
        }
        else
        {
            GameStageSetting.LockEnergy = false;
        }

        if (runesButton.texts[runesButton.scrollIndex] == "On")
        {
            GameStageSetting.EnableRunes = true;
        }
        else
        {
            GameStageSetting.EnableRunes = false;
        }

        if (itemDropButton.texts[itemDropButton.scrollIndex] == "On")
        {
            GameStageSetting.ItemDrop = true;
        }
        else
        {
            GameStageSetting.ItemDrop = false;
        }

        if (stageHazardsButton.texts[stageHazardsButton.scrollIndex] == "On")
        {
            GameStageSetting.StageHazards = true;
        }
        else
        {
            GameStageSetting.StageHazards = false;
        }
    }

    protected override void ButtonSelected()
    {
        selectedButton.transform.localScale += new Vector3(0.07f, 0, 0);
    }

    protected override void ButtonDeselected()
    {
        selectedButton.transform.localScale -= new Vector3(0.07f, 0, 0);
    }
}
