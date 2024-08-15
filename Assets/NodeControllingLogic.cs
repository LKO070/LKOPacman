using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControllingLogic : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject nodeLeft, nodeRight, nodeUp, nodeDown;

    private float shootDistance = 0.4f;

    void Start()
    {
        // Check for movement in each direction
        CheckMovement(Vector2.left, ref canMoveLeft, ref nodeLeft);
        CheckMovement(Vector2.right, ref canMoveRight, ref nodeRight);
        CheckMovement(Vector2.up, ref canMoveUp, ref nodeUp);
        CheckMovement(Vector2.down, ref canMoveDown, ref nodeDown);
    }

    /// <summary>
    /// Checks if movement is possible in the specified direction by performing a raycast.
    /// Updates the movement flag and stores the hit object if it is within the specified shoot distance.
    /// </summary>
    /// <param name="direction">The direction in which to perform the raycast (Vector2).</param>
    /// <param name="canMove">A reference to the boolean flag that indicates if movement in the given direction is possible.</param>
    /// <param name="node">A reference to the GameObject that the raycast hits, if within the shoot distance.</param>
    void CheckMovement(Vector2 direction, ref bool canMove, ref GameObject node)
    {
        // Perform a raycast in the specified direction from the current position
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

        // Loop through all of the game objects that the raycast hits
        for (int i = 0; i < hits.Length; i++)
        {
            // Calculate the distance using the appropriate axis (x or y) based on the direction
            float distance = Mathf.Abs((direction.x != 0 ? hits[i].point.x : hits[i].point.y) - (direction.x != 0 ? transform.position.x : transform.position.y));

            // If the distance is less than the defined shoot distance, update movement and store the node
            if (distance < shootDistance)
            {
                canMove = true;
                node = hits[i].collider.gameObject;
            }
        }
    }
}
