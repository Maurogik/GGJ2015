using UnityEngine;
using System.Collections;

public class EnableForPlatform : MonoBehaviour {

    public bool editor = false;
    public bool windows = false;

	// Use this for initialization
	void OnEnable () {
#if UNITY_EDITOR
        if(editor){
            gameObject.SetActive(true);
            return;
        }
#else
        if(windows){
            gameObject.SetActive(true);
            return;
        }
#endif
        gameObject.SetActive(false);
	}
	
}
