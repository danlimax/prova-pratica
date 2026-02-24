using MediatR;
using ProvaPratica.Domain.Interfaces;
using ProvaPratica.Application.Products.Commands;
using ProvaPratica.Domain.Repositories.Products;

namespace ProvaPratica.Application.Products.Handlers;

public class UploadProductImageHandler : IRequestHandler<UploadProductImageCommand, string>
{
    private readonly IProductsUpdateOnlyRepository _productRepository;
    private readonly IStorageService _storageService;

    public UploadProductImageHandler(IProductsUpdateOnlyRepository productRepository, IStorageService storageService)
    {
        _productRepository = productRepository;
        _storageService = storageService;
    }

    public async Task<string> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductId)
            ?? throw new KeyNotFoundException($"Produto {request.ProductId} não encontrado.");

        
        var imageUrl = await _storageService.UploadAsync(
            request.FileStream,
            request.FileName,
            request.ContentType,
            cancellationToken
        );

        
        product.Image = imageUrl;

        

        return imageUrl;
    }
}