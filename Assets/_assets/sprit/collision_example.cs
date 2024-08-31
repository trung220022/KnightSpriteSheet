using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_example : MonoBehaviour
{
    public GameObject explosion;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        explosion.SetActive(true);
        gameObject.SetActive(false);
    }
}
