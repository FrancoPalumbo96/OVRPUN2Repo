using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class RobotOVR : MonoBehaviour {
    
    private static RobotOVR instance = null;
    public GameObject ovrPrefab;
    private PhotonView PV;
    private VRRig vrRig;

    private VRRig _rig;
    // Start is called before the first frame update
    void Start() {

        vrRig = GetComponent<VRRig>();
        if (instance == null) {
            instance = this;
            Debug.Log("INSTANCE EQUAL TO THIS");
        }
        else if(instance != this) {
            Debug.Log("DOBLE ROBOT DELETED");
            Destroy(vrRig);
            return;
        }
        
        PV = GetComponent<PhotonView>();
        if (PV.IsMine) {

            GameObject ovr = Instantiate(ovrPrefab, this.transform.position, ovrPrefab.transform.rotation);
            _rig = GetComponent<VRRig>();

            string ovrName = ovr.name;
            Debug.Log(ovrName);
            Debug.Log(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            Debug.Log(GameObject.Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor")
                .name);

            _rig.head.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform;
            _rig.leftHand.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/LeftHandAnchor").transform;
            _rig.rightHand.vrTarget = GameObject
                .Find(ovrName + "/OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor").transform;
        }
        else {
            Destroy(vrRig);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
