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
            Debug.Log("�ҷ���");
        }
        else
        {
            Debug.Log("�� �ҷ���");
        }

        string[] data =csvData.text.Split(new char[] { '\n' });//? split('\n')
        for(int i=1;i<data.Length-1; )//ID	ĳ���� �̸�	���	�̷��� ���ٸ� 0 ����
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
            
                    row = Regex.Split(data[i], SPLIT_RE);//��ȣ,�̸�,���� �̷��� ���� �ִ� �����͸� ���� ���� �ƴ� �׳� ���븸 �ִ� �����͸� ������ ���۾�
                    //Debug.Log(string.Format("{0}||{1}||{2}", row[0], row[1], row[2]));
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
        return dialoguesList.ToArray();
    }

    private void Start()
    {
        Parse("�׽�Ʈ����");//
    }
}
