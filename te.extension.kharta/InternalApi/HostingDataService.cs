﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kharta.coria.graphica.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;

using System.Reflection;

[assembly: InternalsVisibleTo("kharta.coria.graphica.test")]
namespace te.extension.kharta.InternalApi
{
    internal class HostingDataService
    {
        internal static KhartaHosting GetHostingApplication(Guid applicationId)
        {
            throw new NotImplementedException();
        }
    }
}
