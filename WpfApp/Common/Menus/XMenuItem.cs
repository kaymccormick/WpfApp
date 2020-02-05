using System ;
using System.Collections.Generic ;
using System.Windows ;
using System.Windows.Input ;
using Autofac.Features.OwnedInstances ;
using NLog ;
using WpfApp.Core.Interfaces ;

namespace Common.Menus
{
    /// <summary></summary>
    /// <seealso cref="IMenuItem" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for XMenuItem
    public class XMenuItem : IMenuItem
    {
        /// <summary>Initializes a new instance of the <see cref="XMenuItem" /> class.</summary>
        /// <param name="func">The function.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public XMenuItem ( Owned < Func < Type , ILogger > > func )
        {
            Logger = func.Value ( typeof ( XMenuItem ) ) ;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        private ILogger Logger { get ; }


        /// <summary>Gets or sets the header.</summary>
        /// <value>The header.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Header
        public string Header { get ; set ; }

        /// <summary>Gets or sets the children.</summary>
        /// <value>The children.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Children
        public IEnumerable < IMenuItem > Children { get ; set ; }

        /// <summary>Gets or sets the command.</summary>
        /// <value>The command.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Command
        public ICommand Command { get ; set ; }

        /// <summary>Gets or sets the command parameter.</summary>
        /// <value>The command parameter.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for CommandParameter
        public object CommandParameter { get ; set ; }

        /// <summary>The object that the command is being executed on.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for CommandTarget
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public IInputElement CommandTarget { get ; }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ToString
        public override string ToString ( )
        {
            return
                $"{nameof ( Header )}: {Header}, {nameof ( Children )}: {Children}, {nameof ( Command )}: {Command}, {nameof ( CommandParameter )}: {CommandParameter}, {nameof ( CommandTarget )}: {CommandTarget}" ;
        }
    }
}