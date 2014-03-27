using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Mapper
{
    public interface IMapperHost
    {
        FrameworkElement SourceSchemaControl { get; }
        FrameworkElement TargetSchemaControl { get; }
    }
}
