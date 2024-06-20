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
        dialogue.line.y = DatabaseManager.instance.indexList[indexNum];//������ ������ �޾ƿ���� ������ �ʿ��Ѱ� ������������ �ƴ� �ε���? ��ųʸ��� ���� �� y�� �ʿ���

        Debug.Log(string.Format("��������{0} �� ����{1}",dialogue.line.x,dialogue.line.y));
        dialogue.dialouses = DatabaseManager.instance.GetDialogues((int)dialogue.line.x, (int)dialogue.line.y);//y�� ã�ƿ��� ��
        
        Debug.Log("������ ������");
        return dialogue.dialouses;
    }

    private void Update()
    {
        //GetDialogue();
        if (Input.GetKeyDown(KeyCode.F)&(num<dialogue.dialouses.Length))//line�� ���� �ؾ��� ��ȭ�� ������ ������ ���Ϸ��� line.y�� �����ؾ���
        {

            //Debug.Log(string.Format("{0}", dialogue.dialouses[num].context[0]));
            //ui.Setname(dialogue.dialouses[num].name);
            gameObject.GetComponentInParent<UIManager>().Setname(dialogue.dialouses[num].name);//�̸� ���� �Ǵ��� ���������� ���뵵 ���� �ϸ� �ɵ�
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
            Debug.Log("��ȭ��");

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
