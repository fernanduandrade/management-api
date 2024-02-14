using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.OrderProducts.Dtos;
using Shop.Domain.OrderProducts;

namespace Shop.Application.OrderProducts.CreateOrderProduct;

public sealed class CreateOrderProductCommandHandler : IRequestHandler<CreateOrderProductCommand, ApiResult<OrderProductDto>>
{
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderProductCommandHandler(IOrderProductRepository orderProductRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderProductRepository = orderProductRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ApiResult<OrderProductDto>> Handle(CreateOrderProductCommand request, CancellationToken cancellationToken)
    {
        var orderProduct = await _orderProductRepository.OrderProductExist(request.ProductId, request.OrderId);

        if (orderProduct is null)
        {
            var newOrderProduct = OrderProduct.Create(request.ProductId, request.OrderId);

            _orderProductRepository.Add(newOrderProduct);
            await _unitOfWork.Commit(cancellationToken);

            var dto = _mapper.Map<OrderProductDto>(newOrderProduct);

            return new ApiResult<OrderProductDto>(dto, ResponseTypeEnum.Success, "Sucesso");
        }

        orderProduct.IncrementQuantity();
        await _unitOfWork.Commit(cancellationToken);
        var updateDto = _mapper.Map<OrderProductDto>(orderProduct);
        return new ApiResult<OrderProductDto>(updateDto, ResponseTypeEnum.Success, "Sucesso");
    }
}