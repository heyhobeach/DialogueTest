using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    [SerializeField] DialogueEvent dialogue;


    public List<string>text = new List<string>();   
    private int num = 0;

    void Update()
    {
        //if(click)


        //Debug.Log(this.gameObject.name);
    }

    private void Start()
    {
        //PushText();
    }

    public void PushText()//���� �ʿ��� ������ getDialoues�ϸ� �迭�� �ޱ� ������ �迭 �ʿ��Ѱ��� �� �޾ƿü� �ְ� �����ε� �ϴ����� ������? 
    {
        dialogue.dialouses = DatabaseManager.instance.GetDialogues(1, 4);//é�Ϳ��� ���۳� ��������
        for (int i = 0; i < dialogue.dialouses.Length; i++)
        {
            //Debug.Log(string.Format("{0}�̸� �̸�{1}",this.gameObject.name,dialogue.dialouses[i].name));
            //Debug.Log(string.Format("{0}{1} is {2}", this.gameObject.name, dialogue.dialouses[1].name, string.Equals(dialogue.dialouses[1].name, this.gameObject.name)));
            //Debug.Log(string.Format("{0}{1}", this.gameObject.name.Length, dialogue.dialouses[1].name.Length));

            if (string.Equals(dialogue.dialouses[i].name, this.gameObject.name))
            {
                //Debug.Log("ã��");
                //Debug.Log(dialogue.dialouses[i].context[0]);
                //Debug.Log(dialogue.dialouses[i].context[1]);
                //dialogue.dialouses = DatabaseManager.instance.GetDialogues(i+1, i+1);
                //dialogue.ad
                for (int j = 0; j < dialogue.dialouses[i].context.Length; j++)
                {
                    //Debug.Log(j);
                    //if (dialogue.dialouses[i])
                    text.Add(dialogue.dialouses[i].context[j]);
                    //Debug.Log(dialogue.dialouses[i].context[j]);
                }
            }
        }
    }
}
