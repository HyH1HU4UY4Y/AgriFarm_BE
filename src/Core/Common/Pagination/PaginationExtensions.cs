using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharedDomain.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Pagination
{
    public static class PaginationExtensions
    {
        public static HttpResponse AddPaginationHeader(this HttpResponse response, PaginationResponseMetadata metadata)
        {
            response.Headers.Add(AdditionHeader.Pagination, JsonConvert.SerializeObject(metadata));

            return response;
        }
    }
}
