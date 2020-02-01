using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T: MonoBehaviour
{
    private Queue<T> _pool;
    private GameObject _prefab;
    private Transform _parent;
    private bool _extendable;

    public Pool(GameObject prefab, int initialPoolSize, Transform objectsParent, bool extendable = false)
    {
        _prefab = prefab;
        _parent = objectsParent;
        _extendable = extendable;

        _pool = new Queue<T>(initialPoolSize);

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = InstantiatePrefab();
            var meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
            _pool.Enqueue(obj.GetComponent<T>());
        }
    }

    private GameObject InstantiatePrefab()
    {
        return Object.Instantiate(_prefab, _parent);
    }

    public void ReturnObjectToPool(T obj)
    {
        _pool.Enqueue(obj);
    }

    public T GetObjectFromPool()
    {
        T obj = null;

        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }
        else if(_extendable)
        {
            obj = InstantiatePrefab().GetComponent<T>();
        }

        return obj;
    }
}
