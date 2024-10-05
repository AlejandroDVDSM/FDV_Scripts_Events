using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _movementSpeed = 2.0f;
    [SerializeField] private float _accuracy = .01f;

    private Vector3 _distance;
    private bool _enableMovement;
    
    private void Update()
    {
        if (!_enableMovement)
            return;
        
        _distance = _target.position - transform.position;
        _distance.y = 0;
        
        if (!(_distance.magnitude > _accuracy))
            return;
        
        transform.Translate(_distance.normalized * (_movementSpeed * Time.deltaTime), Space.World);
    }

    public void GoToTarget()
    {
        _enableMovement = true;
    }
}
