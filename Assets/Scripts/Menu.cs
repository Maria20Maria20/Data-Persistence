using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //надо, чтобы внутренность функции Exit работала
using UnityEngine.SceneManagement; //подключаю библиотеку SceneManagement, чтоб можно было совершать действия с этой/другими сценами

public class Menu : MonoBehaviour
{
    public void StartNew() //функция для кнопки "Старт"
    {
        SceneManager.LoadScene(1); //загружаем другую сцену. 1 - индекс сцены, которую я хочу загрузить. Индекс сцены указан в Build Settings->Scenes In Build->справа от добавленной сцены цифра
    }

    public void Exit() //функция для кнопки "Выход"
    {
#if UNITY_EDITOR //если игра воспроизведена в редакторе юнити, то:
        EditorApplication.ExitPlaymode(); //выходим из режима воспроизведения
#else //в обратном случае (если игра открыта в приложении, а не в юнити):
        Application.Quit(); //выходим из приложения
#endif
    }
}
