using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehaviour : MonoBehaviourPunCallbacks
{
    public GameObject SpikeCollider;
    Animator animator;
    PlayerController player;

    private bool _balloonPopped = false;

    public bool BalloonPopped
    {
        get 
        { 
            return _balloonPopped; 
        }
        set 
        { 
            _balloonPopped = value;
            if (_balloonPopped)
            {
                animator.SetBool("isPopped", true);
                player.canMove = false;
                SpikeCollider.SetActive(false);
                GameManager.instance.balloonsLeft--;
                Debug.Log(GameManager.instance.balloonsLeft);
            }
            
        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
            return;
        if (other.CompareTag("Spike"))
        {
            BalloonPopped = true;
            
        }
    }
}
