using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour{

    public enum Location{
        CORRIDOR,
        CAVE,
        WATER
    }

    public static Location currentLocation;


    public GameObject[] locationChangeTargets;

    public static float LightAccumCheat = 0.0f;

    public void Awake(){
        Application.targetFrameRate = 75;
    }

    private void changeLocation(){
        Debug.Log ("location chnaged " + currentLocation);
        foreach (GameObject obj in locationChangeTargets) {
            obj.SendMessage("OnLocationChanged");
        }
    }

    public void SetWaterState(){
        currentLocation = Location.WATER;
        changeLocation ();
    }

    public void SetCaveState(){
        currentLocation = Location.CAVE;
        changeLocation ();
    }

    public void SetCorridorState(){
        currentLocation = Location.CORRIDOR;
        changeLocation ();
    }
}
