﻿using CodeRank.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}