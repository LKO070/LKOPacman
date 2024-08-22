using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerPellet : MonoBehaviour
{
    public float powerPelletTime = 10f;
    public TMP_Text pelletTimerText;

    private float timer;
    private bool powerpelletActive = false;

    // Start is called before the first frame update
    void Start()
    {
        pelletTimerText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (powerpelletActive)
        {
            timer -= Time.deltaTime;

            if (timer > 0)
            {
                UpdateTimerUI();
            }
            else
            {
                EndPowerPellet();
            }
        }
    }

    /// <summary>
    /// Will Activate PowerPellet logic when player eats power pellet
    /// </summary>
    public void ActivatePowerPellet()
    {
        powerpelletActive = true;
        timer = powerPelletTime;
        pelletTimerText.gameObject.SetActive(true);
        UpdateTimerUI();

        //Trigger logic for ghost running away
        FindObjectOfType<EnemyController>().hasPowerPellet = true;
    }

    /// <summary>
    /// Ends Powerpellet ability and text when timer is up
    /// </summary>
    void EndPowerPellet()
    {
        powerpelletActive = false;
        pelletTimerText.gameObject.SetActive(false);

        //Trigger logic for ghost to cancel running away
        FindObjectOfType<EnemyController>().hasPowerPellet = false;
    }

    //Updates timer UI
    void UpdateTimerUI()
    {
        pelletTimerText.text = "Power Time Remaining: " + Mathf.Ceil(timer).ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivatePowerPellet();
            gameObject.SetActive(false);
        }
    }
}
