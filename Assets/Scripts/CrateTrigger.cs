using System;
using Player;
using Player.States;
using UnityEngine;

public class CrateTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Throwable>(out Throwable throwable))
        {
            if (throwable.IsCatchable())
            {
                PlayerController.Instance.ChangeState(StateEnum.Default);
                GameController.Instance.UpdateScore(1);
            }
            else
            {
                GameController.Instance.LoseLife();
            }

            throwable.Release();
        }
    }
}
