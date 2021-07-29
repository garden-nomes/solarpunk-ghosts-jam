using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    // describes whether the warp is open or closed
    public bool openness = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Hello, world!");
        // check for user input
        if (other.CompareTag("Player") && openness && Input.GetKeyDown("e"))
            // open new scene
            Debug.Log("The user requested to open up the scene.");
    }
}
