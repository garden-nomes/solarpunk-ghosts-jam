using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // TODO: Collisions, or any kind of physics really. I'm not sure yet whether we're using the 2D
    // or 3D physics systems (or neither?) -- tbh it probably depends on whether we want a z-up or y-up
    // coordinate system.

    public float moveSpeed = 3f;

    void Update()
    {
        // disable while in dialogue
        if (GameManager.current.isInDialogue)
        {
            return;
        }

        // simple 2D movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        var velocity = Vector2.ClampMagnitude(new Vector2(horizontal, vertical), 1f) * moveSpeed;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
