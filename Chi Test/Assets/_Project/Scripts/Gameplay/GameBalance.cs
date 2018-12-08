using Game.Event;
using UnityEngine;


namespace Game.Gameplay
{

    public class GameBalance : MonoBehaviour
    {
        [Header("Player"), Range(0, 15)]
        public float DefaultPlayerMinRadius;
        public float PlayerMinRadius { get; private set; }
        [Range(0, 15)]
        public float DefaultPlayerMaxRadius;
        public float PlayerMaxRadius { get; private set; }


        [Range(0, 120)]
        public float DefaultPlayerPower;
        [Range(0, 4)]
        public float DefaultPlayerUpwardsModifier;
        public float PlayerPower { get; private set; }
        public float PlayerUpwardsModifier { get; private set; }


        [Header("Level"), Range(1, 15)]
        public int DefInitialBlocks;
        public int InitialBlocks { get; private set; }


        [Header("Hazards"), Range(1, 15)]
        public int DefHazardMinScale;
        public int HazardMinScale { get; private set; }
        [Range(1, 15)]
        public int DefHazardMaxScale;
        public int HazardMaxScale { get; private set; }

        // Singleton pattern
        private static GameBalance _instance;
        public static GameBalance Instance
        {
            get { return _instance; }
        }

        public bool shouldDownload;


        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;

            if (shouldDownload)
            {
                RemoteSettings.Completed += HandleRemoteSettings;
                RemoteSettings.Updated += RemoteSettings_Updated;
            }

            //Use the current settings, before remote update, or default values if
            //the settings don't exist (yet)
            Read();
        }

        void Start()
        {
            EventManager.TriggerEvent(N.GameBalance.Updated, this);
            EventManager.StartListening(N.Level.NextLevel, OnNextLevel);
        }

        private void HandleRemoteSettings(bool wasUpdatedFromServer, bool settingsChanged, int serverResponse)
        {
            Debug.LogFormat("#Game Balance# HandleRemoteSettings wasUpdatedFromServer: {0}" +
            " settingsChanged: {1}    serverResponse: {2}", wasUpdatedFromServer, settingsChanged, serverResponse);

            Read();
            EventManager.TriggerEvent(N.GameBalance.Updated, this);
        }

        void Read()
        {
            //Player
            PlayerMinRadius = RemoteSettings.GetFloat("PlayerMinRadius", DefaultPlayerMinRadius);
            PlayerMaxRadius = RemoteSettings.GetFloat("PlayerMaxRadius", DefaultPlayerMaxRadius);

            PlayerPower = RemoteSettings.GetFloat("PlayerPower", DefaultPlayerPower);
            PlayerUpwardsModifier = RemoteSettings.GetFloat("PlayerUpwardsModifier", DefaultPlayerUpwardsModifier);

            //Level
            InitialBlocks = RemoteSettings.GetInt("InitialBlocks", DefInitialBlocks);

            //Hazards
            HazardMinScale = RemoteSettings.GetInt("HazardMinScale", DefHazardMinScale);
            HazardMaxScale = RemoteSettings.GetInt("HazardMaxScale", DefHazardMaxScale);

            Debug.Log("#Game Balance# HazardMinScale " + HazardMinScale);
            Debug.Log("#Game Balance# HazardMaxScale " + HazardMaxScale);
        }


        void RemoteSettings_Updated()
        {
            Debug.Log("#Game Balance# RemoteSettings_Updated ");
        }

        void OnNextLevel(object p_data)
        {
            if (shouldDownload)
                RemoteSettings.ForceUpdate();
        }
    }
}