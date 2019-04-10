using UnityEngine;
using System.Collections;

public static class GameStageSetting
{
    private static AActorEnum player1Selection;
    private static AActorEnum player2Selection;
    private static MapSelection selectedMap;

    private static float gameDuration = 600f; //game duration in seconds
    private static float playerStartingHealth = 100f;
    private static bool lockEnergy = false;
    private static bool enableRunes;
    private static bool itemDrop = true;
    private static bool stageHazards;

    public static AActorEnum Player1Selection
    {
        get
        {
            return player1Selection;
        }

        set
        {
            player1Selection = value;
        }
    }

    public static AActorEnum Player2Selection
    {
        get
        {
            return player2Selection;
        }

        set
        {
            player2Selection = value;
        }
    }

    public static float GameDuration
    {
        get
        {
            return gameDuration;
        }

        set
        {
            gameDuration = value;
        }
    }

    public static float PlayerStartingHealth
    {
        get
        {
            return playerStartingHealth;
        }

        set
        {
            playerStartingHealth = value;
        }
    }

    public static bool LockEnergy
    {
        get
        {
            return lockEnergy;
        }

        set
        {
            lockEnergy = value;
        }
    }

    public static bool EnableRunes
    {
        get
        {
            return enableRunes;
        }

        set
        {
            enableRunes = value;
        }
    }

    public static bool ItemDrop
    {
        get
        {
            return itemDrop;
        }

        set
        {
            itemDrop = value;
        }
    }

    public static bool StageHazards
    {
        get
        {
            return stageHazards;
        }

        set
        {
            stageHazards = value;
        }
    }

    public static MapSelection SelectedMap
    {
        get
        {
            return selectedMap;
        }

        set
        {
            selectedMap = value;
        }
    }
}
