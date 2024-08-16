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
    /// Checks if movement is possible in specific direction by performing a raycast.
    /// Updates movement flag and stores hit object if it is within shoot distance.
    /// </summary>
    /// <param name="direction">Direction in which to perform the raycast (Vector2).</param>
    /// <param name="canMove">Reference to Boolean that says if movement in specific direction is possible.</param>
    /// <param name="node">Reference to GameObject that the raycast hits, if within shooting distance.</param>
    void CheckMovement(Vector2 direction, ref bool canMove, ref GameObject node)
    {
        // Performs raycast in the specific direction from the current position
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

        // Loop through all of the game objects that the raycast hits
        for (int i = 0; i < hits.Length; i++)
        {
            // Calculate the distance using the appropriate axis (x or y) based on the direction
            float distance = Mathf.Abs((direction.x != 0 ? hits[i].point.x : hits[i].point.y) - (direction.x != 0 ? transform.position.x : transform.position.y));

            // If distance is less than the defined shoot distance, update movement and store node
            if (distance < shootDistance)
            {
                canMove = true;
                node = hits[i].collider.gameObject;
            }
        }
    }

    /// <summary>
    /// Checks the direction given and returns the next node if you can move that way
    /// returns null if player can't move or if the direction isn't possible to move to
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public GameObject DetermineNodeFromDirection(string direction)
    {
        //Collection to hold keys and tuples in for more efficient use of determining direction
        var directions = new Dictionary<string, (bool canMove, GameObject node)>
        {
            {"left", (canMoveLeft, nodeLeft) },
            {"right", (canMoveRight, nodeRight) },
            {"up", (canMoveUp, nodeUp) },
            {"down", (canMoveDown, nodeDown) }
        };

        //Get value from associated key. If found it stores tuple value
        if (directions.TryGetValue(direction, out var result) && result.canMove)
        {
            return result.node;
        }

        return null;
    }
}
