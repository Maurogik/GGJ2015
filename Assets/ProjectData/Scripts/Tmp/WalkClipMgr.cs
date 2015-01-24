using UnityEngine;
using System.Collections;

public class WalkClipMgr : MonoBehaviour {

    public AudioSource walkSource;
    public AudioClip walkCave;
    public AudioClip walkCorridor;
    public AudioClip walkWater;

    public void OnLocationChanged(){
        switch (GameState.currentLocation) {
        case GameState.Location.CAVE :
            walkSource.clip = walkCave;
            break;
        case GameState.Location.CORRIDOR :
            walkSource.clip = walkCorridor;
            break;
        case GameState.Location.WATER :
            walkSource.clip = walkWater;
            break;
        }
        walkSource.volume = 0.0f;
        walkSource.Play ();
    }
}
