using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehaviour : MonoBehaviourPunCallbacks
{
    public bool balloonPopped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (!photonView.IsMine)
        //    return;
        if (other.gameObject.CompareTag("Spike"))
        {
            balloonPopped = true;
        }
    }
}
