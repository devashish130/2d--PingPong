using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddel : MonoBehaviour
{
    public float speed = 10f;
    public float boundaryY = 4f;
    public bool isPlayerOne = true; // Determines whether this is Player 1 or Player 2

    void Update()
    {
        // Use "Vertical" for Player 1 and "Vertical2" for Player 2
        string inputAxis = isPlayerOne ? "Vertical" : "Vertical2";
        float input = Input.GetAxis(inputAxis);

        Vector2 newPosition = transform.position;
        newPosition.y += input * speed * Time.deltaTime;

        // Clamp the paddle's position within the game boundaries
        newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);
        transform.position = newPosition;
    }
}