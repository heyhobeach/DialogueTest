using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    // Start is called before the first frame update

    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";//���Խ� from chat gpt
    string[] row;
    //public int start = 0, end = 0;
    public Dialogue[] Parse(string _CSVFileName)
    {
        int chap = 1;
        List<Dialogue> dialoguesList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);//csv���� �޾ƿ�����

        if(csvData != null)
        {
            //Debug.Log("�ҷ���");
        }
        else
        {
            //Debug.Log("�� �ҷ���");
        }

        string[] data =csvData.text.Split(new char[] { '\n' });//? split('\n')
        
        for(int i=1+DatabaseManager.instance.lastIndex;i<data.Length-1; )//ID	ĳ���� �̸�	���	�̷��� ���ٸ� 0 ����
        {
            row = Regex.Split(data[i], SPLIT_RE);
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[2];
            dialogue.id = row[0];
            bool isEnd = false;
            //Debug.Log(data[i]);

            List<string> contextList = new List<string>();
            do//���� id�� �����̸� ���⼭ ó���ؾ���
            {                
                contextList.Add(row[3]);//content
                if (row[3].ToString() == "")//��ȭ ������ ����
                {
                    isEnd = true;
                    Debug.Log(string.Format("content =>null"));//���� �Ʒ� �κп� �迭�� ��������
                    contextList.RemoveAt(contextList.Count-1);//������ ���� ����(������ ������ �̰� ������ �����ϰ� ������ �����ϴ� �� �ʿ��� �۾��� �� ���� ������ �ƿ� �� �ִ������� �۾� �ؾ��ҵ�)

                }
                else
                {
                    Debug.Log(string.Format("content =>{0}", row[3]));
                }

                //dialogue.choice = row[4];
                if (++i < data.Length-1)
                {

                    //Debug.Log(string.Format("i�� {0} {1} {2}", i, row[1], row[3]));
                    row = Regex.Split(data[i], SPLIT_RE);//��ȣ,�̸�,���� �̷��� ���� �ִ� �����͸� ���� ���� �ƴ� �׳� ���븸 �ִ� �����͸� ������ ���۾�
                       
                }
                else
                {
                    break;//�̰� ������ �� ����
                }

            } while (row[0].ToString() == "");
            dialogue.context = contextList.ToArray();

            dialoguesList.Add(dialogue); //�̷��� �ϴ°� string �迭�� �ֱ� ����
            if (isEnd)
            {
                Debug.Log("����");
                Debug.Log(dialoguesList[dialoguesList.Count - 1].id);//������� ��������,�̰� ������ ���;���
                DatabaseManager.instance.endLine = int.Parse(dialoguesList[dialoguesList.Count - 1].id);//���� parser�� ������ ������ �𸣰���//end���α��� ����//���� start�� end�����ؾ���
                DatabaseManager.instance.indexList.Add(int.Parse(dialoguesList[dialoguesList.Count - 1].id));
                //break;
            }

            //Debug.Log(row[1]);

        }
        //DatabaseManager.instance.endLine = int.Parse(row[0]);//������ id �������� ����
        return dialoguesList.ToArray();
    }
    private int EndDialogue(int endIndex) //��ȭ ���� ������ int ������ ������ ��ȭ �κ��� �Ѱ��� ����
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
        //Parse("�׽�Ʈ����");//
    }
}
