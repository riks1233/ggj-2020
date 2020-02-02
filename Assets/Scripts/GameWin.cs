using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameWin : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    public GameObject pressSpace;
    private bool ready;

    private void Awake()
    {
        ready = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && ready) {
            //transition to next scene
            print("transition");
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(1f);
        text1.SetActive(true);
        yield return new WaitForSeconds(1f);
        text2.SetActive(true);
        ready = true;
        yield return new WaitForSeconds(4f);
        pressSpace.SetActive(true);

    }
}
