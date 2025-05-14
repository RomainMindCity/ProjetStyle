using UnityEngine;
using Colour = UnityEngine.Color;
using SerialiseField = UnityEngine.SerializeField;



public class Line_Of_Sight_Detector : MonoBehaviour
{

    [SerialiseField]
    private LayerMask _playerLayerMask;

    [SerialiseField]
    private float _detectionRange = 5.0f;

    [SerialiseField]
    private float _detectionHeight = 3f;

    [SerialiseField]
    private bool _showDebugVisuals = true;

    public GameObject Detection(GameObject potentialTarget)
    {
        RaycastHit hit;
        Vector3 direction = potentialTarget.transform.position - transform.position;
        Physics.Raycast(transform.position + Vector3.up * _detectionHeight, direction, out hit, _detectionRange, _playerLayerMask);

        if (hit.collider != null && hit.collider.gameObject == potentialTarget)
        {
            if (_showDebugVisuals && this.enabled)
            {
                Debug.DrawLine(transform.position + Vector3.up * _detectionHeight, potentialTarget.transform.position, Colour.green);
            }
            return hit.collider.gameObject;
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
