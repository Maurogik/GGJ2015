using UnityEngine;
using System.Collections;

public class GameEndMgr : MonoBehaviour {

    public GameObject[] objToActive;

	// Use this for initialization
	void OnEnable () {
	    foreach (GameObject obj in objToActive) {
            obj.SetActive(true);
        }
	}
}
