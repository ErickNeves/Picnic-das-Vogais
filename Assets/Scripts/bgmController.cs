using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController : MonoBehaviour
{
    private static bgmController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }
}
