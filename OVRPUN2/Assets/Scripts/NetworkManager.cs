using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviour, IMatchmakingCallbacks
{
    public const byte InstantiateVrAvatarEventCode = 1; // example code, change to any value between 1 and 199

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    #region IMatchmakingCallbacks

    public void OnJoinedRoom()
    {
        GameObject localAvatar = Instantiate(Resources.Load("LocalAvatar")) as GameObject;
        PhotonView photonView = localAvatar.GetComponent<PhotonView>();

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                CachingOption = EventCaching.AddToRoomCache,
                Receivers = ReceiverGroup.Others
            };

            PhotonNetwork.RaiseEvent(InstantiateVrAvatarEventCode, photonView.ViewID, raiseEventOptions, SendOptions.SendReliable);
        }
        else
        {
            Debug.LogError("Failed to allocate a ViewId.");

            Destroy(localAvatar);
        }
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public void OnLeftRoom()
    {
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == InstantiateVrAvatarEventCode)
        {
            GameObject remoteAvatar = Instantiate(Resources.Load("RemoteAvatar")) as GameObject;
            PhotonView photonView = remoteAvatar.GetComponent<PhotonView>();
            photonView.ViewID = (int) photonEvent.CustomData;
        }
    }
    
    #endregion
}
