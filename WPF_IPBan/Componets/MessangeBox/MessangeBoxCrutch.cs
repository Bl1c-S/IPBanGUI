﻿using System;

namespace WPF_IPBanUtility.View.LoadWindow.MessangeBox;

internal static class MessangeBoxCrutch
{
     public static void ErrorBox(string message)
     {
          DialogMessageBox.InfoBox(Properties.Status.Error, message);
     }
     public static void TwoActionBoxAndLeftButtonNameSelect(Action left, Action right)
     {
          DialogMessageBox.TwoActionBox(left, right, Properties.Messages.LoadSettingsErrorText, Properties.Messages.LoadSettingsErrorTitle, Properties.ButtonNames.Select);
     }
}