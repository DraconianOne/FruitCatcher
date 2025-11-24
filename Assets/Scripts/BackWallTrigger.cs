using UnityEngine;

public class BackWallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Throwable>(out Throwable throwable))
        {
            if (throwable.IsCatchable()) GameController.Instance.LoseLife();
        }
    }
}
