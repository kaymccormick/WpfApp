﻿#region header
// Kay McCormick (mccor)
// 
// WpfApp
// WpfApp
// LoggingSection.cs
// 
// 2020-02-07-2:57 PM
// 
// ---
#endregion
using System.Configuration ;

namespace WpfApp.Config
{
    /// <summary>Configuration section handler for container helper settings.</summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    [ConfigTarget( typeof ( LoggerSettings ))]
    public class LoggingSection : ConfigurationSection
    {
    }

    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LoggerSettings
    public class LoggerSettings
    {

    }
}