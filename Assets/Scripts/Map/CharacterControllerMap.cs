using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMap : MonoBehaviour
{
    public float Speed = 3f;
    public bool IsMoving { get; private set; }

    public Pin CurrentPin { get; private set; }
    private Pin targetPin;
    private MapManager mapManagere;

    public Animator animator;

    public void Initialise(MapManager mapManager, Pin startPin)
    {
        mapManagere = mapManager;
        SetCurrentPin(startPin);
    }


    /// <summary>
    /// This runs once a frame
    /// </summary>
    private void Update()
    {
        if (targetPin == null) return;

        // Get the characters current position and the targets position
        var currentPosition = transform.position;
        var targetPosition = targetPin.transform.position;
        

        // If the character isn't that close to the target move closer
        if (Vector3.Distance(currentPosition, targetPosition + new Vector3(0, 1, 0)) > .02f)
        {
            transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition + new Vector3(0,1,0),
                Time.deltaTime * Speed
            );
        }
        else
        {
            if (targetPin.IsAutomatic)
            {
                // Get a direction to keep moving in
                var pin = targetPin.GetNextPin(CurrentPin);
                MoveToPin(pin);
            }
            else
            {
                SetCurrentPin(targetPin);
            }
        }
    }


    /// <summary>
    /// Check the if the current pin has a reference to another in a direction
    /// If it does the move there
    /// </summary>
    /// <param name="direction"></param>
    public void TrySetDirection(Direction direction)
    {
        // Try get the next pin
        var pin = CurrentPin.GetPinInDirection(direction);

        // If there is a pin then move to it
        if (pin == null) return;
        MoveToPin(pin);
        //if (pin.Equals(pin.LeftPin))
        //{
        //    gameObject.transform.eulerAngles = new Vector3(180, 180, 180);
        //    //this.gameObject.transform.rotation.SetEulerRotation(180, 180, 180);
        //}
        //if (pin.Equals(pin.RightPin))
        //{
        //    gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        //    //this.gameObject.transform.rotation.SetEulerRotation(0, 180, 0);
        //}
    }


    /// <summary>
    /// Move to a new pin
    /// </summary>
    /// <param name="pin"></param>
    private void MoveToPin(Pin pin)
    {
        targetPin = pin;
        CurrentPin.GetComponent<Animator>().SetBool("selected", false);
        //animatorPin.SetBool("active", false);
        if (targetPin.LeftPin)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            //this.gameObject.transform.rotation.SetEulerRotation(180, 180, 180);
        }
        if (targetPin.RightPin)
        {
            gameObject.transform.eulerAngles = new Vector3(180, 180, 180);
            //this.gameObject.transform.rotation.SetEulerRotation(0, 180, 0);
        }
        IsMoving = true;
        animator.SetBool("change", true);
    }


    /// <summary>
    /// Set the current pin
    /// </summary>
    /// <param name="pin"></param>
    public void SetCurrentPin(Pin pin)
    {
        CurrentPin = pin;
        targetPin = null;
        transform.position = pin.transform.position + new Vector3(0,1,0);
        IsMoving = false;

        // Tell the map manager that
        // the current pin has changed
        mapManagere.UpdateGui();
        animator.SetBool("change", false);
        pin.GetComponent<Animator>().SetBool("selected",true);
        //animatorPin.SetBool("active", true);
    }
}
