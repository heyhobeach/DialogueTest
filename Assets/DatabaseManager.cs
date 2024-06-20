using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DatabaseManager instance;//���߿� �̱��� �ɵ�

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic =new Dictionary<int, Dialogue>();
    DialogueParser theParser;

    public int endLine = 0, startLine = 0;//start = 0 end ��ȭ �� ���� => start = end , end = ��ȭ �� �ݺ�(¥����) ��ųʸ� ���ٿ� ��
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
            Dialogue[] dialogues =theParser.Parse(csv_FileName);//���⼭ ���� ��ȭ ��� ������ �� �Ľ� �� ����
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
        //Debug.Log("�ļ�");
        List<Dialogue> dialoguesList = new List<Dialogue>();

        for(int i = 0; i <= endNum - startNum; i++)//1�� 3 ���� ������ ���� ������ �׷��°�
        {
            
            if (startNum + i < 1)
            {
                //Debug.Log("�ѱ�");
                //Debug.Log(string.Format("�ѱ� {0}||{1}||{2}", startNum, endNum, i));
                continue;
            }
            else
            {
                //Debug.Log("else��");
            }//Debug.Log("����Ʈ ����");
            dialoguesList.Add(dialogueDic[startNum + i]);
        }

        return dialoguesList.ToArray();
    }
}
