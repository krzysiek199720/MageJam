using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public List<Image> ammoImages;

    public void setAmmoUI(int count)
    {
        count = Mathf.Clamp(count, 1, 5);

        for(int i = 0; i < 5; ++i)
        {
            if (i + 1 <= count)
                ammoImages[i].gameObject.SetActive(true);
            else
                ammoImages[i].gameObject.SetActive(false);

        }
    }
}
