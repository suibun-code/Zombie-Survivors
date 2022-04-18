using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    [SerializeField] private GameObject gridHolder;

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private Transform spawnBulletTransform;

    [SerializeField] public bool isReloading;
    [SerializeField] public bool isFiring;

    private Vector3 mouseWorldPosition;
    private ThirdPersonController thirdPersonController;
    public StarterAssetsInputs _input;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (Pause.gamePaused)
            return;

        SetCenterPoint();

        if (_input.buildMode)
        {
            gridHolder.SetActive(true);
        }
        else
        {
            gridHolder.SetActive(false);

            if (_input.aim)
            {
                aimVirtualCamera.gameObject.SetActive(true);
                thirdPersonController.SetSensitivity(aimSensitivity);
                thirdPersonController.SetRotateOnMove(false);

                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            }
            else
            {
                aimVirtualCamera.gameObject.SetActive(false);
                thirdPersonController.SetSensitivity(normalSensitivity);
                thirdPersonController.SetRotateOnMove(true);
            }

            if (_input.fire)
            {
                //ShootBullet();
                _input.fire = false;
            }
        }
    }

    public void ShootBullet()
    {
        Vector3 aimDir = (mouseWorldPosition - spawnBulletTransform.position).normalized;
        Instantiate(bulletTransform, spawnBulletTransform.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }

    private void SetCenterPoint()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCam.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, aimColliderLayerMask))
        {
            debugTransform.position = hitInfo.point;
            mouseWorldPosition = hitInfo.point;
        }
    }
}
