using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //����, ����� ������������ ������� Exit ��������
using UnityEngine.SceneManagement; //��������� ���������� SceneManagement, ���� ����� ���� ��������� �������� � ����/������� �������
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour
{
    public static string inputName; //���������� ��� ���������� ����� ������ � ������ ������

    [SerializeField] private Text name; //����� � ���������� ������� ��������� ����, ��� ����� ������ ��� ���
    [SerializeField] private InputField inputField; //����� � ���������� ������� ���� InputField ��� ����� ����� ������
    [SerializeField] Text highScore; //����� � ���������� ������� ���� ��� ������ �������

    void Start() //��� ������ ����:
    {
        HighScore(); //������� ���������� ������� �� ������ �������� ���-�� �����
        if (PersistenceManager.Instance != null) //���� ����� ����� �� � ������ ���, ��:
        {
            PersistenceManager.Instance.LoadScore(); //��������� ������ � �������� ��� ������ (���� ��� ����)
        }
        name.text = inputName; //��������� ����, ���������� ��� ������ = ���������� ��� ���������� ����� ������ (��� ���������� � ��� ���������� ��� ������������� � ������ ������)
    }

    private void HighScore() //������� ������ � ����
    {
        highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //� ���� ��� ������ ������� ������������ ����� "High Score: " + �������� ������� + ��� ������
    }

    public void LoadText() //������� ��� �������� �����, ������� ��� �����
    {
        inputName = inputField.text; //�������� ��� = ��� �� ���������� Text, ������� ��������� � InputField (�������� ������ Text � ������� InputField)
    }

    public void StartNew() //������� ��� ������ "�����"
    {
        SceneManager.LoadScene(1); //��������� ������ �����. 1 - ������ �����, ������� � ���� ���������. ������ ����� ������ � Build Settings->Scenes In Build->������ �� ����������� ����� �����
    }

    public void Exit() //������� ��� ������ "�����"
    {
        PersistenceManager.Instance.SaveScore(); //��� ������ �� ���� ��������� ������ � ��� ������
#if UNITY_EDITOR //���� ���� �������������� � ��������� �����, ��:
        EditorApplication.ExitPlaymode(); //������� �� ������ ���������������
#else //� �������� ������ (���� ���� ������� � ����������, � �� � �����):
        Application.Quit(); //������� �� ����������
#endif
    }
}
