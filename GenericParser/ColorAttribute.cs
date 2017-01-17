using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericParser
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class ColorAttribute : Attribute
    {
        readonly Color color;

        // This is a positional argument
        public ColorAttribute(int argbColor)
        {
            this.color = Color.FromArgb(argbColor);
        }

        public Color ColorValue
        {
            get
            {
                return this.color;
            }
        }
    }
}
