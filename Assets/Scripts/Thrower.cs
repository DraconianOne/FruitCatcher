using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

public class Thrower : MonoBehaviour
{
    
    [SerializeField] private Throwable[] throwablePrefabs; 
    [SerializeField] private Transform throwFromPoint;
    
    private ObjectPool<Throwable> _objectPool;
    [SerializeField] private Transform[] targets = new Transform[3];
    
    // Throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;
    
    //Extra options to control the pool capacity and maximum size    
    [SerializeField] private int defaultCapacity = 5;
    [SerializeField] private int maxSize = 20;
    [SerializeField] private int ballsInPlay = 3;

    private void Awake()
    {
        _objectPool = new ObjectPool<Throwable>
            (CreateThrowable,OnGetFromPool, OnReleaseToPool,
            OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    private Throwable CreateThrowable()
    {
        int i = (GameController.Instance.Score < 10) ? 0 : Random.Range(0, throwablePrefabs.Length);
        Debug.Log(GameController.Instance.Score + " i=" + i);
        Throwable throwableInstance = Instantiate(throwablePrefabs[i]);
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

    public void StopThrowing()
    {
        _objectPool.Clear();
    }

    public void ThrowItem()
    {
        if(_objectPool.CountActive < ballsInPlay){
        Throwable thrownObject = _objectPool.Get();
            if (!thrownObject) return;
            
            thrownObject.Init(throwFromPoint.position, targets[Random.Range(0,3)].position);
            thrownObject.Deactivate(); //Calls coroutine to deactivate
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cornflowerBlue;
        
        Gizmos.DrawLine(throwFromPoint.position, targets[0].position);
        Gizmos.DrawLine(throwFromPoint.position, targets[1].position);
        Gizmos.DrawLine(throwFromPoint.position, targets[2].position);
    }
}
