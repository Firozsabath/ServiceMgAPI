﻿using ServiceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Interfaces
{
    public interface IDropdowns
    {
        IEnumerable<RequestTypes> RequestTypes();
        IEnumerable<ServiceStatuses> ServiceStatuses();
        IEnumerable<PriorityLevels> Priorities();
        IEnumerable<ContractTypes> ContractTypes();

    }
}
