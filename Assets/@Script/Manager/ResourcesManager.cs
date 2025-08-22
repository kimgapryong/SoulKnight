using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    Dictionary<string, UnityEngine.Object> _objects = new Dictionary<string, UnityEngine.Object>();
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if(_objects.ContainsKey(path))
            return _objects[path] as T;

        T obj = Resources.Load<T>($"Prefabs/{path}");
        _objects[path] = obj;

        return obj;
    }

    public GameObject Instantiate(string path, Vector3 vec, Quaternion quan, Transform parent = null, Action<GameObject> callback = null)
    {
        GameObject obj = Instantiate(path, parent, callback);

        obj.transform.localPosition = vec;
        obj.transform.localRotation = quan;

        return obj;
    }
    public GameObject Instantiate(string path, Transform parent = null, Action<GameObject> callback = null)
    {
        GameObject obj = Load<GameObject>(path);

        GameObject clone = UnityEngine.Object.Instantiate(obj, parent);
        clone.name = obj.name;

        callback?.Invoke(clone);

        return clone;
    }

}
