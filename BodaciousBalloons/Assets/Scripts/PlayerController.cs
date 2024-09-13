using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    [HideInInspector]
    public int id;
    [Header("Info")]
    public float moveSpeed;
    public float turnSpeed;
    //public float jumpForce;
    public GameObject hatObject;
    [HideInInspector]
    public float curHatTime;
    [Header("Components")]
    public Rigidbody rig;
    public Player photonPlayer;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (curHatTime >= GameManager.instance.timeToWin && !GameManager.instance.gameEnded)
{
                GameManager.instance.gameEnded = true;
                GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, id);
            }
        }
        //if (photonView.IsMine)
        //{
            Move();
            //if (Input.GetKeyDown(KeyCode.Space))
            //    TryJump();

            //if (hatObject.activeInHierarchy)
            //    curHatTime += Time.deltaTime;
        //}
            
    }
    void Move()
    {
        float rotation = Input.GetAxis("Horizontal") * turnSpeed;
        float forwardInput = Input.GetAxis("Vertical") * moveSpeed;

        rig.angularVelocity = new Vector3(0,rotation,0);

        Vector3 movement = transform.forward * forwardInput;

        rig.velocity = new Vector3(movement.x, rig.velocity.y, movement.z);
    }
    //void TryJump()
    //{
    //    Ray ray = new Ray(transform.position, Vector3.down);
    //    if (Physics.Raycast(ray, 0.7f))
    //        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //}
    [PunRPC]
    public void Initialize(Player player)
    {
        
        photonPlayer = player;
        id = player.ActorNumber;
        GameManager.instance.players[id - 1] = this;
        
        if (!photonView.IsMine)
            rig.isKinematic = true;
        if (id == 1)
            GameManager.instance.GiveHat(id, true);
    }
    
    public void SetHat(bool hasHat)
    {
        hatObject.SetActive(hasHat);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!photonView.IsMine)
            return;
        // did we hit another player?
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (GameManager.instance.GetPlayer(collision.gameObject).id == GameManager.instance.playerWithHat)
{
                // can we get the hat?
                if (GameManager.instance.CanGetHat())
                {
                    // give us the hat
                    GameManager.instance.photonView.RPC("GiveHat", RpcTarget.All, id, false);
                }
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(curHatTime);
        }
        else if (stream.IsReading)
        {
            curHatTime = (float)stream.ReceiveNext();
        }
    }
}
