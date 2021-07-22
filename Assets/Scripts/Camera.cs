using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


enum CameraMode
{
    Static = 0,
    Follow
};

public class Camera : MonoBehaviour
{
    CameraMode mode = CameraMode.Follow;

    // Update is called once per frame
    void LateUpdate()
    {
        switch (mode)
        {
            case CameraMode.Static:
                return;
            case CameraMode.Follow:
                GameObject player = GameObject.Find("Player");
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
                break;
        }
    }


}
