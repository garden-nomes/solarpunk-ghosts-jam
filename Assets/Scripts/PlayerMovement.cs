using UnityEngine;


// PlayerStatus indicates whether the player is moving or standing  
// The order and value of the status declarations are used to compute the animation frame
public enum PlayerStatus : int
{
    walkingTowards = 0, walkingRight = 3, walkingAway = 5, walkingLeft = 8, facingTowards = 10, facingRight = 13, facingAway = 15, facingLeft = 18
}

public class PlayerMovement : MonoBehaviour
{
    // TODO: Collisions, or any kind of physics really. I'm not sure yet whether we're using the 2D
    // or 3D physics systems (or neither?) -- tbh it probably depends on whether we want a z-up or y-up
    // coordinate systems

    public float moveSpeed = 3f;

    public PlayerStatus playerAction = PlayerStatus.facingTowards;

    // determines the time between frames
    public float animationSpeed = 5.0f;

    // { pc01, footup2, footup1, 
    //   pcside, pcsidefootup,
    //   pcback, pcbackfootup1, pcbackfootup2
    //   pcside2, pcside2footup }
    public Sprite[] frames;

    float timeStartedWalking;

    SpriteRenderer spriteRenderer;

    void setFrame()
    {
        // compute the frame index
        int[] vertAnimationOffsets = { 0, 1, 0, 2 };



        if ((int)playerAction < (int)PlayerStatus.facingTowards)
        {
            const int walkingVert = 1;
            const int walkingHoriz = 2;

            int activity = walkingVert;
            switch (playerAction)
            {
                case PlayerStatus.walkingTowards:
                case PlayerStatus.walkingAway:
                    activity = walkingVert;
                    break;
                case PlayerStatus.walkingRight:
                case PlayerStatus.walkingLeft:
                    activity = walkingHoriz;
                    break;
            }

            float timeBetweenFrames = 1.0f / animationSpeed;

            switch (activity)
            {
                case walkingVert:
                    int numVertAnimationFrames = 4;
                    float timeVertAnimationLoop = numVertAnimationFrames * timeBetweenFrames;
                    int vertAnimationOffsetIndex = (int)(Mathf.Repeat(Time.time - timeStartedWalking, timeVertAnimationLoop) / timeBetweenFrames);
                    spriteRenderer.sprite = frames[(int)playerAction + vertAnimationOffsets[vertAnimationOffsetIndex]];
                    break;
                case walkingHoriz:
                    int numHorizAnimationFrames = 2;
                    float timeHorizAnimationLoop = numHorizAnimationFrames * timeBetweenFrames;
                    int horizAnimationOffset = (int)(Mathf.Repeat(Time.time - timeStartedWalking, timeHorizAnimationLoop) / timeBetweenFrames);
                    spriteRenderer.sprite = frames[(int)playerAction + horizAnimationOffset];
                    break;
            }
        }
        else
            spriteRenderer.sprite = frames[(int)playerAction - 10];
    }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeStartedWalking = Time.time;
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

        float horizAbs = Mathf.Abs(horizontal);
        float vertAbs = Mathf.Abs(vertical);

        // compute player action/status given input
        {
            if (Mathf.Abs(horizontal) == Mathf.Abs(vertical))
            {
                if (horizontal == 0.0f)
                {
                    if ((int)playerAction < (int)PlayerStatus.facingTowards)
                    {
                        switch (playerAction)
                        {
                            case PlayerStatus.walkingRight:
                                playerAction = PlayerStatus.facingRight;
                                break;
                            case PlayerStatus.walkingAway:
                                playerAction = PlayerStatus.facingAway;
                                break;
                            case PlayerStatus.walkingLeft:
                                playerAction = PlayerStatus.facingLeft;
                                break;
                            default:
                                playerAction = PlayerStatus.facingTowards;
                                break;
                        }
                        // set frame where player is standing
                        spriteRenderer.sprite = frames[(int)playerAction - 10];
                    }
                    return ;
                }
                else
                {
                    int tempPlayerAction;

                    const int noChange = 0;
                    const int setHoriz = 1;
                    const int setVert = 2;
                    const int wasStanding = 3;

                    int jerking = noChange;

                    if ((int)playerAction >= (int)PlayerStatus.facingTowards)
                    {
                        tempPlayerAction = (int)playerAction - 10;
                        jerking = wasStanding;
                    }
                    else
                        tempPlayerAction = (int)playerAction;

                    switch (tempPlayerAction)
                    {
                        case ((int)PlayerStatus.walkingTowards):
                            if (vertical > 0.0f)
                                jerking = setHoriz;
                            break;
                        case ((int)PlayerStatus.walkingLeft):
                            if (horizontal > 0.0f)
                                jerking = setVert;
                            break;
                        case ((int)PlayerStatus.walkingAway):
                            if (vertical < 0.0f)
                                jerking = setHoriz;
                            break;
                        default:
                            // case playerAction equals PlayerStatus.walkingRight
                            if (horizontal < 0.0f)
                                jerking = setVert;
                            break;
                    }

                    if (jerking != noChange)
                    {
                        timeStartedWalking = Time.time;
                        switch (jerking)
                        {
                            case setHoriz:
                                if (horizontal > 0.0f)
                                    playerAction = PlayerStatus.walkingRight;
                                else
                                    playerAction = PlayerStatus.walkingLeft;
                                break;
                            case setVert:
                                if (vertical > 0.0f)
                                    playerAction = PlayerStatus.walkingAway;
                                else
                                    playerAction = PlayerStatus.walkingTowards;
                                break;
                            case wasStanding:
                                switch (playerAction)
                                {
                                    case PlayerStatus.facingTowards:
                                        playerAction = PlayerStatus.walkingTowards;
                                        break;
                                    case PlayerStatus.facingRight:
                                        playerAction = PlayerStatus.walkingRight;
                                        break;
                                    case PlayerStatus.facingAway:
                                        playerAction = PlayerStatus.walkingAway;
                                        break;
                                    default:
                                        //case facing right
                                        playerAction = PlayerStatus.walkingRight;
                                        break;
                                }
                                break;
                        }
                        spriteRenderer.sprite = frames[(int)playerAction];
                    }
                    else
                        setFrame();
                }
            }
            else
            {
                PlayerStatus playerActionPrev = playerAction;
                if (vertAbs > horizAbs)
                {
                    if (vertical > 0.0f)
                        playerAction = PlayerStatus.walkingAway;
                    else
                        playerAction = PlayerStatus.walkingTowards;
                }
                else
                {
                    if (horizontal > 0.0f)
                        playerAction = PlayerStatus.walkingRight;
                    else
                        playerAction = PlayerStatus.walkingLeft;
                }
                // reset timer and set frame, if applicable
                // else, compute frame given timer
                if (playerAction != playerActionPrev)
                {
                    timeStartedWalking = Time.time;
                    spriteRenderer.sprite = frames[(int)playerAction];
                }
                else
                    setFrame();
            }
        }

        var velocity = Vector2.ClampMagnitude(new Vector2(horizontal, vertical), 1f) * moveSpeed;

        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}


