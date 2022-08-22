
using UnityEditor;
using UnityEditor.Build;
using System.Xml.Linq;

public class ManifestHelper
{
	private XDocument doc;
	private XNamespace ns = @"http://schemas.android.com/apk/res/android";

	public ManifestHelper(string path)
	{
		doc = XDocument.Load(path);
	}

	public void Save(string path)
	{
		doc.Save(path);
	}

	public void SetVersions(string versionName, int versionCode)
	{
		doc.Root.SetAttributeValue(ns + "versionCode", versionCode);
		doc.Root.SetAttributeValue(ns + "versionName", versionName);
	}
}

public class StorePreProcessor : IPreprocessBuild
{
	public const string AndroidManifestPath = "Assets/Plugins/Android/LauncherManifest.xml";

	public int callbackOrder
	{
		get
		{
			return 0;
		}
	}

	public void OnPreprocessBuild(BuildTarget target, string path)
	{
		if (target == BuildTarget.Android)
		{
			ManifestHelper manifest = new ManifestHelper(AndroidManifestPath);

			manifest.SetVersions(PlayerSettings.bundleVersion, PlayerSettings.Android.bundleVersionCode);
			manifest.Save(AndroidManifestPath);
		}
	}
}
