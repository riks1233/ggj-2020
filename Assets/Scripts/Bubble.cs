using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public void TriggerChatBubble()
    {
        print("chatbubble triggered");
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("trigger");
    }

    private void OnMouseDown()
    {
        print("chatbubble triggered");
    }
}
