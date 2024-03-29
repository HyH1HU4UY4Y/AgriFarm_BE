﻿using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.DTOs;
using SharedDomain.Defaults;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands.Suppliers
{
    public class UpdateSupplierCommand : IRequest<SupplierInfoResponse>
    {
        public Guid Id { get; set; }
        public SupplierRequest Supplier { get; set; }
    }

    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, SupplierInfoResponse>
    {
        private ISQLRepository<SupplyContext, Supplier> _suppliers;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<UpdateSupplierCommandHandler> _logger;
        private IMapper _mapper;

        public UpdateSupplierCommandHandler(ISQLRepository<SupplyContext, Supplier> suppliers,
            IUnitOfWork<SupplyContext> unit,
            ILogger<UpdateSupplierCommandHandler> logger,
            IMapper mapper)
        {
            _suppliers = suppliers;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SupplierInfoResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
           
            var item = await _suppliers.GetOne(e => e.Id == request.Id);
            if (item == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(request.Supplier, item);
            await _suppliers.UpdateAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SupplierInfoResponse>(item);
        }
    }

}
