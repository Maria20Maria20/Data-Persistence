using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Game : MonoBehaviour
{
    public Text highScore; //���������� ��� ������ �������

    [SerializeField] private Text scoreText; //����� �� ����� ������ �� ���-�� ��������� �����
    [SerializeField] private Text timerText; //����� �� ����� ������ �� ������� 
    [SerializeField] private GameObject restart; //��� � ���������� ������� ������ ��������
    [SerializeField] private GameObject backToMenu; //��� � ���������� ������� ������ �������� � ����

    private int score; //����� �������� ���-�� ��������� �����
    private float timer = 10; //���-�� ������ � �������
    private Button button; //���������� ��� ���������� Button, ���� ����� ���� ��������� ������, ����� ���� �����������

    void Start() //������������ ��� ������ ����
    {
        PersistenceManager.Instance.LoadScore(); //��������� ��� ������ ������ (���� �� ����������� ���� �� ����� ����)
        if (PersistenceManager.Instance.load) //���� �����-�� ������ (������ ��� ��� ������) ��� ���������, ��:
        {
            highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //��������� ����� "High Score: " + ������ + ��� ������
        }
        button = GameObject.Find("Clicker").GetComponent<Button>(); //��������� ��������� Button � �������� ������� � ������ Clicker, ���� �� ����� ���� ��������������
        score = 0; //��� ������ ���������� ������� ���-�� �����
    }

    void Update() //������������ ������ ����� (��� ������)
    {
        if (timer > 0) //���� ������ ������ ����, ��:
        {
            int intTime = (int)timer; //���������� float � int, ����� ������ ��� �� ������ �����,�� ���� 9, � �� 9.48585
            timer -= Time.deltaTime; //������� ���������� �������� Time.deltaTime �� �����, ���������� � ���������� timer
            timerText.text = "Timer: " + intTime; //��������� ����� "Timer: " + �������� ������ (������� ������ �������� ������)
        }
        else //����� ������ ����� ��� ������ ����, ��:
        {
            TimerEnd(); //�������� ������� � ����� ������
        }
    }

    public virtual void Score() //���������� ������� ����� ����� ������
    {
        score+=1; //���������� ���-�� ����� �� 1
        scoreText.text = "Score: " + score; //��������� ����� "Score: " + ���-�� ��������� �����
    }

    private void TimerEnd() //���������, ����� ���������� ������
    {
        button.enabled = false; //�������� ��������� Button � �������� ������� Clicker, ����� ������ ���� �������� ����, ������ ��� ���� �����������
        timerText.text = "Game Over"; //��������� ���� �����
        HighScore(); //������������ ���������� ������� � ����� ���������, ���� ���������, ������ ��������� ����� ������ ��� ���
        restart.SetActive(true); //���������� ��, ��� ������� � ���������� restart
        backToMenu.SetActive(true); //���������� ��, ��� ������� � ���������� backToMenu
    }

    public void HighScore() //������� ��� ���������� �������
    {
        if (PersistenceManager.Instance.scoreHigh < score) //���� ������� ���-�� ����� ������, ��� ������, ��:
        {
            PersistenceManager.Instance.nameScoreHigh = Menu.inputName; //��� ���������� ������ = �����, ��������� � ����
            PersistenceManager.Instance.scoreHigh = score; //������������ ������� ���-�� ����� � ������� (��������� �������� �������)
            highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //������� ����� � ���������� �������� � ������ ������
            PersistenceManager.Instance.SaveScore(); //��������� ����� ������ � ��� ���������� ������
        }
        else //���� ������� ���-�� ����� ������, ��� ������, ��:
        {
            highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //������� ����� � ���������� �������� � ������ ������
        }

    }

    public void Restart() //������� ��� ��������, ����� ���� ����������
    {
        SceneManager.LoadScene(1); //������ ����������� ����� � �������� 1
    }

    public void BackToMenu() //������� ��� �������� �� ���� � ����
    {
        SceneManager.LoadScene(0); //��������� � ����� � �������� 0
    }

}
