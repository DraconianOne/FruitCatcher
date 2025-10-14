using System;
using UnityEngine;

public class CrateTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Crate got hit by " + other.name);
        Debug.Log("Other type: " + other.GetType());
        if (other.gameObject.TryGetComponent<Throwable>(out Throwable throwable))
        {
            Debug.Log("Found a throwable");
            throwable.Release();
        }
    }
}
