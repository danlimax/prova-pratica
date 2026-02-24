using MediatR;

namespace ProvaPratica.Application.Products.Commands;

public record UploadProductImageCommand(
    int ProductId,
    Stream FileStream,
    string FileName,
    string ContentType
) : IRequest<string>; 