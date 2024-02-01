﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.RiskAssessment.DTOs;
using Service.RiskAssessment.Queries;
using Pagination = Service.RiskAssessment.DTOs.Pagination;
using PaginationDefault = SharedDomain.Defaults.Pagination;

namespace Service.RiskAssessment.Controllers
{
    [Route("api/assessment/risk/")]
    [ApiController]
    public class RiskMastersController : ControllerBase
    {
        private IMediator _mediator;

        public RiskMastersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<DiseaseInfosController>
        //[Authorize(Roles = Roles.SuperAdmin)]
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
    }
}
