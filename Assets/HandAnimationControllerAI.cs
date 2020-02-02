using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandAnimationControllerAI : MonoBehaviour
{
  private Animator _anim;
//    private RightInteractor _handGrab;

	// Use this for initialization
	void Start ()
    {
        _anim = GetComponentInChildren<Animator>();
    ///    _handGrab = GetComponentInChildren<RightInteractor>();
     //   _handGrab = GetComponent<HandGrabbing>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//if we are pressing grab, set animator bool IsGrabbing to true
        if(Input.GetAxis("VRTK_Axis12_RightGrip") >= 0.01f)
        {
                 Debug.Log("grab");
                 AnimateGrab(true);
        }
        else
        {
                 AnimateGrab(false);
        }

        if(Input.GetAxis("VRTK_Axis10_RightTrigger") >= 0.01f)
        {
                 Debug.Log("trig");
        }

        if(Input.GetAxis("VRTK_Axis11_LeftGrip") >= 0.01f)
        {
                 Debug.Log("l grab");
                 //AnimateGrab(true);
        }
        else
        {
                //AnimateGrab(false);
        }

        if(Input.GetAxis("VRTK_Axis9_LeftTrigger") >= 0.01f)
        {
                 Debug.Log("l trig");
        }

    }

    void AnimateGrab(bool DoGrab)
    {
     
           // if (!_anim.GetBool("Grab"))
            {
                _anim.SetBool("Grab", DoGrab);
            }
  
    }

 
}


   /*
         if (GetComponent<VRTK_InteractGrab>().GetGrabbedObject != null) 
         {
            var controllerEvents = GetComponent<VRTK_ControllerEvents>();
            if (controllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.Trigger_Press))
            {
                //Do something on trigger press
                Debug.Log("trig");
            }

            if (controllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.Grip_Press))
            {
                //Do something on grip press
                 Debug.Log("grab");
            }
        }
          */

          /*
        if(null)
        {
            if (!_anim.GetBool("Grab"))
            {
                _anim.SetBool("Grab", true);
            }
        }
        else
        {
            //if we let go of grab, set IsGrabbing to false
            if(_anim.GetBool("Grab"))
            {
                _anim.SetBool("Grab", false);
            }
        }

        */