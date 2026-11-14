﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF_IPBanUtility.Views.IPList;

public class IPListViewProperties(string title, bool isExpanded) : ObservableObject
{
     public string Title { get; } = title;
     public bool IsExpanded { get; set; } = isExpanded;
}
