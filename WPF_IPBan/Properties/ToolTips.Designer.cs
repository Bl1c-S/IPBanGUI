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
    internal class ToolTips {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ToolTips() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WPF_IPBanUtility.Properties.ToolTips", typeof(ToolTips).Assembly);
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
        ///   Looks up a localized string similar to Додати до чорного списку | Заблокувати назавжди.
        /// </summary>
        internal static string AddToBlackList {
            get {
                return ResourceManager.GetString("AddToBlackList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Додати до білого списку | Не буде блокуватись.
        /// </summary>
        internal static string AddToWiteList {
            get {
                return ResourceManager.GetString("AddToWiteList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Копіювати до буферу обміну.
        /// </summary>
        internal static string Copy {
            get {
                return ResourceManager.GetString("Copy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Кількість невдалих спроб входу.
        /// </summary>
        internal static string FailedLoginCount {
            get {
                return ResourceManager.GetString("FailedLoginCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Видалити з списку заблокованих | Розблокувати.
        /// </summary>
        internal static string RemoveBlockList {
            get {
                return ResourceManager.GetString("RemoveBlockList", resourceCulture);
            }
        }
    }
}
