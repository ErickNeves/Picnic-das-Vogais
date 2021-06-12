using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resposta : MonoBehaviour
{

    [SerializeField]
    private GameObject controler;

    [SerializeField]
    private int myId;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseEnter()
    {


        gameObject.GetComponent<Animator>().SetBool("hover", true);
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Animator>().SetBool("hover", false);
    }

    private void OnMouseUpAsButton()
    {

        controler.GetComponent<GameControler>().CheckAnswer(myId);

    }

    public void DisableButton()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableButton()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

}
