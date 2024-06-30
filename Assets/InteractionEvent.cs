using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class InteractionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DialogueEvent dialogue;
    //UIManager ui;

    int num = 0;
    int contentNum = 0;

    int indexNum = 0;

    string[] command;

    string SPLIT_COMMAND_PASER = @"[""!,]";//명령어 분리 정규식

    public Dialogue[] GetDialogue()
    {

        //dialogue.line.x = DatabaseManager.instance.indexList[indexNum++];
        if (indexNum >= DatabaseManager.instance.indexList.Count)
        {
            indexNum = DatabaseManager.instance.indexList.Count - 1;
        }
        Debug.Log("indexNum"+indexNum);
        dialogue.line.y = DatabaseManager.instance.indexList[indexNum];//마지막 라인을 받아오기는 하지만 필요한건 마지막라인이 아닌 인덱스? 딕셔너리에 들어가는 그 y가 필요함

        Debug.Log(string.Format("시작지점{0} 끝 지점{1}",dialogue.line.x,dialogue.line.y));
        dialogue.dialouses = DatabaseManager.instance.GetDialogues((int)dialogue.line.x, (int)dialogue.line.y);//y값 찾아오는 법
        
        Debug.Log("데이터 가져옴");
        return dialogue.dialouses;
    }

    private void Update()
    {
        //GetDialogue();
        //화살표로 선택지 왔다 갔다 하면서 f로 선택
        if ((Input.GetKeyDown(KeyCode.F) & (num < dialogue.dialouses.Length)))//line을 조절 해야함 대화가 끝나는 시점을 정하려면 line.y를 설정해야함
        {

            //Debug.Log(string.Format("{0}", dialogue.dialouses[num].context[0]));
            //ui.Setname(dialogue.dialouses[num].name);
            gameObject.GetComponentInParent<UIManager>().Setname(dialogue.dialouses[num].name);//이름 변경 되는중 마찬가지로 내용도 같이 하면 될듯
            Debug.Log(dialogue.dialouses[num].context.Length);
            //Debug.Log(string.Format("길이{0}", dialogue.dialouses[num].command.Length));
            //foreach(var coms in dialogue.dialouses[num].command)
            //{
            //    foreach (var com in coms)
            //    {
            //        Debug.Log(string.Format("커맨드 체크 =>{0}", com));
            //    }
            //}

            //foreach (var coms in dialogue.dialouses[num].command)
            //{
            //    CallFunction(coms);
            //}

            if (dialogue.dialouses[num].context.Length-1 > contentNum)
            {
                gameObject.GetComponentInParent<UIManager>().SetContent(string.Join("", dialogue.dialouses[num].context[contentNum]));
                Debug.Log(string.Format("메모장 테스트용{0}", dialogue.dialouses[num].test[contentNum]));
                Debug.Log(string.Format("명령어 테스트용{0}", dialogue.dialouses[num].command[contentNum]));
                command = Regex.Split(dialogue.dialouses[num].command[contentNum], SPLIT_COMMAND_PASER, RegexOptions.IgnorePatternWhitespace);
                command = spaceremove(command);
                CallFunction(command);
                foreach(var _com in command)
                {
                    Debug.Log(string.Format("{1} 명령어 {0}", _com,num));
                }
                contentNum++;
                return;
            }
            gameObject.GetComponentInParent<UIManager>().SetContent(string.Join("", dialogue.dialouses[num].context[contentNum]));
            Debug.Log(string.Format("메모장 테스트용{0}", dialogue.dialouses[num].test[contentNum]));
            Debug.Log(string.Format("명령어 테스트용{0}", dialogue.dialouses[num].command[contentNum]));
            command = Regex.Split(dialogue.dialouses[num].command[contentNum], SPLIT_COMMAND_PASER, RegexOptions.IgnorePatternWhitespace);
            command = spaceremove(command);
            CallFunction(command);
            foreach (var _com in command)
            {
                Debug.Log(string.Format("{1} 명령어 {0}", _com, num));
            }
            contentNum = 0;
            num++;
        }
        //if (dialogue.dialouses[num].context.Length > -1 & Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //
        //}
        if (num == dialogue.dialouses.Length)
        {
            Debug.Log("대화끝");

            if (Input.GetKeyDown(KeyCode.X))
            {
                dialogue.line.x = ++dialogue.line.y;
                indexNum++;
                GetDialogue();
                Debug.Log(dialogue.dialouses.Length);
                num = 0;
            }
        }
    }

    private void CallFunction(string[] _functions)
    {
        string SPLIT_NUM = @"([a-z]+|\ )+";//공백 분리 정규식//새로운식([a-z]+|\ )+
        string GET_COMMAND = @"[a-z]{1,}";
        foreach (var func in _functions)
        {

            string[] strarr = Regex.Split(func, SPLIT_NUM);
            string[] filteredSubstrings = strarr.Where(s => s != Regex.Match(s,SPLIT_NUM).ToString()).ToArray();

            if(filteredSubstrings.Length > 0)
            {
                Debug.Log("명령어 인자 있음");
            }
            else
            {
                Debug.Log("명령어 인자 없음");
            }
            //foreach (var str in filteredSubstrings)
            //{
            //    Debug.Log(string.Format("테스트str {0}", str));
            //}
            int n;
            string[] numarr= Array.FindAll(strarr,s=>!string.IsNullOrEmpty(s)&&(int.TryParse(s,out n)));
            //Debug.Log(string.Format("숫자 길이{0}", numarr.Length));
            //foreach(var str in numarr)
            //{
            //    Debug.Log(string.Format("명령어 인자{0}", str));
            //}
            //if (strarr[0].ToString() =="") { Debug.Log("숫자 없는 명령어"); }
            //else
            //{
            //    Debug.Log("숫자 있는 명령어");
            //}
            Debug.Log(string.Format("mat => {0}", func));
            var mat = Regex.Matches(func,GET_COMMAND);
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
            
            }
        }
    }
    private string[] spaceremove(string[] com)
    {
        List<string> temp = new List<string>();
        int index = 0;
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

    private void Start()
    {
        
        GetDialogue();
        foreach(var i in DatabaseManager.instance.indexList)
        {
            Debug.Log(string.Format("list {0}", i));
        }
        
    }
}
