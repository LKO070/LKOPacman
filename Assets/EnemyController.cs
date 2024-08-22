using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum GhostNodeStatesEnum
    {
        respawning,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        movingInNodes
    }

    public enum GhostType
    {
        red,
        blue
    }

    public GhostNodeStatesEnum state;
    public GhostType type;

    [SerializeField] private GameObject ghostNodeLeft, ghostnodeRight, ghostNodeCenter, ghostNodeStart;
    [SerializeField] private GameObject startingNode;

    //Check variable for exit time
    [SerializeField] private bool exitTime;
    public bool hasPowerPellet;
    
    //Reference to the movement script
    private MovementController movementController;

    public GameManager gM;

    // Start is called before the first frame update
    void Awake()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        movementController = GetComponent<MovementController>();

        if (type == GhostType.red)
        {
            state = GhostNodeStatesEnum.leftNode;
            startingNode = ghostNodeLeft;
        }
        else if (type == GhostType.blue)
        {
            state = GhostNodeStatesEnum.rightNode;
            startingNode = ghostnodeRight;
        }

        movementController.currentNode = startingNode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReachedCenterNode(NodeControllingLogic nodeController)
    {
        if (state == GhostNodeStatesEnum.movingInNodes)
        {
            //Determine next node
            if (type == GhostType.red)
            {
                RedDirection();
            }
        }
        else if (state == GhostNodeStatesEnum.respawning)
        {
            //respawning determine
        }
        else
        {
            if (exitTime)
            {
                if (state == GhostNodeStatesEnum.leftNode)
                {
                    state = GhostNodeStatesEnum.centerNode;
                    Debug.Log("working");
                    movementController.SetDirection("right");
                }
                else if (state == GhostNodeStatesEnum.rightNode)
                {
                    state = GhostNodeStatesEnum.centerNode;
                    movementController.SetDirection("left");
                }
                else if (state == GhostNodeStatesEnum.centerNode)
                {
                    state = GhostNodeStatesEnum.startNode;
                    movementController.SetDirection("up");
                }
                else if (state == GhostNodeStatesEnum.startNode)
                {
                    state = GhostNodeStatesEnum.movingInNodes;
                    movementController.SetDirection("left");
                }


            }
        }
    }

    void RedDirection()
    {

        string direction = GetNearestDirection(gM.pacPlayer.transform.position);
        
        if (hasPowerPellet)
        {
            direction = GetFurthestDirection(gM.pacPlayer.transform.position);
        }
        else
        {
            direction = GetNearestDirection(gM.pacPlayer.transform.position);
        }

        movementController.SetDirection(direction);
    }
    
    void BlueDirection()
    {

    }

    /// <summary>
    /// Gets nearest direction towards player
    /// </summary>
    /// <param name="target"></param>
    /// <returns>Nearest Direction</returns>
    string GetNearestDirection(Vector2 target)
    {
        float shortestDistance = Mathf.Infinity;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";

        NodeControllingLogic nodeControl = movementController.currentNode.GetComponent<NodeControllingLogic>();

        // Create an array of directions
        string[] directions = { "up", "down", "left", "right" };

        // Create corresponding node and condition pairs
        Dictionary<string, GameObject> directionNodes = new Dictionary<string, GameObject>()
    {
        { "up", nodeControl.nodeUp },
        { "down", nodeControl.nodeDown },
        { "left", nodeControl.nodeLeft },
        { "right", nodeControl.nodeRight }
    };

        Dictionary<string, bool> moveConditions = new Dictionary<string, bool>()
    {
        { "up", nodeControl.canMoveUp && lastMovingDirection != "down" },
        { "down", nodeControl.canMoveDown && lastMovingDirection != "up" },
        { "left", nodeControl.canMoveLeft && lastMovingDirection != "right" },
        { "right", nodeControl.canMoveRight && lastMovingDirection != "left" }
    };

        foreach (var direction in directions)
        {
            // Check if move is possible and ghost is not reversing
            if (moveConditions[direction])
            {
                // Get the node in the current direction
                GameObject node = directionNodes[direction];

                // Get distance between the node and the player
                float distance = Vector2.Distance(node.transform.position, target);

                // If this is the shortest distance, set the new direction
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    newDirection = direction;
                }
            }
        }

        return newDirection;
    }

    string GetFurthestDirection(Vector2 target)
    {
        float furthestDistance = 0f;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";

        NodeControllingLogic nodeControl = movementController.currentNode.GetComponent<NodeControllingLogic>();

        // Create an array of directions
        string[] directions = { "up", "down", "left", "right" };

        // Create corresponding node and condition pairs
        Dictionary<string, GameObject> directionNodes = new Dictionary<string, GameObject>()
    {
        { "up", nodeControl.nodeUp },
        { "down", nodeControl.nodeDown },
        { "left", nodeControl.nodeLeft },
        { "right", nodeControl.nodeRight }
    };

        Dictionary<string, bool> moveConditions = new Dictionary<string, bool>()
    {
        { "up", nodeControl.canMoveUp && lastMovingDirection != "down" },
        { "down", nodeControl.canMoveDown && lastMovingDirection != "up" },
        { "left", nodeControl.canMoveLeft && lastMovingDirection != "right" },
        { "right", nodeControl.canMoveRight && lastMovingDirection != "left" }
    };

        foreach (var direction in directions)
        {
            // Check if move is possible and ghost is not reversing
            if (moveConditions[direction])
            {
                // Get the node in the current direction
                GameObject node = directionNodes[direction];

                // Get distance between the node and Pac-Man
                float distance = Vector2.Distance(node.transform.position, target);

                // If this is the farthest distance, set the new direction
                if (distance > furthestDistance)
                {
                    furthestDistance = distance;
                    newDirection = direction;
                }
            }
        }

        return newDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gM.PacDeath();
        }
    }

}
