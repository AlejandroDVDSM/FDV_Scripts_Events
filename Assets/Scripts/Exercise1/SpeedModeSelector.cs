using StarterAssets;
using UnityEngine;

public class SpeedModeSelector : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _thirdPersonController;
    
    private float _normalSpeed;
    private float _turboSpeed;

    private void Start()
    {
        _normalSpeed = _thirdPersonController.MoveSpeed;
        _turboSpeed = _thirdPersonController.MoveSpeed * 2;
    }

    public void SetSpeedModeToNormal()
    {
        _thirdPersonController.MoveSpeed = _normalSpeed;
        Debug.Log("NORMAL SPEED");
    }

    public void SetSpeedModeToTurbo()
    {
        _thirdPersonController.MoveSpeed = _turboSpeed;
        Debug.Log("TURBO SPEED");
    }
}
