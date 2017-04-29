using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.Utilities
{
    /// <summary>
    /// Declates extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Short hand for String.Format(str, args).
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatS(this string str, params object[] args)
        {
            return String.Format(str, args);
        }
    }
}
