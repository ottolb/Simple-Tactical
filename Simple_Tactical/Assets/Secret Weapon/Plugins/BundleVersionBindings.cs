using UnityEngine;
using System.Runtime.InteropServices;

public class BundleVersionBindings
{
#if UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern string _GetCFBundleVersion();
#endif

#if UNITY_IPHONE
	public static string BundleVersion
	{
		get
		{
			if(m_bundleVersion == null)
			{
				GetVersionInfo();
			}
			return m_bundleVersion;
		}
	}
#endif

	protected static string m_bundleVersion;

	protected static void GetVersionInfo ()
	{
#if UNITY_IOS && !UNITY_EDITOR
		m_bundleVersion = _GetCFBundleVersion();
#else
		m_bundleVersion = Application.version;
#endif

	}
}