using System;
using UnityEngine;

public class CrateTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Throwable>(out Throwable throwable))
        {
            throwable.Release();
            GameController.Instance.UpdateScore(1);
        }
    }
}
