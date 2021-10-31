using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //����, ����� ������������ ������� Exit ��������
using UnityEngine.SceneManagement; //��������� ���������� SceneManagement, ���� ����� ���� ��������� �������� � ����/������� �������

public class Menu : MonoBehaviour
{
    public void StartNew() //������� ��� ������ "�����"
    {
        SceneManager.LoadScene(1); //��������� ������ �����. 1 - ������ �����, ������� � ���� ���������. ������ ����� ������ � Build Settings->Scenes In Build->������ �� ����������� ����� �����
    }

    public void Exit() //������� ��� ������ "�����"
    {
#if UNITY_EDITOR //���� ���� �������������� � ��������� �����, ��:
        EditorApplication.ExitPlaymode(); //������� �� ������ ���������������
#else //� �������� ������ (���� ���� ������� � ����������, � �� � �����):
        Application.Quit(); //������� �� ����������
#endif
    }
}
