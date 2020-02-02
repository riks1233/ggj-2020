using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpotArea : MonoBehaviour
{

    public int id;
    public Transform exclamationMark;
    public ChatVisibleArea chatVisibleArea;
    // Start is called before the first frame update
    void Start()
    {
        //print(GameEvents.current);
        GameEvents.current.onPlayerSpotted += OnPlayerSpotted;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("entered dialogue");
        if (collision.gameObject.tag.Equals("Player") && !collision.gameObject.GetComponent<Player>().hidden)
        {
            Spotted();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !collision.gameObject.GetComponent<Player>().hidden && !collision.gameObject.GetComponent<Player>().spotted)
        {
            Spotted();
        }
    }

    private void OnPlayerSpotted(int id)
    {
        if (this.id == id)
        {
            Spotted();
        }
    }

    private void Spotted()
    {
        GameEvents.current.GameOver();
        print("AAAAA!!!");
        chatVisibleArea.ShowExclamationMark();
        //GameEvents.current.SetTargetExclamationMark(exclamationMark);

    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerSpotted -= OnPlayerSpotted;
    }
}
