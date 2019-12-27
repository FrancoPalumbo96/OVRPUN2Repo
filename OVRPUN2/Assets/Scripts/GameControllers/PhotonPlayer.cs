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
        Debug.Log(RobotOVR.numOfRobot);
        spawnPoints = PhotonRoom.room.spawnPoints;
        PV = GetComponent<PhotonView>();
        Debug.Log(PhotonRoom.room.spawnPoints.Count);
        //int spawnPicker = Random.Range(0, spawnPoints.Count);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot"/*"PlayerAvatarOVR"*/), 
                spawnPoints[RobotOVR.numOfRobot], Quaternion.identity, 0);
        }
    }
}
