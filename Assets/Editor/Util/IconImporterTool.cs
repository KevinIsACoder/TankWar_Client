using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class TextureImporterTool : AssetPostprocessor
{
	class PlatformSettings
	{
		public string platform;
		public int maxTextureSize;
		public TextureImporterFormat textureFormat;
		public int compressionQuality;
	}

	static PlatformSettings iphoneSettings = new PlatformSettings()
	{
		platform = "iPhone",
		maxTextureSize = 1024,
		textureFormat = TextureImporterFormat.PVRTC_RGBA4,
		compressionQuality = 100
	};

	static PlatformSettings androidSettings = new PlatformSettings()
	{
		platform = "Android",
		maxTextureSize = 1024,
		textureFormat = TextureImporterFormat.RGBA32,
		compressionQuality = 100
	};

	static TextureImporterSettings iconImportSettings = new TextureImporterSettings()
	{
		alphaIsTransparency = false,
		aniso = 0,
		borderMipmap = false,
		compressionQuality = 100,
		convertToNormalMap = false,
		fadeOut = false,
		filterMode = FilterMode.Bilinear,
		generateCubemap = TextureImporterGenerateCubemap.None,
		generateMipsInLinearSpace = false,
		grayscaleToAlpha = false,
		heightmapScale = 0.25f,
		lightmap = false,
		linearTexture = false,
		maxTextureSize = 1024,
		mipmapBias = -1,
		mipmapEnabled = false,
		mipmapFadeDistanceEnd = 3,
		mipmapFadeDistanceStart = 1,
		mipmapFilter = TextureImporterMipFilter.BoxFilter,
		normalMap = false,
		normalMapFilter = TextureImporterNormalFilter.Standard,
		npotScale = TextureImporterNPOTScale.None,
		readable = false,
		seamlessCubemap = false,
		textureFormat = TextureImporterFormat.AutomaticCompressed,
		wrapMode = TextureWrapMode.Clamp,
	};

	[MenuItem("Kabam/Icon/DetectSettings")]
	static void DetectSettings()
	{
		if (Selection.objects == null)
		{
			return;
		}

		foreach (var obj in Selection.objects)
		{
			var path = AssetDatabase.GetAssetPath(obj);
			var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
			if (textureImporter != null)
			{
				TextureImporterSettings settings = new TextureImporterSettings();
				textureImporter.ReadTextureSettings(settings);
				var sb = new System.Text.StringBuilder();

				sb.AppendLine(path);

				sb.AppendLine("alphaIsTransparency = " + settings.alphaIsTransparency);
				sb.AppendLine("aniso = " + settings.aniso);
				sb.AppendLine("borderMipmap = " + settings.borderMipmap);
				sb.AppendLine("compressionQuality = " + settings.compressionQuality);
				sb.AppendLine("convertToNormalMap = " + settings.convertToNormalMap);
				sb.AppendLine("fadeOut = " + settings.fadeOut);
				sb.AppendLine("filterMode = " + settings.filterMode);
				sb.AppendLine("generateCubemap = " + settings.generateCubemap);
				sb.AppendLine("generateMipsInLinearSpace = " + settings.generateMipsInLinearSpace);
				sb.AppendLine("grayscaleToAlpha = " + settings.grayscaleToAlpha);
				sb.AppendLine("heightmapScale = " + settings.heightmapScale);
				sb.AppendLine("lightmap = " + settings.lightmap);
				sb.AppendLine("linearTexture = " + settings.linearTexture);
				sb.AppendLine("maxTextureSize = " + settings.maxTextureSize);
				sb.AppendLine("mipmapBias = " + settings.mipmapBias);
				sb.AppendLine("mipmapEnabled = " + settings.mipmapEnabled);
				sb.AppendLine("mipmapFadeDistanceEnd = " + settings.mipmapFadeDistanceEnd);
				sb.AppendLine("mipmapFadeDistanceStart = " + settings.mipmapFadeDistanceStart);
				sb.AppendLine("mipmapFilter = " + settings.mipmapFilter);
				sb.AppendLine("normalMap = " + settings.normalMap);
				sb.AppendLine("normalMapFilter = " + settings.normalMapFilter);
				sb.AppendLine("npotScale = " + settings.npotScale);
				sb.AppendLine("readable = " + settings.readable);
				sb.AppendLine("seamlessCubemap = " + settings.seamlessCubemap);
				sb.AppendLine("textureFormat = " + settings.textureFormat);
				sb.AppendLine("wrapMode = " + settings.wrapMode);

				Debug.Log(sb);
			}
		}
	}

	[MenuItem("Kabam/Icon/ReimportAllIcons", false, 0)]
	static void ReimportAllIcons()
	{
		// collect folders
		var folders = new List<string>();
		// folders.AddRange(IconSDChecker.iconFolders);

		// foreach (var folder in IconSDChecker.iconFolders)
		// {
		// 	folders.Add(folder + IconSDChecker.ICON_SD_FOLDER_POSTFIX);
		// }

		// filter icons
		var queue = new List<string>();

		foreach (var folder in folders)
		{
			if (!Directory.Exists(folder))
			{
				continue;
			}

			var di = new DirectoryInfo(folder);
			var fis = di.GetFiles();

			foreach (var fi in fis)
			{
				if (fi.Name.EndsWith(".meta"))
				{
					continue;
				}

				string path = fi.FullName.Replace(Application.dataPath, "Assets");
				queue.Add(path);
			}
		}

		// process
		DoReimport(queue);
	}

	[MenuItem("Kabam/Icon/ReimportSelectedIcons", false, 20)]
	static void ReimportSelectedIcons()
	{
		if (Selection.objects == null)
		{
			return;
		}

		var queue = new List<string>();

		foreach (var obj in Selection.objects)
		{
			var path = AssetDatabase.GetAssetPath(obj);
			queue.Add(path);
		}

		// process
		DoReimport(queue);
	}

	static void DoReimport(List<string> queue)
	{
		// process icons
		var result = new System.Text.StringBuilder();
		result.AppendLine("Job done!");

		for (int i = 0; i < queue.Count; ++i)
		{
			TextureImporter textureImporter = AssetImporter.GetAtPath(queue[i]) as TextureImporter;

			if (textureImporter != null)
			{
				bool processed = Apply(queue[i], textureImporter);
				if (processed)
				{
					result.AppendLine(queue[i]);
				}
			}

			bool cancel = EditorUtility.DisplayCancelableProgressBar(
				"Process Icons",
				"Progress: " + i + "/" + queue.Count,
				(float)i / queue.Count);

			if (cancel)
			{
				Debug.LogWarning("User canceled progress!");
				break;
			}
		}

		EditorUtility.ClearProgressBar();
		AssetDatabase.Refresh();

		Debug.Log(result);
	}

	static bool Apply(string path, TextureImporter textureImporter)
	{
		// texture settings
		TextureImporterSettings textureImporterSettings = new TextureImporterSettings();
		textureImporter.ReadTextureSettings(textureImporterSettings);
		bool isEqual = IsEqual(textureImporterSettings, iconImportSettings);
		if (!isEqual)
		{
			textureImporter.SetTextureSettings(iconImportSettings);
		}

		// iphone settings
		PlatformSettings settings = new PlatformSettings();
		settings.platform = iphoneSettings.platform;
		textureImporter.GetPlatformTextureSettings(settings.platform, out settings.maxTextureSize, out settings.textureFormat, out settings.compressionQuality);
		if (settings.maxTextureSize > 1024)
		{
			Debug.LogWarning("IOS MaxTextureSize: " + settings.maxTextureSize + " Path: " + path);
		}
		bool isIphoneEqual = IsEqual(settings, iphoneSettings, iphoneSettings.textureFormat);
		if (!isIphoneEqual)
		{
			textureImporter.SetPlatformTextureSettings(iphoneSettings.platform, iphoneSettings.maxTextureSize, iphoneSettings.textureFormat, iphoneSettings.compressionQuality, true);
		}

		// android settings
		settings.platform = androidSettings.platform;
		textureImporter.GetPlatformTextureSettings(settings.platform, out settings.maxTextureSize, out settings.textureFormat, out settings.compressionQuality);
		if (settings.maxTextureSize > 1024)
		{
			Debug.LogWarning("Android MaxTextureSize: " + settings.maxTextureSize + " Path: " + path);
		}
		bool isAndroidEqual = IsEqual(settings, androidSettings, TextureImporterFormat.RGBA16);
		if (!isAndroidEqual)
		{
			if (settings.textureFormat == TextureImporterFormat.RGBA16)
			{
				textureImporter.SetPlatformTextureSettings(androidSettings.platform, androidSettings.maxTextureSize, settings.textureFormat, androidSettings.compressionQuality, true);
			}
			else
			{
				textureImporter.SetPlatformTextureSettings(androidSettings.platform, androidSettings.maxTextureSize, androidSettings.textureFormat, androidSettings.compressionQuality, true);
			}
		}

		// import
		if (!isEqual || !isIphoneEqual || !isAndroidEqual)
		{
			AssetDatabase.ImportAsset(path);
			return true;
		}

		return false;
	}

	static bool IsEqual(TextureImporterSettings a, TextureImporterSettings b)
	{
		if (a.alphaIsTransparency == b.alphaIsTransparency &&
			a.aniso == b.aniso &&
			a.borderMipmap == b.borderMipmap &&
			a.compressionQuality == b.compressionQuality &&
			a.convertToNormalMap == b.convertToNormalMap &&
			a.fadeOut == b.fadeOut &&
			a.filterMode == b.filterMode &&
			a.generateCubemap == b.generateCubemap &&
			a.generateMipsInLinearSpace == b.generateMipsInLinearSpace &&
			a.grayscaleToAlpha == b.grayscaleToAlpha &&
			a.heightmapScale == b.heightmapScale &&
			a.lightmap == b.lightmap &&
			a.linearTexture == b.linearTexture &&
			a.maxTextureSize == b.maxTextureSize &&
			a.mipmapBias == b.mipmapBias &&
			a.mipmapEnabled == b.mipmapEnabled &&
			a.mipmapFadeDistanceEnd == b.mipmapFadeDistanceEnd &&
			a.mipmapFadeDistanceStart == b.mipmapFadeDistanceStart &&
			a.mipmapFilter == b.mipmapFilter &&
			a.normalMap == b.normalMap &&
			a.normalMapFilter == b.normalMapFilter &&
			a.npotScale == b.npotScale &&
			a.readable == b.readable &&
			a.seamlessCubemap == b.seamlessCubemap &&
			a.textureFormat == b.textureFormat &&
			a.wrapMode == b.wrapMode)
		{
			return true;
		}

		return false;
	}

	static bool IsEqual(PlatformSettings a, PlatformSettings b, TextureImporterFormat textureImporterFormat)
	{
		if (a.platform == b.platform &&
		    a.maxTextureSize == b.maxTextureSize &&
		    (a.textureFormat == b.textureFormat || a.textureFormat == textureImporterFormat) &&
		    a.compressionQuality == b.compressionQuality)
		{
			return true;
		}

		return false;
	}
}
