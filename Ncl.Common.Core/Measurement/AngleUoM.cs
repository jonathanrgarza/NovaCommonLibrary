using System.ComponentModel;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Measurement
{
    /// <summary>
    ///     The angle Unit of Measure.
    /// </summary>
    public enum AngleUoM
    {
        [Description("Radian")]
        [Abbreviation("rad")]
        Radian = 0, //SI unit / Default

        [Description("Degree")]
        [Abbreviation("°")]
        Degree,

        [Description("Revolution")]
        [Abbreviation("rev")]
        Revolution, //A.K.A a Turn
    }
}
