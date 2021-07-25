using System.ComponentModel;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Measurement
{
    /// <summary>
    ///     The distance Unit of Measure.
    /// </summary>
    public enum DistanceUoM
    {
        [Description("Meters")]
        [Abbreviation("m")]
        Meter = 0, //SI unit / Default

        [Description("Millimeters")]
        [Abbreviation("mm")]
        Millimeter,
        [Description("Centimeters")]
        [Abbreviation("cm")]
        Centimeter,
        [Description("Kilometers")]
        [Abbreviation("km")]
        Kilometer,

        //U.S. customary units
        [Description("Inches")]
        [Abbreviation("in")]
        Inch,
        [Description("Feet")]
        [Abbreviation("ft")]
        Foot,
        [Description("Yards")]
        [Abbreviation("yd")]
        Yard,
        [Description("Miles")]
        [Abbreviation("mi")]
        Mile
    }
}
