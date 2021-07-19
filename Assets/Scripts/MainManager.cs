using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager mainManager; // create static instance of this class

    private void Awake()
    {
        if (mainManager != null)
        {
            Destroy(gameObject);
            return;
        }

        mainManager = this;
        DontDestroyOnLoad(gameObject); // GameObject attached to this script not to be destroyed when the scene changes.
    }
}
