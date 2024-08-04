using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 텍스트 번호 담기 위함
    /// </summary>
    int[] arr;
    /// <summary>
    /// 선택지 문자열
    /// </summary>
    string[] test;
    /// <summary>
    /// 선택지 색상
    /// </summary>
    /// 

    int select_count;
    string test_str;
    public string color = "black";

    /// <summary>
    /// 현재 나오고 있는 선택지 번호
    /// </summary>
    int index = 0;
    //public Text
    public TMP_Text namemesh;
    public TMP_Text content;

    public bool is_select_show = false;

    IEnumerator co = null;

    [SerializeField]
    public float typing_speed = 0.05f;
    //public InteractionEvent interactionEvent;

    // Start is called before the first frame update
    void Start()
    {
        co = Typing("");
        //namemesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        //namemesh.text = "조현섭";
    }

    public void Setname(string name)
    {
        namemesh.text = name;
    }

    public void SetContent(string _content)
    {
        StopCoroutine(co);
        co = Typing(_content);
        StartCoroutine(co);
        //content.text = _content;
    }
    public void SetContent(string[] _contentArr,int contentLength)//배열로 받을 예정
    {
        StopCoroutine(co);
        //co = Typing(_content);
        co = TextSliding(_contentArr, contentLength);
        StartCoroutine(co);

        //content.text = _contentArr;
    }

    public void ChoiceColorChange(int countNum)
    {
        string color_str = test[countNum];
        color_str = string.Format("");
        //content.text.Replace(test[countNum], color_str);
    }
    public void ChangeText(int countNum)
    {
        TMP_Text TMP;
        for (int i =0;i< select_count; i++)
        {
            TMP = content.transform.parent.GetChild(i).GetComponent<TMP_Text>();
            if (i== countNum)
            {
                TMP.color = Color.black;
            }
            else
            {
                TMP.color = Color.gray;
            }

        }  
        //TMP_Text TMP = content.transform.parent.GetChild(countNum).GetComponent<TMP_Text>();
        //TMP.color = Color.black;
    }

    public void UpArrow(ref int countNum)
    {
        if (is_select_show) return;
        Debug.Log("UpArrow countNum" + countNum);
        countNum--;
        content.color = Color.gray;
        //content.text = test_str;
        Debug.Log(content.transform.parent.GetChild(countNum).GetComponent<TMP_Text>().text);
        ChangeText(countNum);
        //TMP_Text TMP = content.transform.parent.GetChild(countNum).GetComponent<TMP_Text>();
        //TMP.color = Color.black;
        string color_str;
        color_str = string.Format("{0}{1}{2}", "<color=", color, ">");
        string change_str = color_str + test[countNum] + "</color>";
        string teststr = "";
        //for (int i = 0; i < test.Length; i++)
        //{
        //    if (i == countNum)
        //    {
        //        teststr += change_str + "<br>";
        //        continue;
        //    }
        //    teststr += test[i] + "<br>";
        //
        //}
        //content.text = teststr;
        //content.text=content.text.Replace(test[countNum], change_str);
        Debug.Log(change_str);
        
    }
    public void DownArrow(ref int countNum) 
    {
        if (is_select_show) return;
        Debug.Log("UpArrow countNum" + countNum);
        countNum++;
        content.color = Color.gray;
        //content.text = test_str;

        Debug.Log(content.transform.parent.GetChild(countNum).GetComponent<TMP_Text>());
        ChangeText(countNum);
        //TMP_Text TMP = content.transform.parent.GetChild(countNum).GetComponent<TMP_Text>();
        //TMP.color = Color.black;
        string color_str ;
        color_str = string.Format("{0}{1}{2}", "<color=", color, ">");
        string change_str = color_str + test[countNum] + "</color>";
        string teststr = "";
        //for (int i = 0; i < test.Length; i++)
        //{
        //    if (i == countNum)
        //    {
        //        teststr += change_str+"<br>";
        //        continue;
        //    }
        //    teststr += test[i]+"<br>";
        //
        //}
        //content.text = teststr;
        //content.text=content.text.Replace(test[countNum], change_str);
        Debug.Log(change_str);
        
    }

    IEnumerator Typing(string str)
    {
        content.text = null;
        if (content.color != Color.black)
        {
            content.color = Color.black;
        }
        if (str == "")
        {
            yield return null;
        }
        for (int i = 0; i < str.Length; i++)
        {
            content.text+= str[i];
            yield return new WaitForSeconds(typing_speed);
        }
    }

    IEnumerator TextSliding(string[] strArr,int contentLengh)//배열로 받을 예정
    {
        select_count = strArr.Length;
        //strArr[0] = string.Format("{0}{1}{2}{3}{4}", "<color=", color, ">", strArr[0], "</color>");
        Debug.Log("strArr"+strArr[0]);
        content.text = strArr[0];
        content.color = Color.black;
        for (int i = 1; i < contentLengh; i++)//오브젝트 생성과 텍스트 배치
        {
            Debug.Log(content.transform.parent.name);
            TMP_Text select = Instantiate(content,this.transform.position,Quaternion.identity);
            select.transform.parent = content.transform.parent;
            select.text = strArr[i];
            select.color = Color.gray;
        }
        is_select_show = true;
        //content.text = null;
        //content.color = Color.gray;
        index = -1;
        //test_str = string.Join("", strArr);
        test = (string[])strArr.Clone();
        
        
        foreach (var str in strArr)//문자열을 여기서 수정해야할까? 싶은 생각
        {
            is_select_show = true;
            if (str == "")
            {
                yield return null;
            }
            //content.text += str;
            //content.text += "<br>";
            //index++;
            

            yield return new WaitForSeconds(1);
            
            Debug.Log("index ="+index);
        }

        Debug.Log("test 길이 " + test.Length);
        arr = new int[test.Length];//전역으로 빠져야함, 해당 위치에 >삽입
        index = 0;
        foreach (var str in test)
        {
            //indexOf
            //test_str += str;
            //arr[index]=content.text.IndexOf(str, StringComparison.OrdinalIgnoreCase);
            //Debug.Log(string.Format("정규식 테스트{0}, {1}", str, arr[index]));
            //Debug.Log("test안에" + str);
            //index++;
        }
        Debug.Log(test_str);
        //
        is_select_show = false;


    }

    public void Test()
    {
        Debug.Log("call");
    }
}
