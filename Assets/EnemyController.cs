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

    public void ReachedCenterNode(NodeControllingLogic nodeController)
    {
        if (state == GhostNodeStatesEnum.movingInNodes)
        {
            //Determine next node
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
    
}
