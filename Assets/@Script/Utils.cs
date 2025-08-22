using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        T com = obj.GetComponent<T>();
        if(com == null)
            com = obj.AddComponent<T>();
        return com;
    }

    public static T FindChild<T>(this GameObject obj, string name) where T : UnityEngine.Object
    {
        if (typeof(T) == typeof(GameObject))
            return FindChild(obj, name) as T;

        foreach (var child in obj.GetComponentsInChildren<T>())
            if (child.name.Equals(name))
                return child;

        return null;
    }

    public static GameObject FindChild(GameObject obj, string name)
    {
        foreach(Transform t in obj.GetComponentsInChildren<Transform>())
            if(t.name == name)
                return t.gameObject;

        return null;
    }
}
