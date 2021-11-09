using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace MirrorTest
{
    public class Player : NetworkBehaviour
    {
        public static Player localPlayer;
        [SyncVar] public string matchID;

        NetworkMatch networkMatchChecker;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (localPlayer != null && localPlayer != this)
            {
                Destroy(gameObject);
            }
            else if (isLocalPlayer)
            {
                localPlayer = this;
            }
            networkMatchChecker = GetComponent<NetworkMatch>();
        }

        public void HostGame()
        {
            string matchID = MatchMaker.GetRandomMatchID();
            CmdHostGame(matchID);
        }

        [Command]
        void CmdHostGame(string _matchID)
        {
            matchID = _matchID;
            if(MatchMaker.instance.HostGame(_matchID, gameObject))
            {
                networkMatchChecker.matchId = _matchID.ToGuid();
                TargetHostGame(true, _matchID);
                Debug.Log("hosted");
            }
            else
            {
                TargetHostGame(false, _matchID);
                Debug.Log("Not hosted");
            }
        }

        [TargetRpc]
        void TargetHostGame(bool success, string _matchID)
        {
            UILobby.instance.HostSuccess(success);
        }


        public void JoinGame()
        {
            string matchID = MatchMaker.GetRandomMatchID();
            CmdJoinGame(matchID);
        }

        [Command]
        void CmdJoinGame(string _matchID)
        {
            matchID = _matchID;
            if (MatchMaker.instance.JoinGame(_matchID, gameObject))
            {
                networkMatchChecker.matchId = _matchID.ToGuid();
                TargetJoinGame(true, _matchID);
                Debug.Log("hosted");
            }
            else
            {
                TargetJoinGame(false, _matchID);
                Debug.Log("Not hosted");
            }
        }

        [TargetRpc]
        void TargetJoinGame(bool success, string _matchID)
        {
            UILobby.instance.JoinSuccess(success);
        }
    }
}

