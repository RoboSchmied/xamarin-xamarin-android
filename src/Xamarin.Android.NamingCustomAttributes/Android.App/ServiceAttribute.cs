//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'manifest-attribute-codegen'.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using System;

namespace Android.App;

[Serializable]
[AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed partial class ServiceAttribute : Attribute, Java.Interop.IJniNameProviderAttribute {
	public ServiceAttribute ()
	{
	}

	public bool DirectBootAware { get; set; }

	public bool Enabled { get; set; }

	public bool Exported { get; set; }

	public Android.Content.PM.ForegroundService ForegroundServiceType { get; set; }

	public string? Icon { get; set; }

	public bool IsolatedProcess { get; set; }

	public string? Label { get; set; }

	public string? Name { get; set; }

	public string? Permission { get; set; }

	public string? Process { get; set; }

	public string? RoundIcon { get; set; }

}
