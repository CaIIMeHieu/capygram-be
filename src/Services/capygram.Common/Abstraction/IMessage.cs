﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
namespace capygram.Common.Abstraction
{
    [ExcludeFromTopology]    
    
    public interface IMessage : IRequest
    {
    }
}
