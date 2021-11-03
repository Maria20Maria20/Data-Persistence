using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Game : MonoBehaviour
{
    public Text highScore; //переменная для вывода рекорда

    [SerializeField] private Text scoreText; //вывод на экран текста по кол-ву набранных очков
    [SerializeField] private Text timerText; //вывод на экран текста по таймеру 
    [SerializeField] private GameObject restart; //тут в инспекторе указана кнопка рестарта
    [SerializeField] private GameObject backToMenu; //тут в инспекторе указана кнопка возврата в меню

    private int score; //вывод текущего кол-ва набранных очков
    private float timer = 10; //кол-во секунд в таймере
    private Button button; //переменная для компонента Button, чтоб можно было выключать кликер, когда игра закончилась

    void Start() //показывается при старте игры
    {
        PersistenceManager.Instance.LoadScore(); //загружаем при старте рекорд (чтоб он показывался прям во время игры)
        if (PersistenceManager.Instance.load) //если какие-то данные (рекорд или имя игрока) уже загружены, то:
        {
            highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //выводится текст "High Score: " + рекорд + имя игрока
        }
        button = GameObject.Find("Clicker").GetComponent<Button>(); //подключаю компонент Button у игрового объекта с именем Clicker, чтоб им можно было манипулировать
        score = 0; //при старте обнуляется текущее кол-во очков
    }

    void Update() //показывается каждый фрейм (тут таймер)
    {
        if (timer > 0) //если таймер больше нуля, то:
        {
            int intTime = (int)timer; //преобразую float в int, чтобы таймер был из целого числа,то есть 9, а не 9.48585
            timer -= Time.deltaTime; //отнимаю постоянную величину Time.deltaTime от числа, указанного в переменной timer
            timerText.text = "Timer: " + intTime; //отображаю слово "Timer: " + обратный отсчёт (сколько секунд осталось играть)
        }
        else //еслии таймер равен или меньше нуля, то:
        {
            TimerEnd(); //вызываем функцию с таким именем
        }
    }

    public virtual void Score() //показывает сколько очков игрок набрал
    {
        score+=1; //увеличиваю кол-во очков на 1
        scoreText.text = "Score: " + score; //выводится текст "Score: " + кол-во набранных очков
    }

    private void TimerEnd() //выводится, когда закончился таймер
    {
        button.enabled = false; //отключаю компонент Button у игрового объекта Clicker, чтобы нельзя было набирать очки, потому что игра закончилась
        timerText.text = "Game Over"; //выводится этот текст
        HighScore(); //используется содержимое функции с таким названием, чтоб проверить, сейчас поставлен новый рекорд или нет
        restart.SetActive(true); //активируем то, что указано в переменной restart
        backToMenu.SetActive(true); //активируем то, что указано в переменной backToMenu
    }

    public void HighScore() //функция для сохранения рекорда
    {
        if (PersistenceManager.Instance.scoreHigh < score) //если текущее кол-во очков больше, чем рекорд, то:
        {
            PersistenceManager.Instance.nameScoreHigh = Menu.inputName; //имя набравшего рекорд = имени, введённому в меню
            PersistenceManager.Instance.scoreHigh = score; //приравниваем текущее кол-во очков к рекорду (обновляем значение рекорда)
            highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //выводим текст с обновлённым рекордом и именем игрока
            PersistenceManager.Instance.SaveScore(); //сохраняем новый рекорд и имя набравшего рекорд
        }
        else //если текущее кол-во очков меньше, чем рекорд, то:
        {
            highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //выводим текст с предыдущим рекордом и именем игрока
        }

    }

    public void Restart() //функция для рестарта, когда игра окончилась
    {
        SceneManager.LoadScene(1); //заново загружается сцена с индексом 1
    }

    public void BackToMenu() //функция для возврата из игры в меню
    {
        SceneManager.LoadScene(0); //переходим к сцене с индексом 0
    }

}
