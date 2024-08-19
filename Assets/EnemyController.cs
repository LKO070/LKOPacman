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

    [SerializeField] private GameObject ghostNodeLeft, ghostnodeRight, startingNode;

    //Check variable for exit time
    [SerializeField] private bool exitTime;
    
    //Reference to the movement script
    private MovementController movementController;

    // Start is called before the first frame update
    void Awake()
    {
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

    
    public void CenterOfNode(NodeControllingLogic nodeControl)
    {
        if (state == GhostNodeStatesEnum.movingInNodes)
        {
            //Determine next node
        }
        else if (state == GhostNodeStatesEnum.respawning)
        {
            //Determine nearest route to base
        }
        else
        {
            //If time to exit base
            if (exitTime)
            {
                GhostToNextState();
            }
        }
    }

    private void GhostToNextState()
    {
        switch (state)
        {
            case GhostNodeStatesEnum.leftNode:
                state = GhostNodeStatesEnum.centerNode;
                movementController.SetDirection("right");
                break;
            case GhostNodeStatesEnum.rightNode:
                state = GhostNodeStatesEnum.centerNode;
                movementController.SetDirection("left");
                break;
            case GhostNodeStatesEnum.centerNode:
                state = GhostNodeStatesEnum.startNode;
                movementController.SetDirection("up");
                break;
            case GhostNodeStatesEnum.startNode:
                state = GhostNodeStatesEnum.movingInNodes;
                movementController.SetDirection("left");
                break;
        }
    }

    private void SetStateDirection(GhostNodeStatesEnum newState, string direction)
    {
        state = newState;
        movementController.SetDirection(direction);
    }
    
}
