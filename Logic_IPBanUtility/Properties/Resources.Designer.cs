﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logic_IPBanUtility.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Logic_IPBanUtility.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IPThreatApiKey\r\nFailedLoginAttemptsBeforeBan\r\nFirewallRulePrefix\r\nBanTime\r\nResetFailedLoginCountForUnbannedIPAddresses\r\nClearBannedIPAddressesOnRestart\r\nClearFailedLoginsOnSuccessfulLogin\r\nProcessInternalIPAddresses\r\nExpireTime\r\nCycleTime\r\nMinimumTimeBetweenFailedLoginAttempts\r\nMinimumTimeBetweenSuccessfulLoginAttempts\r\nWhitelist\r\nWhitelistRegex\r\nBlacklist\r\nBlacklistRegex\r\nTruncateUserNameChars\r\nUserNameWhitelist\r\nUserNameWhitelistRegex\r\nUserNameWhitelistMinimumEditDist [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DefaultKey {
            get {
                return ResourceManager.GetString("DefaultKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FailedLoginAttemptsBeforeBan\r\nBanTime\r\nExpireTime\r\nWhitelist.
        /// </summary>
        internal static string DefaultKeyEnable {
            get {
                return ResourceManager.GetString("DefaultKeyEnable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не знайдено теку: .
        /// </summary>
        internal static string DirectoryNotFoundException {
            get {
                return ResourceManager.GetString("DirectoryNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Виникла помилка під час запису файлу:.
        /// </summary>
        internal static string ErrorFileWrite {
            get {
                return ResourceManager.GetString("ErrorFileWrite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Під час запису зміни стану ключа, в списку завантажених keyIdentis, не знайдено ключ:.
        /// </summary>
        internal static string ErrorWriteKeyIdentiChanged {
            get {
                return ResourceManager.GetString("ErrorWriteKeyIdentiChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не знайдено файл: .
        /// </summary>
        internal static string FileNotFoundException {
            get {
                return ResourceManager.GetString("FileNotFoundException", resourceCulture);
            }
        }
    }
}
