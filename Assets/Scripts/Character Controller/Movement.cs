using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    [Header("PLAYER MOVEMENT")]//how to write a header
    [Space(10)]//making space between the text in Unity
    [Header("Characters MoveDirection")]
    //vector3 called moveDirection
    public Vector3 moveDirection;
    //we will use this to apply movement in worldspace
    //private CharacterController (https://docs.unity3d.com/ScriptReference/CharacterController.html) charC
    public CharacterController _charC;
    [Header("Character Variables")]
    //public float variables jumpSpeed, speed, gravity
    public float jumpSpeed;
    public float speed;
    public float gravity;
    //Another thing to write "public float jumpSpeed, speed, gravity;"

    public static bool canMove;
    #endregion
    #region Start
    private void Start()
    {
        //charc is on this game object we need to get the character controller that is attached to it
        _charC = this.GetComponent<CharacterController>();
        canMove = true;
    }
    #endregion
    #region Update
    private void Update()
    {
        if (canMove)
        {
            //if our character is grounded
            if (_charC.isGrounded) //we are able to move in game scene meaning
            {
                //moveDir has the value of Input.Get Axis.. Horizontal, 0, Vertical
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//"Horizontal" is "A & D" & "Vertical" is "W & S"
                                                                                                       //moveDir is transformed in the direction of our moveDir
                moveDirection = transform.TransformDirection(moveDirection);//forward according to the player (that is the actual person who plays the player) on its local (meaning where the character is heading).
                                                                            //our moveDir is then multiplied by our speed
                moveDirection *= speed;
                //we can also jump if we are grounded so
                //if the input button for jump is pressed then
                if (Input.GetButton("Jump"))
                {
                    //our moveDir.y is equal to our jump speed
                    moveDirection.y = jumpSpeed;
                }

            }

            //regardless of if we are grounded or not the players moveDir.y is always affected by gravity timesed my time.deltaTime to normalize it
            moveDirection.y -= gravity * Time.deltaTime;
            //we then tell the character Controller that it is moving in a direction timesed Time.deltaTime
            _charC.Move(moveDirection * Time.deltaTime);


        }
    }
    #endregion
}
