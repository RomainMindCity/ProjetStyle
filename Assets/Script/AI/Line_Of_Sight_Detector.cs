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
    private float _detectionHeight = 3f;

    [SerialiseField]
    private bool _showDebugVisuals = true;

    public GameObject Detection(GameObject potentialTarget)
    {
        potentialTarget = potentialTarget.transform.root.gameObject;
        Debug.Log("Potential Target: " + potentialTarget.name);
        Vector3 origin = transform.position + Vector3.up * _detectionHeight;
        Vector3 direction = (potentialTarget.transform.position + Vector3.up * _detectionHeight) - origin;
        float distance = direction.magnitude;
        direction.Normalize();

        if (Physics.Raycast(origin, direction, out RaycastHit hit, Mathf.Min(distance, _detectionRange), _playerLayerMask))
        {
            Debug.Log("yeah");
            if (hit.collider != null)
            {
            Debug.Log("Hit: " + hit.collider.gameObject.tag);
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            Debug.Log("Hit: " + hit.collider.gameObject.layer);

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Hit Player: " + hit.collider.gameObject.name);
                if (_showDebugVisuals && enabled)
                {
                Debug.DrawLine(origin, hit.collider.transform.position, Colour.red);
                }
                return hit.collider.gameObject;
            }
            }
        }
        else
        {
            return null;
        }
        return null;
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
