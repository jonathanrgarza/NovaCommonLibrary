using System.ComponentModel;

namespace Ncl.Common.Csv
{
    /// <summary>
    ///     Determines the integrity mode for <see cref="CsvStreamWriter"/>.
    /// </summary>
    public enum IntegrityMode
    {
        /// <summary>
        ///     No integrity checks
        /// </summary>
        [Description("No integrity checks")]
        None,
        /// <summary>
        ///     Integrity checks are enabled and integrity is maintained by silently adding empty fields when possible
        /// </summary>
        [Description("Integrity checks are enabled and integrity is maintained by silently adding empty fields when possible")]
        Loose,
        /// <summary>
        ///     Integrity checks are enabled and integrity is maintained by throwing exceptions when integrity is violated
        /// </summary>
        [Description("Integrity checks are enabled and integrity is maintained by throwing exceptions when integrity is violated")]
        Strict
    }
}