using UnityEngine;
using System.Collections;

public class AmbiantSoundMgr : MonoBehaviour {

    public AudioSource source;
    public AudioClip cave;
    public AudioClip corridor;
    //public AudioClip walkWater;
    
    public void OnLocationChanged(){
        switch (GameState.currentLocation) {
        case GameState.Location.CAVE :
            source.clip = cave;
            break;
        case GameState.Location.CORRIDOR :
            source.clip = corridor;
            break;
        /*case GameState.Location.WATER :
            walkSource.clip = walkWater;
            break;*/
        }
        //walkSource.volume = 0.0f;
        source.Play ();
    }
}
