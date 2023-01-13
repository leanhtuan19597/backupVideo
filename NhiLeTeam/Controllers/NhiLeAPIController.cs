using Microsoft.AspNetCore.Mvc;
using NhiLeTeam.Data;
using NhiLeTeam.Models;
using NhiLeTeam.Models.Dto;

namespace NhiLeTeam.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/NhiLeAPI")]
    [ApiController]
    public class NhiLeAPIController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult <IEnumerable<NhiLeDTO>> GetNhiLes()
        {
            return Ok(NhiLeStore.nhileList);
        }

        [HttpGet("{id:int}", Name ="GetNhile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NhiLeDTO> GetNhiLes(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var nhile = NhiLeStore.nhileList.FirstOrDefault(x => x.Id == id);
            if (nhile == null) 
            {
                return NotFound();
            }

            return Ok(nhile);

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NhiLeDTO> CreateNhiLe([FromBody] NhiLeDTO nhileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (nhileDTO == null)
            {
                return BadRequest(nhileDTO);
            }
            if (nhileDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            nhileDTO.Id = NhiLeStore.nhileList.OrderByDescending(u => u.Id).FirstOrDefault().Id +1;
            NhiLeStore.nhileList.Add(nhileDTO);

            return CreatedAtRoute("GetNhile", new {id = nhileDTO.Id}, nhileDTO);

        }

        private static IWebHostEnvironment _webHostEnvironmen;

        
        public NhiLeAPIController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironmen= webHostEnvironment;
        }

        [HttpPost]
        [Route("UploadNotLimit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [DisableRequestSizeLimit()]
        public async Task<string> UploadNotLimit([FromForm] UserDTO obj)
        {
            if (obj.files.Length > 0) 
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironmen.WebRootPath + "\\Video\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironmen.WebRootPath + "\\Video\\");

                    }
                    using(FileStream filestream = System.IO.File.Create(_webHostEnvironmen.WebRootPath+ "\\Video\\" + obj.files.FileName))
                    {
                        obj.files.CopyTo(filestream);
                        filestream.Flush();
                        return "\\Video\\" + obj.files.FileName;
                    }

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Upload Failed";
            }
        }

        

        [HttpPost]
        [Route("Upload100MB")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [RequestSizeLimit(100000000)]
        public async Task<string> Upload100MB([FromForm] UserDTO obj)
        {
            if (obj.files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironmen.WebRootPath + "\\Video\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironmen.WebRootPath + "\\Video\\");

                    }
                    using (FileStream filestream = System.IO.File.Create(_webHostEnvironmen.WebRootPath + "\\Video\\" + obj.files.FileName))
                    {
                        obj.files.CopyTo(filestream);
                        filestream.Flush();
                        return "\\Video\\" + obj.files.FileName;
                    }

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Upload Failed";
            }
        }

        [HttpGet]
        [Route("PlayVideo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> PlayVideo()
        {
            try
            {
                var fileName = "video1764801458.mp4";
                string path = Path.Combine(_webHostEnvironmen.WebRootPath, "Video/") + fileName;  // the video file is in the wwwroot/files folder
                var filestream = System.IO.File.OpenRead(path);
                return Results.File(filestream, contentType: "video/mp4", fileDownloadName: fileName, enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Something Went Wrong in the {nameof(PlayVideo)}");
                return Results.BadRequest();
            }
        }

        
    }
}

