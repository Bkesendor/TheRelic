using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollide : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    Collider col;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Lo");
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Lo")
        {
            rb.isKinematic = true;
            col.isTrigger = true;
        }
    }

}
