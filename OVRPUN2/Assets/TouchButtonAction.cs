using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Video;

public class TouchButtonAction : MonoBehaviourPun {

    public VideoPlayer videoPlayer;
    private float yOnPos = 1.065f;
    private float yOffPoss = 1.1f;
    private bool isOn = false;
    
    private bool waiting = false;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if(!other.gameObject.name.Contains("coll_hands:b_l_index2")) return;
        if(waiting)return;
        StartCoroutine(Wait());
        if (!isOn) {
            GetComponent<Renderer>().material.color = Color.red;
            Vector3 pos = transform.position;
            isOn = true;
            transform.position = new Vector3(pos.x, yOnPos, pos.z);
            videoPlayer.Play();
            
        }
        else {
            GetComponent<Renderer>().material.color = Color.green;
            Vector3 pos = transform.position;
            isOn = false;
            transform.position = new Vector3(pos.x, yOffPoss, pos.z);
            videoPlayer.Pause();
        }
        if(photonView.IsMine)
            SyncVideo(videoPlayer.time);


    }

    private IEnumerator Wait() {
        waiting = true;
        yield return new WaitForSeconds(1);
        waiting = false;
    }
    
    private IEnumerator MovingButton() {
        //TODO move down
        /*for (int i = 0; i < 1; i++) {
            
            
            yield return null;
        }*/
        yield return null;
        GetComponent<Renderer>().material.color = Color.red;
    }
    
    private void SyncVideo(double time) {
        photonView.RPC("setVideoTime", RpcTarget.AllBuffered, time);
    }

    [PunRPC]
    private void setVideoTime(double time) {
        videoPlayer.time = time;
        Debug.Log("Video Time: " + videoPlayer.time);
    }
}
