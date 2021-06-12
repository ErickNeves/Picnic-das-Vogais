using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{

    [SerializeField]
    GameObject ioio;

    public void showIoIo()
    {
        ioio.SetActive(true);
    }


}
