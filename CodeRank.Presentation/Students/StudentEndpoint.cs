using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Presentation.Students;

 

public static class StudentEndpoint
{
public static void MapEndpoints(IEndpointRouteBuilder app)
{
   // CreateStudent.MapEndpoint(app);
    GetStudent.MapEndpoint(app); 
}
}
