﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_IPBanUtility.Properties {
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
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WPF_IPBanUtility.Properties.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для застосування змін служба IPBan буде перезавантажена після закриття вікна..
        /// </summary>
        public static string KeysChanged {
            get {
                return ResourceManager.GetString("KeysChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Виберіть теку де розміщується IPBan, після чого IPBanGUI буде скинуто до налаштувань за замовчуванням. На налаштування IPBan ці зміни ніяк не вплинуть..
        /// </summary>
        public static string LoadSettingsErrorText {
            get {
                return ResourceManager.GetString("LoadSettingsErrorText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Помилка завантаження налаштувань.
        /// </summary>
        public static string LoadSettingsErrorTitle {
            get {
                return ResourceManager.GetString("LoadSettingsErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Помилка збереження.
        /// </summary>
        public static string SaveError {
            get {
                return ResourceManager.GetString("SaveError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Встановлення налаштувать за замовчування вплине тільки на IPBanGUI. На налаштування ключів у вікні &quot;Ключі&quot; це ніяк не вплине..
        /// </summary>
        public static string SetDefaultSettingsText {
            get {
                return ResourceManager.GetString("SetDefaultSettingsText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Скидання налаштувань.
        /// </summary>
        public static string SetDefaultSettingsTitle {
            get {
                return ResourceManager.GetString("SetDefaultSettingsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ви можете надіслати нам ваші пропозиції щодо вдосконалення програми або повідомити про помилки, написавши на GitHub, або зв&apos;язатись з нами по електронній пошті, Mail..
        /// </summary>
        public static string SupportDescription {
            get {
                return ResourceManager.GetString("SupportDescription", resourceCulture);
            }
        }
    }
}
