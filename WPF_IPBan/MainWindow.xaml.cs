﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_IPBanUtility
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class MainWindow : Window
     {
          public Action? WindowClosing;
          public MainWindow()
          {
               InitializeComponent();
          }

          private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
          {
               WindowClosing?.Invoke();
               e.Cancel = false;
          }
     }
}
