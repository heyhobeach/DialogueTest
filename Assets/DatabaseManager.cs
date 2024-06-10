using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DatabaseManager instance;//나중에 싱글턴 될듯

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

        for(int i = 0; i <= endNum - startNum; i++)//1과 3 사이 내용을 가져 오려고 그러는것
        {
            dialoguesList.Add(dialogueDic[startNum + i]);
        }

        return dialoguesList.ToArray();
    }
}
