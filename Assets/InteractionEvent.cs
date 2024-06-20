using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DialogueEvent dialogue;
    //UIManager ui;

    int num = 0;
    int contentNum = 0;

    int indexNum = 0;

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
        if (Input.GetKeyDown(KeyCode.F)&(num<dialogue.dialouses.Length))//line을 조절 해야함 대화가 끝나는 시점을 정하려면 line.y를 설정해야함
        {

            //Debug.Log(string.Format("{0}", dialogue.dialouses[num].context[0]));
            //ui.Setname(dialogue.dialouses[num].name);
            gameObject.GetComponentInParent<UIManager>().Setname(dialogue.dialouses[num].name);//이름 변경 되는중 마찬가지로 내용도 같이 하면 될듯
            Debug.Log(dialogue.dialouses[num].context.Length);
            
            if (dialogue.dialouses[num].context.Length-1 > contentNum)
            {
                gameObject.GetComponentInParent<UIManager>().SetContent(string.Join("", dialogue.dialouses[num].context[contentNum]));
                contentNum++;
                return;
            }
            gameObject.GetComponentInParent<UIManager>().SetContent(string.Join("", dialogue.dialouses[num].context[contentNum]));
            contentNum = 0;
            num++;
        }
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

    private void Start()
    {
        
        GetDialogue();
        foreach(var i in DatabaseManager.instance.indexList)
        {
            Debug.Log(string.Format("list {0}", i));
        }
        
    }
}
