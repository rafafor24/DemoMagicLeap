using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ClosedPinch : MonoBehaviour
{


    #region Private Variables  
    [SerializeField, Tooltip("KeyPose to track.")]
    private MLHandKeyPose _keyPoseToTrack;

    [Space, SerializeField, Tooltip("Flag to specify if left hand should be tracked.")]
    private bool _trackLeftHand = true;

    [SerializeField, Tooltip("Flag to specify id right hand should be tracked.")]
    private bool _trackRightHand = true;

    #endregion

    #region Unity Methods
    void Update()
    {

        float confidenceLeft = _trackLeftHand ? GetKeyPoseConfidence(MLHands.Left) : 0.0f;
        float confidenceRight = _trackRightHand ? GetKeyPoseConfidence(MLHands.Right) : 0.0f;
        float confidenceValue = Mathf.Max(confidenceLeft, confidenceRight);


        if (confidenceValue > 0.5f)
        {
            transform.localScale -= new Vector3(0.01F, 0.01F, 0.01F);
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
