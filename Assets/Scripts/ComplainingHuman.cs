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
        print("i collide! id: " + id);
        Destroy(gameObject);
    }

    public void sayHi()
    {
        print("hi");
    }

    public void TriggerHighlight()
    {
        if (isHighlighted)
        {
            highlight.color = new Color(1, 1, 1, 0);
            isHighlighted = false;
            GameEvents.humansSelected -= 1;
        } else
        {
            highlight.color = new Color(1, 1, 1, 1);
            isHighlighted = true;
            GameEvents.humansSelected += 1;
            GameEvents.current.SelectComplainingHuman(id, matchingInteger, gameObject);
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


}
