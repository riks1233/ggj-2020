using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnd : MonoBehaviour
{
    public GameObject text1;
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
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(0.8f);
        text1.SetActive(true);
        yield return new WaitForSeconds(2f);
        ready = true;
        pressSpace.SetActive(true);

    }
}
