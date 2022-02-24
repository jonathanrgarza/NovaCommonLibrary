using System;
using System.IO;
using System.Reflection;
using Ncl.Common.Core.Preferences;
using Ncl.Common.Core.Utilities;
using Ncl.Common.Core.Xml;

namespace Ncl.Common.Wpf.Preferences
{
    /// <summary>
    ///     A preference service with Windows specific paths.
    /// </summary>
    public class WindowsPreferenceService : PreferenceServiceBase
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="WindowsPreferenceService" />.
        /// </summary>
        /// <param name="xmlSerializationService">A XML serialization service.</param>
        /// <exception cref="InvalidOperationException">An assembly name could not be found.</exception>
        public WindowsPreferenceService(IXmlSerializationService xmlSerializationService)
        {
            XmlSerializationService = xmlSerializationService;

            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string programName = Assembly.GetExecutingAssembly().FullName ??
                                 throw new InvalidOperationException("No assembly name");
            DefaultDirectory = Path.Combine(localAppData, programName);
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="WindowsPreferenceService" />.
        /// </summary>
        /// <param name="xmlSerializationService">A XML serialization service.</param>
        /// <param name="defaultDirectory">The default directory path.</param>
        /// <exception cref="InvalidOperationException">An assembly name could not be found.</exception>
        public WindowsPreferenceService(IXmlSerializationService xmlSerializationService, string defaultDirectory)
        {
            Guard.AgainstNullArgument(defaultDirectory, nameof(defaultDirectory));

            XmlSerializationService = xmlSerializationService;
            DefaultDirectory = defaultDirectory;
        }

        /// <inheritdoc />
        public override string DefaultDirectory { get; }

        /// <inheritdoc />
        public override string? FallbackDirectory => string.Empty; //Current working directory.

        /// <inheritdoc />
        protected override IXmlSerializationService XmlSerializationService { get; }
    }
}