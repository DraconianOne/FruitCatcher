using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class Throwable : MonoBehaviour
{

    public float throwSpeed = 5.0f;
    [SerializeField] private float timeoutDelay = 3f;
    
    private IObjectPool<Throwable> _objectPool;
    
    public IObjectPool<Throwable> ObjectPool {set => _objectPool = value;}
    
    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    public void Release()
    {
        _objectPool.Release(this);
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Reset position?

        // Release the projectile back to the pool
        Release();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        MoveThrowable();
    }
    
    void MoveThrowable()
    {
            transform.Translate(Vector3.left * throwSpeed * Time.deltaTime);
    }
    
}
