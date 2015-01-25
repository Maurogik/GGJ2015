using UnityEngine;
using System.Collections;

public class ActivateObjectOnmessage : MonoBehaviour {

    public GameObject target;

    void Activate(){
        target.SetActive (true);
    }
}
