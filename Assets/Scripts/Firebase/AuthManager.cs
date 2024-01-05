using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyTransition;

public class AuthManager : MonoBehaviour
{
    public Button loadButton;
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    [Header("Cloud UI")]
    public GameObject CloudUI;
    public GameObject LoginUI;
    public TMP_Text username;

    [Header("Confimation Cloud")]
    public GameObject confirmDialog;
    public TMP_Text confirmText;
    public TMP_Text actionText;
    public Button confirmBtn;
    public Button cancelBtn;

    private string actionToConfirm;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    FileDataHandler dataHandler;
    private bool isUserLoggedIn = false;

    void Awake()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
                CheckUserLoginStatus();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }
    public void CloudButton()
    {
        if (auth.CurrentUser != null)
        {
            // User is already logged in
            User = auth.CurrentUser;
            isUserLoggedIn = true;
            username.SetText("Welcome " + User.DisplayName + "!");
            CloudUI.SetActive(true);
        }
        else
        {
            LoginUI.SetActive(true);
            isUserLoggedIn = false;
        }
    }
    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    public void LogoutButton()
    {
        // Call the logout function
        Logout();
    }
    private void CheckUserLoginStatus()
    {
        if (auth.CurrentUser != null)
        {
            // User is already logged in
            User = auth.CurrentUser;
            isUserLoggedIn = true;
            Debug.Log("User is already logged in: " + User.DisplayName);
            username.SetText("Welcome " + User.DisplayName + "!");
        }
        else
        {
            isUserLoggedIn = false;
        }
    }
    private void Logout()
    {
        if (isUserLoggedIn)
        {
            // Log the user out
            auth.SignOut();
            isUserLoggedIn = false;
            // Optionally, hide UI elements for a logged-out user.
            CloudUI.SetActive(false);
        }
    }
    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            CloudUI.SetActive(true);
            username.SetText("Welcome " + User.DisplayName + "!");
            LoginUI.SetActive(false);
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        warningRegisterText.text = "<color=green>Register Complete Please Go to Login";
                    }
                }
            }
        }
    }
    public void ExportDataToFirebase()
    {
        if (User == null)
        {
            Debug.LogWarning("User is not logged in. Cannot export data.");
            return;
        }

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string[] profileIds = { "0", "1", "2" };
        string userID = User.UserId;
        bool checkTask = false;
        foreach (string profileId in profileIds)
        {
            string userPath = $"{userID}/saves/{profileId}";

            // Retrieve the data for the current profile ID from your data manager.
            Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();
            GameData profileData = null;
            profilesGameData.TryGetValue(profileId, out profileData);

            // If there's data available for this profile ID, export it.
            if (profileData != null)
            {
                // Serialize the GameData to JSON
                string jsonData = JsonUtility.ToJson(profileData);

                // Set the data in Firebase Realtime Database under the user's path for this profile ID.

                reference.Child(userPath).SetRawJsonValueAsync(jsonData).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        checkTask = true;
                        Debug.Log($"Data exported to Firebase Realtime Database for profile ID: {profileId}");
                    }
                    else if (task.IsFaulted)
                    {
                        actionText.text = "Failed to export data";
                        Debug.LogError($"Failed to export data for profile ID {profileId}: {task.Exception}");
                    }
                });
            }
            else
            {
                Debug.LogWarning($"No save file for profile ID: {profileId}");
            }
        }

        if (checkTask == true)
        {
            actionText.text = $"Data exported to Firebase";
        }
    }

    public void ImportDataFromFirebase()
    {
        if (User == null)
        {
            Debug.LogWarning("User is not logged in. Cannot import data.");
            return;
        }

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        string userID = User.UserId;

        // Define the list of profile IDs you want to import
        string[] profileIds = { "0", "1", "2" };

        foreach (string profileId in profileIds)
        {
            string userPath = $"{userID}/saves/{profileId}";

            // Retrieve data from Firebase Realtime Database
            reference.Child(userPath).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        // Deserialize the JSON data to your custom data structure (e.g., GameData)
                        string jsonData = snapshot.GetRawJsonValue();
                        GameData profileData = JsonUtility.FromJson<GameData>(jsonData);

                        if (profileData != null)
                        {
                            dataHandler.Save(profileData, profileId);
                            actionText.text = "Data imported";
                            Debug.Log($"Data imported for profile ID: {profileId}");
                            Application.Quit();
                        }
                        else
                        {
                            Debug.LogWarning($"Failed to deserialize data for profile ID: {profileId}");
                        }
                    }
                    else
                    {
                        actionText.text = $"No data found";
                        Debug.LogWarning($"No data found for profile ID: {profileId}");
                    }
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError($"Failed to retrieve data for profile ID {profileId}: {task.Exception}");
                }
            });
        }
    }
    public void ExportButton()
    {
        actionToConfirm = "Export";
        confirmText.text = "Do you want to export your data?";
        confirmDialog.SetActive(true);
    }

    public void ImportButton()
    {
        actionToConfirm = "Import";
        confirmText.text = "Do you want to import data? Your current progress will be replaced.\n After importing application will close Automatically.";
        confirmDialog.SetActive(true);
    }

    public void ConfirmAction()
    {
        confirmDialog.SetActive(false);

        if (actionToConfirm == "Export")
        {
            ExportDataToFirebase();
        }
        else if (actionToConfirm == "Import")
        {
            ImportDataFromFirebase();
        }
    }

    public void CancelAction()
    {
        confirmDialog.SetActive(false);
        Debug.Log("Action canceled.");
    }
}