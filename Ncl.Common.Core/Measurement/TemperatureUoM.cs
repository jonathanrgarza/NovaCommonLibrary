using System.ComponentModel;
using Ncl.Common.Core.Infrastructure;

namespace Ncl.Common.Core.Measurement
{
    /// <summary>
    ///     The distance Unit of Measure.
    /// </summary>
    public enum TemperatureUoM
    {
        [Description("Kelvin")]
        [Abbreviation("K")]
        Kelvin = 0, //SI unit / Default

        [Description("Celsius")]
        [Abbreviation("°C")]
        Celsius,

        //U.S. customary unit
        [Description("Fahrenheit")]
        [Abbreviation("°F")]
        Fahrenheit
    }
}