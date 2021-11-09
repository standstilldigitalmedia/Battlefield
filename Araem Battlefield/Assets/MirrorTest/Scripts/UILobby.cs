using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MirrorTest
{
    public class UILobby : MonoBehaviour
    {
        public static UILobby instance;

        [SerializeField] InputField joinMatchInput;
        [SerializeField] Button joinButton;
        [SerializeField] Button hostButton;
        [SerializeField] Canvas lobbyCanvas;

        public void Host()
        {
            joinMatchInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;
            Player.localPlayer.HostGame();
        }

        public void HostSuccess(bool success)
        {
            if(success)
            {
                lobbyCanvas.enabled = true;
            }
            else
            {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }

        public void JoinSuccess(bool success)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
            }
            else
            {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }

        public void Join()
        {
            joinMatchInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;
        }

        private void Start()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else 
            {
                instance = this;
            }
        }
    }        
}

