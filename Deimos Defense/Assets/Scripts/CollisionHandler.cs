using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + "--Collided with" + other.gameObject.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.name} **Triggered by** {other.gameObject.name}");
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
