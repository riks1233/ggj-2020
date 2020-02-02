using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplainingHuman : MonoBehaviour
{
    public int id;
    public int matchingInteger;
    public SpriteRenderer highlight;
    public bool isHighlighted = false;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onComplainingHumanHighlight += OnHumanHighlight;
        GameEvents.current.onResetHighlights += OnResetHighlight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("ComplainingHumanoid") && collision.gameObject.GetComponent<ComplainingHuman>().matchingInteger == matchingInteger)
        {
            Destroy(gameObject);
            //Destroy(collision.gameObject);
            if (gameObject.Equals(GameEvents.lastSelectedHuman))
            {
                print("explosion!");
                GameEvents.current.LaunchPairSound();
                if (GameEvents.pairsFound == GameEvents.pairsTotal)
                {
                    GameEvents.current.GameWin();
                } else
                {
                    GameEvents.current.NextBackgroundSound();
                }
                Vector3 explosionPos = transform.position;
                explosionPos = new Vector3(explosionPos.x, explosionPos.y + 0.5f, explosionPos.z);
                Instantiate(GameEvents.current.heartExplosion, explosionPos, new Quaternion());
            }
        }
        //print("i collide! id: " + id);
    }

    public void TriggerHighlight()
    {
        if (!GameEvents.gameOver)
        {
            if (isHighlighted)
            {
                highlight.color = new Color(1, 1, 1, 0);
                isHighlighted = false;
                GameEvents.humansSelected -= 1;
            }
            else
            {
                highlight.color = new Color(1, 1, 1, 1);
                isHighlighted = true;
                GameEvents.humansSelected += 1;
                GameEvents.current.SelectComplainingHuman(id, matchingInteger, gameObject);
            }
        }

    }

    private void OnHumanHighlight(int id, int matchingInteger, GameObject selectedHuman)
    {
        if (this.matchingInteger == matchingInteger && this.id != id && isHighlighted)
        {
            // found a pair!
            // let event system know about it
            GameEvents.pairFound = true;
        }
    }

    private void OnResetHighlight()
    {
            highlight.color = new Color(1, 1, 1, 0);
            isHighlighted = false;
    }

    private void OnDestroy()
    {
        GameEvents.current.onComplainingHumanHighlight -= OnHumanHighlight;
        GameEvents.current.onResetHighlights -= OnResetHighlight;
    }

    public int GetDialogueID()
    {
        return transform.parent.GetChild(1).GetComponent<DialogueSpotArea>().id;
    }


}
