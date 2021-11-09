using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace MirrorTest
{
    public class AutohostClient : MonoBehaviour
    {
        [SerializeField] NetworkManager networkManager;

        private void Start()
        {
            if (!Application.isBatchMode)
            {
                networkManager.StartClient();
            }
            else
            {

            }
        }

        public void JoinLocal()
        {
            networkManager.networkAddress = "localhost";
            networkManager.StartClient();
        }
    }
}

