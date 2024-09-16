using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks//, IPunObservable
{
    public bool canMove = true;
    [HideInInspector]
    public int id;
    [Header("Info")]
    public float moveSpeed;
    public float turnSpeed;
    //public float jumpForce;
    //public GameObject hatObject;
    //[HideInInspector]
    //public float curHatTime;
    [Header("Components")]
    public Rigidbody rig;
    public Player photonPlayer;

    void Update()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    if (curHatTime >= GameManager.instance.timeToWin && !GameManager.instance.gameEnded)
        //    {
        //        GameManager.instance.gameEnded = true;
        //        GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, id);
        //    }
        //}
        if (photonView.IsMine)
        {
            if (canMove)
            {
                Move();
            }


        }

    }
    void Move()
    {
        float rotation = Input.GetAxis("Horizontal") * turnSpeed;
        float forwardInput = Input.GetAxis("Vertical") * moveSpeed;

        rig.angularVelocity = new Vector3(0,rotation,0);

        Vector3 movement = transform.forward * forwardInput;

        rig.velocity = new Vector3(movement.x, rig.velocity.y, movement.z);
    }
    
    [PunRPC]
    public void Initialize(Player player)
    {
        
        photonPlayer = player;
        id = player.ActorNumber;
        GameManager.instance.players[id - 1] = this;
        
        if (!photonView.IsMine)
            rig.isKinematic = true;
        
    }
    [PunRPC]
    public void BounceBackandTurnAround()
    {
        rig.AddForce(new Vector3(0,120,-120));
        rig.AddTorque(0,180,0);
        StartCoroutine(BounceBackCooldown());

    }
    IEnumerator BounceBackCooldown()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
    }


}
