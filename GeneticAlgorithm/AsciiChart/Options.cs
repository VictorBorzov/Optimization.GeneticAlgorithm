using System;

namespace Cli.AsciiChart
{
    public class Options
    {
        private int _axisLabelLeftMargin = 1;
        private int _axisLabelRightMargin = 1;


        /// <summary>
        /// The margin between the axis label and the left of the output.
        /// </summary>
        public int AxisLabelLeftMargin
        {
            get => _axisLabelLeftMargin;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Margin must be >= 0");
                _axisLabelLeftMargin = value;
            }
        }

        /// <summary>
        /// The margin between the axis label and the axis.
        /// </summary>
        public int AxisLabelRightMargin
        {
            get => _axisLabelRightMargin;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Margin must be >= 0");
                _axisLabelRightMargin = value;
            }
        }

        /// <summary>
        /// Roughly the number of lines to scale the output to.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// The background fill.
        /// </summary>
        public char Fill { get; set; } = ' ';

        /// <summary>
        /// The axis label format.
        /// </summary>
        public string AxisLabelFormat { get; set; } = "0.00";
    }
}