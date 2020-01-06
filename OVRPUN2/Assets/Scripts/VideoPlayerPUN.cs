
using Photon.Pun;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerPUN : MonoBehaviourPun, IPunObservable {
    private VideoPlayer videoPlayer;
    
    // Start is called before the first frame update
    void Start() {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    /*[PunRPC]*/
    private void setVideoTime(double videoTime) {
        videoPlayer.time = videoTime;
        Debug.Log("Video Time: " + videoPlayer.time);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(videoPlayer.time);
        }

        if (stream.IsReading) {
            setVideoTime((double)stream.ReceiveNext());
        }
    }
}
