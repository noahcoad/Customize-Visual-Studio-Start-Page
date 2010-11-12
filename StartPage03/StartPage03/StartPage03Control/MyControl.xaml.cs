// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using System.IO;

namespace StartPage03Control
{
	/// <summary>
	/// Interaction logic for MyControl.xaml
	/// </summary>
	public partial class MyControl : UserControl
	{
		public MyControl()
		{
			InitializeComponent();
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			// Load control user settings from previous session.
			// Example: string value = StartPageSettings.RetrieveString("MySetting");

			string path = StartPageSettings.RetrieveString("ImagePath");
			if (path != null && File.Exists(path)) StartPageImage = path;
		}

		private StartPageSettings _settings;
		/// <summary>
		/// Return a StartPageSettings object.
		///
		/// Use: StartPageSettings.StoreString("MySettingName", "MySettingValue");
		/// Use: string value = StartPageSettings.RetrieveString("MySettingName");
		///
		/// Note: As this property is using the Start Page tool window DataContext to retrieve the Visual Studio DTE, 
		/// this property can only be used after the UserControl is loaded and the inherited DataContext is set.
		/// </summary>
		private StartPageSettings StartPageSettings
		{
			get
			{
				if (_settings == null)
				{
					DTE2 dte = Utilities.GetDTE(DataContext);
					ServiceProvider serviceProvider = Utilities.GetServiceProvider(dte);
					_settings = new StartPageSettings(serviceProvider);
				}
				return _settings;
			}
		}

		private void ChooseImageButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Images (*.png;*.jpg)|*.png;*.jpg";
			bool? result = ofd.ShowDialog();
			if (result == true)
			{
				StartPageImage = ofd.FileName;
				StartPageSettings.StoreString("ImagePath", StartPageImage);
			}
		}
		

		public string StartPageImage
		{
			get { return (string)GetValue(StartPageImageProperty); }
			set { SetValue(StartPageImageProperty, value); }
		}

		// Using a DependencyProperty as the backing store for StartPageImage.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty StartPageImageProperty =
				DependencyProperty.Register("StartPageImage", typeof(string), typeof(MyControl), null);

		
	}
}
