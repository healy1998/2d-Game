using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    public bool isFiring;
    public Text ammoDisplay;
    // Start is called before the first frame update
    void Start()
    {
        ammoDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Weapon.isReloading == true)
            ammoDisplay.text = "Reloading";
        else
            ammoDisplay.text = "Ammo: " + Weapon.currentAmmo.ToString();
    }
}
