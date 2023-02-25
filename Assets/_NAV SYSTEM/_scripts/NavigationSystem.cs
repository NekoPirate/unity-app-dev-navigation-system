using System.Collections;
using UnityEngine;

public class NavigationSystem : MonoBehaviour
{
    [Header("SECTION_DATA")]
    [SerializeField] private SectionData[] _sectionsData;

    [Header("Container")]
    [SerializeField] private GameObject _sectionsContainer;
    [SerializeField] private GameObject _buttonsContainer;
    
    [Header("Components Prefab")]
    [SerializeField] private GameObject _prefab_section; 
    [SerializeField] private GameObject _prefab_tabButton;

    [Header("Animation Curve")]
    [SerializeField] private AnimationCurve slideCurve;

    private GameObject[] _tabButtons;
    private GameObject[] _sections;

    private int currentSectionIndex;
    private int previousSectionIndex;

    private float slideDuration;
    private bool isSliding;

    private void Awake()
    {
        _sections = new GameObject[_sectionsData.Length];
        _tabButtons = new GameObject[_sectionsData.Length];

        currentSectionIndex = 0;
        previousSectionIndex = 0;

        slideDuration = .4f;
        isSliding = false;
    }

    private void Start() { InstantiateAll(); }
    
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::

    private void InstantiateAll()
    {
        SectionData data;

        for (int i = 0; i < _sectionsData.Length; i++)
        {
            data = _sectionsData[i];
            data.id = i;
            //----------------------

            InstanziateSection(out GameObject newSection, data.section_name, data.btn_thumb);

            InstantiateTabButton(out GameObject newTabBTN, data.btn_thumb, data.id);

            //------------------------

            _sections[i] = newSection;
            _tabButtons[i] = newTabBTN;
            
            //------------------------
            _tabButtons[i].SetActive(true);
        }
        // Nascondi tutte le sezioni e mostra solo quella selezionata
        for (int i = 0; i < _sections.Length; i++)
        {
            _sections[i].SetActive(i == currentSectionIndex ? true : false);
        }
        // Colora 
        for (int i = 0; i < _tabButtons.Length; i++)
        {
            _tabButtons[i].GetComponent<TabBTN>().PaintTheButton(i == currentSectionIndex ? true : false);
        }
    }


    // Instantiate and Setup Section from DATA
    private void InstanziateSection(out GameObject newSection, string section_name, Sprite btn_thumb)
    {
        newSection = Instantiate(_prefab_section, _sectionsContainer.transform);

        newSection.GetComponent<Section>().SetupSection(section_name, btn_thumb);
    }
    
    // Instantiate and Setup TabBTN from DATA
    private void InstantiateTabButton(out GameObject newTabBTN, Sprite btn_thumb,  int id)
    {
        newTabBTN = Instantiate(_prefab_tabButton, _buttonsContainer.transform);

        newTabBTN.GetComponent<TabBTN>().SetupButton(btn_thumb);

        newTabBTN.GetComponent<BTN>().onButtonClick.AddListener(() => ShowSection(id));
    }

    // TabBTN handler
    private void ShowSection(int sectionIndex)
    {
        if (isSliding) return;

        if (sectionIndex == currentSectionIndex) return;

        //----------------------------------------------

        previousSectionIndex = currentSectionIndex;
        currentSectionIndex = sectionIndex;

        //----------------------------------------------

        // Nascondi tutte le sezioni e mostra solo quella selezionata
        for (int i = 0; i < _sections.Length; i++)
        {
            _sections[i].SetActive((i == sectionIndex || i == previousSectionIndex) ? true : false);
        }
        // Colora 
        for (int i = 0; i < _tabButtons.Length; i++)
        {
            _tabButtons[i].GetComponent<TabBTN>().PaintTheButton(i == sectionIndex ? true : false);
        }

        StartCoroutine(SlideTransition(_sections[previousSectionIndex], _sections[currentSectionIndex]));
    }

    private IEnumerator SlideTransition(GameObject outgoingSection, GameObject incomingSection)
    {
        isSliding = true;
        //----------------

        float startTime = Time.time;

        Vector3 incomingStartPosition;
        Vector3 incomingEndPosition;
        Vector3 outgoingStartPosition;
        Vector3 outgoingEndPosition;

        if(currentSectionIndex > previousSectionIndex)
        {
            incomingStartPosition = Vector3.right * Screen.width;  
            incomingEndPosition = Vector3.zero;

            outgoingStartPosition = Vector3.zero;
            outgoingEndPosition = Vector3.left * Screen.width;
        }
        else
        {
            incomingStartPosition = Vector3.left * Screen.width;
            incomingEndPosition = Vector3.zero;

            outgoingStartPosition = Vector3.zero;
            outgoingEndPosition = Vector3.right * Screen.width;
        }

        outgoingSection.transform.SetAsLastSibling();
        incomingSection.SetActive(true); // ???

        while(Time.time - startTime < slideDuration)
        {
            float t = slideCurve.Evaluate((Time.time - startTime) / slideDuration);
            incomingSection.transform.localPosition = Vector3.Lerp(incomingStartPosition, incomingEndPosition, t);
            outgoingSection.transform.localPosition = Vector3.Lerp(outgoingStartPosition, outgoingEndPosition, t);

            yield return null;
        }
        incomingSection.transform.localPosition = Vector3.zero;
        incomingSection.transform.SetAsLastSibling();

        outgoingSection.SetActive(false);

        //----------------
        isSliding = false;
    }
}
