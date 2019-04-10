using UnityEngine;
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

    //Outputs
    private AActorEnum selectedActor;

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
            PlayHoverSound();
        }
        else if (inputDevice.DPadLeft.WasPressed)
        {
            buttonIndex--;
            PlayHoverSound();
        }

        if (inputDevice.Action2)
        {
            ConfirmSelection();
            PlaySelectSound();
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
        selectedButton.onClick.Invoke();
        if(playerNum == 0)
        {
            GameStageSetting.Player1Selection = selectedActor;
        }else if(playerNum == 1)
        {
            GameStageSetting.Player2Selection = selectedActor;
        }

        if (otherPlayerMenu.SelectionConfirmed)
        {
            LoadSelectedMap();
        }
    }

    private void LoadSelectedMap()
    {
        if(GameStageSetting.SelectedMap == MapSelection.Arena)
        {
            //Map1
            SceneManager.LoadScene("SampleScene");
            SoundManager.instance.PlayMusic(SoundManager.instance.battle00);
        }
        else if(GameStageSetting.SelectedMap == MapSelection.Tavern)
        {
            SceneManager.LoadScene("Map2");
            SoundManager.instance.PlayMusic(SoundManager.instance.battle01);
        }
    }

    public void SelectWarrior()
    {
        selectedActor = AActorEnum.Warrior;
    }

    public void SelectMage()
    {
        selectedActor = AActorEnum.Mage;
    }

    public void SelectArcher()
    {
        selectedActor = AActorEnum.Archer;
    }

    protected override void PreviousMenu()
    {
        PlayBackSound();
        SceneManager.LoadScene("Start");
    }
}
