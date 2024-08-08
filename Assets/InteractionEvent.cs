using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Unity.Loading;

public class InteractionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DialogueEvent dialogue;
    //UIManager ui;

    int num = 0;
    int contentNum = 0;

    int indexNum = 0;
    int contentlength = 0;

    string[] command = new string[1];
    bool start = false;

    /// <summary>
    /// 명령어 끼리 , 를 분리하는 정규식
    /// </summary>
    string SPLIT_COMMAND_PASER = @"[""!,]";//명령어 분리 정규식

    public Dialogue[] GetDialogue()
    {

        if (indexNum >= DatabaseManager.instance.indexList.Count)
        {
            indexNum = DatabaseManager.instance.indexList.Count - 1;
        }
        dialogue.line.y = DatabaseManager.instance.indexList[indexNum];//마지막 라인을 받아오기는 하지만 필요한건 마지막라인이 아닌 인덱스? 딕셔너리에 들어가는 그 y가 필요함
        dialogue.dialouses = DatabaseManager.instance.GetDialogues((int)dialogue.line.x, (int)dialogue.line.y);//y값 찾아오는 법
        Debug.Log("길이" + dialogue.dialouses.Length);
        return dialogue.dialouses;
    }
    private void Start()
    {

        GetDialogue();
        command[0] = "";
        foreach (var i in DatabaseManager.instance.indexList)
        {
            Debug.Log(string.Format("list {0}", i));
        }
    }

    private void Update()
    {
        if ((num <= dialogue.dialouses.Length))//line을 조절 해야함 대화가 끝나는 시점을 정하려면 line.y를 설정해야함
        {
            HandleDialogue();
        }
        if (num > dialogue.dialouses.Length)
        {
            EndDialogue();
        }
    }

    private void HandleCommand()
    {
        if (command.Length > 0)
        {
            Debug.Log("command size is " + command.Length);
            command = spaceremove(command);
            CallFunction(command);
        }
    }

    private void HandleDialogue()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleCommand();
            
            if (num < dialogue.dialouses.Length)
            {
                gameObject.GetComponentInParent<UIManager>().Setname(dialogue.dialouses[num].name);//이름 변경 되는중 마찬가지로 내용도 같이 하면 될듯
                                                                                                   //Debug.Log(dialogue.dialouses[num].context.Length);
                Debug.Log(string.Format("num => {0} contentnum ={1}", num, contentNum));
                contentlength = dialogue.dialouses[num].context.Length;
                Debug.Log(contentlength);
                if (contentlength == 1)
                {
                    gameObject.GetComponentInParent<UIManager>().SetContent(string.Join("", dialogue.dialouses[num].context[contentNum]));

                    Debug.Log(string.Format("num => {0} 대화 길이 =>{1}", num, dialogue.dialouses.Length));

                }
                else
                {
                    Debug.Log("선택지 부분");
                    string[] textSum = new string[contentlength];
                    //gameObject.GetComponentInParent<UIManager>().SetContent(string.Join("", ""));
                    for (int index = 0; index < contentlength; index++)//한번만 호출 되어야함
                    {
                        textSum[index] = dialogue.dialouses[num].context[index];


                    }
                    gameObject.GetComponentInParent<UIManager>().SetContent(textSum);
                    if (start == false)
                    {
                        start = true;
                        StartCoroutine(ChocieTimer(10, start, Timeover));
                    }
                }


                command = Regex.Split(dialogue.dialouses[num].command[contentNum], SPLIT_COMMAND_PASER, RegexOptions.IgnorePatternWhitespace);
            }
            contentNum = 0;
            num++;
        }
        if (contentlength > 1)//선택지 부분
        {
            Debug.Log("선택지 부분");
            string textSum = "";
            if (start == false)
            {
                start = true;
                StartCoroutine(ChocieTimer(5, start, Timeover));
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //countnum은 downArrow가 실행 되면 값이 변하게 되어있음
                if (contentlength - 1 > (contentNum))
                {
                    gameObject.GetComponentInParent<UIManager>().DownArrow(ref contentNum);
                    command = Regex.Split(dialogue.dialouses[num - 1].command[contentNum], SPLIT_COMMAND_PASER, RegexOptions.IgnorePatternWhitespace);
                    return;
                }

            }

            if (Input.GetKeyDown(KeyCode.UpArrow) & (contentNum > 0))
            {
                Debug.Log(string.Format("선택지 확인 {0}, {1}", num - 1, contentNum));
                Debug.Log("명령어" + command);
                gameObject.GetComponentInParent<UIManager>().UpArrow(ref contentNum);
                command = Regex.Split(dialogue.dialouses[num - 1].command[contentNum], SPLIT_COMMAND_PASER, RegexOptions.IgnorePatternWhitespace);
            }

        }
        else
        {
            start = false;
        }
    }

    private void EndDialogue()
    {
        Debug.Log("대화끝");

        if (Input.GetKeyDown(KeyCode.X)&(indexNum< DatabaseManager.instance.indexList.Count))
        {
            dialogue.line.x = ++dialogue.line.y;
            indexNum++;
            GetDialogue();
            Debug.Log(dialogue.dialouses.Length);
            num = 0;
        }
    }

    private void CallFunction(string[] _functions)
    {
        string SPLIT_NUM = @"([a-z]+|\ )+";//공백 분리 정규식//새로운식([a-z]+|\ )+
        string GET_COMMAND = @"[a-z]{1,}";
        foreach (var func in _functions)
        {

            string[] strarr = Regex.Split(func, SPLIT_NUM);
            string[] filteredSubstrings = strarr.Where(s => s != Regex.Match(s, SPLIT_NUM).ToString()).ToArray();
            int n;
            string[] numarr = Array.FindAll(strarr, s => !string.IsNullOrEmpty(s) && (int.TryParse(s, out n)));
            Debug.Log(string.Format("mat => {0}", func));
            var mat = Regex.Matches(func, GET_COMMAND);
            Debug.Log(string.Format("커맨드 체크 =>{0}", mat));
            switch (mat[0].ToString())
            {
                case "size":
                    { size(filteredSubstrings); }
                    break;
                case "speed":
                    { speed(filteredSubstrings); }
                    break;
                case "time":
                    { time(); }
                    break;
                case "brutal":
                    { brutal(); }
                    break;
                case "police":
                    { police(); }
                    break;
                case "play":
                    { play(); }
                    break;
                case "anime":
                    { anime(filteredSubstrings); }
                    break;
                case "move":
                    { move(filteredSubstrings); }
                    break;

            }
            command = new string[1] { "" };
        }
    }
    private string[] spaceremove(string[] com)//공백 제거 함수
    {
        List<string> temp = new List<string>();
        //int index = 0;
        foreach (var j in com)
        {
            if (j.ToString() != "")
            {
                temp.Add(j);
            }
        }

        return temp.ToArray();

    }


    public void size(string[] command_args)//시작 끝 수치
    {
        Debug.Log(string.Format("switch_size {0} {1} {2}", command_args[0], command_args[1], command_args[2]));
        //Debug.Log("switch_size");
    }
    public void speed(string[] command_args)//시작 끝 수치
    {
        Debug.Log(string.Format("switch_speed {0} {1} {2}", command_args[0], command_args[1], command_args[2]));
    }
    public void time()
    {
        Debug.Log("switch_time");
    }
    public void brutal()
    {
        Debug.Log("switch_brutal");
    }
    public void police()
    {
        Debug.Log("switch_plice");
    }
    public void play()
    {
        Debug.Log("switch_play");
    }
    public void anime(string[] command_args)//시작 끝 종류
    {
        Debug.Log(string.Format("switch_anime {0} {1} {2}", command_args[0], command_args[1], command_args[2]));
    }
    public void move(string[] command_args)
    {
        Debug.Log(string.Format("switch_move {0}", command_args[0]));
        num = int.Parse(command_args[0]);//그냥 변환이 안 되는중
        //Debug.Log("자료형"+command_args)

        num--;
        Debug.Log(num + "변환 테스트");
        contentNum = 0;
    }



    public void Timeover()
    {
        Debug.Log("time over");
        if (contentlength > 1 && num <= dialogue.dialouses.Length)
        {
            Debug.Log(string.Format("{0} num {1} contentNum", num - 1, contentNum));
            command = Regex.Split(dialogue.dialouses[num - 1].command[0], SPLIT_COMMAND_PASER, RegexOptions.IgnorePatternWhitespace);
            command = spaceremove(command);
            CallFunction(command);
            num++;
            contentNum = 0;
        }
    }
    IEnumerator ChocieTimer(float seconds, bool start, Action act)
    {
        if (start)
        {
            Debug.Log("코루틴 초" + seconds);
            yield return new WaitForSeconds(seconds);
            Debug.Log("time 코루틴 시작");
        }

        act();
    }
}