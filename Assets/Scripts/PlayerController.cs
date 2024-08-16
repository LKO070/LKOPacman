using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

}
