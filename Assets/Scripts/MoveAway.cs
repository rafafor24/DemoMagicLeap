using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MoveAway : MonoBehaviour
{


    #region Private Variables  
    [SerializeField, Tooltip("KeyPose to track.")]
    private MLHandKeyPose _keyPoseToTrack;

    [Space, SerializeField, Tooltip("Flag to specify if left hand should be tracked.")]
    private bool _trackLeftHand = true;

    [SerializeField, Tooltip("Flag to specify id right hand should be tracked.")]
    private bool _trackRightHand = true;
    

    private float speed = 50.0f;
    private Camera _camera;
    #endregion

    #region Unity Methods
    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {

        float confidenceLeft = _trackLeftHand ? GetKeyPoseConfidence(MLHands.Left) : 0.0f;
        float confidenceRight = _trackRightHand ? GetKeyPoseConfidence(MLHands.Right) : 0.0f;
        float confidenceValue = Mathf.Max(confidenceLeft, confidenceRight);





        if (confidenceValue > 0.5f)
        {
            //vec.x = Input.GetAxis("Horizontal") * 25 * Time.deltaTime;
            //vec.z = Input.GetAxis("Vertical") * 25 * Time.deltaTime;
            //vec = Camera.main.transform.TransformDirection(vec.x, 0, vec.z);
            //force = vec.normalized * speed;
            GetComponent<Rigidbody>().AddForce(_camera.gameObject.transform.forward * speed);
        }

    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Get the confidence value for the hand being tracked.
    /// </summary>
    /// <param name="hand">Hand to check the confidence value on. </param>
    /// <returns></returns>
    private float GetKeyPoseConfidence(MLHand hand)
    {
        if (hand != null)
        {
            if (hand.KeyPose == _keyPoseToTrack)
            {
                return hand.KeyPoseConfidence;
            }
        }
        return 0.0f;
    }
    #endregion
}
