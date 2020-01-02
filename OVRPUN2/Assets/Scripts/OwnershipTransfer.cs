using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPun
{

    public void Transfer() {
        base.photonView.RequestOwnership();
    }
    
}
