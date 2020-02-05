﻿using System ;
using System.Diagnostics ;
using System.IO ;
using System.Runtime.CompilerServices ;
using Castle.DynamicProxy ;
using NLog ;
using WpfApp.Core.Logging ;

namespace Logging
{
    /// <summary>Attempt to hook into NLog and fix it up for my application.</summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LoggerProxyHelper
    public class LoggerProxyHelper
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see
        ///         cref="T:System.Object" />
        ///     class.
        /// </summary>
        public LoggerProxyHelper ( ProxyGenerator generator ) { Generator = generator ; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public LoggerProxyHelper ( ProxyGenerator generator , LogDelegates.LogMethod logMethod )
        {
            Generator    = generator ;
            UseLogMethod = logMethod ;
        }

        public ProxyGenerator Generator { get ; }

        public LogDelegates.LogMethod UseLogMethod { get ; set ; }

        public LogFactory CreateLogFactory ( LogFactory logFactory )
        {
            if ( logFactory == null )
            {
                logFactory = LogManager.LogFactory ;
            }

            var opts = new ProxyGenerationOptions ( new LoggerFactoryHook ( UseLogMethod ) ) ;
            opts.Initialize ( ) ;
            var proxy = ( LogFactory ) Generator.CreateClassProxyWithTarget (
                                                                             logFactory.GetType ( )
                                                                           , Type.EmptyTypes
                                                                           , logFactory
                                                                           , opts
                                                                           , new
                                                                                 LogFactoryInterceptor (
                                                                                                        Generator
                                                                                                      , UseLogMethod
                                                                                                       )
                                                                            ) ;
            return proxy ;
        }

        public static Logger GetCurrentClassLogger ( [ CallerFilePath ] string path = null )
        {
            var name = "default" ;
            if ( path != null )
            {
                name = Path.GetFileNameWithoutExtension ( path ) ;
            }

            // fixme
            MyLogFactory  myLogFactory = LogManager.Configuration?.LogFactory as MyLogFactory ;
            if ( myLogFactory == null )

            {
                Debug.WriteLine ( "no log factory of my type" ) ;
                return LogManager.GetCurrentClassLogger ( ) ;
                // throw new NotImplementedException ( ) ;
            }

            if ( myLogFactory.GetDoLogMessage ( ) != null )
            {
                Debug.WriteLine ( myLogFactory.GetDoLogMessage ( ).ToString ( ) ) ;
            }
            else
            {
                Debug.WriteLine ( "no dologmessage" ) ;
            }

            var logger = myLogFactory.GetLogger ( name ) ;
            return logger ;
        }
    }
}