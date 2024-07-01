using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DatabaseManager instance;//나중에 싱글턴 될듯

    public string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic =new Dictionary<int, Dialogue>();
    DialogueParser theParser;

    public int endLine = 0, startLine = 0;//start = 0 end 대화 끝 시점 => start = end , end = 대화 끝 반복(짜야함) 딕셔너리 접근용 수
    public int lastIndex = 0;

    public List<int> indexList;

    public static bool isFinish = false;

    private void Awake()
    {
        //indexList.Add(0);
        if (instance == null)
        {
            instance = this;
            theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues =theParser.Parse(csv_FileName);//여기서 지금 대화 모든 내용을 다 파싱 한 상태
            //startLine=
            for(int i =0;i<dialogues.Length;i++)
            {
                dialogueDic.Add(i +1, dialogues[i]);
            }
            isFinish= true;
        }
    }

    public Dialogue[] GetDialogues(int startNum ,int endNum)
    {
        //Debug.Log("파서");
        List<Dialogue> dialoguesList = new List<Dialogue>();

        for(int i = 0; i <= endNum - startNum; i++)//1과 3 사이 내용을 가져 오려고 그러는것
        {
            
            if (startNum + i < 1)
            {
                //Debug.Log("넘김");
                //Debug.Log(string.Format("넘김 {0}||{1}||{2}", startNum, endNum, i));
                continue;
            }
            else
            {
                //Debug.Log("else문");
            }//Debug.Log("리스트 삽입");
            dialoguesList.Add(dialogueDic[startNum + i]);
        }

        return dialoguesList.ToArray();
    }
}
