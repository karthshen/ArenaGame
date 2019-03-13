using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class MenuInputHandler : MonoBehaviour
{
    [SerializeField]
    private int playerNum;

    public List<Menu> menus;

    public Menu menu;

    [SerializeField]
    private GameObject selector;

    public GameObject Selector
    {
        get
        {
            return selector;
        }

        set
        {
            selector = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Selector = menu.getSelector();
    }

    // Update is called once per frame
    void Update()
    {
        //Null Check
        if (!menu.InputHandler)
            menu.InputHandler = this;

        var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

        if(inputDevice != null && menu != null && menu.enabled)
        {
            HandleInput(inputDevice);
        }
    }

    private void HandleInput(InputDevice inputDevice)
    {
        if (menu)
        {
            menu.HandleInput(inputDevice);
        }
    }

    public void ReloadCanvas()
    {
        menu.gameObject.SetActive(true);
        selector = menu.getSelector();
    }
}
