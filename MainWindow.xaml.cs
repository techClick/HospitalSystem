﻿using System;
using System.Windows;
using System.Windows.Interop;
using System.Diagnostics;

namespace Hospital_System_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void PageOne(object sender, RoutedEventArgs e)
        {
            framer.Content = new Page1();
            SetHeader("REGISTER PATIENT");
        }
        private void PageTwo(object sender, RoutedEventArgs e)
        {
            framer.Content = new Page2();
            SetHeader("PATIENT PURCHASES");
        }
        public void SetHeader(string value)
        {
            heading.Text = value;
        }
        public static string strFriendlyName = AppDomain.CurrentDomain.FriendlyName;
        Process[] pro = Process.GetProcessesByName(
            strFriendlyName.Substring(0, strFriendlyName.LastIndexOf('.')));
        public class WindowWrapper : System.Windows.Forms.IWin32Window
        {
            public WindowWrapper(IntPtr handle)
            {
                _hwnd = handle;
            }

            public WindowWrapper(Window window)
            {
                _hwnd = new WindowInteropHelper(window).Handle;
            }

            public IntPtr Handle
            {
                get { return _hwnd; }
            }

            private IntPtr _hwnd;
        }
        public System.Windows.Forms.IWin32Window Wind()
        {
            return new WindowWrapper(pro[0].MainWindowHandle);
        }
    }
}
