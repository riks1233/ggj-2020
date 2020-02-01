using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpotArea : MonoBehaviour
{

    public int id;
    // Start is called before the first frame update
    void Start()
    {
        //print(GameEvents.current);
        GameEvents.current.onPlayerEnterDialogueZone += OnPlayerEnter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("entered dialogue");
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameEvents.current.PlayerEnterDialogueZone(id);
        }
    }

    private void OnPlayerEnter(int id)
    {
        if (this.id == id)
        {
            OnPlayerEnter();
        }
    }

    private void OnPlayerEnter()
    {
        print("spotted at dialogue with id " + id);
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerEnterDialogueZone -= OnPlayerEnter;
    }
}
