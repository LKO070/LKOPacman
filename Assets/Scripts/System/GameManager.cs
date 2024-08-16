using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioSource siren;
    public AudioSource eat1, eat2;

    public int currentEatAudio = 0;
    [SerializeField] private int score;
    void Awake()
    {
        siren = GetComponent<AudioSource>();
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
