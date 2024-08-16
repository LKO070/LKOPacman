using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject currentNode;
    public float speed = 4f;

    public string direction = "";
    public string lastMovingDirection = "";

    private SpriteRenderer sR;

    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get node control component
        NodeControllingLogic currentNodeController = currentNode.GetComponent<NodeControllingLogic>();

        //Move towards currently selected node at real time
        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);

        bool reverseDirection = false;

        if (
            (direction == "left" && lastMovingDirection == "right") ||
            (direction == "right" && lastMovingDirection == "left") ||
            (direction == "up" && lastMovingDirection == "down") ||
            (direction == "down" && lastMovingDirection == "up")
            )
        {
            reverseDirection = true;
        }

        //Calculate if player is at center of the current node
        if ((transform.position.x == currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y) || reverseDirection)
        {
            //Get next node from node controller using player's current direction
            GameObject newNode = currentNodeController.DetermineNodeFromDirection(direction);

            //If possible to move to specific direction
            if (newNode != null)
            {
                currentNode = newNode;
                lastMovingDirection = direction;
            }
            //If it is not possible to move to specific direction, keep moving towards same direction
            else
            {
                direction = lastMovingDirection;
                newNode = currentNodeController.DetermineNodeFromDirection(direction);
                if (newNode != null)
                {
                    currentNode = newNode;
                }
            }
        }

        if (direction == "left")
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            sR.flipX = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            sR.flipX = false;
        }

        if (direction == "up")
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            sR.flipX = false;
        }
        else if (direction == "down")
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            sR.flipX = false;
        }

    
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
