using System.Collections.Generic;

namespace KadoNem.ProjectNinja.Static
{
    public static class ConstantValues
    {
        //SETTINGS
        public const string MUSIC_SAVE_PATH = "Music";
        public const string SFX_SAVE_PATH = "Sfx";
        public const string LANGUAGE = "Language";
        public const string MUSIC_TOGGLE = "MusicToggle";
        public const string SFX_TOGGLE = "SfxToggle";
        public const float VOLUME_ON = 0f;
        public const float VOLUME_OFF = -144f;

        //GAME PROGRESS
        public const string LOCAL_STATE = DATA_FOLDER + "LockState";
        public const string LEVEL_STARS = DATA_FOLDER + "LevelStars";
        public const string TOTAL_STARS = DATA_FOLDER + "Stars";
        public const string CURRENT_CHAMPIONSHIP_TRACK = DATA_FOLDER + "Current_Track";

        //GAME PREPARATION
        public const string PLAYER_CAR = DATA_FOLDER + "PlayerCar";
        public const string BOT_CAR = DATA_FOLDER + "BotCar";
        public const string GAME_MODE = DATA_FOLDER + "GameMode";
        public const string TRACK = DATA_FOLDER + "Track";
        public const string NUMBER_OF_PLAYERS = DATA_FOLDER + "NumberOfPlayers";
        public const string NUMBER_OF_LAPS = DATA_FOLDER + "NumberOfLaps";
        public const string TRACK_REVERSED = DATA_FOLDER + "TrackReversed";
        public const string CHAMPIONSHIP_FINISHED = DATA_FOLDER + "Championship_Finished";
        public const string CHAMPIONSHIP_PODIUM = DATA_FOLDER + "ChampionshipPodium";
        public const string DIFICULTY_MODE = DATA_FOLDER + "DificultyMode";
        public const string DIFICULTY_MULTIPLIER = DATA_FOLDER + "DificultyMultiplier";

        //DATABASES
        public const string PLAYER_DATA_BASE = DATA_FOLDER + "PlayerDataBase";
        public const string UNLOCKABLES_SAVE_PATH = DATA_FOLDER + "Unlockables";
        public const string BLOCK_SAVE_PATH = DATA_FOLDER + "Block";

        //SCENE LOADER
        public const string CURRENT_SCENE_INDEX = DATA_FOLDER + "CurrentSceneIndex";

        //FOLDERS
        public const string DATA_FOLDER = ".lBkAbiZuFtVUhVsBv35x/";

        //TONGUE TWISTER - LOCALIZATION PLUGIN
        public const string PLAYER_NAME_LOCAL = "HeaderCanvas.PlayerName";

        //LISTS
        public static readonly IReadOnlyDictionary<int, string> STRING_NUMBERS = new Dictionary<int, string>()
        {
            {0, "0"},
            {1, "1"},
            {2, "2"},
            {3, "3"},
            {4, "4"},
            {5, "5"},
            {6, "6"},
            {7, "7"},
            {8, "8"},
            {9, "9"},
            {10, "10"},
        };

        public static readonly IReadOnlyDictionary<int, string> STRING_PODUIM_POSITIONS = new Dictionary<int, string>()
        {
            {1, "1ST"},
            {2, "2ND"},
            {3, "3RD"},
            {4, "4TH"},
            {5, "5TH"},
            {6, "6TH"},
        };
    }



    public static class AdMobConstants
    {
#if UNITY_ANDROID
        //public const string APP_ID = "ca-app-pub-3940256099942544~3347511713"; //Test ID
        public const string BANNER_AD_ID = "ca-app-pub-3940256099942544/6300978111";
        public const string INTERSTITIAL_AD_ID = "ca-app-pub-3940256099942544/1033173712";
        public const string REWARDED_VIDEO_AD_ID = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IOS
        //public const string APP_ID = "ca-app-pub-3940256099942544~1458002511"; //Test ID
        public const string BANNER_AD_ID = "ca-app-pub-3940256099942544/2934735716";
        public const string INTERSTITIAL_AD_ID = "ca-app-pub-3940256099942544/4411468910";
        public const string REWARDED_VIDEO_AD_ID = "ca-app-pub-3940256099942544/1712485313";
#endif
    }
}
