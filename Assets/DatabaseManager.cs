using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DatabaseManager instance;//���߿� �̱��� �ɵ�

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic =new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues =theParser.Parse(csv_FileName);
            for(int i =0;i<dialogues.Length;i++)
            {
                dialogueDic.Add(i +1, dialogues[i]);
            }
            isFinish= true;
        }
    }

    public Dialogue[] GetDialogues(int startNum ,int endNum)
    {
        List<Dialogue> dialoguesList = new List<Dialogue>();

        for(int i = 0; i <= endNum - startNum; i++)//1�� 3 ���� ������ ���� ������ �׷��°�
        {
            dialoguesList.Add(dialogueDic[startNum + i]);
        }

        return dialoguesList.ToArray();
    }
}
