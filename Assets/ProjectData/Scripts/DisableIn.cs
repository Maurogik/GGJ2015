using UnityEngine;
using System.Collections;

public class DisableIn : MonoBehaviour {

    public float duration = 10.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine (disableIn (duration));
	}
	
	IEnumerator disableIn(float time){
        yield return new WaitForSeconds (time);
        gameObject.SetActive (false);
    }
}
