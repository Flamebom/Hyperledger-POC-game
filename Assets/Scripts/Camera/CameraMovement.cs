using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D playerBody;
    public Vector3 offset;
    public SpriteRenderer playerSprite;

    public float playerOffset = 0.01f;
    private float smoothCameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerReference.player.GetComponent<Transform>();
        playerSprite = PlayerReference.player.GetComponent<SpriteRenderer>();
        playerBody = PlayerReference.player.GetComponent<Rigidbody2D>();
        smoothCameraSpeed = 1 / playerBody.velocity.magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.localScale.x < 0)
        {
            offset.x = -playerOffset;
        }
        else if (player.transform.localScale.x > 0)
        {
            offset.x = playerOffset;
        }
        Vector3 targetPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothCameraSpeed);
        smoothedPosition.z = -10;
        transform.position = smoothedPosition;
    }
}
