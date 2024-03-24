using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Service.Disease.Commands;
using Service.RiskAssessment.Commands;
using Service.RiskAssessment.DTOs;
using Service.RiskAssessment.Queries;
using SharedDomain.Common;
using SharedDomain.Defaults;
using SharedDomain.Entities.Risk;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using Pagination = Service.RiskAssessment.DTOs.Pagination;
using PaginationDefault = SharedDomain.Defaults.Pagination;

namespace Service.RiskAssessment.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/risk-assessment/")]
    [ApiVersion("1.0")]
    [Authorize]
    public class RiskAssessmentsController : ControllerBase
    {
        private IMediator _mediator;

        public RiskAssessmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-list-master")]
        [Authorize(Roles = Roles.SuperAdmin)]
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

        [HttpGet("detail")]
        [Authorize]
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
                var riskMaster = new RiskMaster
                {
                    RiskName = request.riskName,
                    RiskDescription = request.riskDescription,
                    CreateBy = request.createBy,
                    IsDraft = request.isDraft
                };
                var riskItems = new List<RiskItem>();
                // Risk Item
                if (request.riskItems!.Count() > 0)
                {
                    foreach (var item in request.riskItems!)
                    {
                        riskItems.Add(new RiskItem
                        {
                            RiskItemTitle = item.riskItemTile,
                            RiskItemContent = item.riskItemContent,
                            RiskItemDiv = item.riskItemDiv,
                            RiskItemType = item.riskItemType,
                            Must = item.must,
                            RiskMaster = riskMaster
                        });
                    }
                }
                var rs = await _mediator.Send(new CreateRiskMasterCommand
                {
                    riskMaster = riskMaster,
                    riskItems = riskItems
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
        public async Task<IActionResult> UpdateRiskAssessment([FromQuery] Guid id, [FromBody] RiskAssessmentUpdateRequest request)
        {
            RiskAssessmentUpdateResponse response = new RiskAssessmentUpdateResponse();
            try
            {
                if (request.riskItems == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Update fail!"
                    };
                } else
                {
                    var rsDel = await _mediator.Send(new DeleteRiskAssessmentCommand
                    {
                        Id = id,
                    });
                    if (rsDel == null)
                    {
                        response.statusCode = NoContent().StatusCode;
                        response.message = new List<string>
                        {
                            "Update fail!"
                        };
                    } else
                    {
                        var riskMaster = new RiskMaster
                        {
                            Id = id,
                            RiskName = request.riskName,
                            RiskDescription = request.riskDescription
                        };

                        var riskItems = new List<RiskItem>();
                        // Risk Item
                        if (request.riskItems!.Count() > 0)
                        {
                            foreach (var item in request.riskItems!)
                            {
                                if (item.itemId == Guid.Empty)
                                {
                                    riskItems.Add(new RiskItem
                                    {
                                        RiskMasterId = id,
                                        RiskItemTitle = item.riskItemTile,
                                        RiskItemContent = item.riskItemContent,
                                        RiskItemDiv = item.riskItemDiv,
                                        RiskItemType = item.riskItemType,
                                        RiskMaster = riskMaster
                                    });
                                }
                                else
                                {
                                    riskItems.Add(new RiskItem
                                    {
                                        Id = item.itemId,
                                        RiskMasterId = id,
                                        RiskItemTitle = item.riskItemTile,
                                        RiskItemContent = item.riskItemContent,
                                        RiskItemDiv = item.riskItemDiv,
                                        RiskItemType = item.riskItemType,
                                        RiskMaster = riskMaster
                                    });
                                }
                            }
                        }
                        var rs = await _mediator.Send(new UpdateRiskMasterCommand
                        {
                            riskMasterId = id,
                            riskMaster = riskMaster,
                            riskItems = riskItems
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
        [HttpPost("implement")]
        public async Task<IActionResult> ImplementAssessment([FromBody] RiskAssessmentImplRequset request)
        {
            RiskAssessmentImplResponse response = new RiskAssessmentImplResponse();
            try
            {
                if (request.riskAssessmentImpl != null)
                {
                    List<RiskItemContent> riskItemContents = new List<RiskItemContent>();
                    foreach (var item in request.riskAssessmentImpl)
                    {
                        riskItemContents.Add(new RiskItemContent
                        {
                            RiskItemId = item.riskItemId,
                            RiskMappingId = item.riskMappingId,
                            Anwser = item.answer
                        });
                    }
                    var rs = await _mediator.Send(new CreateRiskContentCommand
                    {
                        riskItemContents = riskItemContents
                    });
                    if (rs)
                    {
                        response.statusCode = Ok().StatusCode;
                    } else
                    {
                        response.statusCode = NoContent().StatusCode;
                        response.message = new List<string>
                        {
                            "Insert fail!"
                        };
                    }
                } else
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Insert fail!"
                    };
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
        [HttpPost("create-risk-mapping")]
        public async Task<IActionResult> CreateRiskMapping([FromBody] RiskAssessmentMappingRequest request)
        {
            RiskAssessmentMappingResponse response = new RiskAssessmentMappingResponse();
            try
            {
                var rs = await _mediator.Send(new CreateRiskMappingCommand
                {
                    riskMasterId = request.riskMasterId,
                    taskId = request.taskId
                });
                if (rs)
                {
                    response.statusCode = Ok().StatusCode;
                } else
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Insert fail!"
                    };
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
        [HttpGet("get-list-mapping")]
        public async Task<IActionResult> GetListMapping([FromQuery] RiskAssessmentListMappingRequest request)
        {
            RiskAssessmentListMappingResponse response = new RiskAssessmentListMappingResponse();
            // Get all data
            var rsAll = await _mediator.Send(new GetListRiskMappingQuery
            {
                taskId = request.taskId
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
                var rsSearch = await _mediator.Send(new GetListRiskMappingQuery
                {
                    taskId = request.taskId,
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
        [HttpGet("get-by-task")]
        public async Task<IActionResult> GetByActivity([FromQuery][Required]Guid taskId)
        {
            var rs = await _mediator.Send(new GetRiskMappingDetailQuery { 
                taskId = taskId 
            });

            return Ok(new DefaultResponse<RiskMappingResponse>
            {
                Data = rs,
                Status = Ok().StatusCode,
            });
        }
        [HttpGet("check-status")]
        public async Task<IActionResult> CheckStatus([FromQuery] RiskAssessmentCheckStatusRequest request)
        {
            RiskAssessmentCheckStatus response = new RiskAssessmentCheckStatus();

            int rsItemCount = await _mediator.Send(new GetRiskItemQuery
            {
                riskMasterId = request.riskMasterId
            });


            int rsItemContetnCount = await _mediator.Send(new GetRiskItemContentQuery
            {
                riskMappingId = request.riskMappingId
            });

            if (rsItemCount != rsItemContetnCount)
            {
                response.status = 0;
            } else
            {
                response.status = 1;
            }
            response.statusCode = Ok().StatusCode;
            return Ok(response);
        }
    }
}
