using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    // Start is called before the first frame update

    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialoguesList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);
        if(csvData != null)
        {
            Debug.Log("불러옴");
        }
        else
        {
            Debug.Log("못 불러옴");
        }

        string[] data =csvData.text.Split(new char[] { '\n' });//? split('\n')
        for(int i=1;i<data.Length-1; )//ID	캐릭터 이름	대사	이런것 없다면 0 부터
        {
            string[] row = Regex.Split(data[i], SPLIT_RE);
            //Debug.Log(string.Format("{0}||{1}||{2}", row[0], row[1], row[2]));
            //Debug.Log(data.Length);
            //Debug.Log(row.Length);

            //if (row[0].ToString() == "" & row[1].ToString() == "" & row[2].ToString() == "")
            //{
            //    break;
            //}
            //Debug.Log(row[0]);
            
            //Debug.Log(row[2]);
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[1];
            Debug.Log(row[1]);

            List<string> contextList = new List<string>();
            do
            {
                //if (row[0].ToString() == "" & row[1].ToString() == "" & row[2].ToString() == "")
                //{
                //    break;
                //}
                contextList.Add(row[2]);
                Debug.Log(row[2]);
                if (++i < data.Length-1)
                {
            
                    row = Regex.Split(data[i], SPLIT_RE);//번호,이름,내용 이렇게 전부 있는 데이터를 위한 것이 아닌 그냥 내용만 있는 데이터를 나누느 ㄴ작업
                    //Debug.Log(string.Format("{0}||{1}||{2}", row[0], row[1], row[2]));
                }
                else
                {
                    break;//이게 없으면 못 멈춤
                }
            } while (row[0].ToString() == "");
            dialogue.context = contextList.ToArray();

            dialoguesList.Add(dialogue); //이렇게 하는게 string 배열을 넣기 위해
            //Debug.Log(row[1]);


            //do//자꾸 무한루프 빠짐
            //{
            //    contextList.Add(row[2]);
            //    Debug.Log(row[2]);
            //    //++i;
            //    //if (++i < data.Length)//안들어가면?
            //    //{
            //    //    row = Regex.Split(data[i], SPLIT_RE);
            //    //}
            //} while (row[0].ToString() == "");

            //Debug.Log(data[i]);//확인용
        }
        return dialoguesList.ToArray();
    }

    private void Start()
    {
        Parse("테스트파일");//
    }
}
