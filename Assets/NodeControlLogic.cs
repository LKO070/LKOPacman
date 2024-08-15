using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControlLogic : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject nodeLeft, nodeRight, nodeUp, nodeDown;

    private float shootDistance = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D[] hitsDown;
        //Shoot raycast line moving down
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up);

        //Loop through all of the gameobjects that the raycast hits
        for (int i = 0; i < hitsDown.Length; i++)
        {
            //Calculating distance using the Y axis
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            
            if (distance < shootDistance) 
            {
                canMoveDown = true;
                //
                nodeDown = hitsDown[i].collider.gameObject;
            }

        }

        RaycastHit2D[] hitsUp;
        //Shoot raycast line moving down
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);

        //Loop through all of the gameobjects that the raycast hits
        for (int i = 0; i < hitsUp.Length; i++)
        {
            //Calculating distance using the Y axis
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);

            if (distance < shootDistance)
            {
                canMoveUp = true;
                //
                nodeUp = hitsUp[i].collider.gameObject;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
