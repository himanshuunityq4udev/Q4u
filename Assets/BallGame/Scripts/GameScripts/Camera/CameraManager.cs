using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup _targetGroup;

    [SerializeField] Transform _target;

    private void Start()
    {
        SetTargetGroup();
    }


    public void SetTargetGroup()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _targetGroup.AddMember(_target, 1, 1);
    }
}