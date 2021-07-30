using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    // describes whether the warp is open or closed
    public bool openness = false;
    public bool occupiedness = false;
    public Collider2D player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (occupiedness && Input.GetKeyDown("e"))
           Debug.Log("player interacting with entrance");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (openness && collision.CompareTag("Player"))
        {
            player = collision;
            occupiedness = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        occupiedness = false;
    }
}
