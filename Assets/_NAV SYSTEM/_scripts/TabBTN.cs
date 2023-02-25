using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabBTN : MonoBehaviour
{
    [SerializeField] Image sample_image;

    public void SetupButton(Sprite btn_image)
    {
        sample_image.sprite = btn_image;
        sample_image.color = DataSystem.LIGHT_SECONDARY_COLOR;
    }

    public void PaintTheButton(bool b)
    {
        sample_image.color = b ? DataSystem.ACTIVE_COLOR : DataSystem.LIGHT_SECONDARY_COLOR;
    }
}
