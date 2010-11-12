// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace StartPage03Control
{
	/// <summary>
	/// Helper class to persist and retrieve Start Page user settings.
	/// </summary>
	class StartPageSettings
	{
		/// <summary>
		/// Constructor which take the Visual Studio ServiceProvider.
		/// </summary>
		/// <param name="serviceProvider">Visual Studio ServiceProvider</param>
		public StartPageSettings(ServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		private ServiceProvider _serviceProvider;
		private ServiceProvider ServiceProvider
		{
			get
			{
				return _serviceProvider;
			}
		}

		private IVsWritableSettingsStore _settingsStore = null;
		/// <summary>
		/// Return the Visual Studio writable user settings store.
		/// </summary>
		private IVsWritableSettingsStore SettingsStore
		{
			get
			{
				if (_settingsStore == null)
				{
					IVsSettingsManager settingsManager = ServiceProvider.GetService(typeof(SVsSettingsManager)) as IVsSettingsManager;

					settingsManager.GetWritableSettingsStore((uint)__VsSettingsScope.SettingsScope_UserSettings, out _settingsStore);
				}
				return _settingsStore;
			}
		}

		private const string SettingsRootPrefix = @"StartPage\Settings\";
		private string _settingsRoot;
		/// <summary>
		/// Generate a unique settings store collection name based on project name and GUID.
		/// </summary>
		private string SettingsRoot
		{
			get
			{
				if (_settingsRoot == null)
				{
					StringBuilder settingsRoot = new StringBuilder(SettingsRootPrefix);
					settingsRoot.Append("StartPage03Control");
					settingsRoot.Append(".");
					settingsRoot.Append("42275320-8198-4822-ac0a-ad6a5ff5ca34");

					_settingsRoot = settingsRoot.ToString();
				}

				return _settingsRoot;
			}
		}

		/// <summary>
		/// Set the specified string value in the Start Page user settings.
		/// </summary>
		/// <param name="name">Setting name</param>
		/// <param name="value">string value</param>
		public void StoreString(string name, string value)
		{
			int exists = 0;
			SettingsStore.CollectionExists(SettingsRoot, out exists);
			if (exists != 1)
			{
				SettingsStore.CreateCollection(SettingsRoot);
			}
			SettingsStore.SetString(SettingsRoot, name, value);
		}

		/// <summary>
		/// Get the specified string value from the Start Page user settings.
		/// </summary>
		/// <param name="name">setting name</param>
		/// <returns>string value</returns>
		public string RetrieveString(string name)
		{
			string value;
			SettingsStore.GetStringOrDefault(SettingsRoot, name, "", out value);
			return value;
		}
	}
}
