using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace ProvaPratica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterProductUseCase useCase, [FromBody] RequestProductJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateProductUseCase useCase, [FromRoute] BigInteger id, [FromBody] RequestProductJson request)
        {
            var response = await useCase.Execute(id, request);
            return NoContent(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
        [FromServices] IDeleteProductUseCase useCase,
        [FromRoute]int id)
        {
            await useCase.Execute(id);

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ResponseProductsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllRewards([FromServices] IGetAllProductsUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Rewards.Count != 0)
                return Ok(response);

            return NoContent();
        }
    }
