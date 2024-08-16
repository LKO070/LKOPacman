using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MovementController movementController;

    public SpriteRenderer sR;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<MovementController>();
        animator = GetComponentInChildren<Animator>();
        sR = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Moving", true);

        var keyDirections = new Dictionary<KeyCode, string>
        {
            { KeyCode.LeftArrow, "left"},
            { KeyCode.RightArrow, "right"},
            { KeyCode.UpArrow, "up"},
            { KeyCode.DownArrow, "down"}
        };

        foreach (var entry in keyDirections)
        {
            if (Input.GetKey(entry.Key))
            {
                movementController.SetDirection(entry.Value);
            }
        }

        if (movementController.lastMovingDirection == "left")
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            sR.flipX = false;
            animator.SetInteger("Direction", 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            sR.flipX = true;
            animator.SetInteger("Direction", 0);
        }

        if (movementController.lastMovingDirection == "up")
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            sR.flipX = true;

            animator.SetInteger("Direction", 1);
        }
        else if (movementController.lastMovingDirection == "down")
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            sR.flipX = true;

            animator.SetInteger("Direction", 1);
        }

    }

}
