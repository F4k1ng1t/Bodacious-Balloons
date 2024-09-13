using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server ! !");
    }
    public override void OnCreatedRoom()
    {
        Debug.Log($"Created Room : {PhotonNetwork.CurrentRoom.Name}");
    }
}

