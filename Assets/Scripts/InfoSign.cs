using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSign : MonoBehaviour
{
    public Animator text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("privet");
        if (collision.gameObject.tag.Equals("Player"))
        {
            //text.SetActive(true);
            text.SetTrigger("trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            text.SetTrigger("trigger");
        }
    }
}
