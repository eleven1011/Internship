using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    //moving horizontal and vertical variables
    float moveX, moveY;

    //required components
    Rigidbody2D rb;
    PhotonView pv;

    void Start()
    {
        //getting the components 
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //move only our player , no other player can move with our inputs
        if (pv.IsMine)
        {
            moveX = Input.GetAxis("Horizontal") * speed;
            moveY = Input.GetAxis("Vertical") * speed;

            rb.velocity = new Vector2(moveX, moveY);

        }
    }
    
    //when player collides ,it enter trigger this and join channel
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //methods called from anoher script
        AgoraManagerVoice.Instance.rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
    }

    //when player stop colliding ,it exit trigger this and leave channel
    private void OnTriggerExit2D(Collider2D collision)
    {
        //methods called from anoher script
        AgoraManagerVoice.Instance.rtcEngine.LeaveChannel();
    }
}
