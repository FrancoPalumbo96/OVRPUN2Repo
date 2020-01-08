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

    private float waitTime = 10f;
    private float timePassed = 10f;

    private void Start() {
        MyEvents.current.playerEnteredRoom += SyncVideoNewPlayer(videoPlayer.time, videoPlayer.isPlaying);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if(!other.gameObject.name.Contains("coll_hands:b_l_index2")) return;
        if(photonView != null) photonView.RequestOwnership();
        if(waiting)return;
        if(!photonView.IsMine) return;
        StartCoroutine(Wait());
        if (!isOn) {
            GetComponent<Renderer>().material.color = Color.red;
            Vector3 pos = transform.position;
            isOn = true;
            transform.position = new Vector3(pos.x, yOnPos, pos.z);
            videoPlayer.Play();
            SyncVideo(videoPlayer.time, isOn);

        }
        else {
            GetComponent<Renderer>().material.color = Color.green;
            Vector3 pos = transform.position;
            isOn = false;
            transform.position = new Vector3(pos.x, yOffPoss, pos.z);
            videoPlayer.Pause();
            SyncVideo(videoPlayer.time, isOn);

        }


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
    
    private void SyncVideo(double time, bool on) {
        photonView.RPC("setVideoTime", RpcTarget.AllBuffered, time, on);
    }

    [PunRPC]
    private void setVideoTime(double time, bool on) {
        videoPlayer.time = time;
        if(on)
            videoPlayer.Play();
        else 
            videoPlayer.Pause();
        
        Debug.Log("Video Time: " + videoPlayer.time);
    }

    /*private void Update() {
        if (!base.photonView.IsMine) return;
        timePassed -= Time.deltaTime;
        if (!(timePassed <= 0)) return;
        SyncVideo(videoPlayer.time, videoPlayer.isPlaying);
        timePassed = waitTime;
    }*/

    private System.Action SyncVideoNewPlayer(double time, bool on) {
        SyncVideo(time, on);
        return null;
    }
    
    
}
