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

    private void OnTriggerStay2D(Collider2D other)
    {
        // check for user input
        if (openness && Input.GetKeyDown("e") && other.CompareTag("Player"))
            // open new scene
            Debug.Log("The user requested to open up the scene.");
    }
}
