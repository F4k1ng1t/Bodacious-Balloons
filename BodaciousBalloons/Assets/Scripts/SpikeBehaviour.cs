using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpikeBehaviour : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [Header("Components")]
    public Rigidbody rig;
    public GameObject BalloonCar;
    public PlayerController player;
    void Start()
    {
        rig = BalloonCar.GetComponent<Rigidbody>();
        BoxCollider boxCollider = this.GetComponent<BoxCollider>();
        player = BalloonCar.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (!photonView.IsMine)
        //    return;
        Debug.Log($"collision happened with {other.gameObject.tag}");
        if (other.CompareTag("Spike") && other.GetComponentInParent<Rigidbody>().velocity == Vector3.zero && rig.velocity != Vector3.zero)
        {
            Debug.Log("Spikes Collided Properly");
            player.canMove = false;
            rig.velocity = new Vector3(0,0,0);
            GameManager.instance.GetPlayer(other.transform.parent.gameObject).photonView.RPC("BounceBackandTurnAround", GameManager.instance.GetPlayer(other.transform.parent.gameObject).photonPlayer);

        }
    }
}
