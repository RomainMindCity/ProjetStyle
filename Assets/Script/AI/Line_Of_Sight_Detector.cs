using UnityEngine;
using Colour = UnityEngine.Color;
using SerialiseField = UnityEngine.SerializeField;

public class Line_Of_Sight_Detector : MonoBehaviour
{

    [SerialiseField]
    private LayerMask _playerLayerMask;

    [SerialiseField]
    private float _detectionRange = 10.0f;

    [SerialiseField]
    private float _detectionHeight = 0f;

    [SerialiseField]
    private bool _showDebugVisuals = true;

    private Transform _hitBoxTransform;

    public GameObject Detection(GameObject potentialTarget)
    {
        RaycastHit hit;

        // // Get the target object
        // GameObject potentialTarget = GameObject.FindGameObjectsWithTag("Player")[0].transform.parent.gameObject;
        // Debug.Log("potentialTarget: " + potentialTarget);
        // _hitBoxTransform = potentialTarget.transform.Find("Physics/HitBox");
        // potentialTarget = _hitBoxTransform.gameObject;
        // Debug.Log("potentialTarget: " + potentialTarget);

        // Check if the target is within range
        Vector3 direction = potentialTarget.transform.position - transform.position;

        Debug.DrawRay(transform.position,direction, Color.green, 1f);
        Physics.Raycast(transform.position , direction, out hit, _detectionRange, _playerLayerMask);

        // Check if the raycast hit the target
        // if (hit.collider != null && hit.collider.gameObject == potentialTarget)
        // {
        //     if (_showDebugVisuals && this.enabled)
        //     {
        //         Debug.DrawLine(transform.position + Vector3.up * _detectionHeight, potentialTarget.transform.position, Colour.green);
        //     }
        //     return potentialTarget;
        // }
        // else
        // {
        //     return null;
        // }
        if (hit.collider != null && hit.collider.gameObject == potentialTarget)
        {
            return potentialTarget;
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (_showDebugVisuals)
        {
            Gizmos.color = Colour.red;
            Gizmos.DrawSphere(transform.position + Vector3.up * _detectionHeight, 0.3f);
        }
    }
}
