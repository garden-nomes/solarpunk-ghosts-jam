using UnityEngine;


// PlayerStatus indicates whether the player is moving or standing  
// The order and value of the status declarations are used to compute the animation frame
public enum PlayerStatus : int
{
    Walking_Towards, Walking_Right, Walking_Away, Walking_Left, Facing_Towards, Facing_Right, Facing_Away, Facing_Left
}

public class PlayerMovement : MonoBehaviour
{
    // TODO: Collisions, or any kind of physics really. I'm not sure yet whether we're using the 2D
    // or 3D physics systems (or neither?) -- tbh it probably depends on whether we want a z-up or y-up
    // coordinate system.

    public float moveSpeed = 3f;

    public PlayerStatus prevStatus = PlayerStatus.Facing_Towards;
    public PlayerStatus currStatus = PlayerStatus.Facing_Towards;

    public float animationSpeed = 1.0f;

    public Texture2D[] frames;


    private void Start()
    {
        
    }

    void Update()
    {
        // disable while in dialogue
        if (GameManager.current.isInDialogue)
        {
            return;
        }


        // get player velocity from input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // check if the player is standing
        if (horizontal == 0.0f && horizontal == vertical)
        {
            if ((int) currStatus >= (int) PlayerStatus.Facing_Towards)
                return;
            
            // modify the active texture  and player status if the player has changed from moving to standing
        }

        if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            // vert is greater than horiz; the player is moving either towards or away from the camera
           
            if (vertical < 0f)
                // the player is moving towards the camera
                ;
        }
        else
        {
            // horiz is greater than or equal to vert
        }

        var velocity = Vector2.ClampMagnitude(new Vector2(horizontal, vertical), 1f) * moveSpeed;


        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
