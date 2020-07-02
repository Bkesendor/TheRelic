using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour
{
    Collider col;
    private void Start()
    {
        col = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        col.isTrigger = true;
    }
}
