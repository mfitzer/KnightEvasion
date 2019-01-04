using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    //Vive Controller Input
    public SteamVR_TrackedObject leftTrackedObj;
    public SteamVR_TrackedController leftTrackedController;
    public SteamVR_Controller.Device leftController
    {
        get { return SteamVR_Controller.Input((int)leftTrackedObj.index); }
    }
    public SteamVR_TrackedObject rightTrackedObj;
    public SteamVR_TrackedController rightTrackedController;
    public SteamVR_Controller.Device rightController
    {
        get { return SteamVR_Controller.Input((int)rightTrackedObj.index); }
    }

    UIManager uiManager;

    void rightTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (GameManager.gameState != GameManager.GAMESTATE.PLAYING)
        {
            uiManager.startGame();
        }
    }

    void rightTriggerDown()
    {

    }

    void rightTriggerUp()
    {

    }

    void rightPadClicked(object sender, ClickedEventArgs e)
    {
        
    }

    void rightGripped(object sender, ClickedEventArgs e)
    {
        
    }

    void leftTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (GameManager.gameState != GameManager.GAMESTATE.PLAYING)
        {
            uiManager.startGame();
        }
    }

    void leftTriggerDown()
    {

    }

    void leftTriggerUp()
    {

    }

    void leftPadClicked(object sender, ClickedEventArgs e)
    {
        
    }    

    void leftGripped(object sender, ClickedEventArgs e)
    {
        
    }

    public void triggerHapticFeedBack(bool left, ushort leftDuration, bool right, ushort rightDuration)
    {
        if (left)
        {
            leftController.TriggerHapticPulse(leftDuration);
        }
        if (right)
        {
            rightController.TriggerHapticPulse(rightDuration);
        }
    }

    // Use this for initialization
    void Start()
    {
        //Registering functions with input events
        leftTrackedController.PadClicked += leftPadClicked;
        leftTrackedController.TriggerClicked += leftTriggerClicked;
        leftTrackedController.Gripped += leftGripped;
        rightTrackedController.PadClicked += rightPadClicked;
        rightTrackedController.TriggerClicked += rightTriggerClicked;
        rightTrackedController.Gripped += rightGripped;

        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool leftPadPressed = leftController.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
        
        bool rightPadPressed = rightController.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
        

        if (leftController.GetHairTriggerDown())
        {
            leftTriggerDown();
        }
        if (rightController.GetHairTriggerDown())
        {
            rightTriggerDown();
        }
        if (leftController.GetHairTriggerUp())
        {
            leftTriggerUp();
        }
        if (rightController.GetHairTriggerUp())
        {
            rightTriggerUp();
        }
    }
}
