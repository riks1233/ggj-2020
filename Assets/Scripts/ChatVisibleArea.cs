using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatVisibleArea : MonoBehaviour
{
    public Animator[] bubbles;
    public Animator exclamationMark;
    public bool isVisible;

    private void Awake()
    {
        isVisible = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggerEnterWasCalled");
        if (collision.gameObject.tag.Equals("Player"))
        {
            isVisible = true;
            foreach (Animator bubble in bubbles) {
                if (bubble != null)
                {
                    bubble.SetTrigger("trigger");
                }
            }
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
            isVisible = false;
            foreach (Animator bubble in bubbles)
            {
                if (bubble != null)
                {
                    bubble.SetTrigger("trigger");
                }
            }
        }
    }

    public void ShowExclamationMark()
    {
        if(isVisible)
        {
            print("isvisible");
            foreach (Animator bubble in bubbles)
            {
                if (bubble != null)
                {
                    bubble.SetTrigger("trigger");
                }
            }
        }
        exclamationMark.SetTrigger("trigger");
    }
}
