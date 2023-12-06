using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    //[SerializeField] private InputAction _action;
    [SerializeField] private float _speedMovement = 30f;

    [Header("Limits for posotions")]
    [SerializeField] private float _RangeXPos = 8f;
    [SerializeField] private float _minRangeYPos = 6f;
    [SerializeField] private float _maxRangeYPos = -3f;

    [Header("Limits for rotation")]
    [SerializeField] private float _positionPitchFactor = -2f;
    [SerializeField] private float _controlPitchFactor = -15f;
    [SerializeField] private float _positionYawFactor = 2f;
    [SerializeField] private float _controlRollFactor = -20f;

    private float horizontalThrow;
    private float verticalThrow;

    void Start()
    {
        
    }

   /* private void OnEnable()
    {
        _action.Enable();
    }

    private void OnDisable()
    {
        _action.Disable();
    }*/

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        horizontalThrow = Input.GetAxis("Horizontal"); //_Action.ReadValue<Vector2>().x;
        verticalThrow = Input.GetAxis("Vertical"); //_Action.ReadValue<Vector2>().y;

        float xOffset = horizontalThrow * _speedMovement * Time.deltaTime;
        float xNewPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(xNewPos, -_RangeXPos, _RangeXPos);

        float yOffset = verticalThrow * _speedMovement * Time.deltaTime;
        float yNewPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(yNewPos, _minRangeYPos, _maxRangeYPos);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * _positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * _controlPitchFactor;

        float pitch =  pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * _positionYawFactor;
        float roll = horizontalThrow * _controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}