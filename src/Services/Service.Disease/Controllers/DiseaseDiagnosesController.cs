using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Disease.Commands;
using Service.Disease.DTOs;
using Service.Disease.Queries;
using System.Data;
using PaginationDefault = SharedDomain.Defaults.Pagination;
using Pagination = Service.Disease.DTOs.Pagination;
using Microsoft.AspNetCore.Authorization;
using SharedDomain.Defaults;
using Asp.Versioning;

namespace Service.Disease.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/disease/disease-diagnoses/")]
    [ApiVersion("1.0")]
    [Authorize]
    public class DiseaseDiagnosesController : ControllerBase
    {
        private IMediator _mediator;

        public DiseaseDiagnosesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get")]
        [Authorize(Roles = Roles.SuperAdmin)]
        public async Task<IActionResult> Get([FromQuery] DiseaseDiagnosesRequest request)
        {
            DiseaseDiagnosesResponse response = new DiseaseDiagnosesResponse();
            // Get all data
            var rsAll = await _mediator.Send(new GetDiseaseDiagnosesQuery
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
                var rsSearch = await _mediator.Send(new GetDiseaseDiagnosesQuery
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
        [HttpGet("get-by-id")]
        [Authorize(Roles = Roles.SuperAdmin)]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            // Get by id
            DiseaseDiagnosesDetailResponse response = new DiseaseDiagnosesDetailResponse();
            var rs = await _mediator.Send(new GetDiseaseDiagnosesByIdQuery
            {
                DiseaseDiagnosesId = id
            });
            if (rs != null)
            {
                response.data = rs;
                response.statusCode = Ok().StatusCode;
            } else
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string> {
                    "Data not found!"
                };
            }
            return Ok(response);
        }
        [HttpPut("edit-status-feedback")]
        [Authorize(Roles = Roles.SuperAdmin)]
        public async Task<IActionResult> UpdateStatusFeedBack([FromBody] DiseaseDiagnosesUpdateRequest request)
        {
            DiseaseDiagnosesUpdateResponse response = new DiseaseDiagnosesUpdateResponse();
            try
            {
                var rs = await _mediator.Send(new UpdateDiseaseDiagnosesCommand
                {
                    Id = request.Id,
                    FeedbackStatus = request.FeedbackStatus
                });
                if (rs == null)
                {
                    response.statusCode = NoContent().StatusCode;
                    response.message = new List<string>
                    {
                        "Invalid id!"
                    };
                } else
                {
                    response.statusCode = Ok().StatusCode;
                }
            } catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Update fail!"
                };
            }
            return Ok(response);
        }
        [HttpPost("add")]
        public async Task<IActionResult> InsertStatusFeedBack([FromBody] DiseaseDiagnosesInsertRequest request)
        {
            DiseaseDiagnosesInsertResponse response = new DiseaseDiagnosesInsertResponse();
            try
            {
                var rs = await _mediator.Send(new CreateDiseaseDiagnosesCommand
                {
                    PlantDiseaseId = request.PlantDiseaseId,
                    Description = request.Description,
                    Feedback = request.Feedback,
                    Location = request.Location,
                    CreateBy = request.CreateBy,
                    LandId = request.LandId
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
            } catch (Exception)
            {
                response.statusCode = NoContent().StatusCode;
                response.message = new List<string>
                {
                    "Insert fail!"
                };
            }
            return Ok(response);
        }
        [HttpGet("download")]
        [Authorize(Roles = Roles.SuperAdmin)]
        public async Task<IActionResult> DownloadExcelFile([FromQuery] DiseaseDiagnosesRequest request)
        {
            DiseaseDiagnosesExportResponse response = new DiseaseDiagnosesExportResponse();
            var rsAll = await GetData(request);
            string base64String;
            using (var wb = new XLWorkbook())
            {
                var sheet = wb.AddWorksheet(rsAll, "Employee Records");

                // Apply font color to columns 1 to 5
                sheet.Columns(1, 5).Style.Font.FontColor = XLColor.Black;

                using (var ms = new MemoryStream())
                {
                    wb.SaveAs(ms);

                    // Convert the Excel workbook to a base64-encoded string
                    base64String = Convert.ToBase64String(ms.ToArray());
                }
            }
            response.statusCode = Ok().StatusCode;
            response.data = base64String;
            return Ok(response);
        }
        [NonAction]
        private async Task<DataTable> GetData(DiseaseDiagnosesRequest request)
        {
            DataTable data = new DataTable();
            data.TableName = "Employee List";
            data.Columns.Add("#", typeof(string));
            data.Columns.Add("Predict result", typeof(string));
            data.Columns.Add("Description", typeof(string));
            data.Columns.Add("Feedback", typeof(string));
            data.Columns.Add("Date", typeof(string));
            var diseaseData = await _mediator.Send(new GetDiseaseDiagnosesQuery
            {
                keyword = request.keyword,
                searchDateFrom = request.searchDateFrom,
                searchDateTo = request.searchDateTo
            });
            if (diseaseData.Count > 0)
            {
                int no = 1;
                diseaseData.ForEach(x =>
                {
                    data.Rows.Add(no, x.PlantDisease!.DiseaseName, x.Description, x.Feedback, x.CreatedDate);
                    no++;
                });
            }
            return data;

        }

        [HttpPut("update-feedback-content")]
        public async Task<IActionResult> UpdateFeedBack([FromBody] FeedbackUpdateRequest request)
        {
            FeedbackUpdateResponse response = new FeedbackUpdateResponse();
            try
            {
                var rs = await _mediator.Send(new UpdateFeedbackCommand
                {
                    Id = request.Id,
                    Feedback = request.Feedback
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
                    "Update fail!"
                };
            }
            return Ok(response);
        }
    }
}
