﻿#pragma checksum "C:\Workshop\Visual Studio 2012\SpeedMath\SpeedMath\PanoramaPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8E5A49FADCA8D9760FD3BBA4E6EF0A05"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Mathbrain {
    
    
    public partial class PanoramaPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton Play;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama PanoramaControl;
        
        internal Microsoft.Phone.Controls.PanoramaItem itemhow;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock how;
        
        internal Microsoft.Phone.Controls.PanoramaItem itemabout;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Mathbrain;component/PanoramaPage.xaml", System.UriKind.Relative));
            this.Play = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("Play")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PanoramaControl = ((Microsoft.Phone.Controls.Panorama)(this.FindName("PanoramaControl")));
            this.itemhow = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("itemhow")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.how = ((System.Windows.Controls.TextBlock)(this.FindName("how")));
            this.itemabout = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("itemabout")));
        }
    }
}

