// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace GI.Common.XF.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        public static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;
        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue(UserNameKey, value); }
        }

        private const string FNameKey = "fName_key";
        private static readonly string FNameDefault = string.Empty;
        public static string FName
        {
            get { return AppSettings.GetValueOrDefault(FNameKey, FNameDefault); }
            set { AppSettings.AddOrUpdateValue(FNameKey, value); }
        }

        private const string LNameKey = "lName_key";
        private static readonly string LNameDefault = string.Empty;
        public static string LName
        {
            get { return AppSettings.GetValueOrDefault(LNameKey, LNameDefault); }
            set { AppSettings.AddOrUpdateValue(LNameKey, value); }
        }

        private const string UserEmailKey = "useremail_key";
        private static readonly string UserEmailDefault = string.Empty;
        public static string UserEmail
        {
            //get { return AppSettings.GetValueOrDefault(UserEmailKey, UserEmailDefault); }

            get { return "gismotest@ebrso.onmicrosoft.com"; }
            set { AppSettings.AddOrUpdateValue(UserEmailKey, value); }
        }

        private const string UserPasswordKey = "userpassword_key";
        private static readonly string UserPasswordDefault = string.Empty;
        public static string UserPassword
        {
            get { return AppSettings.GetValueOrDefault(UserPasswordKey, UserPasswordDefault); }
            set { AppSettings.AddOrUpdateValue(UserPasswordKey, value); }
        }

        private const string LastLoginKey = "lastlogintime_key";
        private static readonly DateTime LastLoginDefault = DateTime.Now;
        public static DateTime LastLogin
        {
            get { return AppSettings.GetValueOrDefault(LastLoginKey, LastLoginDefault).ToLocalTime(); }
            set { AppSettings.AddOrUpdateValue(LastLoginKey, value); }
        }

        private const string CompletedTypeKey = "Completed_key";
        private static readonly string CompletedTypeDefault = String.Empty;
        public static string CompletedType
        {
            get { return AppSettings.GetValueOrDefault(CompletedTypeKey, CompletedTypeDefault); }
            set { AppSettings.AddOrUpdateValue(CompletedTypeKey, value); }
        }

        private const string PersonSignKey = "PersonSign_key";
        private static readonly string PersonSignDefault = null;
        public static string PersonSign
        {
            get { return AppSettings.GetValueOrDefault(PersonSignKey, PersonSignDefault); }
            set { AppSettings.AddOrUpdateValue(PersonSignKey, value); }
        }

        private const string OfficerSignKey = "OfficerSign_key";
        private static readonly string OfficerSignDefault = null;
        public static string OfficerSign
        {
            get { return AppSettings.GetValueOrDefault(OfficerSignKey, OfficerSignDefault); }
            set { AppSettings.AddOrUpdateValue(OfficerSignKey, value); }
        }

        private const string AgencyLogoKey = "AgencyLogo_key";
        private static readonly string AgencyLogoDefault = null;
        public static string AgencyLogo
        {
            get { return AppSettings.GetValueOrDefault(AgencyLogoKey, AgencyLogoDefault); }
            set { AppSettings.AddOrUpdateValue(AgencyLogoKey, value); }
        }

        private const string OfficerIdKey = "AgencyId_key";
        private static readonly string OfficerIdDefault = null;
        public static string OfficerId
        {
            get { return AppSettings.GetValueOrDefault(OfficerIdKey, OfficerIdDefault); }
            set { AppSettings.AddOrUpdateValue(OfficerIdKey, value); }
        }

        private const string TokenKey = "Token_key";
        private static readonly string TokenDefault = null;
        public static string Token
        {
            get { return AppSettings.GetValueOrDefault(TokenKey, TokenDefault); }
            set { AppSettings.AddOrUpdateValue(TokenKey, value); }
        }

        private const string LastPrintedTicketKey = "LastPrintedTicket_key";
        private static readonly string LastPrintedTicketDefault = null;
        public static string LastPrintedTicket
        {
            get { return AppSettings.GetValueOrDefault(LastPrintedTicketKey, LastPrintedTicketDefault); }
            set { AppSettings.AddOrUpdateValue(LastPrintedTicketKey, value); }
        }

        private const string LastPrintTypeKey = "LastPrintType_key";
        private static readonly string LastPrintTypeDefault = null;
        public static string LastPrintType
        {
            get { return AppSettings.GetValueOrDefault(LastPrintTypeKey, LastPrintTypeDefault); }
            set { AppSettings.AddOrUpdateValue(LastPrintTypeKey, value); }
        }

        private const string TktSumnNumberKey = "TktSumnNumber_key";
        private static readonly string TktSumnNumberDefault = null;
        public static string TktSumnNumber
        {
            get { return AppSettings.GetValueOrDefault(TktSumnNumberKey, TktSumnNumberDefault); }
            set { AppSettings.AddOrUpdateValue(TktSumnNumberKey, value); }
        }

        private const string AudioTextKey = "AudioText_key";
        private static readonly string AudioTextDefault = null;
        public static string AudioText
        {
            get { return AppSettings.GetValueOrDefault(AudioTextKey, AudioTextDefault); }
            set { AppSettings.AddOrUpdateValue(AudioTextKey, value); }
        }

        private const string LastSOSDTKey = "LastSOSDT_key";
        private static readonly string LastSOSDTDefault = null;
        public static string LastSOSDT
        {
            get { return AppSettings.GetValueOrDefault(LastSOSDTKey, LastSOSDTDefault); }
            set { AppSettings.AddOrUpdateValue(LastSOSDTKey, value); }
        }

        private const string UploadDetailsKey = "UploadDetails_key";
        private static readonly string UploadDetailsDefault = null;
        public static string UploadDetails
        {
            get { return AppSettings.GetValueOrDefault(UploadDetailsKey, UploadDetailsDefault); }
            set { AppSettings.AddOrUpdateValue(UploadDetailsKey, value); }
        }

        private const string URIKey = "URI_key";
        private static readonly string URIDefault = null;
        public static string URI
        {
            get { return AppSettings.GetValueOrDefault(URIKey, URIDefault); }
            set { AppSettings.AddOrUpdateValue(URIKey, value); }
        }

        public static string ScannedBarcode, ScannedBarcodeFormat;
        
    }
}