﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace FluentAvalonia.UI;

/// <summary>
/// Helper class for storing localized string for FluentAvalonia/WinUI controls
/// </summary>
/// <remarks>
/// The string resources are taken from the WinUI repo. Not all resources in WinUI
/// may be available here, only those that are known to be used in a control
/// </remarks>
public class FALocalizationHelper
{
    private FALocalizationHelper()
    {
        var json = File.ReadAllText("D:/repos/Localization_NavigationView.json");

        _mappings = JsonSerializer.Deserialize<Dictionary<string, LocalizationEntry>>(json);
    }

    static FALocalizationHelper()
    {
        Instance = new FALocalizationHelper();
    }

    public static FALocalizationHelper Instance { get; }

    /// <summary>
    /// Gets a string resource by the specified name using the CurrentCulture
    /// </summary>
    public string GetLocalizedStringResource(string resName) =>
        GetLocalizedStringResource(CultureInfo.CurrentCulture, resName);

    /// <summary>
    /// Gets a string resource by the specified name and using the specified culture
    /// </summary>
    /// <remarks>
    /// InvariantCulture is not supported here and will default to en-US
    /// </remarks>
    public string GetLocalizedStringResource(CultureInfo ci, string resName)
    {
        // Don't allow InvariantCulture - default to en-us in that case
        if (ci == CultureInfo.InvariantCulture)
            ci = new CultureInfo("en-us");

        if (_mappings.ContainsKey(resName))
        {
            if (_mappings[resName].ContainsKey(ci.Name))
            {
                return _mappings[resName][ci.Name];
            }
        }

        return string.Empty;
    }

    // <ResourceName, Entries>
    private readonly Dictionary<string, LocalizationEntry> _mappings;

    /// <summary>
    /// Dictionary of language entries for a resource name. &lt;language, value&gt; where
    /// language is the abbreviated name, e.g., en-US
    /// </summary>
    public class LocalizationEntry : Dictionary<string, string>
    {

    }
}
