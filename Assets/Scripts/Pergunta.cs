using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pergunta : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioVogal;

    [SerializeField]
    private int correctID;

    [SerializeField]
    private GameObject myVowel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void animVowel()
    {

    }

    public void PlayMySound()
    {
        audioVogal.Play();
    }

    public int GetMyId()
    {
        return correctID;
    }
}
