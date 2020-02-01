using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public static bool pairFound;
    public static int humansSelected;
    public static GameObject lastSelectedHuman;
    //EventSystem.onGroundTypeChange += <MethodName> // subscribe methods to this event
    public event Action onGroundTypeChange;

    private void Awake()
    {
        current = this;
        pairFound = false;
        humansSelected = 0;
        lastSelectedHuman = null;
    }
    //call this whenever specific trigger is entered
    public void GroundTypeChange()
    {
        if (onGroundTypeChange != null)
        {
            onGroundTypeChange();
        }
    }

    public event Action<int> onPlayerEnterDialogueZone;
    //call this whenever specific trigger is entered
    public void PlayerEnterDialogueZone(int id)
    {
        if (onPlayerEnterDialogueZone != null)
        {
            onPlayerEnterDialogueZone(id);
        }
    }

    public event Action<int, int, GameObject> onComplainingHumanHighlight;

    public void SelectComplainingHuman(int id, int matchingInteger, GameObject selectedHuman)
    {
        if (onComplainingHumanHighlight != null)
        {
            onComplainingHumanHighlight(id, matchingInteger, selectedHuman);
        }
        if (humansSelected > 1)
        {
            if (!pairFound)
            {
                print("game over");
                // game over
            }
            else
            {
                print("pair found");
                LeanTween.move(lastSelectedHuman, selectedHuman.transform.position, 1f).setEaseInQuad();
                //lastSelectedHuman
            }
            ResetHighlights();
        }
        lastSelectedHuman = selectedHuman;
    }

    public event Action onResetHighlights;

    public void ResetHighlights()
    {
        if (onResetHighlights != null)
        {
            onResetHighlights();
        }
        pairFound = false;
        humansSelected = 0;
    }
}
