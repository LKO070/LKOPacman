using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private AudioSource siren;
    private int currentEatAudio = 0;
    
    public AudioSource eat1, eat2;

    //Variables for the score
    [SerializeField] private int score;
    public TMP_Text scoreText;

    public GameObject pacPlayer;
    void Awake()
    {
        score = 0;
        currentEatAudio = 0;
        siren = GetComponent<AudioSource>();
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void EatenPellet(NodeControllingLogic nodeControl)
    {
        if (currentEatAudio == 0)
        {
            eat1.Play();
            currentEatAudio = 1;
        }
        else if (currentEatAudio == 1)
        {
            eat2.Play();
            currentEatAudio = 0;
        }

        AddScore(2);
        //Add score

        //Check amount of remaining pellets

        //Check amount of pellets eaten

        //Check for power pellet
    }
}
