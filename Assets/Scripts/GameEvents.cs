using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameEvents : MonoBehaviour
{
    // Start animation
    public SpriteRenderer blackScreen;
    public static GameEvents current;
    public CinemachineVirtualCamera CMcam;
    public CinemachineFramingTransposer CMftrsp;
    public static bool gameOver;
    public static bool gameStarted;
    public static int pairsFound;
    public static bool pairFound;
    public static int humansSelected;
    public static GameObject lastSelectedHuman;
    public GameObject heartExplosion;
    //EventSystem.onGroundTypeChange += <MethodName> // subscribe methods to this event
    public event Action onGroundTypeChange;
    public Player player;

    private void Awake()
    {
        current = this;
        pairsFound = 0;
        pairFound = false;
        humansSelected = 0;
        lastSelectedHuman = null;
        gameOver = false;
        gameStarted = false;
    }

    private void Start()
    {
        CMftrsp = CMcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        StartCoroutine(StartGameAnimation());
    }
    //call this whenever specific trigger is entered
    public void GroundTypeChange()
    {
        if (onGroundTypeChange != null)
        {
            onGroundTypeChange();
        }
    }

    public event Action<int> onPlayerSpotted;
    //call this whenever specific trigger is entered
    public void PlayerSpottedAtDialogueZone(int id)
    {
        if (onPlayerSpotted != null)
        {
            onPlayerSpotted(id);
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
                // game over
                GameOverFromMistake(selectedHuman);
            }
            else
            {
                print("pair found");
                pairsFound++;
                LeanTween.move(lastSelectedHuman, selectedHuman.transform.position, 2f).setEaseInQuad();
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

    public void GameOver()
    {
        gameOver = true;
        player.spotted = true;
        //PlayerSpottedAtDialogueZone(lastSelectedHuman.GetComponent<ComplainingHuman>().GetDialogueID());
    }

    public void GameOverFromMistake(GameObject lastSelectedHuman)
    {
        GameOver();
        PlayerSpottedAtDialogueZone(lastSelectedHuman.GetComponent<ComplainingHuman>().GetDialogueID());
    }

    IEnumerator StartGameAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        blackScreen.color = new Color(0, 0, 0, 0);
        //CMftrsp.m_XDamping = 20;
        //CMftrsp.m_YDamping = 20;
        LeanTween.move(player.gameObject, new Vector3(0.3f, 1.85f, 0), 1f).setEaseInQuad();
        yield return new WaitForSeconds(2f);
        //LeanTween.move(player.gameObject, new Vector3(0.3f, 1.85f, 0), 0.5f).setEaseInQuad();
        //CMftrsp.m_XDamping = 1;
        //CMftrsp.m_YDamping = 1;
        player.transform.Rotate(new Vector3(0, 0, -90));
        CMcam.Follow = player.transform;
        //yield return new WaitForSeconds(2f);
        //CMcam.transform.position = new Vector3(3, 1.7f, -10);

        yield return new WaitForSeconds(0.3f);
        gameStarted = true;

    }
}
