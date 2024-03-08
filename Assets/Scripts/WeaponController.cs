using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    AttackController[] weapons;

    AttackController _currentWeapon;

    int _selectedWeapon;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        HandleScrollWheel();
        HandleMouseClick();
        HandleAttack();
    }

    private void HandleScrollWheel()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0F)
        {
            _selectedWeapon = scrollWheel > 0.0F
                ? _selectedWeapon + 1
                : _selectedWeapon - 1;
            if (_selectedWeapon >= weapons.Length)
            {
                _selectedWeapon = 0;
            }
            else if (_selectedWeapon < 0)
            {
                _selectedWeapon = weapons.Length - 1;
            }

            SelectWeapon();
        }
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonUp(0)) // 0 corresponds to the left mouse button
        {
            // Toggle between sword and spear on left mouse button click
            _selectedWeapon = (_selectedWeapon == 0) ? 1 : 0;
            SelectWeapon();
        }
    }

    private void HandleAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            _currentWeapon.Attack();
        }
    }

    private void SelectWeapon()
    {
        for (int index = 0; index < weapons.Length; index++)
        {
            bool isActive = (_selectedWeapon == index);
            AttackController controller = weapons[index];
            controller.gameObject.SetActive(isActive);
            if (isActive)
            {
                _currentWeapon = controller;
            }
        }
    }
}
