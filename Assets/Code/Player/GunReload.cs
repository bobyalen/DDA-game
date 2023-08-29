using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReload : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Guns guns;
    void ReloadGun()
    {
        guns.resetReload();
    }

    void resetShot()
    {
        guns.ResetShot();
    }
    void Reload()
    {
        if (guns.ammoCount() <= 0)
        {
            guns.Reload();
        }
    }
}
