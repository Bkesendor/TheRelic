using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    public bool isShaking;

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("KDmg") != null)
        {
            GetComponent<Animator>().SetTrigger("isShake");
        }
        else
        {
            GetComponent<Animator>().ResetTrigger("isShake");
        }
    }
}
