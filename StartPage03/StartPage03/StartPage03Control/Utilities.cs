// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.ComponentModel;
using System.Diagnostics;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace StartPage03Control
{
	class Utilities
	{
		/// <summary>
		/// Get the Visual Studio DTE object from the Start Page tool window DataContext.
		/// This allows controls to interact with Visual Studio through the DTE object model.
		/// </summary>
		/// <param name="dataContext">Start Page tool window DataContext</param>
		public static DTE2 GetDTE(object dataContext)
		{
			ICustomTypeDescriptor typeDescriptor = dataContext as ICustomTypeDescriptor;
			Debug.Assert(typeDescriptor != null, "Could not get ICustomTypeDescriptor from dataContext. Was the Start Page tool window DataContext overwritten?");
			PropertyDescriptorCollection propertyCollection = typeDescriptor.GetProperties();
			return propertyCollection.Find("DTE", false).GetValue(dataContext) as DTE2;
		}

		/// <summary>
		/// Return the Visual Studio IServiceProvider interface.
		/// This allows controls to interact with Visual Studio through proffered services.
		/// </summary>
		/// <param name="dte">Visual Studio DTE object</param>
		public static ServiceProvider GetServiceProvider(DTE2 dte)
		{
			return new ServiceProvider((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)dte);
		}
	}
}
