using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Disease.Commands;
using Service.RiskAssessment.Commands;
using Service.RiskAssessment.DTOs;
using Service.RiskAssessment.Queries;
using Pagination = Service.RiskAssessment.DTOs.Pagination;
using PaginationDefault = SharedDomain.Defaults.Pagination;

namespace Service.RiskAssessment.Controllers
{
    [Route("api/risk-assessment/")]
    [ApiController]
    public class RiskAssessmentsController : ControllerBase
    {
        private IMediator _mediator;

        public RiskAssessmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<DiseaseInfosController>
        //[Authorize(Roles = Roles.Admin)]
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] RiskMasterRequest request)
        {
            RiskMasterResponse response = new RiskMasterResponse();
            // Get all data
            var rsAll = await _mediator.Send(new GetRiskMasterQuery
            {
                keyword = request.keyword,
                isDraft = request.isDraft,
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
                var rsSearch = await _mediator.Send(new GetRiskMasterQuery
                {
                    keyword = request.keyword,
                    isDraft = request.isDraft,
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

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            // Get by id
            RiskDetailResponse response = new RiskDetailResponse();
            var rs = await _mediator.Send(new GetRiskByIdQuery
            {
                RiskMasterId = id
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
        public async Task<IActionResult> InsertRiskAssessment([FromBody] RiskAssessmentInsertRequest request)
        {
            RiskAssessmentInsertResponse response = new RiskAssessmentInsertResponse();
            try
            {
                RiskMasterDTO riskMaster = new RiskMasterDTO()
                {
                    RiskName = request.RiskName,
                    RiskDescription = request.RiskDescription,
                    CreateBy = request.CreateBy,
                    RiskItems = request.RiskItems,
                };

                var rs = await _mediator.Send(new CreateRiskMasterCommand
                {
                    riskMaster = riskMaster,
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

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateRiskAssessment([FromBody] RiskAssessmentUpdateRequest request)
        {
            RiskAssessmentUpdateResponse response = new RiskAssessmentUpdateResponse();
            try
            {
                RiskMasterDTO riskMaster = new RiskMasterDTO()
                {
                    Id = request.Id,
                    RiskName = request.RiskName,
                    RiskDescription = request.RiskDescription,
                    IsDraft = request.IsDraft,
                    UpdateBy = request.UpdateBy,
                    RiskItems = request.RiskItems,
                };

                var rs = await _mediator.Send(new UpdateRiskMasterCommand
                {
                    riskMaster = riskMaster,
                });

                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Update fail!"
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
        public async Task<IActionResult> DeleteRiskAssessment([FromQuery] Guid id)
        {

            RiskAssessmentDeleteResponse response = new RiskAssessmentDeleteResponse();
            try
            {
                var rs = await _mediator.Send(new DeleteRiskAssessmentCommand
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
