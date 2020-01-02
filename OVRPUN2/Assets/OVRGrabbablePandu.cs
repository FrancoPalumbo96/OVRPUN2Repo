/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Licensed under the Oculus Utilities SDK License Version 1.31 (the "License"); you may not use
the Utilities SDK except in compliance with the License, which is provided at the time of installation
or download, or which otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at
https://developer.oculus.com/licenses/utilities-1.31

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using System;
using Photon.Pun;
using UnityEngine;
using Random = Unity.Mathematics.Random;

/// <summary>
/// An object that can be grabbed and thrown by OVRGrabber.
/// </summary>
public class OVRGrabbablePandu : OVRGrabbable
{
    

    private PhotonView photonView;
	/// <summary>
	/// If true, the object can currently be grabbed.
	/// </summary>
    

	/// <summary>
	/// Notifies the object that it has been grabbed.
	/// </summary>
	virtual public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        m_grabbedBy = hand;
        m_grabbedCollider = grabPoint;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        photonView.RequestOwnership();
    }
	


    protected virtual void Start()
    {
        m_grabbedKinematic = GetComponent<Rigidbody>().isKinematic;
        photonView = GetComponent<PhotonView>();

    }

    
}

