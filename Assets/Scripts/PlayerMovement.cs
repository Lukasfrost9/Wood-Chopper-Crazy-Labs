using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerCharacter")]
    public float playerSpeed = 2.0f;
    public float gravityValue = -9.81f;

    [Header("Upgrades")]
    public float ChoppingTime;
    public float ChoppingPower;

    [Header("References")]
    public Joystick moveJoystick;
    public CharacterController controller;
    public CameraTracking Camera;
    public AudioManager Audio;
    public Animator AnimationController;
    GameObject TreeToChop;
    TMP_Text DebugText;
    List<GameObject> targets;
    Vector3 move;

    private void Start()
    {
        AnimationController = gameObject.GetComponentInParent<Animator>();
        DebugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        targets = new List<GameObject>();
    }


    void Update()
    {
        if (moveJoystick.InputDirection != Vector3.zero)
        {
            move = moveJoystick.InputDirection;
            AnimationController.SetBool("IsWalking", true);
            TreeToChop = null;
            AnimationController.SetBool("IsChopping", false);
            transform.rotation = Quaternion.LookRotation(moveJoystick.InputDirection);
        }
        else
        {
            AnimationController.SetBool("IsWalking", false);
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        controller.Move(move * Time.deltaTime * playerSpeed);

        //Call Camera move  
        Camera.UpdateCameraPosition();
    }


    private void OnTriggerExit(Collider collision)
    {
        DebugText.text = "";
        AnimationController.SetBool("IsChopping", false);
    }
    void OnTriggerStay(Collider other)
    {
        if (moveJoystick.InputDirection == Vector3.zero && other.gameObject.CompareTag("Tree") && AnimationController.GetBool("IsChopping") == false)
        {
            AnimationController.SetBool("IsChopping", true);
            TreeToChop = other.gameObject;
            StartCoroutine("Chopping");
        }
    }

    IEnumerator Chopping()
    {
        if (TreeToChop != null) // If Tree exists chop it
        {
            Audio.Play("WoodChop");
            AnimationController.SetBool("IsChopping", true);
            AnimationController.SetBool("IsWalking", false);
            DebugText.text = TreeToChop.name + " has been chopped for damage equal to " + ChoppingPower;
            DebugText.text += ("\n Current Tree health is " + TreeToChop.GetComponent<TreeScript>().TreeHealth);
            TreeToChop.GetComponent<TreeScript>().Chopped(ChoppingPower);
            yield return new WaitForSeconds(ChoppingTime);
            AnimationController.SetBool("IsChopping", false);

            StartCoroutine("Chopping");
        }
        else //Tree doesn't exist or is null, stop chopping, remove it
        {
            TreeToChop = null;
            AnimationController.SetBool("IsChopping", false);

        }

    }
}
