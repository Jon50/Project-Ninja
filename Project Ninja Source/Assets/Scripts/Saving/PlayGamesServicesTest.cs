//using UnityEngine;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using TMPro;
//using UnityEngine.UI;
//using System;

//public class PlayGamesServicesTest : MonoBehaviour
//{
//    [SerializeField] private TextMeshProUGUI _debugText;
//    //[SerializeField] private TextMeshProUGUI _coinsText;
//    //[SerializeField] private Image _imageColor;

//    //private int _coins;

//    public static Action fastAction;

//    private void Start()
//    {
//        Initialize();
//        SignInUserWithPlayGames(SignInInteractivity.CanPromptOnce);
//    }

//    private void Initialize()
//    {
//        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).EnableSavedGames().Build());
//        PlayGamesPlatform.Activate();
//        _debugText.text = "Play Games Services Initialized";
//    }

//    private void SignInUserWithPlayGames( SignInInteractivity signInInteractivity )
//    {
//        _debugText.text = "Authenticating...";

//        PlayGamesPlatform.Instance.Authenticate(signInInteractivity, result =>
//        {
//            if(result == SignInStatus.Success)
//            {
//                _debugText.text = "User Signed In Successfully";
//                fastAction?.Invoke();
//            }
//            else
//                _debugText.text = "Sign In Failed";
//        });
//    }

//    //Linked to a button
//    public void ManualSignIn() => SignInUserWithPlayGames(SignInInteractivity.CanPromptAlways);

//    public void SignOutUserWithPlayGames()
//    {
//        PlayGamesPlatform.Instance.SignOut();
//        _debugText.text = "User Signed Out";
//    }

//    //public void RewardCoinsConsumable( int val )
//    //{
//    //    _coins += val;
//    //    _coinsText.text = _coins.ToString();
//    //}

//    //public void RewardColorNonConsumable()
//    //{
//    //    _imageColor.color = Color.red;
//    //}


//    //[SerializeField] private TextMeshProUGUI _valueToSave;

//    //public void SaveToGoogleCloud()
//    //{
//    //    GoogleCloudSave<string>.SaveToCloud(_valueToSave.text);
//    //}

//    //public void LoadFromGoogleCloud()
//    //{
//    //    GoogleCloudSave<string>.LoadFromCloud(( result ) =>
//    //    {
//    //        _valueToSave.text = result;
//    //    });
//    //}
//}
