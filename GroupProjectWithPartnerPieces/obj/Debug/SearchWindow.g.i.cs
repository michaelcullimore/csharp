﻿#pragma checksum "..\..\SearchWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "40F2B84993C91786E69C31F6CB1BD1E8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FinalProject;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FinalProject {
    
    
    /// <summary>
    /// SearchWindow
    /// </summary>
    public partial class SearchWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectInvoice;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Cancel;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox InvoiceNumber;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox InvoiceDate;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TotalCharge;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid InvoiceGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\SearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetSelection;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GroupProjectWithPartnerPieces;component/searchwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SearchWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SelectInvoice = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\SearchWindow.xaml"
            this.SelectInvoice.Click += new System.Windows.RoutedEventHandler(this.SelectInvoice_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\SearchWindow.xaml"
            this.Cancel.Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.InvoiceNumber = ((System.Windows.Controls.ComboBox)(target));
            
            #line 12 "..\..\SearchWindow.xaml"
            this.InvoiceNumber.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.InvoiceNumber_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.InvoiceDate = ((System.Windows.Controls.ComboBox)(target));
            
            #line 13 "..\..\SearchWindow.xaml"
            this.InvoiceDate.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.InvoiceDate_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TotalCharge = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\SearchWindow.xaml"
            this.TotalCharge.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TotalCharge_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.InvoiceGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 15 "..\..\SearchWindow.xaml"
            this.InvoiceGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.InvoiceGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ResetSelection = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\SearchWindow.xaml"
            this.ResetSelection.Click += new System.Windows.RoutedEventHandler(this.ResetSelection_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

