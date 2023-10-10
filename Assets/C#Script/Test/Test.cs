using System;
using System.Collections;
using C_Script.Common.Model.EventCentre;
using C_Script.Common.Model.Singleton;
using C_Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace C_Script.Test
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
            InvokeRepeating(nameof(t),1,0.1f);
        }

        private void t()
        {
            Debug.Log(1);
        }
    }
}
