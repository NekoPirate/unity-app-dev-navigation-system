using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataSystem : MonoBehaviour
{
    public static Color LIGHT_PRIMARY_COLOR { get; private set; }
    public static Color LIGHT_SECONDARY_COLOR { get; private set; }
    public static Color DARK_PRIMARY_COLOR { get; private set; }
    public static Color DARK_SECONDARY_COLOR { get; private set; }
    public static Color ACTIVE_COLOR { get; private set; }

    [SerializeField] Color ActiveColor;

    //:::::::::::::::::::::::::::::::::::::::
    public void Awake()
    {
        // Setup Highlight Color
        ACTIVE_COLOR = ActiveColor;

        // Setup Light Theme Colors
        LIGHT_PRIMARY_COLOR = new Color(1f, 1f, 1f, 1f);
        LIGHT_SECONDARY_COLOR = new Color(.9f, .9f, .9f, 1f);

        // Setup Dark Theme Colors
        DARK_PRIMARY_COLOR = new Color(.2f, .2f, .2f, 1f);
        DARK_SECONDARY_COLOR = new Color(.1f, .1f, .1f, 1f);
    }
}
