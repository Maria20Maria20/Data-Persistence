using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //надо, чтобы внутренность функции Exit работала
using UnityEngine.SceneManagement; //подключаю библиотеку SceneManagement, чтоб можно было совершать действия с этой/другими сценами
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour
{
    public static string inputName { get; private set; } //переменная для сохранения имени игрока в разных сценах. Чтоб никто не мог сломать игру, поменяв эту общедоступное переменную, ставлю геттер (get;) (режим чтения).
                                                         //Потом ставлю закрытый сеттер, чтоб переменной можно было пользоваться только в классах, наследованных от MonoBehaviour (а если оставить только геттер, то будут ошибки)

    [SerializeField] private Text name; //здесь в инспекторе указано текстовое поле, где игрок вводит своё имя
    [SerializeField] private InputField inputField; //здесь в инспекторе указано поле InputField для ввода имени игрока
    [SerializeField] Text highScore; //здесь в инспекторе указано поле для вывода рекорда
    [SerializeField] private GameObject notifyName; //здесь указано уведомление для игрока, который, не вводя имени, хочет зайти в игру

    void Start() //при старте меню:
    {
        HighScore(); //выводим содержимое функции по самому большому кол-ву очков
        if (PersistenceManager.Instance != null) //если игрок зашёл не в первый раз, то:
        {
            PersistenceManager.Instance.LoadScore(); //загружаем рекорд и введённое имя игрока (если оно есть)
        }
        name.text = inputName; //текстовое поле, содержащее имя игрока = переменная для сохранения имени игрока (идёт перезапись в эту переменную для использования в других сценах)
    }

    private void HighScore() //выводим рекорд в меню
    {
        highScore.text = "High Score: " + PersistenceManager.Instance.scoreHigh + " " + PersistenceManager.Instance.nameScoreHigh; //в поле для вывода рекорда отображается текст "High Score: " + значение рекорда + имя игрока
    }

    public void LoadText() //функция для загрузки имени, которое ввёл игрок
    {
        inputName = inputField.text; //введённое имя = имя из компонента Text, который находится у InputField (дочерний объект Text у объекта InputField)
    }

    public void StartNew() //функция для кнопки "Старт"
    {
        if (inputName != null) //если игрок ввёл своё имя, то:
        {
            SceneManager.LoadScene(1); //загружаем другую сцену. 1 - индекс сцены, которую я хочу загрузить. Индекс сцены указан в Build Settings->Scenes In Build->справа от добавленной сцены цифра
        }
        else //если не ввёл, то:
        {
            StartCoroutine(NoName()); //включается последовательность действий с именем NoName
        }
    }

    IEnumerator NoName() //последовательность действий (IEnumerator) для тех, кто не указал своё имя (NoName)
    {
        notifyName.gameObject.SetActive(true); //включается игровой объект, указанный в инспекторе в переменной notifyName
        yield return new WaitForSeconds(3); //проходит 3 секунды
        notifyName.gameObject.SetActive(false); //выключается игровой объект, указанный в инспекторе в переменной notifyName
    }

    public void Exit() //функция для кнопки "Выход"
    {
        PersistenceManager.Instance.SaveScore(); //при выходе из игры сохраняем рекорд и имя игрока
#if UNITY_EDITOR //если игра воспроизведена в редакторе юнити, то:
        EditorApplication.ExitPlaymode(); //выходим из режима воспроизведения
#else //в обратном случае (если игра открыта в приложении, а не в юнити):
        Application.Quit(); //выходим из приложения
#endif
    }
}
