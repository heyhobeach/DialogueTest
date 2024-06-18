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
        
        //Debug.Log(string.Format("{0} 라인 까지 있습니다",data.Length));
        //end=data.Length;
        for(int i=1;i<data.Length-1; )//ID	캐릭터 이름	대사	이런것 없다면 0 부터
        {
            row = Regex.Split(data[i], SPLIT_RE);
            //Debug.Log(string.Format("i는 {0} {1} {2}", i, row[1], row[3]));
            //if (row[1].ToString() != chap.ToString())
            //{
            //    Debug.Log("다음챕터");
            //    break;
            //}
            //if (row[1].ToString()==" ")
            //{
            //    Debug.Log("넘김");
            //    continue;
            //}
            //Debug.Log(row[1].ToString());
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[2];
            //Debug.Log(data[i]);

            List<string> contextList = new List<string>();
            do//만약 id가 공백이면 여기서 처리해야함
            {
                //if (row[4].ToString() != "")//그냥 선택지 부분
                //{
                //    //Debug.Log("선택지 있음");
                //}
                //if (row[1].ToString()==" ")
                //{
                //    EndDialogue(i);//while뿐 아니라 for까지 중단 방법 찾아야함
                //    //i++;//없으면 무한 반복중
                //    //i=data.Length-1;
                //    break;//break하면 while을 빠져나가는데 i가 증가 안 된상태로 for 계속 돌아서 무한 반복 i증가 이후 중단 해야할듯
                //    //continue;//continue는 다른 대사도 파싱중
                //}
                contextList.Add(row[3]);

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
        DatabaseManager.instance.endLine = int.Parse(row[0]);//마지막 id 가져오기 위함
        return dialoguesList.ToArray();
    }
    private int EndDialogue(int endIndex) //대화 종료 시점을 int 형으로 마지막 대화 부분을 넘겨줄 것임
    {
        Debug.Log("대화 종료");
        return endIndex;
    }
    

    private void Start()
    {
        //Parse("테스트파일");//
    }
}
