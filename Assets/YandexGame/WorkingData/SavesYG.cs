
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public int AllCoins = 0;

        public int RecordDistance = 0;

        public int LevelsSave = 1;

        public bool[] wasBought = new bool[4];

        public int PlayerIndex;

        public float MusicSliderValue = 10;

        public float SoundSliderValue = 10;

        public string LastLoginKey = "01.01.0001";

        public Quest CurrentQuest = new Quest();

        public bool QuestWasComplete = false;

        public int DailyRewardIndex = 0;

        public bool[] DailyRewardComplete = new bool[7];

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;

            wasBought[0] = true;
        }
    }
}
