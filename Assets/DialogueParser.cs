using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    // Start is called before the first frame update

    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";//정규식 from chat gpt
    string[] row;
    //public int start = 0, end = 0;
    public Dialogue[] Parse(string _CSVFileName)
    {
        int chap = 1;
        List<Dialogue> dialoguesList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);//csv파일 받아오는중

        if(csvData != null)
        {
            //Debug.Log("불러옴");
        }
        else
        {
            //Debug.Log("못 불러옴");
        }

        string[] data =csvData.text.Split(new char[] { '\n' });//? split('\n')
        
        for(int i=1+DatabaseManager.instance.lastIndex;i<data.Length-1; )//ID	캐릭터 이름	대사	이런것 없다면 0 부터
        {
            row = Regex.Split(data[i], SPLIT_RE);
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[2];
            dialogue.id = row[0];
            bool isEnd = false;
            //Debug.Log(data[i]);

            List<string> contextList = new List<string>();
            do//만약 id가 공백이면 여기서 처리해야함
            {                
                contextList.Add(row[3]);//content
                if (row[3].ToString() == "")//대화 끝나는 시점
                {
                    isEnd = true;
                    Debug.Log(string.Format("content =>null"));//여기 아래 부분에 배열을 지워야함
                    contextList.RemoveAt(contextList.Count-1);//마지막 라인 삭제(개선점 지금은 이게 공백을 삽입하고 공백을 삭제하는 불 필요한 작업을 함 따라서 공백을 아에 안 넣는쪽으로 작업 해야할듯)

                }
                else
                {
                    Debug.Log(string.Format("content =>{0}", row[3]));
                }

                //dialogue.choice = row[4];
                if (++i < data.Length-1)
                {

                    //Debug.Log(string.Format("i는 {0} {1} {2}", i, row[1], row[3]));
                    row = Regex.Split(data[i], SPLIT_RE);//번호,이름,내용 이렇게 전부 있는 데이터를 위한 것이 아닌 그냥 내용만 있는 데이터를 나누느 ㄴ작업
                       
                }
                else
                {
                    break;//이게 없으면 못 멈춤
                }

            } while (row[0].ToString() == "");
            dialogue.context = contextList.ToArray();

            dialoguesList.Add(dialogue); //이렇게 하는게 string 배열을 넣기 위해
            if (isEnd)
            {
                Debug.Log("종료");
                Debug.Log(dialoguesList[dialoguesList.Count - 1].id);//종료시점 직전까지,이게 여러개 나와야함
                DatabaseManager.instance.endLine = int.Parse(dialoguesList[dialoguesList.Count - 1].id);//지금 parser가 어디까지 나올지 모르겠음//end라인까지 끊김//이후 start와 end수정해야함
                DatabaseManager.instance.indexList.Add(int.Parse(dialoguesList[dialoguesList.Count - 1].id));
                //break;
            }

            //Debug.Log(row[1]);

        }
        //DatabaseManager.instance.endLine = int.Parse(row[0]);//마지막 id 가져오기 위함
        return dialoguesList.ToArray();
    }
    private int EndDialogue(int endIndex) //대화 종료 시점을 int 형으로 마지막 대화 부분을 넘겨줄 것임
    {
        if (row[0].ToString() == "")
        {
            //dialoub
            Debug.Log("null");
        }
        return endIndex;
    }
    

    private void Start()
    {
        //Parse("테스트파일");//
    }
}
