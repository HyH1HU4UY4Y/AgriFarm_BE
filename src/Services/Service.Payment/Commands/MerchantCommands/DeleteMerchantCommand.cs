using AutoMapper;
using Infrastructure.Payment.Context;
using MediatR;
using Service.Payment.DTOs.MerchantDTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Pay;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Payment.Commands.MerchantCommands
{
    public class DeleteMerchantCommand : IRequest<MerchantDTO>
    {
        public Guid Id { get; set; }
    }

    public class DeleteMerchantCommandHandler : IRequestHandler<DeleteMerchantCommand, MerchantDTO>
    {
        private ISQLRepository<PaymentContext, Merchant> _repo;
        private readonly IUnitOfWork<PaymentContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteMerchantCommandHandler> _logger;


        public DeleteMerchantCommandHandler(IMapper mapper,
            IUnitOfWork<PaymentContext> unitOfWork,
            ISQLRepository<PaymentContext, Merchant> repo,
            ILogger<DeleteMerchantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repo = repo;
            _logger = logger;
        }

        public async Task<MerchantDTO> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting delete...");
            try
            {
                var item = await _repo.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (item != null)
                {
                    await _repo.SoftDeleteAsync(item);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("End delete...");
                }
                return _mapper.Map<MerchantDTO>(item);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End delete...");
                throw new NotFoundException("Delete fail!");
            }
        }
    }
}
