
using UnityEngine;
using Mirror;
using System;

namespace MirrorTest
{
    [System.Serializable]
    public class Match
    {
        public Match(){}

        public string matchID;
        SyncList<GameObject> players = new SyncList<GameObject>();
        //public SyncListString players = new SyncListString();

        public Match(string _matchID, GameObject _player)
        {
            this.matchID = _matchID;
            players.Add(_player);
        }

        public void AddPlayer(GameObject _player)
        {
            players.Add(_player);
        }
    }

    /*[System.Serializable]
    public class SyncListGameObject : SyncList<GameObject>
    {
        public SyncListGameObject(){}
    }

    [System.Serializable]
    public class SyncListMatch : SyncList<Match>
    {
        public SyncListMatch() { }
    }

    [System.Serializable]
    public class SyncListString : SyncList<string>
    {
        public SyncListString() { }
    }*/

    public class MatchMaker : NetworkBehaviour
    {
        public static MatchMaker instance;
        public SyncList<Match> matches = new SyncList<Match>();
        public SyncList<string> matchIDs = new SyncList<string>();

        public bool HostGame(string _matchID, GameObject _player)
        {
            if(!matchIDs.Contains(_matchID))
            {
                matchIDs.Add(_matchID);
                matches.Add(new Match(_matchID, _player));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool JoinGame(string _matchID, GameObject _player)
        {
            if (matchIDs.Contains(_matchID))
            {
                for(int i = 0; i < matches.Count; i++)
                {
                    if(matches[i].matchID == _matchID)
                    {
                        matches[i].AddPlayer(_player);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetRandomMatchID()
        {
            string id = string.Empty;
            for(int a = 0; a < 5; a++)
            {
                int random = UnityEngine.Random.Range(0, 36);
                if(random < 26)
                {
                    id += (char)(random + 65);
                }
                else
                {
                    id += (random - 26).ToString();
                }
            }
            return id;
        }

        private void Awake()
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
