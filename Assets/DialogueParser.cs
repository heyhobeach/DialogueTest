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
        
        //Debug.Log(string.Format("{0} ���� ���� �ֽ��ϴ�",data.Length));
        //end=data.Length;
        for(int i=1;i<data.Length-1; )//ID	ĳ���� �̸�	���	�̷��� ���ٸ� 0 ����
        {
            row = Regex.Split(data[i], SPLIT_RE);
            //Debug.Log(string.Format("i�� {0} {1} {2}", i, row[1], row[3]));
            //if (row[1].ToString() != chap.ToString())
            //{
            //    Debug.Log("����é��");
            //    break;
            //}
            //if (row[1].ToString()==" ")
            //{
            //    Debug.Log("�ѱ�");
            //    continue;
            //}
            //Debug.Log(row[1].ToString());
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[2];
            //Debug.Log(data[i]);

            List<string> contextList = new List<string>();
            do//���� id�� �����̸� ���⼭ ó���ؾ���
            {
                //if (row[4].ToString() != "")//�׳� ������ �κ�
                //{
                //    //Debug.Log("������ ����");
                //}
                //if (row[1].ToString()==" ")
                //{
                //    EndDialogue(i);//while�� �ƴ϶� for���� �ߴ� ��� ã�ƾ���
                //    //i++;//������ ���� �ݺ���
                //    //i=data.Length-1;
                //    break;//break�ϸ� while�� ���������µ� i�� ���� �� �Ȼ��·� for ��� ���Ƽ� ���� �ݺ� i���� ���� �ߴ� �ؾ��ҵ�
                //    //continue;//continue�� �ٸ� ��絵 �Ľ���
                //}
                contextList.Add(row[3]);

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
            //Debug.Log(row[1]);


            //do//�ڲ� ���ѷ��� ����
            //{
            //    contextList.Add(row[2]);
            //    Debug.Log(row[2]);
            //    //++i;
            //    //if (++i < data.Length)//�ȵ���?
            //    //{
            //    //    row = Regex.Split(data[i], SPLIT_RE);
            //    //}
            //} while (row[0].ToString() == "");

            //Debug.Log(data[i]);//Ȯ�ο�
        }
        DatabaseManager.instance.endLine = int.Parse(row[0]);//������ id �������� ����
        return dialoguesList.ToArray();
    }
    private int EndDialogue(int endIndex) //��ȭ ���� ������ int ������ ������ ��ȭ �κ��� �Ѱ��� ����
    {
        Debug.Log("��ȭ ����");
        return endIndex;
    }
    

    private void Start()
    {
        //Parse("�׽�Ʈ����");//
    }
}
