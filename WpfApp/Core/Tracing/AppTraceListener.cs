﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// AppTraceListener.cs
// 
// 2020-01-25-1:08 PM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.IO ;
using System.Linq ;
using System.Text ;
using System.Windows ;
using System.Windows.Markup ;
using System.Xml.Serialization ;
using JetBrains.Annotations ;
using NLog ;

namespace WpfApp.Core.Tracing
{
    /// <summary></summary>
    /// <seealso cref="System.Diagnostics.TraceListener" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for AppTraceListener
    [ UsedImplicitly ]
    public class AppTraceListener : TraceListener
    {
        /// <summary>The logger</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>Gets or sets the routed events.</summary>
        /// <value>The routed events.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for RoutedEvents
        public Dictionary < string , Info > RoutedEvents { get ; } =
            new Dictionary < string , Info > ( ) ;

        /// <summary>Writes trace and event information to the listener specific output.</summary>
        /// <param name="eventCache">
        ///     A <see cref="T:System.Diagnostics.TraceEventCache" />
        ///     object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        ///     A name used to identify the output, typically the name of
        ///     the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        ///     One of the
        ///     <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the
        ///     type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        public override void TraceEvent (
            TraceEventCache eventCache
          , string          source
          , TraceEventType  eventType
          , int             id
        )
        {
            var i = 0 ;
            foreach ( var q in eventCache.LogicalOperationStack )
            {
                Logger.Error ( $"{id}{i}: {source} {q.GetType ( )}: {q}" ) ;
                i ++ ;
            }
        }

        // ReSharper disable once RedundantOverriddenMember
        /// <summary>
        ///     Writes trace information, a message, and event information to the
        ///     listener specific output.
        /// </summary>
        /// <param name="eventCache">
        ///     A <see cref="T:System.Diagnostics.TraceEventCache" />
        ///     object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        ///     A name used to identify the output, typically the name of
        ///     the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        ///     One of the
        ///     <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the
        ///     type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">A message to write.</param>
        public override void TraceEvent (
            TraceEventCache eventCache
          , string          source
          , TraceEventType  eventType
          , int             id
          , string          message
        )
        {
            base.TraceEvent (
                             eventCache
                           , source
                           , eventType
                           , id
                           , message
                            ) ;
        }

        /// <summary>
        ///     Writes trace information, a formatted array of objects and event
        ///     information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">
        ///     A <see cref="T:System.Diagnostics.TraceEventCache" />
        ///     object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        ///     A name used to identify the output, typically the name of
        ///     the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        ///     One of the
        ///     <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the
        ///     type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="format">
        ///     A format string that contains zero or more format items,
        ///     which correspond to objects in the <paramref name="args" /> array.
        /// </param>
        /// <param name="args">
        ///     An <see langword="object" /> array containing zero or more
        ///     objects to format.
        /// </param>
        public override void TraceEvent (
            TraceEventCache eventCache
          , string          source
          , TraceEventType  eventType
          , int             id
          , string          format
          , params object[] args
        )
        {
            var xmlDict = new Dictionary < string , string > ( ) ;
            var doOutput = true ;
            for ( var i = 0 ; i < args.Length - 1 ; i += 2 )
            {
                var key = args[ i ] as string ;
                var o = args[ i + 1 ] ;
                string desc = null ;
                if ( o is RoutedEvent re )
                {
                    if ( ! RoutedEvents.TryGetValue ( re.Name , out var info ) )
                    {
                        RoutedEvents[ re.Name ] = info = new Info ( 0 ) ;
                    }

                    info.count += 1 ;
                    if ( re.Name == "ScrollChanged" )
                    {
                        doOutput = false ;
                    }

                    desc = re.Name ;
                }
                else if ( o is FrameworkElement fe )
                {
                    desc = $"{o.GetType ( ).Name}[{fe.Name}]" ;
                }
                else if ( o is bool )
                {
                    desc = o.GetType ( ) + "[" + o + "]" ;
                }
                else if ( o is RoutedEventArgs )
                {
                    desc = o.GetType ( ).ToString ( ) ;
                }

                //d[ args[ i ].ToString ( ) ] = args[ i + 1 ] ;
                if ( desc != null )
                {
                    xmlDict[ key ] = desc ;
                    continue ;
                }

                try
                {
                    var b = new StringBuilder ( ) ;

                    XamlWriter.Save ( args[ i + 1 ] , new StringWriter ( b ) ) ;
                    xmlDict[ args[ i ].ToString ( ) ] = b.ToString ( ) ;
                }
                catch ( Exception )
                {
                    if ( desc == null )
                    {
                        throw ;
                    }

                    xmlDict[ key ] = desc ;

                    try
                    {
                        var serializer = new XmlSerializer ( args[ i + 1 ].GetType ( ) ) ;
                        var b = new StringBuilder ( ) ;
                        serializer.Serialize ( new StringWriter ( b ) , args[ i + 1 ] ) ;
                        xmlDict[ args[ i ].ToString ( ) ] = b.ToString ( ) ;
                    }
                    catch ( Exception )
                    {
                        // ignored
                    }
                }
            }

            if ( ! doOutput )
            {
                return ;
            }

            Logger.Trace (
                          string.Join (
                                       "; "
                                     , xmlDict.AsQueryable ( )
                                              .Select (
                                                       ( pair , i ) => $"{pair.Key} = {pair.Value}"
                                                      )
                                      )
                         ) ;
        }

        /// <summary>
        ///     When overridden in a derived class, writes the specified message to
        ///     the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write ( string message ) { Logger.Debug ( message ) ; }

        /// <summary>
        ///     When overridden in a derived class, writes a message to the listener
        ///     you create in the derived class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine ( string message ) { Logger.Debug ( message ) ; }
    }
}