using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using Player;

public class Throwable : MonoBehaviour
{

    public ThrownObject thrownObject;
    public float throwSpeed = 5.0f;
    [SerializeField] private float timeoutDelay = 3f;
    private Vector3 _throwTarget;

    private IObjectPool<Throwable> _objectPool;

    public IObjectPool<Throwable> ObjectPool
    {
        set => _objectPool = value;
    }

    public bool IsCatchable() { return thrownObject.IsCatchable; }

    public int Points() { return thrownObject.ItemPoints; }
    
    private void Start()
    {
        GameController.Instance.GameOver += Release;
        GameController.Instance.LifeLost += Release;
    }
    
    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    public void Init(Vector3 origin, Vector3 target)
    {
        this.transform.position = origin;
        _throwTarget = target;
    }

    public void Release()
    {
        _objectPool.Release(this);
    }

    //TODO This is an unnecessary override all because LifeLost has a parameter
    public void Release(int i)
    {
        this.Release();
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
            //transform.Translate(Vector3.left * throwSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _throwTarget, throwSpeed * Time.deltaTime);
    }
    
}
