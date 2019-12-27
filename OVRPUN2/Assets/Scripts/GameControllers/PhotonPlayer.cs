using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{

    private PhotonView PV;

    public GameObject myAvatar;
    
    private List<Vector3> spawnPoints;

   
    
    // Start is called before the first frame update
    void Start() {
        PV = GetComponent<PhotonView>();
       
        if (PV.IsMine)
        {
            PhotonRoom.AddPlayer();           
            spawnPoints = PhotonRoom.room.spawnPoints;
            Debug.Log(spawnPoints[PhotonRoom.players-1]);
            
            Debug.Log(PhotonRoom.room.spawnPoints.Count);
            
            int spawnPicker = Random.Range(0, spawnPoints.Count);
            
            
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot"/*"PlayerAvatarOVR"*/), 
                spawnPoints[spawnPicker], Quaternion.identity, 0);
        }
        Debug.Log("Numb of Players: "+PhotonRoom.players);
    }
}
