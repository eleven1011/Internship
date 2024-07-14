using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using System;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class AgoraManagerVoice : MonoBehaviourPunCallbacks
{
    string appID = "613624aa397641999634bcc740e3a2c4";

    public static AgoraManagerVoice Instance;
    

    public IRtcEngine rtcEngine;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (string.IsNullOrEmpty(appID))
        {
            Debug.LogError("App ID not set in VoiceChatManager script");
            return;
        }

        rtcEngine = IRtcEngine.GetEngine(appID);

        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnLeaveChannel;
        rtcEngine.OnError += OnError;

        rtcEngine.EnableSoundPositionIndication(true);
    }
    

    void OnError(int error, string msg)
    {
        Debug.LogError("Error with Agora: " + msg);
    }

    void OnLeaveChannel(RtcStats stats)
    {
        Debug.Log("Left channel with duration " + stats.duration);
    }

    void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Joined channel " + channelName);

        Hashtable hash = new Hashtable();
        hash.Add("agoraID", uid.ToString());
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;
    }

    

    void OnDestroy()
    {
        IRtcEngine.Destroy();
    }
}
