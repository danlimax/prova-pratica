using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProvaPratica.Application.Products.Commands;
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
        private readonly IMediator _mediator;

        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; 

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
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

        [HttpPost("{productId:int}/image")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadImage(int productId, IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            if (file.Length > MaxFileSizeBytes)
                return BadRequest("Arquivo excede o tamanho máximo de 5 MB.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                return BadRequest($"Extensão não permitida. Use: {string.Join(", ", AllowedExtensions)}");

            await using var stream = file.OpenReadStream();

            var command = new UploadProductImageCommand(
                productId,
                stream,
                file.FileName,
                file.ContentType
            );

            var imageUrl = await _mediator.Send(command, cancellationToken);

            return Ok(new { imageUrl });
        }

    }
}
