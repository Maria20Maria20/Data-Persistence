using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance { get; private set; } //к компонентам этого скрипта можно обращаться через переменную Instance. Чтоб никто не мог сломать игру, поменяв эту общедоступное переменную, ставлю геттер (get;) (режим чтения).
                                                                    //Потом ставлю закрытый сеттер, чтоб переменной можно было пользоваться только в классах, наследованных от MonoBehaviour (а если оставить только геттер, то будут ошибки)

    public int scoreHigh; //переменная для вывода рекорда
    public string nameScoreHigh; //переменная для вывода имени игрока
    public bool load = false; //проверка, загружен ли предыдущий результат (кол-во очков) или нет.

    [SerializeField] private Text highText; //то, где будут отображаться рекорд и имя игрока

    private void Awake()
    {
        LoadScore(); //загружаю рекорд и имя игрока перед началом игры
        //Debug.Log(scoreHigh); //чисто проверочка в консоли, меняется ли рекорд
        //Debug.Log(nameScoreHigh); //чисто проверочка в консоли, меняется ли имя игрока
        if (Instance != null) //если этот скрипт уже использовался, то:
        {
            Destroy(gameObject); //удаляем его (надо, чтобы игровой объект PersistenceManager в иерархии не копировался по 100500 раз при повторном переходе на одну и ту же сцену)
            return; //возвращаемся
        }
        Instance = this; //с помощью PersistenceManager.Instance можно обращаться к любой части этого скрипта
        DontDestroyOnLoad(gameObject); //объекты, прикреплённые к этому скрипту, не удаляются в новой сцене (рекорд и имя игрока)
    }

    [System.Serializable] //класс, указанный ниже, сериализует данные (сохраняет их даже за пределами игры)
    class SaveData //в этом классе содержится то, что будет сохраняться и загружаться посессионно (то есть можно выйти из игры, данные всё равно будут сохранены)
    {
        public int scoreHigh; //рекорд
        public string nameScoreHigh; //имя игрока
    }

    public void SaveScore() //функция для сохранения рекорда + имени игрока
    {
        SaveData data = new SaveData(); //сохраняю значение переменной из SaveData
        data.scoreHigh = scoreHigh; //сохранённое значение рекорда = значение рекорда, которое надо сохранить
        data.nameScoreHigh = nameScoreHigh; //сохранённое значение имени игрока = значение имени игрока, которое надо сохранить
        string json = JsonUtility.ToJson(data); //преобразуем сохранённое значение (data) в файл json (ToJson)
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //выделяем отдельное место для записи файла с расширением json
    }

    public void LoadScore() //функция для загрузки рекорда + имени игрока
    {
        load = true; //то есть к функции LoadScore обратились (это надо для условия в скрипте Game->void Start)
        string path = Application.persistentDataPath + "/savefile.json"; //десериализуем формат json в данные. 1 этап: вытаскиваем json в переменную с именем path
        if (File.Exists(path)) //если файл path существует (то есть есть json, который можно десериализовать) то:
        {
            string json = File.ReadAllText(path); //2 этап: читаем json как path
            SaveData data = JsonUtility.FromJson<SaveData>(json); //3 этап: преобразуем json в переменную data (ту самую переменную, где сохранённое значение нужной переменной)
            scoreHigh = data.scoreHigh; //переменная рекорда, находящаяся в игре (значение, которое надо сохранить) = сохранённой переменной рекорда (сохранённое значение)
            nameScoreHigh = data.nameScoreHigh; //переменная имени игрока, находящаяся в игре (значение, которое надо сохранить) = сохранённой переменной имени игрока (сохранённое значение)
        }
    }
}
