using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{

    public static PhotonRoom room;
    private PhotonView PV;

    public int multiplayerScene;

    public int currentScene;

    public List<Vector3> spawnPoints;

    public static int players;
    // Start is called before the first frame update
    private void Awake()
    {
        //setup singelton
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
            spawnPoints = new List<Vector3> {
                new Vector3(3f,1,0f),
                new Vector3(3,1,3),
                new Vector3(3,1,-3),
                new Vector3(-3f,1, 0f),
                new Vector3(-3f,1, 3f),
                new Vector3(-3f,1, -3f),
            };
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
                spawnPoints = new List<Vector3> {
                    new Vector3(1,0,1),
                    new Vector3(-1,0, 1)
                };
                
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }

    

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Has joined room");
        if(!PhotonNetwork.IsMasterClient) return;
        StartGame();

    }

    private void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.LoadLevel(multiplayerScene);
    }

    private void OnSceneFinishLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayerScene)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position,
            Quaternion.identity, 0);
    }

    public static void AddPlayer() {
        players = players + 1;
    }

}
