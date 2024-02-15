using AutoMapper;
using Infrastructure.Payment.Context;
using MediatR;
using Service.Payment.DTOs.MerchantDTOs;
using SharedDomain.Defaults;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Pay;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Payment.Commands.MerchantCommands
{
    public class SetActiveMerchantCommand : IRequest<MerchantDTO>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class SetActiveMerchantCommandHandler : IRequestHandler<SetActiveMerchantCommand, MerchantDTO>
    {
        private ISQLRepository<PaymentContext, Merchant> _repo;
        private readonly IUnitOfWork<PaymentContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SetActiveMerchantCommandHandler> _logger;


        public SetActiveMerchantCommandHandler(IMapper mapper,
            IUnitOfWork<PaymentContext> unitOfWork,
            ISQLRepository<PaymentContext, Merchant> repo,
            ILogger<SetActiveMerchantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repo = repo;
            _logger = logger;
        }

        public async Task<MerchantDTO> Handle(SetActiveMerchantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _repo.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (item != null)
                {
                    item.IsActive = request.IsActive;
                    await _repo.UpdateAsync(item);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("End update...");
                }
                return _mapper.Map<MerchantDTO>(item);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End update...");
                throw new NotFoundException("Update fail!");
            }
        }
    }
}
