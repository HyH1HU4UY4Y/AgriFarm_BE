﻿using Asp.Versioning;
using Infrastructure.ChecklistGlobalGAP.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ChecklistGlobalGAP.Commnad;
using Service.ChecklistGlobalGAP.DTOs;
using Service.ChecklistGlobalGAP.Queries;
using SharedDomain.Entities.ChecklistGlobalGAP;
using PaginationDefault = SharedDomain.Defaults.Pagination;
using Pagination = Service.ChecklistGlobalGAP.DTOs.Pagination;
using SharedDomain.Defaults;

namespace Service.ChecklistGlobalGAP.Controllers
{
    [Route("api/v{version:apiVersion}/checklist-global-GAP")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class ChecklistGlobalGAPController : ControllerBase
    {
        private IMediator _mediator;
        private readonly ChecklistGlobalGAPContext _context;
        public ChecklistGlobalGAPController(IMediator mediator, ChecklistGlobalGAPContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("get-list")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetList([FromQuery] ChecklistGlobalGAPGetListRequest request)
        {
            ChecklistGlobalGAPGetListResponse response = new ChecklistGlobalGAPGetListResponse();
            var rsAll = await _mediator.Send(new GetListChecklistGlobalGAPQuery
            {
                userId = request.userId
            });
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
                var rsSearch = await _mediator.Send(new GetListChecklistGlobalGAPQuery
                {
                    userId = request.userId,
                    pageId = request.pageId,
                    perPage = request.perPage,
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
        [HttpPost("add-list")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddList([FromBody] ChecklistGlobalGAPAddListRequest request)
        {
            ChecklistGlobalGAPAddListResponse response = new ChecklistGlobalGAPAddListResponse();
            try
            {
                var rs = await _mediator.Send(new CreateListChecklistGlobalGAPCommand()
                {
                    userId = request.userId,
                    checklistMasterId = request.checklistMasterId
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
                    response.data = rs;
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
        [HttpPost("create-checklist")]
        [Authorize(Roles = Roles.SuperAdmin)]
        public async Task<IActionResult> CreateCheckList([FromBody] ChecklistGlobalGAPCreateRequest request)
        {
            ChecklistGlobalGAPCreateResponse response = new ChecklistGlobalGAPCreateResponse();
            try
            {
                // ChecklistMaster
                var checklistMaster = new ChecklistMaster
                {
                    Name = request.name,
                    isDraft = request.isDraft
                };
                var checklistItems = new List<ChecklistItem>();
                // ChecklistItem
                if (request.checklistItems!.Count() > 0)
                {
                    foreach (var item in request.checklistItems!)
                    {
                        checklistItems.Add(new ChecklistItem
                        {
                            OrderNo = item.orderNo,
                            AfNum = item.afNum,
                            Title = item.title,
                            LevelRoute = item.levelRoute,
                            Content = item.content,
                            IsResponse = item.isResponse,
                            ChecklistMaster = checklistMaster
                        });
                    }
                }
                
                var rs = await _mediator.Send(new CreateChecklistGlobalGAPCommand
                {
                    checklistMaster = checklistMaster,
                    checklistItems = checklistItems
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
                    response.data = rs;
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
        [HttpPost("add-item-response")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddItemResponse([FromBody] ChecklistGlobalGAPCreateResponseRequest request)
        {
            ChecklistGlobalGAPAddItemResponse response = new ChecklistGlobalGAPAddItemResponse();
            try
            {
                var rs = await _mediator.Send(new CreateResponseChecklistGlobalGAPCommand
                {
                    checklistItemResponses = request
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
            } catch
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Insert fail!"
                };
            }
            return Ok(response);
        }
        [HttpDelete("delete-list")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteList([FromQuery] Guid id)
        {
            ChecklistGlobalGAPDeleteListResponse response = new ChecklistGlobalGAPDeleteListResponse();
            try
            {
                var rs = await _mediator.Send(new DeleteListChecklistGlobalGAPCommand
                {
                    checklistMappingId = id
                });
                if (!rs)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Delete fail!"
                    };
                } else
                {
                    response.statusCode = Ok().StatusCode;
                }
            } catch
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Delete fail!"
                };
            }
            return Ok(response);
        }
        [HttpGet("get-checklist")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetChecklist([FromQuery] Guid id)
        {
            ChecklistGlobalGAPGetChecklistResponse response = new ChecklistGlobalGAPGetChecklistResponse();
            var rs = await _mediator.Send(new GetContentChecklistGlobalGAPQuery
            {
                checklistMappingId = id
            });
            if (rs == null)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string> {
                    "Data not found!"
                };
            } else
            {
                response.statusCode = Ok().StatusCode;
                response.data = rs;
            }
            return Ok(response);
        }
    }
}
