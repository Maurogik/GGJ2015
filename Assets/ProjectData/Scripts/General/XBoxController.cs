using UnityEngine;
using System.Collections;
#if !UNITY_EDITOR_OSX
using XInputDotNetPure; // Required in C#
#endif

public class XBoxController : MonoBehaviour {

	#if !UNITY_EDITOR_OSX
		bool playerIndexSet = false;

		PlayerIndex playerIndex;
		GamePadState state;
		GamePadState prevState;
	#else
		public float rotationSpeed = 5.0f;
	#endif

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

		#if !UNITY_EDITOR_OSX
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
		#endif

        
    }

	public bool GetButtonADown(){
		#if !UNITY_EDITOR_OSX
		// Detect if a button was pressed this frame
		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) {
			return true;
		}
		#endif
		return false;
	}
	
	public bool GetButtonA(){
		#if !UNITY_EDITOR_OSX
		// Detect if a button was pressed this frame
		if (state.Buttons.A == ButtonState.Pressed) {
			return true;
		}
		#endif
		return false;
	}
	
	public bool GetButtonBDown(){
		#if !UNITY_EDITOR_OSX
		// Detect if a button was pressed this frame
		if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed) {
			return true;
		}
		#endif
		return false;
	}
	
	public bool GetButtonB(){
		#if !UNITY_EDITOR_OSX
		// Detect if a button was pressed this frame
		if (state.Buttons.B == ButtonState.Pressed) {
			return true;
		}
		#endif
		return false;
	}
	
	public void Vibrate(float leftVal, float rightVal){
		#if !UNITY_EDITOR_OSX
		GamePad.SetVibration (playerIndex, leftVal, rightVal);
		#endif
	}

	public Vector2 GetLeftStick(){
		#if !UNITY_EDITOR_OSX
		return new Vector2 (state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
		#else
		return new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		#endif
	}
	
	public Vector2 GetRightStick(){
		#if !UNITY_EDITOR_OSX
		return new Vector2 (state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
		#else
		return new Vector2 (rotationSpeed * Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		#endif

	}
}
