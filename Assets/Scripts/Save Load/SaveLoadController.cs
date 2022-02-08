using UnityEngine;


public class SaveLoadController : MonoBehaviour
{
    [Header("Пример - save.gamesave")]
    [SerializeField] private SaveGameData _data;
    public SaveGameData Data => _data;
    public string baseLoadNamel = "save.gamesaveone";
    [SerializeField] private static string loadName ;

    private static MainPlayer mainPlayer;
    private static TargetsManager targetsManager;

    [ContextMenu("Save")]
    private static void SaveGame() {
        mainPlayer = MainPlayer.Instance;
        targetsManager = TargetsManager.Instance;
        SaveManager.SaveData(mainPlayer, targetsManager, loadName);
    }

    /*    IEnumerator SaveTimer() {
            while (true)
            {
                yield return new WaitForSeconds(5);
                //Debug.Log("Сохранил игру");
                SaveGame();
            }
        }*/

    
    private void Awake()
    {
        loadName = baseLoadNamel;
        //Debug.Log(loadName);
    }
    public static void SaveOut() {
        SaveGame();
    }

    public void DeleteSaves()
    {
        SaveManager.DeleteSave();
    }

    private void Start()
    {
      //  StartCoroutine(SaveTimer());
        SetDataFromSave();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F1))
        {
            MainPlayer.Instance.Money = 500;
            MainPlayer.Instance.ShowMessage("+500 денег");
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F4))
        {
            DeleteSaves();
            MainPlayer.Instance.ShowMessage("Сброс сохранений");
        }

    }

    public void SetDataFromSave()
    {
        SaveGameData data = SaveManager.LoadData(loadName);
       
        if (data == null) return;

        _data.money = data.money;
        _data.exp = data.exp;
        _data.policeStates = data.policeStates;
        _data.housesState = data.housesState;


        MainPlayer.Instance.money = data.money;
        MainPlayer.Instance.raiting = data.exp;

        TargetsManager.Instance.factoriesSecurity.upg_moreCar = data.policeStates.upg_teach;
        TargetsManager.Instance.factoriesSecurity.upg_teach = data.policeStates.upg_teach;
        TargetsManager.Instance.factoriesSecurity.upg_powerUp = data.policeStates.upg_powerUp;
        TargetsManager.Instance.factoriesSecurity.upg_tablet = data.policeStates.upg_tablet;
        TargetsManager.Instance.factoriesSecurity.upg_radio = data.policeStates.upg_radio;
        TargetsManager.Instance.factoriesSecurity.upg_fleshers = data.policeStates.upg_fleshers;

        for (int i = 0; i < TargetsManager.Instance.houses.Count; i++)
        {
            TargetsManager.Instance.houses[i].upg_zabor_or_signalization = data.housesState[i].upg_zabor_or_signalization;
            TargetsManager.Instance.houses[i].upg_camera = data.housesState[i].upg_camera;
        }
    }

}
