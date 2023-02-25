using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SectionData", menuName = "DATA/SectionData")]
public class SectionData : ScriptableObject
{
    public int id;

    public string section_name;

    public Sprite btn_thumb;
}
