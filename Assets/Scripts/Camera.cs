using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum CameraMode
{
    Static = 0,
    Follow
};

public class Camera : MonoBehaviour
{

    public Transform player_tf;


    public CameraMode mode = CameraMode.Follow;

    // Update is called once per frame
    void LateUpdate()
    {
        switch (mode)
        {
            case CameraMode.Static:
                return;
            case CameraMode.Follow:
                transform.position = new Vector3(player_tf.position.x, player_tf.transform.position.y, -10);
                break;
        }
    }


}
