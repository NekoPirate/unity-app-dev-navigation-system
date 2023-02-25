using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Section : MonoBehaviour
{
    //header-text
    [SerializeField] TextMeshProUGUI text_header;
    [SerializeField] Image sample_image;
    
    public void SetupSection(string section_name, Sprite section_sample_content)
    {
        text_header.text = section_name;
        sample_image.sprite = section_sample_content;

        text_header.color = DataSystem.LIGHT_SECONDARY_COLOR;
        sample_image.color = DataSystem.LIGHT_SECONDARY_COLOR;
    }
}
