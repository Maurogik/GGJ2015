using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class XBoxController : MonoBehaviour {

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    public static XBoxController instance = null;
    
    // Use this for initialization
    void Start()
    { 
        instance = this;
        // No need to initialize anything for the plugin
    }
    
    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected) {
            for (int i = 0; i < 4; ++i) {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState (testPlayerIndex);
                if (testState.IsConnected) {
                    Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
        
        prevState = state;
        state = GamePad.GetState (playerIndex);

        
        // Set vibration according to triggers
        GamePad.SetVibration (playerIndex, state.Triggers.Left, state.Triggers.Right);
        
        // Make the current object turn
        transform.localRotation *= Quaternion.Euler (0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
    }

	public bool GetButtonADown(){
        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) {
            return true;
        }
        return false;
	}

    public bool GetButtonA(){
        // Detect if a button was pressed this frame
        if (state.Buttons.A == ButtonState.Pressed) {
            return true;
        }
        return false;
    }

    public bool GetButtonBDown(){
        // Detect if a button was pressed this frame
        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed) {
            return true;
        }
        return false;
    }
    
    public bool GetButtonB(){
        // Detect if a button was pressed this frame
        if (state.Buttons.B == ButtonState.Pressed) {
            return true;
        }
        return false;
    }

    public void Vibrate(float leftVal, float rightVal){
        GamePad.SetVibration (playerIndex, leftVal, rightVal);
    }

    public Vector2 GetLeftStick(){
        return new Vector2 (state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
    }

    public Vector2 GetRightStick(){
        return new Vector2 (state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
    }

}
