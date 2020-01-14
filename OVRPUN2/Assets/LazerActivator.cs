using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LazerActivator : MonoBehaviour {

    public GameObject visualLazer;
    public GameObject lazerEventSystem;

    private bool isLazerActive = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DeactivateLazer();
    }

    private void Update() {
        LazerManager();
    }

    private void LazerManager() {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) {
            
            if (isLazerActive) {
                DeactivateLazer();
            }
            else {
                ActivateLazer();
            }
            
        }
        /*if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch)) {
            Debug.LogWarning("Activated Lazer Left");
        }*/
    }

    private void DeactivateLazer() {
        visualLazer.SetActive(false);
        lazerEventSystem.SetActive(false);
        isLazerActive = false;
        Debug.LogWarning("Lazer Deactivated");

    }

    private void ActivateLazer() {
        visualLazer.SetActive(true);
        lazerEventSystem.SetActive(true);
        isLazerActive = true;
        Debug.LogWarning("Lazer Ativated");

    }
}
