// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace FutebolAPP.App.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string UserIdKey = "userId";
        private static readonly string UserIdDefault = string.Empty;

        private const string AuthTokenKey = "authToken";
        private static readonly string AuthTokenDefault = string.Empty;

        private const string UserFullNameKey = "userFullName";
        private static readonly string UserFullNameDefault = string.Empty;

        private const string UserPictureURLKey = "userPictureURL";
        private static readonly string userPictureURLDefault = string.Empty;

        #endregion

        #region Properties

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(UserIdKey, value);
            }
        }

        public static string UserFullName
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(UserFullNameKey, UserFullNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(UserFullNameKey, value);
            }
        }

        public static string UserPictureURL
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(UserPictureURLKey, userPictureURLDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(UserPictureURLKey, value);
            }
        }

        public static string AuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value);
            }
        }

        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);

        #endregion
    }
}