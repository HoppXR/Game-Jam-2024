using UnityEngine;

public class TremblingMotion : MonoBehaviour
{
    public float moveDistance = 1.0f;
    public float moveSpeed = 2.0f;

    private Vector3 startPosition;
    private bool moveRight = true;

    void Start()
    {
        // Save the initial position of the capsule
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position based on the trembling motion
        float newPosition = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        // Determine the direction of movement
        if (moveRight)
            transform.position = startPosition + new Vector3(newPosition, 0f, 0f);
        else
            transform.position = startPosition + new Vector3(-newPosition, 0f, 0f);

        // Toggle the direction when reaching the end of the trembling motion
        if (newPosition >= moveDistance)
            moveRight = !moveRight;
    }
}
