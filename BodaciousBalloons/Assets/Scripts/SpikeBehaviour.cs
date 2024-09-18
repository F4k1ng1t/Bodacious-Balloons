using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpikeBehaviour : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    Rigidbody rig;
    public GameObject BalloonCar;
    PlayerController player;
    void Start()
    {
        rig = BalloonCar.GetComponent<Rigidbody>();
        player = BalloonCar.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
            return;
        Debug.Log($"collision happened with {other.gameObject.tag}");
        if (other.CompareTag("Spike") && rig.velocity != Vector3.zero)
        {
            Debug.Log("Spikes Collided Properly");
            player.canMove = false;
            rig.velocity = new Vector3(0,0,0);
            player.photonView.RPC("BounceBackandTurnAround", GameManager.instance.GetPlayer(other.transform.parent.gameObject).photonPlayer);

        }
        else if (other.CompareTag("Spike") && rig.velocity == Vector3.zero)
        {
            player.BounceBackandTurnAround();
        }
    }
}
