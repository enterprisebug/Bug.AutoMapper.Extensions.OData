using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.AspNet.OData;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using OdataAutomapperServerQuery.Data;
using OdataAutomapperServerQuery.ReadModel;

namespace OdataAutomapperServerQuery.Controllers.V1
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("StandardProjectReport")]
    public class StandardProjectReportController : ODataController
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public StandardProjectReportController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ODataRoute]
        [EnableQuery(PageSize = 2)]
        public IQueryable<StandardProjectReportReadModel> Get(ODataQueryOptions<StandardProjectReportReadModel> odataQuery)
        {
            // from my point of view the "PageSize" mentioned in the EnableQuery Attribute is not reflected.
            // in this example 4 entities are loaded into memory and ony 10 are returned back to the client.
            // I was expecting the filtering takes place on the server
            var data = _context.ProjectReport.Get(_mapper, odataQuery); // returns 4 entities

            // My expectation is to have only 2 entries after _context.ProjectReport.Get(_mapper, odataQuery);
            return _mapper.Map<IList<StandardProjectReportReadModel>>(data).AsQueryable(); // return 2 to the client
        }
    }
}
