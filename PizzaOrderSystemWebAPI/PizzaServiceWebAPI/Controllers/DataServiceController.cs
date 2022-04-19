using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderSystemWebAPI.DataModel;

namespace PizzaOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class DataServiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestApi()
        {
            return Ok("Test Done..!");
        }

        [HttpGet]
        public IActionResult GetPizzaListBy(int Id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    return Unauthorized("Unable to find Token indetities");
                }
                return Ok(ApiDataModel.Instance.GetPizzaListBy(Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error -> " + ex);
            }
        }

        [HttpGet]
        public IActionResult GetPizzaIngredientsListData()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    return Unauthorized("Unable to find Token indetities");
                }
                return Ok(ApiDataModel.Instance.GetPizzaIngredientsListData());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error -> " + ex);
            }
        }
    }
}
