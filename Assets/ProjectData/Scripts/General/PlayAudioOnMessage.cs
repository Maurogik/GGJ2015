using UnityEngine;
using System.Collections;

public class PlayAudioOnMessage : MonoBehaviour {

    public AudioSource source;

    public void OnPlay(){
        source.Play ();
    }
}
