using UnityEngine;
using System.Collections;
using InControl;

public class MenuInputHandler : MonoBehaviour
{
    [SerializeField]
    private int playerNum;
    public Menu menu;

    public GameObject selector;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            menu.HandleInput(this, inputDevice);
        }
    }
}
