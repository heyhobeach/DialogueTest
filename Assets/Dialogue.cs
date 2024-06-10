using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue:MonoBehaviour
{
    [Tooltip("ĳ���� �̸�")]
    public string name;

    [Tooltip("��� ����")]
    public string[] context;
}
[System.Serializable]
public class DialogueEvent
{
    public string name;
    public Vector2 line;//��� ������ ����?
    public Dialogue[] dialouses;//�������� �� �ϴ°��̱� �빮��
    
}