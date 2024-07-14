using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance;
    public GameObject playerspawn;

    string roomName = "room";
    

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //establishing connection
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        //connected 
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        
        Debug.Log("Joined Lobby");
        //search for a random room
        JoinRandom();
    }

    public void CreateRoom()
    {
        //create a room with random name
        RoomNameGenerater();
        PhotonNetwork.CreateRoom(roomName);
    }
    void RoomNameGenerater()
    {
        int num=Random.Range(0, 100);
        roomName=roomName+num.ToString();
    }

    public void JoinRandom()
    {
        //ries to join random room
        PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinedRoom()
    {
        // instantiate player here
        PhotonNetwork.Instantiate(Path.Combine("players"), playerspawn.transform.position, Quaternion.identity);

        Debug.Log("Room joined");
        roomName = PhotonNetwork.CurrentRoom.Name;

        
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //if no room found then create room
        RoomNameGenerater();
        PhotonNetwork.CreateRoom(roomName);
        Debug.Log("Room Creation " );
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
        Debug.LogError("Room Creation Failed: " + message);

    }
    
}
