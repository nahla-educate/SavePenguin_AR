using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpPenguin : MonoBehaviour
{
    public float jumpHeight = 1.0f;
    float forwardDistance = 3.5f;
    private bool isJumping = false;

    void Start()
    {
    }

    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;

            Vector3 jumpTarget = transform.position + Vector3.up * jumpHeight + transform.forward * forwardDistance;

            StartCoroutine(MoveToPosition(jumpTarget));
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float totalTime = 0.5f;

        Vector3 startingPosition = transform.position;

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset variables for the downward motion
        elapsedTime = 0f;
        startingPosition = transform.position;

        while (elapsedTime < totalTime)
        {
            // Move downward during the second half of the jump
            transform.position = Vector3.Lerp(targetPosition, startingPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly the starting position
        transform.position = startingPosition;

        isJumping = false;
    }
}
