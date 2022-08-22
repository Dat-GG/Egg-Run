
#if UNITY_EDITOR_WIN

using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class ChangeWindowTitle
{

	[DllImport( "User32" )]
	private static extern IntPtr GetActiveWindow();
	[DllImport( "User32" )]
	private static extern bool SetWindowTextA(IntPtr hWnd, string lpString);

	static ChangeWindowTitle()
	{
		EditorApplication.update += () =>
		{
			var handle = GetActiveWindow();
			SetWindowTextA(
				handle,
				string.Format( "{0} - {1} - Unity {2}",
				Application.productName,
				SceneManager.GetActiveScene().name,
				Application.unityVersion )
				);
		};
	}
}

#endif