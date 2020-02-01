using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    //EventSystem.onGroundTypeChange += <MethodName> // subscribe methods to this event
    public event Action onGroundTypeChange;

    private void Awake()
    {
        current = this;
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

    //public event Action onPlayerEnterDialogueZone;
    ////call this whenever specific trigger is entered
    //public void PlayerEnterDialogueZone()
    //{
    //    if (onPlayerEnterDialogueZone != null)
    //    {
    //        onPlayerEnterDialogueZone();
    //    }
    //}
}
