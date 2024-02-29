using Asp.Versioning;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Disease.Commands;
using Service.Disease.DTOs;
using Service.Disease.Queries;
using SharedDomain.Defaults;
using Pagination = Service.Disease.DTOs.Pagination;
using PaginationDefault = SharedDomain.Defaults.Pagination;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Disease.Controllers
{
    [Route("api/v{version:apiVersion}/disease/disease-info/")]
    [Authorize(Roles = Roles.SuperAdmin)]
    [ApiVersion("1.0")]
    [ApiController]
    public class DiseaseInfosController : ControllerBase
    {
        private IMediator _mediator;

        public DiseaseInfosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<DiseaseInfosController>
        //[Authorize(Roles = Roles.SuperAdmin)]
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] DiseaseInfoRequest request)
        {
            DiseaseInfoResponse response = new DiseaseInfoResponse();
            // Get all data
            var rsAll = await _mediator.Send(new GetDiseaseInfoQuery
            {
                keyword = request.keyword,
                searchDateFrom = request.searchDateFrom,
                searchDateTo = request.searchDateTo
            });
            // Search, pagination
            if (rsAll.Count() > 0)
            {
                if (request.perPage == 0)
                {
                    request.perPage = (int)PaginationDefault.perPage;
                }
                if (request.pageId == 0)
                {
                    request.pageId = (int)PaginationDefault.pageId;
                }
                var rsSearch = await _mediator.Send(new GetDiseaseInfoQuery
                {
                    keyword = request.keyword,
                    searchDateFrom = request.searchDateFrom,
                    searchDateTo = request.searchDateTo,
                    perPage = request.perPage,
                    pageId = request.pageId,
                    getAllDataFlag = false
                });
                response.data = rsSearch;
                response.Pagination = new Pagination
                {
                    totalRecord = rsAll.Count(),
                    perPage = request.perPage,
                    pageId = request.pageId
                };
                response.statusCode = Ok().StatusCode;
            }
            if (response.Pagination.totalRecord == 0)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string> {
                    "Data not found!"
                };
            }
            return Ok(response);
        }

        // GET: api/<DiseaseInfosController>
        //[Authorize(Roles = Roles.SuperAdmin)]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            // Get by id
            DiseaseInfoDetailResponse response = new DiseaseInfoDetailResponse();
            var rs = await _mediator.Send(new GetDiseaseInfoByIdQuery
            {
                DiseaseId = id
            });
            if (rs != null)
            {
                response.data = rs;
                response.statusCode = Ok().StatusCode;
            }
            else
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string> {
                    "Data not found!"
                };
            }
            return Ok(response);
        }


        [HttpPost("add")]
        public async Task<IActionResult> InsertDisease([FromBody] DiseaseInfoInsertRequest request)
        {
            DiseaseInfoInsertResponse response = new DiseaseInfoInsertResponse();
            try
            {
                var rs = await _mediator.Send(new CreateDiseaseInfoCommand
                {
                    DiseaseName = request.DiseaseName!,
                    Symptoms = request.Symptoms!,
                    Cause = request.Cause!,
                    PreventiveMeasures = request.PreventiveMeasures!,
                    Suggest = request.Suggest!,
                    CreateBy = request.CreateBy
                });
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Insert fail!"
                    };
                }
                else
                {
                    response.statusCode = Ok().StatusCode;
                }
            }
            catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Insert fail!"
                };
            }
            return Ok(response);
        }

        [HttpPut("edit-disease")]
        public async Task<IActionResult> UpdateDisease([FromBody] DiseaseInfoUpdateRequest request)
        {
            DiseaseInfoUpdateResponse response = new DiseaseInfoUpdateResponse();
            try
            {
                var rs = await _mediator.Send(new UpdateDiseaseInfoCommand
                {
                    Id = request.Id,
                    DiseaseName = request.DiseaseName!,
                    Symptoms = request.Symptoms!,
                    Cause = request.Cause!,
                    PreventiveMeasures = request.PreventiveMeasures!,
                    Suggest = request.Suggest!,
                    UpdateBy = request.UpdateBy
                }) ;
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Invalid id!"
                    };
                }
                else
                {
                    response.statusCode = Ok().StatusCode;
                }
            }
            catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Update fail!"
                };
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDisease([FromQuery] Guid id)
        {

            DiseaseInfoDeleteResponse response = new DiseaseInfoDeleteResponse();
            try
            {
                var rs = await _mediator.Send(new DeleteDiseaseInfoCommand
                {
                    Id = id,
                });
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Invalid id!"
                    };
                }
                else
                {
                    response.statusCode = Ok().StatusCode;
                }
            }
            catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Delete fail!"
                };
            }
            return Ok(response);
        }
    }
}
