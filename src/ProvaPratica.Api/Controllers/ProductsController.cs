using Microsoft.AspNetCore.Mvc;
using ProvaPratica.Application.Products.Delete;
using ProvaPratica.Application.Products.GetAll;
using ProvaPratica.Application.Products.Register;
using ProvaPratica.Application.Products.Update;
using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;

namespace ProvaPratica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseProductsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllProducts([FromServices] IGetAllProductsUseCase useCase, [FromQuery] ProductsFilter filter)
        {
            var response = await useCase.Execute(filter);

            if (response.Products.Count != 0)
                return Ok(response);

            return NoContent();
        }

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
        public async Task<IActionResult> Update([FromServices] IUpdateProductUseCase useCase, [FromRoute] int id, [FromBody] RequestProductJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpDelete]
       [Route("{id}")]
       [ProducesResponseType(StatusCodes.Status204NoContent)]
       [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
       public async Task<IActionResult> Delete([FromServices] IDeleteProductUseCase useCase, [FromRoute] int id)
        {
            await useCase.Execute(id);

            return NoContent();
        }

      


       
    }
}
