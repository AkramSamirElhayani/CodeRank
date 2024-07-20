using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Presentation.Instructors;



public static class InstructorEndpoint
{
    public static void MapEndpoints(IEndpointRouteBuilder app  )
    {

      //  CreateInstructor.MapEndpoint(app);
        GetInstructor.MapEndpoint(app);
    }
}
