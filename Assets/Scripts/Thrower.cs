using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;
public class Thrower : MonoBehaviour
{
    
    [SerializeField] private Throwable throwablePrefab; 
    [SerializeField] private Transform throwFromPoint;
    
    private IObjectPool<Throwable> _objectPool;
    
    // Throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;
    
    //Extra optins to control the pool capacity and maximum size    
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private void Awake()
    {
        _objectPool = new ObjectPool<Throwable>
            (CreateThrowable,OnGetFromPool, OnReleaseToPool,
            OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);

    }

    private Throwable CreateThrowable()
    {
        Throwable throwableInstance = Instantiate(throwablePrefab);
        throwableInstance.ObjectPool = _objectPool;
        return throwableInstance;
    }

    private void OnReleaseToPool(Throwable throwableObject)
    {
        throwableObject.gameObject.SetActive(false);
    }
    
    private void OnGetFromPool(Throwable pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }
    
    private void OnDestroyPooledObject(Throwable pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ThrowItem();
        }
    }

    public void ThrowItem()
    {
        Throwable thrownObject = _objectPool.Get();
        if (!thrownObject) return;
        
        Debug.Log("Throwing object");
        
        thrownObject.transform.position = throwFromPoint.position;
        
        thrownObject.Deactivate(); //Calls coroutine to deactivate
        
    }
    
}
