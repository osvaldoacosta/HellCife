using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunInventory : MonoBehaviour
{
    [SerializeField] public List<Gun> guns;
    [SerializeField] private ushort currentWeaponIndex;
    public static Action<ushort,Gun> onWeaponChange; 
    private ushort carryingSize;
    private Dictionary<Gun, Action> onGunChange;

    void Start()
    {
        currentWeaponIndex = 0;
        carryingSize = 2;
        EquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("currentWeapon: "+ guns[currentWeaponIndex]);
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
            EquipWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex= 1;
            EquipWeapon();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (carryingSize >1)
            {
                currentWeaponIndex = 2;
                Debug.Log("changed index to:"+currentWeaponIndex);
                EquipWeapon();

            }
        }
    }

    public ushort GetCurrentWeaponIndex()
    {
        return this.currentWeaponIndex;
    }

    public void IncreaseCarryingSize()
    {
        carryingSize = 3; //Como so tem 3 armas equipaveis, n eh necessario botar um valor modular, ou incrementar, evitando futuros bug's na mecanica. 
    }
    public Gun GetCurrentGun()
    {
        if(guns.Count > currentWeaponIndex) return guns[currentWeaponIndex];
        return null;
    }
    public void AddNewGun(Gun gun)
    {
        guns.Add(gun);
    }

    public void EquipWeapon()
    {
        //Desativo todos
        foreach (Gun gun in guns)
        {
            if(gun == null){
              continue;
            }
            gun.gameObject.SetActive(false);
        }
        //Posso colocar um ~yield da anima��o de troca de arma aqui
        Gun gunToEquip = guns[currentWeaponIndex];
        Debug.Log("Changed to: " + gunToEquip?.name);

        //Troca pra arma escolhida
        gunToEquip?.gameObject?.SetActive(true);

        //Muda o jeito em que ele segura a arma
        onGunChange[gunToEquip](); 

        //Se n�o tiver uma arma secund�ria, ele fica pelado >_<
        onWeaponChange?.Invoke(currentWeaponIndex,GetCurrentGun()); 
    }


}
