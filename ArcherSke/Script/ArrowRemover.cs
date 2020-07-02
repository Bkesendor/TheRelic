using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRemover : MonoBehaviour
{

    private void Update()
    {
        StartCoroutine(CleaningTeam());
    }

    IEnumerator CleaningTeam()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
