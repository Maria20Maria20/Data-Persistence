using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance { get; private set; } //� ����������� ����� ������� ����� ���������� ����� ���������� Instance. ���� ����� �� ��� ������� ����, ������� ��� ������������� ����������, ������ ������ (get;) (����� ������).
                                                                    //����� ������ �������� ������, ���� ���������� ����� ���� ������������ ������ � �������, ������������� �� MonoBehaviour (� ���� �������� ������ ������, �� ����� ������)

    public int scoreHigh; //���������� ��� ������ �������
    public string nameScoreHigh; //���������� ��� ������ ����� ������
    public bool load = false; //��������, �������� �� ���������� ��������� (���-�� �����) ��� ���.

    [SerializeField] private Text highText; //��, ��� ����� ������������ ������ � ��� ������

    private void Awake()
    {
        LoadScore(); //�������� ������ � ��� ������ ����� ������� ����
        //Debug.Log(scoreHigh); //����� ���������� � �������, �������� �� ������
        //Debug.Log(nameScoreHigh); //����� ���������� � �������, �������� �� ��� ������
        if (Instance != null) //���� ���� ������ ��� �������������, ��:
        {
            Destroy(gameObject); //������� ��� (����, ����� ������� ������ PersistenceManager � �������� �� ����������� �� 100500 ��� ��� ��������� �������� �� ���� � �� �� �����)
            return; //������������
        }
        Instance = this; //� ������� PersistenceManager.Instance ����� ���������� � ����� ����� ����� �������
        DontDestroyOnLoad(gameObject); //�������, ������������ � ����� �������, �� ��������� � ����� ����� (������ � ��� ������)
    }

    [System.Serializable] //�����, ��������� ����, ����������� ������ (��������� �� ���� �� ��������� ����)
    class SaveData //� ���� ������ ���������� ��, ��� ����� ����������� � ����������� ����������� (�� ���� ����� ����� �� ����, ������ �� ����� ����� ���������)
    {
        public int scoreHigh; //������
        public string nameScoreHigh; //��� ������
    }

    public void SaveScore() //������� ��� ���������� ������� + ����� ������
    {
        SaveData data = new SaveData(); //�������� �������� ���������� �� SaveData
        data.scoreHigh = scoreHigh; //���������� �������� ������� = �������� �������, ������� ���� ���������
        data.nameScoreHigh = nameScoreHigh; //���������� �������� ����� ������ = �������� ����� ������, ������� ���� ���������
        string json = JsonUtility.ToJson(data); //����������� ���������� �������� (data) � ���� json (ToJson)
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //�������� ��������� ����� ��� ������ ����� � ����������� json
    }

    public void LoadScore() //������� ��� �������� ������� + ����� ������
    {
        load = true; //�� ���� � ������� LoadScore ���������� (��� ���� ��� ������� � ������� Game->void Start)
        string path = Application.persistentDataPath + "/savefile.json"; //������������� ������ json � ������. 1 ����: ����������� json � ���������� � ������ path
        if (File.Exists(path)) //���� ���� path ���������� (�� ���� ���� json, ������� ����� ���������������) ��:
        {
            string json = File.ReadAllText(path); //2 ����: ������ json ��� path
            SaveData data = JsonUtility.FromJson<SaveData>(json); //3 ����: ����������� json � ���������� data (�� ����� ����������, ��� ���������� �������� ������ ����������)
            scoreHigh = data.scoreHigh; //���������� �������, ����������� � ���� (��������, ������� ���� ���������) = ���������� ���������� ������� (���������� ��������)
            nameScoreHigh = data.nameScoreHigh; //���������� ����� ������, ����������� � ���� (��������, ������� ���� ���������) = ���������� ���������� ����� ������ (���������� ��������)
        }
    }
}
