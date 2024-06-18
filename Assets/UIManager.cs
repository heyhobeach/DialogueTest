using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public Text
    public TMP_Text namemesh;
    public TMP_Text content;
    //public InteractionEvent interactionEvent;

    // Start is called before the first frame update
    void Start()
    {
        //namemesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        //namemesh.text = "Á¶Çö¼·";
    }

    public void Setname(string name)
    {
        namemesh.text = name;
    }

    public void SetContent(string _content)
    {
        content.text = _content;
    }

    public void test()
    {
        Debug.Log("call");
    }
}
