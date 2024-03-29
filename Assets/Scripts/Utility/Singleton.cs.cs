﻿using UnityEngine;
using System;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        if (!instance)
        {
            if (typeof(T) != GetType())
            {
                Destroy(this);
                throw new Exception("Singleton instance type mismatch!");
            }
            instance = this as T;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

}
