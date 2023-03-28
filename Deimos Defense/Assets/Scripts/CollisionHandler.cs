using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
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
        crashVFX.Play();
        DisableChildMeshRenderers(gameObject);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void DisableChildMeshRenderers(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            // Recursively disable MeshRenderers in nested children
            DisableChildMeshRenderers(child.gameObject);
        }
    }
}
