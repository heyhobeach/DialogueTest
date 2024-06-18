using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DialogueEvent dialogue;
    UIManager ui;

    int num = 0;
    int contentNum = 0;

    public Dialogue[] GetDialogue()
    {
        dialogue.line.y = DatabaseManager.instance.endLine;//������ ������ �޾ƿ���� ������ �ʿ��Ѱ� ������������ �ƴ� �ε���? ��ųʸ��� ���� �� y�� �ʿ���
        Debug.Log(dialogue.line.y);
        dialogue.dialouses = DatabaseManager.instance.GetDialogues((int)dialogue.line.x, (int)dialogue.line.y);//y�� ã�ƿ��� ��
        return dialogue.dialouses;
    }

    private void Update()
    {
        //GetDialogue();
        if (Input.GetKeyDown(KeyCode.F)&(num<dialogue.line.y))

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
    }

    private void Start()
    {
        
        GetDialogue();
        Debug.Log(string.Format("��ȭ ���� {0}",dialogue.dialouses.Length));
    }
}
