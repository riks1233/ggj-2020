using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatVisibleArea : MonoBehaviour
{
    public Animator bubble;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            bubble.SetTrigger("trigger");
        }
        //if (collision.gameObject.tag.Equals("Player"))
        //{
        //    print("hi");
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            bubble.SetTrigger("trigger");
        }
    }
}
