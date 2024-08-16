using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public AudioSource siren;
    void Awake()
    {
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
