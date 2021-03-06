using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    public GameObject weaponToSpawn;

    public ThirdPersonShooterController playerController;
    public Animator playerAnimator;
    Weapon equippedWeapon;

    [SerializeField] GameObject weaponSocketLocation;
    private Transform gripIKSocketLocation;

    bool firingPressed = false;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    public AudioSource shootAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<ThirdPersonShooterController>();
        playerAnimator = GetComponent<Animator>();

        GameObject spawnedWeapon = Instantiate(
            weaponToSpawn,
            weaponSocketLocation.transform.position,
            weaponSocketLocation.transform.rotation,
            weaponSocketLocation.transform);

        equippedWeapon = spawnedWeapon.GetComponent<Weapon>();
        equippedWeapon.Initialize(this);
        PlayerEvents.invokeOnWeaponEquipped(equippedWeapon);
        gripIKSocketLocation = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!playerController.isReloading)
        {
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
        }
    }

    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInMag <= 0)
        {
            StartReloading();
            return;
        }

        playerAnimator.SetTrigger(isFiringHash);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    void StopFiring()
    {
        playerAnimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        StartReloading();
    }

    public void StartReloading()
    {
        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0)
        {
            return;
        }

        equippedWeapon.StartReloading();
        playerController.isReloading = true;
        playerAnimator.SetBool(isReloadingHash, true);

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (playerAnimator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        playerAnimator.SetBool(isReloadingHash, false);
        equippedWeapon.StopReloading();
        CancelInvoke(nameof(StopReloading));

        if (firingPressed)
        {
            StartFiring();
        }
    }
}
