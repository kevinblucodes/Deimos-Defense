using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);

        //Debug.Log("Hit by " + other.gameObject);
    }
}
