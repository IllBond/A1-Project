using Helpers;
using UnityEngine;


public class SaveLoadController : MonoBehaviour
{
    [Header("������ - save.gamesave")]
    [SerializeField] private SaveGameData _data;
    public SaveGameData Data => _data;
    public string baseLoadNamel = "save.gamesaveone";
    [SerializeField] private static string loadName ;

    private static MainPlayer mainPlayer;
    private static TargetsManager targetsManager;

    [ContextMenu("Save")]
    private async static void SaveGame() {
        mainPlayer = MainPlayer.Instance;
        targetsManager = TargetsManager.Instance;
        await SaveManager.SaveData(mainPlayer, targetsManager, loadName);
    }

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

#if UNITY_STANDALONE
    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F1))
        {
            MainPlayer.Instance.Money = 500;
            MainPlayer.Instance.ShowMessage("+500 �����");
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F4))
        {
            DeleteSaves();
            MainPlayer.Instance.ShowMessage("����� ����������");
        }
    }
#endif

    public async void SetDataFromSave()
    {
        SaveGameData data = await SaveManager.LoadData(loadName);
       
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
            await AsyncHelper.Delay(() =>
            {
                TargetsManager.Instance.houses[i].upg_zabor_or_signalization = data.housesState[i].upg_zabor_or_signalization;
                TargetsManager.Instance.houses[i].upg_camera = data.housesState[i].upg_camera;
            });
        }
    }

}
